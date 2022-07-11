package com.ernestborel;

import hk.SharedPreferencesSetting.ScreenInformation;

import java.util.ArrayList;
import java.util.Timer;
import java.util.TimerTask;

import com.Util.RealView.RealViewPagerController;
import com.Util.RealView.RealViewPagerControllerListener;
import com.ernestborel.CollectionObject.PagePointLiner;
import com.ernestborel.wallpaper.WallpaperPage;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;

import android.os.Handler;
import android.os.Message;
import android.provider.MediaStore;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.view.animation.RotateAnimation;

import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.ImageView.ScaleType;

public class WallpaperLayout implements RealViewPagerControllerListener{
	
	RelativeLayout mainLayout;
	RealViewPagerController pagerController;
	
	private PagePointLiner pagerPointer;
	
	private final int MSG_SHOW_POP_UP_BOX = 1;
	private final int MSG_REFRESH_BTN	= 2;
	
	Button leftBtn;
	Button rightBtn;
	
	Context myContext;
	
	private final int TOTAL_NUMBER_OF_WALLPAPER	= 4;
	
	int[] wallpaperResourceID = {
			R.drawable.wallpaper_01,
			R.drawable.wallpaper_02,
			R.drawable.wallpaper_03,
			R.drawable.wallpaper_04,
//			R.drawable.wallpaper_05,
//			R.drawable.wallpaper_06,
//			R.drawable.wallpaper_07
	};
	
	Button downloadBtn;
	int currentPage;
	
	ArrayList<WallpaperPage> wallpaperList = new ArrayList<WallpaperPage>();
	
	RefreshHandler myHandler = new RefreshHandler();
	
	int topbarHeight = 0;
	
	public WallpaperLayout(Context context) {				
		LayoutInflater _layoutInflater = (LayoutInflater)context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		mainLayout = (RelativeLayout)_layoutInflater.inflate(R.layout.layout_wallpapers, null);
		myContext = context;
		
		downloadBtn = (Button)mainLayout.findViewById(R.id.Wallpaper_DownloadBtn);
		ViewGroup _viewGroup = (ViewGroup) mainLayout.findViewById(R.id.Wallpaper_PointerLayout);
		_viewGroup.setClickable(false);
		
		downloadBtn.setOnClickListener(new OnClickListener() {			
			@Override
			public void onClick(View v) {
				//// TODO Auto-generated method stub
				//wallpaperList.get(currentPage).getImageView().getBackground().
				Bitmap bitmap = BitmapFactory.decodeResource(myContext.getResources(),wallpaperResourceID[currentPage]);				
				MediaStore.Images.Media.insertImage(					
						myContext.getContentResolver(), bitmap
						, "ErnestBorel Wallpaper " + (currentPage + 1), "Wallpaper " + (currentPage + 1));
				
				bitmap.recycle();
				bitmap = null;	
				myHandler.sendEmptyMessage(MSG_SHOW_POP_UP_BOX);
			}
		});
		
		pagerPointer = new PagePointLiner(myContext, _viewGroup);
		
		ImageView _topbar = (ImageView)mainLayout.findViewById(R.id.Wallpaper_TopBar);
		topbarHeight = _topbar.getBackground().getIntrinsicHeight();
		
		leftBtn = (Button)mainLayout.findViewById(R.id.Wallpaper_LeftBtn);
		leftBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pagerController.moveToPage(currentPage -1);
				
			}
		});
		
		rightBtn = (Button)mainLayout.findViewById(R.id.Wallpaper_RightBtn);
		rightBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pagerController.moveToPage(currentPage + 1);				
			}
		});
		
		pagerPointer.setTotalPage(TOTAL_NUMBER_OF_WALLPAPER);
		currentPage = 0;
	}
	
	public void showWallpaper(){
		
		downloadBtn.setBackgroundDrawable(null);
		downloadBtn.setBackgroundResource(R.drawable.wallpaper_downloadbtn);
		
		pagerController = new RealViewPagerController(myContext, mainLayout, this);
		
//		RelativeLayout.LayoutParams _params = (RelativeLayout.LayoutParams)pagerController.getRealViewer().getLayoutParams();		
//		_params.setMargins(0, topbarHeight * 5, 0, 0);
//		_params.addRule(RelativeLayout.CENTER_HORIZONTAL);
//		_params.addRule(RelativeLayout.CENTER_VERTICAL);
//				
//		pagerController.getRealViewer().setLayoutParams(_params);
		
		wallpaperList.removeAll(wallpaperList);	
		for(int i = 0; i < TOTAL_NUMBER_OF_WALLPAPER; i++){
			WallpaperPage _page = new WallpaperPage(myContext);
			pagerController.addView(_page.getView());
			wallpaperList.add(_page);		
			ViewGroup.LayoutParams _params = (ViewGroup.LayoutParams)_page.getView().getLayoutParams();
			_params.width = ScreenInformation.getInstance().getscreenWidth();
			_params.height = ScreenInformation.getInstance().getscreenHeight();
		}		
		pagerController.startRealViewController();
		myHandler.sendEmptyMessage(MSG_REFRESH_BTN);
		
		downloadBtn.bringToFront();
		leftBtn.bringToFront();
		rightBtn.bringToFront();
	}
	
	public void hideWallpaper(){
		for( int i =0; i < wallpaperList.size(); i++){
			WallpaperPage _page = wallpaperList.get(i);
			_page.releasePage();
		}
		
		pagerController.release();
		pagerController = null;
	}
	
	public ViewGroup getView(){
		return mainLayout;
	}

	
	/*** RealViewPagerControllerListener  */
	@Override
	public void showCurrentPage(int _index, View _currentView){		
		currentPage = _index;
		pagerPointer.setPage(_index);
		myHandler.sendEmptyMessage(MSG_REFRESH_BTN);
		pagerController.moveComplete();
	} 
	
	@Override
	public void hidePage(View _page, int _index){
		Log.d("Wallpaper", "Wallpaper Hide");		
		wallpaperList.get(_index).hidePage();
	}
	
	@Override
	public void loadPage(View _page, int _index){		
		wallpaperList.get(_index).showPage(wallpaperResourceID[_index]);		
//		ImageView _imgView = (ImageView)_page.findViewById(R.id.Wallpaper_ImageView);		
//		if( _imgView.getDrawable() == null){		
//			_imgView.setImageResource(wallpaperResourceID[_index]);			
//		}						
	}

	@Override
	public void releasePage(View _page, int _index) {
		wallpaperList.get(_index).releasePage();
		mainLayout.removeView(wallpaperList.get(_index).getView());
//		ImageView _imgView = (ImageView)_page.findViewById(R.id.Wallpaper_ImageView);
//		_imgView.setBackgroundDrawable(null);
//		_imgView = null;		
	}
	
	class RefreshHandler extends Handler{
		
		@Override  
		public void handleMessage(Message msg) {  
			if( msg.what == MSG_SHOW_POP_UP_BOX){
				AlertDialog alertDialog = new AlertDialog.Builder(myContext).create();
				alertDialog.setButton(myContext.getString(R.string.pdialog_ok), new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int which) {
					} });
				alertDialog.setMessage(myContext.getString(R.string.pdialog_complete));
				alertDialog.show();
			}else if( msg.what == MSG_REFRESH_BTN){
				if(currentPage == 0){
					leftBtn.setVisibility(View.INVISIBLE);
					leftBtn.setClickable(false);
					
				}else{
					leftBtn.setVisibility(View.VISIBLE);
					leftBtn.setClickable(true);
				}
				
				if( currentPage == TOTAL_NUMBER_OF_WALLPAPER - 1){
					rightBtn.setVisibility(View.INVISIBLE);
					rightBtn.setClickable(false);
					
				}else{
					rightBtn.setVisibility(View.VISIBLE);
					rightBtn.setClickable(true);
				}
			}
		}
	}
}
