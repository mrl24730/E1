package com.ernestborel.CollectionObject;

import hk.ImageLoader.ImageLoader;
import hk.SharedPreferencesSetting.SaveTextHandler;
import hk.SharedPreferencesSetting.ScreenInformation;
import hk.SharedPreferencesSetting.UserProfile.timerTask;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.lang.reflect.Method;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;

import com.ernestborel.R;
import com.ernestborel.CollectionObject.CollectionDetailPage.RefreshHandler;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.res.Configuration;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.graphics.drawable.Drawable;
import android.hardware.Camera;
import android.hardware.Camera.PictureCallback;
import android.hardware.Camera.ShutterCallback;
import android.hardware.Camera.Size;
import android.net.Uri;
import android.os.Build;
import android.os.Handler;
import android.os.Message;
import android.provider.MediaStore;
import android.provider.MediaStore.Images;
import android.view.Display;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.view.View.OnTouchListener;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;

public class TryMeCameraView extends RelativeLayout implements SurfaceHolder.Callback, OnClickListener, OnTouchListener{
	private View view;
	Context context;
	CollectionDataListener collectionDataListener = null;
	TouchViewControl touchViewControl = null;
	
	private ViewGroup cameraView = null;
	private SurfaceView mSurfaceView;
	private SurfaceHolder mHolder;
    private Camera myCamera;
    private Button pickBtn;
//    private Button cancelBtn;
    
	private ViewGroup showImageView = null;
    private ImageView imageView;
	private ViewGroup imageBarView = null;
    private Button retakeBtn;
    private Button saveBtn;
    private Button instagramBtn;
    private Button retakeBtn2;
    private Button saveBtn2;
    private ViewGroup captureImageLayout = null;
	ImageView watchImageView;
	ImageLoader imageLoader;
	ImageView watchOutlineView;
    
    boolean previewing = false;
    
    RefreshHandler handler = new RefreshHandler();
    final int SHOW_CAMERA = 0;
	final int SHOW_IMAGE = 1;
	final int SHOW_IMAGE_WITHWATCH = 2;
	final int CAPTURE_IMAGE_WITHWATCH = 3;
	final int CAPTURE_IMAGE_WITHWATCH_LOADING = 4;
	final int CAPTURE_IMAGE_WITHWATCH_COMP = 5;
	final int CAPTURE_IMAGE_WITHWATCH_INSTAGRAM = 10;
	
	final int SHOW_TIPS	= 6;
	final int HIDE_TIPS	= 7;
	final int SHOW_OUTLINE	= 8;
	final int HIDE_OUTLINE	 = 9;
	
	Timer timer = new Timer(true);
	Timer fadeTimer;
	
	ProgressDialog pdialog;
	AlertDialog alertDialog;
	
	ImageView popupMsg;
	
	Animation fadeInAnimation;
	Animation fadeOutAnimation;
	
	
	public TryMeCameraView(Context _context, CollectionDataListener _collectionDataListener, String _imageUrl) {
		super(_context);
		context = _context;
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.layout_tryme,this);
		collectionDataListener = _collectionDataListener;
		
		//cameraView
		cameraView = (ViewGroup) view.findViewById(R.id.TryMe_CameraView);
		mSurfaceView = (SurfaceView)cameraView.findViewById(R.id.TryMe_CameraSurfaceView);
		mSurfaceView.setFocusable(true);
		mSurfaceView.setFocusableInTouchMode(true);
		mSurfaceView.setClickable(true);
		mHolder = mSurfaceView.getHolder();
	 	mHolder.addCallback(this);
	  	mHolder.setType(SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS);
	  	
	  	fadeInAnimation = new AlphaAnimation(0, 1);
	  	fadeInAnimation.setFillAfter(true);
	  	fadeInAnimation.setDuration(600);	  	
	  	
	  	fadeOutAnimation = new AlphaAnimation(1, 0);
	  	fadeOutAnimation.setFillAfter(true);
	  	fadeOutAnimation.setDuration(300);

	  	pickBtn = (Button) cameraView.findViewById(R.id.TryMe_PickPhotoBtn);
//		cancelBtn = (Button) cameraView.findViewById(R.id.TryMe_CancelBtn);
		pickBtn.setOnClickListener(this);
//		cancelBtn.setOnClickListener(this);
		
		watchOutlineView = (ImageView)view.findViewById(R.id.TryMe_Outline);
		popupMsg = (ImageView)view.findViewById(R.id.TryMe_PopupMsg);
		
		
		//showImageView
		showImageView = (ViewGroup) view.findViewById(R.id.TryMe_ImageLayout);
		imageView = (ImageView) showImageView.findViewById(R.id.TryMe_ImageView);
		imageBarView = (ViewGroup) view.findViewById(R.id.TryMe_ImageBarLayout);
		retakeBtn = (Button) imageBarView.findViewById(R.id.TryMe_RetakeBtn);
	    retakeBtn.setOnClickListener(this);
	    saveBtn = (Button) imageBarView.findViewById(R.id.TryMe_SaveBtn);
	    saveBtn.setOnClickListener(this);
	    instagramBtn = (Button) imageBarView.findViewById(R.id.TryMe_InstagramBtn);
	    instagramBtn.setOnClickListener(this);
		retakeBtn2 = (Button) imageBarView.findViewById(R.id.TryMe_RetakeBtn2);
	    retakeBtn2.setOnClickListener(this);
	    saveBtn2 = (Button) imageBarView.findViewById(R.id.TryMe_SaveBtn2);
	    saveBtn2.setOnClickListener(this);
	    
	    if (appInstalledOrNot()) 
	    {
	    	retakeBtn.setVisibility(View.VISIBLE);
	    	saveBtn.setVisibility(View.VISIBLE);
	    	retakeBtn2.setVisibility(View.GONE);
	    	saveBtn2.setVisibility(View.GONE);
	    	instagramBtn.setVisibility(View.VISIBLE);
	    }else
	    {
	    	retakeBtn.setVisibility(View.GONE);
	    	saveBtn.setVisibility(View.GONE);
	    	retakeBtn2.setVisibility(View.VISIBLE);
	    	saveBtn2.setVisibility(View.VISIBLE);
	    	instagramBtn.setVisibility(View.INVISIBLE);
	    }
	    
	    captureImageLayout = (ViewGroup) view.findViewById(R.id.TryMe_CaptureImageLayout);
	    
		imageLoader = new ImageLoader(_context, R.drawable.collection_detail_largeimage_loading);
		watchImageView = (ImageView) view.findViewById(R.id.TryMe_WatchImage);
		imageLoader.DisplayImage(_imageUrl, watchImageView);
//		watchImageView.setMaxZoom(3f);
		watchImageView.setOnTouchListener(this);
		
		
		touchViewControl = new TouchViewControl(_context, watchImageView);
		
		
		handler.sendEmptyMessage(SHOW_OUTLINE);
		//handler.sendEmptyMessage(SHOW_TIPS);
			
	}
	

	public void onDestroy()
	{
		handler = null;
		if(myCamera != null)
		{
			myCamera.stopPreview();
			myCamera.release();
			myCamera = null;
		}
		
		if(watchImageView != null)
		{
			watchImageView.setBackgroundDrawable(null);
		}
		
		if(imageLoader != null)
		{
			imageLoader.clearCache();
			imageLoader = null;
		}
		if(touchViewControl != null)
		{
			touchViewControl.onDestroy();
			touchViewControl = null;
		}
		
		if(pdialog != null)
		{
			pdialog.hide();
			pdialog.dismiss();
			pdialog = null;
		}
		
		if(alertDialog != null)
		{
			alertDialog.hide();
			alertDialog.dismiss();
			alertDialog = null;
		}
		
		if(timer != null)
		{
			timer.cancel();
			timer = null;
		}
		
		fadeInAnimation = null;
		fadeOutAnimation = null;
		
		if(fadeTimer != null){
			fadeTimer.cancel();
			fadeTimer = null;
		}
	}

	@Override
	public void surfaceChanged(SurfaceHolder arg0, int arg1, int arg2, int arg3) {
		// TODO Auto-generated method stub
		if(previewing){
			myCamera.stopPreview();
			previewing = false;
		}
		
		try {
			myCamera.setPreviewDisplay(arg0);
			
			Camera.Parameters parameters = myCamera.getParameters();
		    
			if (Integer.parseInt(Build.VERSION.SDK) >= 8)
			{
				setDisplayOrientation(myCamera, 90);
			}else {
				if (getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT) {
					parameters.set("orientation", "portrait");
					parameters.set("rotation", 90);
				}
				if (getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE) {
					parameters.set("orientation", "landscape");
					parameters.set("rotation", 90);
				}
			}
			myCamera.setParameters(parameters);
		    
			myCamera.startPreview();
			previewing = true;
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	 protected void setDisplayOrientation(Camera camera, int angle) {
		Method downPolymorphic;
		try {
			downPolymorphic = camera.getClass().getMethod("setDisplayOrientation", new Class[] { int.class });
	        if (downPolymorphic != null)
	        	downPolymorphic.invoke(camera, new Object[] { angle });
		} catch (Exception e1) {
		}
	 }

	@Override
	public void surfaceCreated(SurfaceHolder arg0) {
		// TODO Auto-generated method stub
		if(myCamera == null)
		{
			myCamera = Camera.open();			
		}else
		{
			myCamera.startPreview();
		}
	}

	@Override
	public void surfaceDestroyed(SurfaceHolder arg0) {
		// TODO Auto-generated method stub
		if(myCamera != null)
		{
		  myCamera.stopPreview();
		  myCamera.release();
		  myCamera = null;
		}
		previewing = false;
	}
	
	private Bitmap Bytes2Bimap(byte[] b){  
        if(b.length!=0){
            return BitmapFactory.decodeByteArray(b, 0, b.length);  
        }  
        else {
            return null;  
        }  
	}
	
	class RefreshHandler extends Handler {
    	@Override  
    	public void handleMessage(Message msg) {
    		switch (msg.what) {
			case SHOW_CAMERA:
		        cameraView.setVisibility(View.VISIBLE);
		        showImageView.setVisibility(View.GONE);
		        surfaceCreated(null);
				imageView.setImageBitmap(null);
		        imageView.setBackgroundDrawable(null);
				break;
				
			case SHOW_IMAGE:
				showImage(false);
				break;
				
			case SHOW_IMAGE_WITHWATCH:
				showImage(true);
				break;
				
			case CAPTURE_IMAGE_WITHWATCH:
				captureScreen(true);
				break;
				
			case CAPTURE_IMAGE_WITHWATCH_LOADING:
				if(pdialog != null)
				{
					pdialog.hide();
					pdialog.dismiss();
					pdialog = null;
				}
				if(alertDialog != null)
				{
					alertDialog.hide();
					alertDialog.dismiss();
					alertDialog = null;
				}
				pdialog = new ProgressDialog(context);		
				pdialog.setCancelable(false);
				pdialog.setMessage("Loading ....");
				pdialog.show();
				
				timer.schedule(new timerTask(), 100);
				break;
				
			case CAPTURE_IMAGE_WITHWATCH_COMP:
				showEndMsg();
				break;
				
			case SHOW_TIPS:				
				popupMsg.startAnimation(fadeInAnimation);
				fadeTimer = new Timer();
				fadeTimer.schedule(new TimerTask() {
					
					@Override
					public void run() {
						handler.sendEmptyMessage(HIDE_TIPS);
					}
				}, 2500);
				
				break;
				
			case HIDE_TIPS:
				popupMsg.startAnimation(fadeOutAnimation);		
				if(fadeTimer != null){
					fadeTimer.cancel();
					fadeTimer = null;
				}				
				break;
				
			case SHOW_OUTLINE:
				watchImageView.setVisibility(View.VISIBLE);
				Matrix _matrix = new Matrix();		
//				_matrix.postTranslate((float )0.0f, (float)-100.0f);
//						(float)(ScreenInformation.getInstance().getscreenWidth() - watchImageView.getDrawable().getIntrinsicWidth() * 0.5)  / 2.0f,
//						(float)(ScreenInformation.getInstance().getscreenHeight() - watchImageView.getDrawable().getIntrinsicHeight() * 0.5)  / 2.0f);
				_matrix.postScale(0.7f, 0.7f);//watchImageView.getDrawable().getIntrinsicWidth() / 2, watchImageView.getDrawable().getIntrinsicHeight() / 2);
				_matrix.postTranslate(0, 300);
				
				watchImageView.setImageMatrix(_matrix);
//				((RelativeLayout.LayoutParams)watchImageView.getLayoutParams()).setMargins(
//						(int)(ScreenInformation.getInstance().getscreenWidth() - watchImageView.getDrawable().getIntrinsicWidth() * 0.7)  / 2,
//						(int)(ScreenInformation.getInstance().getscreenHeight() - watchImageView.getDrawable().getIntrinsicHeight() * 0.7)  / 2, 0, 0);
				
				_matrix = null;
				
				break;
				
			case HIDE_OUTLINE:
				watchImageView.setVisibility(View.INVISIBLE);
				break;
				
			case CAPTURE_IMAGE_WITHWATCH_INSTAGRAM:
				instagramShare();
//				if (appInstalledOrNot()) 
//				{
//				}else
//				{
//					if(pdialog != null)
//					{
//						pdialog.hide();
//						pdialog.dismiss();
//						pdialog = null;
//					}
//					if(alertDialog != null)
//					{
//						alertDialog.hide();
//						alertDialog.dismiss();
//						alertDialog = null;
//					}
//					
//					alertDialog = new AlertDialog.Builder(context).create();
//					alertDialog.setButton(context.getString(R.string.pdialog_ok), new DialogInterface.OnClickListener() {
//						public void onClick(DialogInterface dialog, int which) {
////							collectionDataListener.closeTry();
//							return;
//						} });
//					alertDialog.setMessage(context.getString(R.string.instagram_not_found));
//					alertDialog.show();
//				}
				break;
			}
    	}
	}
	
	public void showImage(Boolean _withWatch) 
	{
        cameraView.setVisibility(View.GONE);
        showImageView.setVisibility(View.VISIBLE);
    	imageBarView.setVisibility(View.VISIBLE);
		watchImageView.setVisibility(View.VISIBLE);
//        if(_withWatch)
//        {
//        	imageBarView.setVisibility(View.GONE);
//    		watchImageView.setVisibility(View.VISIBLE);
//        }else
//        {
//        	imageBarView.setVisibility(View.VISIBLE);
//    		watchImageView.setVisibility(View.GONE);
//        }
	}
	
	public Boolean captureScreen(Boolean _path)
	{
//		View v1 = captureImageLayout.getRootView();
		captureImageLayout.setDrawingCacheEnabled(true);
		Bitmap bm = captureImageLayout.getDrawingCache();
		int w = bm.getWidth();
		int h = bm.getHeight();
		
		Drawable d = context.getResources().getDrawable(R.drawable.general_menutopbar);
		int viewHeight = d.getIntrinsicHeight();
		int viewWidth = d.getIntrinsicWidth();
		
		bm = Bitmap.createBitmap(bm, 0, viewHeight, w, h-viewHeight);
		
		if(_path)
		{
			MediaStore.Images.Media.insertImage(context.getContentResolver(), bm, "Ernest Borel", "Ernest Borel");
			handler.sendEmptyMessage(CAPTURE_IMAGE_WITHWATCH_COMP);
		}else
		{
			String fname = "saveImage.png";
			File file = SaveTextHandler.getInstance().getSaveFname(fname);
			if (file.exists ()) file.delete (); 
			try
			{
				FileOutputStream out = new FileOutputStream(file);
				bm.compress(Bitmap.CompressFormat.PNG, 90, out);
				out.flush();
				out.close();
			} catch (Exception e) {
				e.printStackTrace();
				return false;
			}
		}
		
		return true;
	}
	
	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		if(v == pickBtn)
		{
//			camera.takePicture(null, null, null, this);
			handler.sendEmptyMessage(HIDE_OUTLINE);
			handler.sendEmptyMessage(SHOW_TIPS);
			myCamera.takePicture(myShutterCallback,myPictureCallback_RAW, myPictureCallback_JPG);
		}else /*if(v == cancelBtn)
		{
			collectionDataListener.closeTry();
		}else*/ if(v == retakeBtn || v == retakeBtn2)
		{			
			handler.sendEmptyMessage(SHOW_OUTLINE);
			if(fadeTimer != null){								
				handler.sendEmptyMessage(HIDE_TIPS);
			}
			
			handler.sendEmptyMessage(SHOW_CAMERA);
		}else if(v == saveBtn || v == saveBtn2)
		{
			handler.sendEmptyMessage(CAPTURE_IMAGE_WITHWATCH_LOADING);
		}else if(v == instagramBtn)
		{
			handler.sendEmptyMessage(CAPTURE_IMAGE_WITHWATCH_INSTAGRAM);
		}
//		handler.sendEmptyMessage(SHOW_IMAGE_WITHWATCH);
	}
	
	ShutterCallback myShutterCallback = new ShutterCallback(){

		 @Override
		 public void onShutter() {
		  // TODO Auto-generated method stub
		 
		 }
	};

	PictureCallback myPictureCallback_RAW = new PictureCallback(){

		@Override
		 public void onPictureTaken(byte[] arg0, Camera arg1) {
		  // TODO Auto-generated method stub
		 
		 }
	};

	PictureCallback myPictureCallback_JPG = new PictureCallback(){

		 @Override
		 public void onPictureTaken(byte[] arg0, Camera arg1) {
		  // TODO Auto-generated method stub
//		  Bitmap bitmapPicture  = BitmapFactory.decodeByteArray(arg0, 0, arg0.length);
			 
			Bitmap bitmap = Bytes2Bimap(arg0);
			//set rotate
			int w = bitmap.getWidth();
			int h = bitmap.getHeight();

			// Setting post rotate to 90
			Matrix mtx = new Matrix();
			mtx.postRotate(90);
			
			// Rotating Bitmap
			bitmap = Bitmap.createBitmap(bitmap, 0, 0, w, h, mtx, true);
			imageView.setImageBitmap(bitmap);
	        imageView.setBackgroundDrawable(null);
	        handler.sendEmptyMessage(SHOW_IMAGE_WITHWATCH);
		 }
	};


	@Override
	public boolean onTouch(View v, MotionEvent event) {
		// TODO Auto-generated method stub
		return touchViewControl.onTouch(v, event);
	}
	
	
	public class timerTask extends TimerTask
	{
		public void run()
	    {
			handler.sendEmptyMessage(CAPTURE_IMAGE_WITHWATCH);
	    }
	};
	
	public void showEndMsg() 
	{
		if(pdialog != null)
		{
			pdialog.hide();
			pdialog.dismiss();
			pdialog = null;
		}
		if(alertDialog != null)
		{
			alertDialog.hide();
			alertDialog.dismiss();
			alertDialog = null;
		}
		
		alertDialog = new AlertDialog.Builder(context).create();
		alertDialog.setButton(context.getString(R.string.pdialog_ok), new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int which) {
				collectionDataListener.closeTry();
				return;
			} });
		alertDialog.setMessage(context.getString(R.string.pdialog_complete));
		alertDialog.show();
	}
	
	private boolean appInstalledOrNot() {

		boolean app_installed = false;
		try {
			ApplicationInfo info = context.getPackageManager().getApplicationInfo("com.instagram.android", 0);
			app_installed = true;
		} catch (PackageManager.NameNotFoundException e) {
			app_installed = false;
		}
		return app_installed;
	}
	
	private void instagramShare() 
	{
		if(captureScreen(false))
		{
			shareInstagram(Uri.parse("file://"+SaveTextHandler.getInstance().getRoot()+"/saveImage.png"));
		}
	}
	
	/* this mathod actually share image to Instagram, It accept Uri */
	private void shareInstagram(Uri uri){
		
		/// Method 1 : Optimize
		Intent shareIntent = new Intent(android.content.Intent.ACTION_SEND);
		shareIntent.setType("image/*"); // set mime type 
		shareIntent.putExtra(Intent.EXTRA_STREAM,uri); // set uri 
		shareIntent.putExtra(Intent.EXTRA_TEXT, "#ERNESTBOREL");
		shareIntent.setPackage("com.instagram.android");
		context.startActivity(shareIntent);

		collectionDataListener.closeTry();
		
		// Intent for action_send  
//		Intent shareIntent = new Intent(android.content.Intent.ACTION_SEND);
//		shareIntent.setType("image/*"); // set mime type 
//		shareIntent.putExtra(Intent.EXTRA_STREAM,uri); // set uri 
//		
//		//following logic is to avoide option menu, If you remove following logic then android will display list of application which support image/* mime type
//		PackageManager pm = getPackageManager();
//		List<ResolveInfo> activityList = pm.queryIntentActivities(shareIntent, 0);
//		for (final ResolveInfo app : activityList) {
//		    if ((app.activityInfo.name).contains("instagram")) { // search for instagram from app list
//		        final ActivityInfo activity = app.activityInfo;
//		        final ComponentName name = new ComponentName(activity.applicationInfo.packageName, activity.name); 
//		        shareIntent.addCategory(Intent.CATEGORY_LAUNCHER);
//		        shareIntent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK |             Intent.FLAG_ACTIVITY_RESET_TASK_IF_NEEDED);
//		        shareIntent.setComponent(name); // set component 
//		        startActivity(shareIntent);
//		        break;
//		   }
//		}
	}
}
