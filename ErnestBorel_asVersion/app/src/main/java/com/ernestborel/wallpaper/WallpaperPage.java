package com.ernestborel.wallpaper;

import hk.SharedPreferencesSetting.ScreenInformation;

import com.ernestborel.R;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Bitmap;

import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;

import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RelativeLayout;

public class WallpaperPage {
	
	RelativeLayout mainLayout;
	ImageView myImageView;
	Bitmap myBitmap = null;
	
	UIHandler myHandler = new UIHandler();
	
	private final int MSG_LOAD_PAGE	= 0;
	private final int MSG_HIDE_PAGE = 1;
	private final int MSG_RELEASE_PAGE	= 2;
	
	int resourceID;	
	Context myContext;
	
	public WallpaperPage(Context _context) {
		LayoutInflater _layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		mainLayout = (RelativeLayout)_layoutInflater.inflate(R.layout.page_wallpaper, null);
		
		myImageView = (ImageView)mainLayout.findViewById(R.id.Wallpaper_ImageView);		
		RelativeLayout.LayoutParams _params = (RelativeLayout.LayoutParams )myImageView.getLayoutParams();
		float _ratio = myImageView.getBackground().getIntrinsicHeight() * 1.0f / myImageView.getBackground().getIntrinsicWidth();
	
		_params.width = (int) (ScreenInformation.getInstance().getscreenWidth() * 0.7);
		_params.height = (int) (_params.width * 1.775);		
		//myImageView = null;
		myContext = _context;
	}
	
	
	public ViewGroup getView(){
		return mainLayout;
	}
	
	public void showPage(int _resourceID){
		resourceID = _resourceID;		
		//if(myImageView == null){
			
		myHandler.sendEmptyMessage(MSG_LOAD_PAGE);
		//}
	}
	
	public void hidePage(){
		myHandler.sendEmptyMessage(MSG_HIDE_PAGE);
	}
	
	public void releasePage(){
		myHandler.sendEmptyMessage(MSG_RELEASE_PAGE);		
	}
	
	public ImageView getImageView(){
		return myImageView;
	}
	
	@SuppressLint("HandlerLeak")
	class UIHandler extends Handler{
		@Override  
		public void handleMessage(Message msg) {  
		//	Drawable _drawable = null;
			switch( msg.what){
			case MSG_LOAD_PAGE:
//				if(myBitmap == null){
					 //yBitmap = BitmapFactory.decodeResource(myContext.getResources(), resourceID);			
				myImageView.setBackgroundResource(resourceID);//(myBitmap);
			
//				if( myImageView == null){
//					myImageView = new ImageView(myContext);
//					myImageView.setLayoutParams(new RelativeLayout.LayoutParams(320, 480));
//					mainLayout.addView(myImageView);
//					myImageView.setImageResource(resourceID);						
//				}				
				 
				//myImageView.setImageResource(r);
				break;
			
			case MSG_HIDE_PAGE:
				Log.d("Hide", "Hide Page~~~~~~~~~~~~~~");			
//				if( myImageView != null){
//					mainLayout.removeView(myImageView);
//					myImageView = null;
//				}
				//myImageView.setImageResource();
				//myImageView.destroyDrawingCache();
//				if(myBitmap != null){
//					myBitmap.recycle();
//					myBitmap = null;
//				}
				myImageView.setBackgroundResource(R.drawable.empty_px);	
				System.gc();
				break;
				
			case MSG_RELEASE_PAGE:
				if( myImageView != null){
					mainLayout.removeView(myImageView);
					myImageView = null;
				}
				myContext = null;
//				_drawable = myImageView.getDrawable();				
//				_drawable = null;
//				myImageView.setImageDrawable(null);
//				myImageView.destroyDrawingCache();
//				if(myBitmap != null){
//					myBitmap.recycle();
//					myBitmap = null;
//				}
//				myImageView.setImageResource(R.drawable.empty_px);	
				mainLayout.removeAllViews();
				myHandler = null;
				System.gc();
				break;			
			}
		}
	}
	
}
