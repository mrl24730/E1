package hk.SharedPreferencesSetting;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import android.content.Context;
import android.content.res.AssetManager;
import android.os.Environment;
import android.util.Log;

public class AssetsDataHandler {
	private static AssetsDataHandler instance = null;
	
	public static AssetsDataHandler getInstance(){
		if( instance == null){
			instance = new AssetsDataHandler();
		}
		return instance;
	}
	
	public AssetsDataHandler()
	{
        File myDir = new File(getSaveFname());
    	myDir.mkdirs();
    	myDir = null;
	}
	
	public void copyAssets(Context _context, String _listName) {
	    AssetManager assetManager = _context.getAssets();
	    String[] files = getAssetsList(_context, _listName);
	    for(String filename : files) {
	        InputStream in = null;
	        OutputStream out = null;
	        try {
	          in = assetManager.open(_listName+"/"+filename);
	          out = new FileOutputStream(getSaveFname()+"/"+filename);
	          copyFile(in, out);
	          in.close();
	          in = null;
	          out.flush();
	          out.close();
	          out = null;
	        } catch(IOException e) {
	            Log.e("tag", "Failed to copy asset file: " + filename, e);
	        }       
	    }
	}
	
	public Boolean copyAssetsImage(Context _context, String _listName, String _fileName)
	{
		AssetManager assetManager = _context.getAssets();
		 
		InputStream in = null;
	    OutputStream out = null;
		try {
			in = assetManager.open(_listName+"/"+_fileName);
			out = new FileOutputStream(getSaveFname()+"/"+_fileName);
			copyFile(in, out);
			in.close();
			in = null;
			out.flush();
			out.close();
			out = null;
		} catch(IOException e) {
			Log.e("tag", "Failed to copy asset file: " + _fileName, e);
			return false;
		}
		return true;
	}
	
	public String getAssetsTxtFile(Context _context, String _listName, String _fileName)
	{
		String text = null;
		try {
			InputStream is = _context.getAssets().open(_listName+"/"+_fileName);
			int size = is.available(); 

	        byte[] buffer = new byte[size]; 
	        is.read(buffer); 
	        is.close(); 

	        // Convert the buffer into a string. 
	        text = new String(buffer);
//	        Log.d("text", "text:"+text);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return text;
	}
	
	public String[] getAssetsList(Context _context, String _listName)
	{
		String[] files = null;
	    try {
	        files = _context.getAssets().list(_listName);
	    } catch (IOException e) {
	        Log.e("tag", "Failed to get asset file list.", e);
	    }
	    return files;
	}
	
	private void copyFile(InputStream in, OutputStream out) throws IOException {
	    byte[] buffer = new byte[1024];
	    int read;
	    while((read = in.read(buffer)) != -1){
	      out.write(buffer, 0, read);
	    }
	}
	
	private String getSaveFname()
    {
    	String root = Environment.getExternalStorageDirectory().toString();
//    	String root = context.getPackageResourcePath();
//    	String root = context.getFilesDir().getAbsolutePath();
    	return root + "/ErnestBorel";
    }
}
