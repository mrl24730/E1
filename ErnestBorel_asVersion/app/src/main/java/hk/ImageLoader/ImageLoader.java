package hk.ImageLoader;

import hk.SharedPreferencesSetting.AssetsDataHandler;
import hk.SharedPreferencesSetting.UserProfile;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Collections;
import java.util.Map;
import java.util.WeakHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.Drawable;
import android.os.Environment;
import android.util.Log;
import android.widget.ImageView;

public class ImageLoader {
    
    MemoryCache memoryCache=new MemoryCache();
    FileCache fileCache;
    private Map<ImageView, String> imageViews=Collections.synchronizedMap(new WeakHashMap<ImageView, String>());
    ExecutorService executorService; 
    String saveUrl;
    ImageView saveImageView;
    Context context;
    Drawable stub_id = null;
//    int stub_id = 0;
    
    public ImageLoader(Context _context, int _defaultImage){
    	context = _context;
        fileCache=new FileCache(context);
        executorService=Executors.newFixedThreadPool(5);
        stub_id = _context.getResources().getDrawable(_defaultImage);
//        stub_id = _defaultImage;
    }
    
//    final int stub_id = R.drawable.loading_image;
    public void DisplayImage(String url, ImageView imageView)
    {
    	if(checkImageSaved(url))
    	{
            //Log.d("saveImage", "saveImage loadUrl:"+url);
    		if(saveUrl != null)
        	{
        		saveUrl = null;
        	}
        	if(saveImageView != null)
        	{
        		saveImageView = null;
        	}
        	saveUrl = url;
            saveImageView = imageView;
            imageViews.put(imageView, url);
            Bitmap bitmap=memoryCache.get(url);
            if(bitmap!=null)
            {
                imageView.setImageBitmap(bitmap);
            	imageView.setBackgroundDrawable(null);
            }else
            {
                queuePhoto(url, imageView);
                if(imageView != null)
                {
//                	imageView.setImageResource(stub_id);
//                	imageView.setImageDrawable(null);
                	imageView.setImageBitmap(null);
                	imageView.setBackgroundDrawable(stub_id);
                }
            }
    	}else
    	{
        	String fname = getImageName(url);
    		File file = getSaveFname(fname);
    		imageViews.put(imageView, file.getPath());
            Bitmap bitmap=memoryCache.get(file.getPath());
            if(bitmap == null)
            {
            	bitmap = BitmapFactory.decodeFile(file.getPath());
	    		memoryCache.put(file.getPath(), bitmap);
            }
    		
//    		Log.d("DisplayImage", "DisplayImage fname:"+fname+" bitmap:"+bitmap);
    		if(bitmap != null)
            {
        		imageView.setImageBitmap(bitmap);
    			imageView.setBackgroundDrawable(null);
            }else
            {
            	imageView.setImageBitmap(null);
            	imageView.setBackgroundDrawable(stub_id);
            }
    		bitmap = null;
    	}
    }
    
    private void queuePhoto(String url, ImageView imageView)
    {
        PhotoToLoad p=new PhotoToLoad(url, imageView);
        executorService.submit(new PhotosLoader(p));
    }
    
    private Bitmap getBitmap(String url) 
    {
        File f=fileCache.getFile(url);
        
        //from SD cache
        Bitmap b = decodeFile(f);
        if(b!=null)
            return b;
        
        //from web
        try {
            Bitmap bitmap=null;
            URL imageUrl = new URL(url);
            HttpURLConnection conn = (HttpURLConnection)imageUrl.openConnection();
            conn.setConnectTimeout(30000);
            conn.setReadTimeout(30000);
            conn.setInstanceFollowRedirects(true);
            InputStream is=conn.getInputStream();
            byte[]  data = new byte[102400];
			int length;
			ByteArrayOutputStream buffer = new ByteArrayOutputStream();

	        while ((length = is.read(data, 0, data.length)) != -1) {
	        	  buffer.write(data, 0, length);
	        }
	        
	        bitmap = BitmapFactory.decodeByteArray(buffer.toByteArray(), 0, buffer.toByteArray().length);
	        
	        saveImage(url, bitmap);
            return bitmap;
        } catch (Exception ex){
           ex.printStackTrace();
           return null;
        }
    }

    //decodes image and scales it to reduce memory consumption
    private Bitmap decodeFile(File f){
        try {
            //decode image size
            BitmapFactory.Options o = new BitmapFactory.Options();
            o.inJustDecodeBounds = true;
            BitmapFactory.decodeStream(new FileInputStream(f),null,o);
            
            //Find the correct scale value. It should be the power of 2.
            final int REQUIRED_SIZE=70;
            int width_tmp=o.outWidth, height_tmp=o.outHeight;
            int scale=1;
            while(true){
                if(width_tmp/2<REQUIRED_SIZE || height_tmp/2<REQUIRED_SIZE)
                    break;
                width_tmp/=2;
                height_tmp/=2;
                scale*=2;
            }
            
            //decode with inSampleSize
            BitmapFactory.Options o2 = new BitmapFactory.Options();
            o2.inSampleSize=scale;
            return BitmapFactory.decodeStream(new FileInputStream(f), null, o2);
        } catch (FileNotFoundException e) {}
        return null;
    }
    
    //Task for the queue
    private class PhotoToLoad
    {
        public String url;
        public ImageView imageView;
        public PhotoToLoad(String u, ImageView i){
            url=u; 
            imageView=i;
        }
    }
    
    class PhotosLoader implements Runnable {
        PhotoToLoad photoToLoad;
        PhotosLoader(PhotoToLoad photoToLoad){
            this.photoToLoad=photoToLoad;
        }
        
        @Override
        public void run() {
            if(imageViewReused(photoToLoad))
                return;
            Bitmap bmp=getBitmap(photoToLoad.url);
            memoryCache.put(photoToLoad.url, bmp);
            if(imageViewReused(photoToLoad))
                return;
            BitmapDisplayer bd=new BitmapDisplayer(bmp, photoToLoad);
            Activity a=(Activity)photoToLoad.imageView.getContext();
            a.runOnUiThread(bd);
        }
    }
    
    boolean imageViewReused(PhotoToLoad photoToLoad){
        String tag=imageViews.get(photoToLoad.imageView);
        if(tag==null || !tag.equals(photoToLoad.url))
            return true;
        return false;
    }
    
    //Used to display bitmap in the UI thread
    class BitmapDisplayer implements Runnable
    {
        Bitmap bitmap;
        PhotoToLoad photoToLoad;
        public BitmapDisplayer(Bitmap b, PhotoToLoad p){bitmap=b;photoToLoad=p;}
        public void run()
        {
            if(imageViewReused(photoToLoad))
            {
            	DisplayImage(saveUrl, saveImageView);
            	return;
            }
//            Log.d("saveImage", "saveImage bitmap:"+bitmap);
//            Log.d("saveImage", "saveImage saveUrl:"+saveUrl);
            if(bitmap!=null)
            {
            	//saveImage(photoToLoad.url, bitmap);
                photoToLoad.imageView.setImageBitmap(bitmap);
                photoToLoad.imageView.setBackgroundDrawable(null);
            }else
            {
            	photoToLoad.imageView.setImageDrawable(null);
//                photoToLoad.imageView.setImageResource(stub_id);
            }
            saveUrl = null;
    		saveImageView = null;
        }
    }

    public void clearCache() {
        memoryCache.clear();
        fileCache.clear();
    }
    
    private void saveImage(String _imageUrl, Bitmap bmp)
    {
    	Log.d("saveImage", "saveImage _imageUrl:"+_imageUrl);
    	String fname = getImageName(_imageUrl);
    	File file = getSaveFname(fname);
    	if (file.exists ()) file.delete (); 
    	try
    	{
    		FileOutputStream out = new FileOutputStream(file);
    		bmp.compress(Bitmap.CompressFormat.PNG, 90, out);
    		out.flush();
    		out.close();
    	} catch (Exception e) {
    		e.printStackTrace();
    	}
    }
    
    private Boolean checkImageSaved(String _imageUrl)
    {
    	if(_imageUrl != null)
    	{
    		String imageName = getImageName(_imageUrl);
        	File file = getSaveFname(imageName);
        	if(file.exists())
        	{
        		Log.d("saveImage", "saveImage saved");
        		return false;
        	}else
        	{
        		if(AssetsDataHandler.getInstance().copyAssetsImage(context, UserProfile.ASSETS_KEY_IMAGE, imageName))
            	{
        			return false;
            	}
        	}
    		return true;
    	}
		return true;
    }
    
    public String getImageName(String _imageUrl)
    {
    	String[] separated = _imageUrl.split("&name=");
    	if(separated.length > 1)
    	{
    		return separated[separated.length-1];
    	}else
    	{
    		separated = _imageUrl.split("/");
    	}
    	return separated[separated.length-1];
    }
    
    private File getSaveFname(String _fname)
    {
    	String root = Environment.getExternalStorageDirectory().toString();
//    	String root = context.getPackageResourcePath();
//    	String root = context.getFilesDir().getAbsolutePath();
    	File myDir = new File(root + "/ErnestBorel");
    	myDir.mkdirs();
    	File file = new File (myDir, _fname);
    	return file;
    }
}
