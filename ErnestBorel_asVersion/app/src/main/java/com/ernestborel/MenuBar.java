package com.ernestborel;

import hk.SharedPreferencesSetting.DataHandler;
import hk.SharedPreferencesSetting.LanguageHandler;
import hk.SharedPreferencesSetting.NetworkConnectChecker;

import java.util.Timer;
import java.util.TimerTask;

import android.content.Context;
import android.graphics.Typeface;
import android.net.ConnectivityManager;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.view.animation.TranslateAnimation;
import android.view.animation.Animation.AnimationListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;


public class MenuBar {

	FrameLayout mainLayout;
	FrameLayout menuBarBgLayout;
	LinearLayout tableLayout;
	
	Button hiddenBtn;
	Button homeBtn;
	Button collectionsBtn;
	Button bandBtn;	
	Button newsBtn;	
	Button storesBtn;
	Button aftersaleBtn;
	Button wallpaperBtn;
	Button timeBtn;
	Button menuBtn;
	Button topMainBtn;
	
	String subTitleString;
	
	public final static int MENU_BAR_EVENT_HIDE_BAR	= -1;
	public final static int MENU_BAR_EVENT_HOME = 0;
	public final static int MENU_BAR_EVENT_COLLECTIONS = 1;
	public final static int MENU_BAR_EVENT_BRAND = 2;
	public final static int MENU_BAR_EVENT_NEWS = 3;
	public final static int MENU_BAR_EVENT_STORES = 4;
	public final static int MENU_BAR_EVENT_AfterSale = 8;
	public final static int MENU_BAR_EVENT_WALLPAPERS = 5;
	public final static int MENU_BAR_EVENT_TIME = 6;
	public final static int MENU_BAR_UPDATE_TITLE = 7;
	public final static int MENU_BAR_HIDE_VIEW	= 100;
	
	
	MenuBarResponser listener;
	MenuBarHandler myHandler = new MenuBarHandler();
	
	//Animation 
	boolean isPlayingAnimation = false;
	boolean isShowMenuBar = false;	
	private Animation showMenuBar;
	private Animation hideMenuBar;		
	
	private Animation showTopBar;
	private Animation hideTopBar;
	private Animation showBackBtnAnim;
	private Animation hideBackBtnAnim;
	
	private float buttonListHeight = 0;
	
	Timer myTimer = new Timer();
	ResumeMenuBarFunction resumeTask;

	private final int MENU_BAR_ANIMATION_TIME = 200;	
	
	//Handler Msg
	private final int MSG_MENU_BAR_INVISIBLE = 0;
	private final int MSG_MENU_BAR_VISIBLE	= 1;	
	private final int MSG_SHOW_BACK_BTN	= 2;
	private final int MSG_HIDE_BACK_BTN = 3;
	private final int MSG_RELOAD_MENU_BAR = 4;
	private final int MSG_TOP_BAR_VISIBLE = 5;
	private final int MSG_TOP_BAR_INVISIBLE = 6;
	private final int MSG_TOP_BAR_HIDE_TMP_IMGVIEW = 7;
	private final int MSG_TOP_BAR_SHOW_TMP_IMGVIEW = 8;
	
	int pageMsg;
	
	//TopBar 
	RelativeLayout topBar;
	Button backBtn;
	TextView topBarTitle;
	
	boolean isShowBackBtn = false;
	boolean isShowTopBar = false;
	
	Context myContext;
	
	ImageView tmpImageView;
	
	public MenuBar(Context _context, MenuBarResponser _listener) {
		LayoutInflater _layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		mainLayout = (FrameLayout)_layoutInflater.inflate(R.layout.util_menubar, null);
	   	menuBarBgLayout = (FrameLayout)mainLayout.findViewById(R.id.MenuBar_BGFrame);
		
		tableLayout = (LinearLayout)menuBarBgLayout.findViewById(R.id.MenuBar_Table);
		listener = _listener;		
		
		myContext = _context;
		
		tmpImageView = (ImageView)mainLayout.findViewById(R.id.MenuBar_MainLayerImage);
		
		initAllBtn();
		initAnimation();
		initTopBar();
	}
	
	public ViewGroup getView(){
		return mainLayout;
	}
	
	public void initAnimation(){		
		//float _startPosY = tableLayout.getBackground().getIntrinsicHeight() * -1 + menuBtn.getLineHeight(); 

    	showMenuBar = new TranslateAnimation(0, 0, buttonListHeight * - 1, 0);
    	hideMenuBar = new TranslateAnimation(0, 0, 0, buttonListHeight * -1);

    	showMenuBar.setDuration(MENU_BAR_ANIMATION_TIME);
    	showMenuBar.setFillAfter(true);
    	
    	hideMenuBar.setDuration(MENU_BAR_ANIMATION_TIME);
    	hideMenuBar.setFillAfter( true );  
    	
    	hideMenuBar.setAnimationListener(new AnimationListener() {
			
			@Override
			public void onAnimationStart(Animation animation) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void onAnimationRepeat(Animation animation) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void onAnimationEnd(Animation animation) {
				// TODO Auto-generated method stub
				myHandler.sendEmptyMessage(MSG_TOP_BAR_SHOW_TMP_IMGVIEW);				
			}
		});
		
		Animation _defaultAnim = new TranslateAnimation(0, 0, 0, buttonListHeight * -1);
    	_defaultAnim.setDuration(10);
    	_defaultAnim.setFillAfter(true);    
    	menuBarBgLayout.startAnimation(_defaultAnim);    	
    	_defaultAnim = null;
    	
    	myHandler.sendEmptyMessage(MSG_MENU_BAR_INVISIBLE);
	}	
	
	public void initAllBtn(){
		
		menuBtn = (Button)tableLayout.findViewById(R.id.MenuBar_MainBtn);
		menuBtn.setClickable(false);
		menuBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				didPressMenuBtn();				
			}
		});
		
		topMainBtn = (Button)menuBarBgLayout.findViewById(R.id.MenuBar_TopMainBtn);
		topMainBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {				
				didPressMenuBtn();	
			}
		});
		
		hiddenBtn = (Button)mainLayout.findViewById(R.id.MenuBar_HiddenBtn);
		hiddenBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				//listener.didPassMenuBarEvent(MENU_BAR_EVENT_HIDE_BAR);
				didPressMenuBtn();
			}
		});		
		
		homeBtn = (Button)tableLayout.findViewById(R.id.MenuBar_HomeBtn);
		homeBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pageMsg = MENU_BAR_EVENT_HOME;
				hideTopBar();
				listener.didPassMenuBarEvent(MENU_BAR_EVENT_HOME);
				didPressMenuBtn();
			}
		});
		buttonListHeight += homeBtn.getBackground().getIntrinsicHeight();
		
		collectionsBtn = (Button)tableLayout.findViewById(R.id.MenuBar_CollectionBtn);
		collectionsBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pageMsg = MENU_BAR_EVENT_COLLECTIONS;
				showTopBar();
				listener.didPassMenuBarEvent(MENU_BAR_EVENT_COLLECTIONS);
				didPressMenuBtn();
			}
		});
		buttonListHeight += collectionsBtn.getBackground().getIntrinsicHeight();
		
		bandBtn = (Button)tableLayout.findViewById(R.id.MenuBar_BrandBtn);
		bandBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pageMsg = MENU_BAR_EVENT_BRAND;	
				showTopBar();
				listener.didPassMenuBarEvent(MENU_BAR_EVENT_BRAND);
				didPressMenuBtn();
			}
		});
		buttonListHeight += bandBtn.getBackground().getIntrinsicHeight();
		
		newsBtn = (Button)tableLayout.findViewById(R.id.MenuBar_NewsBtn);
		newsBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pageMsg = MENU_BAR_EVENT_NEWS;	
				showTopBar();
				listener.didPassMenuBarEvent(MENU_BAR_EVENT_NEWS);
				didPressMenuBtn();
			}
		});
		buttonListHeight += newsBtn.getBackground().getIntrinsicHeight();
		
		storesBtn = (Button)tableLayout.findViewById(R.id.MenuBar_StoreBtn);
		storesBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pageMsg = MENU_BAR_EVENT_STORES;	
				showTopBar();
				listener.didPassMenuBarEvent(MENU_BAR_EVENT_STORES);
				didPressMenuBtn();
			}
		});
		buttonListHeight += storesBtn.getBackground().getIntrinsicHeight();
		
		aftersaleBtn = (Button)tableLayout.findViewById(R.id.MenuBar_AfterSaleseBtn);
		aftersaleBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pageMsg = MENU_BAR_EVENT_AfterSale;	
				showTopBar();
				listener.didPassMenuBarEvent(MENU_BAR_EVENT_AfterSale);
				didPressMenuBtn();
			}
		});
		buttonListHeight += aftersaleBtn.getBackground().getIntrinsicHeight();
		
		
		wallpaperBtn = (Button)tableLayout.findViewById(R.id.MenuBar_WallpaperBtn);
		wallpaperBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pageMsg = MENU_BAR_EVENT_WALLPAPERS;
				showTopBar();
				listener.didPassMenuBarEvent(MENU_BAR_EVENT_WALLPAPERS);	
				didPressMenuBtn();
			}
		});
		buttonListHeight += wallpaperBtn.getBackground().getIntrinsicHeight();
		
		timeBtn = (Button)tableLayout.findViewById(R.id.MenuBar_TimeBtn);
		timeBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				pageMsg = MENU_BAR_EVENT_TIME;
				showTopBar();				
				listener.didPassMenuBarEvent(MENU_BAR_EVENT_TIME);
				didPressMenuBtn();
			}
		});
		buttonListHeight += timeBtn.getBackground().getIntrinsicHeight();
	}
	
	public void initTopBar(){	
		topBar = (RelativeLayout) mainLayout.findViewById(R.id.MenuBar_TopBar);
		backBtn = (Button) topBar.findViewById(R.id.MenuBar_BackBtn);	
		
//		Typeface face=Typeface.createFromAsset(myContext.getAssets(), "fonts/times.ttf");
		topBarTitle = (TextView) topBar.findViewById(R.id.MenuBar_Title);
//		topBarTitle.setTypeface(face);		
		
		int _topBarWidth = topBar.getBackground().getIntrinsicWidth();
		
		backBtn.setOnClickListener(new OnClickListener() {			
			@Override
			public void onClick(View v) {
				listener.didPressBackBtn();
			}
		});
		
		showTopBar = new TranslateAnimation(_topBarWidth, 0, 0 ,0);
		showTopBar.setDuration(MENU_BAR_ANIMATION_TIME);
		showTopBar.setFillAfter(true);
		
		hideTopBar = new TranslateAnimation(0,_topBarWidth, 0, 0);
		hideTopBar.setDuration(1);
		hideTopBar.setFillAfter(true);
		
//		Animation _anim = new TranslateAnimation(0, _topBarWidth, 0, 0);
//		_anim.setDuration(1);
//		_anim.setFillAfter(true);
//		topBar.startAnimation(_anim);		
//		_anim = null;
		topBar.setVisibility(View.INVISIBLE);
		
		showBackBtnAnim = new AlphaAnimation(0, 1);
		showBackBtnAnim.setDuration(MENU_BAR_ANIMATION_TIME);
		showBackBtnAnim.setFillAfter(true);
		
		hideBackBtnAnim = new AlphaAnimation(1, 0);
		hideBackBtnAnim.setDuration(MENU_BAR_ANIMATION_TIME);
		hideBackBtnAnim.setFillAfter(true);	
		
	}
	
	public void reloadMenuBar(){
		myHandler.sendEmptyMessage(MSG_RELOAD_MENU_BAR);
	}
	
	public void setTopBarTitle(String _txt){
		subTitleString = _txt;
		topBarTitle.setText(_txt);
	}
	
	public void showTopBar(){
		if(!isShowTopBar){
			Log.d("show top bar", "Show Top Bar La~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
//			topBar.startAnimation(showTopBar);		
//			isShowTopBar = true;


			if(!(NetworkConnectChecker.getInstance().getNetworkConnect()))
			{
				if(pageMsg == MENU_BAR_EVENT_HIDE_BAR || pageMsg == MENU_BAR_EVENT_HOME || pageMsg == MENU_BAR_EVENT_COLLECTIONS)
				{
					myHandler.sendEmptyMessage(MSG_TOP_BAR_VISIBLE);
				}
			}else
			{
				myHandler.sendEmptyMessage(MSG_TOP_BAR_VISIBLE);
			}
		}
	}
	
	public void hideTopBar(){
		if(isShowTopBar){
			Log.d("hide top bar", "Hide Top Bar La~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			myHandler.sendEmptyMessage(MSG_TOP_BAR_INVISIBLE);
			//topBar.startAnimation(hideTopBar);
						
		}
	}
	
	public void showBackBtn(){
		if(!isShowBackBtn){						
			backBtn.startAnimation(showBackBtnAnim);
			isShowBackBtn = true;
			
			myHandler.sendEmptyMessage(MSG_SHOW_BACK_BTN);
		}
	}
	
	public void hideBackBtn(){
		
		if(isShowBackBtn){
			
			backBtn.startAnimation(hideBackBtnAnim);
			isShowBackBtn = false;
			
			myTimer.schedule(new TimerTask() {
				
				@Override
				public void run() {
					myHandler.sendEmptyMessage(MSG_HIDE_BACK_BTN);				
				}
			}, MENU_BAR_ANIMATION_TIME);	
		}
	}
	
	public void didPressMenuBtn(){    	
		if(isPlayingAnimation){
			return;
		}						
					
		if( isShowMenuBar){
			menuBarBgLayout.startAnimation(hideMenuBar);			
	
		}else{					
			myHandler.sendEmptyMessage(MSG_MENU_BAR_VISIBLE);
			menuBarBgLayout.startAnimation(showMenuBar);
			myHandler.sendEmptyMessage(MSG_TOP_BAR_HIDE_TMP_IMGVIEW);			
		}
		isShowMenuBar = !isShowMenuBar;
		isPlayingAnimation = true;	
	
		resumeTask = new ResumeMenuBarFunction();
		myTimer.schedule(resumeTask, MENU_BAR_ANIMATION_TIME);	
	}
	
	public void showMeunBar()
	{
		mainLayout.setVisibility(View.VISIBLE);
		menuBtn.setVisibility(View.VISIBLE);
//		topMainBtn.setVisibility(View.VISIBLE);
	}
	
	public void meunBarInvisible()
	{
		hiddenBtn.setVisibility(View.INVISIBLE);
		homeBtn.setVisibility(View.INVISIBLE);
		collectionsBtn.setVisibility(View.INVISIBLE);
		bandBtn.setVisibility(View.INVISIBLE);	
		newsBtn.setVisibility(View.INVISIBLE);	
		storesBtn.setVisibility(View.INVISIBLE);
		aftersaleBtn.setVisibility(View.INVISIBLE);
		wallpaperBtn.setVisibility(View.INVISIBLE);
		timeBtn.setVisibility(View.INVISIBLE);		
		menuBtn.setClickable(false);
		topMainBtn.setVisibility(View.VISIBLE);
	}
	
	public void meunBarVisible()
	{
		hiddenBtn.setVisibility(View.VISIBLE);
		homeBtn.setVisibility(View.VISIBLE);
		collectionsBtn.setVisibility(View.VISIBLE);
		
//		if(NetworkConnectChecker.getInstance().getNetworkConnect())
//		{
			bandBtn.setVisibility(View.VISIBLE);
			newsBtn.setVisibility(View.VISIBLE);
			storesBtn.setVisibility(View.VISIBLE);
			aftersaleBtn.setVisibility(View.VISIBLE);
			wallpaperBtn.setVisibility(View.VISIBLE);
			timeBtn.setVisibility(View.VISIBLE);
//		}else
//		{
//			bandBtn.setVisibility(View.GONE);
//			newsBtn.setVisibility(View.GONE);
//			storesBtn.setVisibility(View.GONE);
////			wallpaperBtn.setVisibility(View.VISIBLE);
////			timeBtn.setVisibility(View.VISIBLE);
//		}

		menuBtn.setClickable(true);
		topMainBtn.setVisibility(View.INVISIBLE);
	}
	
	public void hideMeunBar()
	{
		mainLayout.setVisibility(View.GONE);
		menuBtn.setVisibility(View.GONE);
//		topMainBtn.setVisibility(View.GONE);
	}
	
	class MenuBarHandler extends Handler{
		@Override  
		public void handleMessage(Message msg) {  
			switch( msg.what){
				
				case MSG_MENU_BAR_INVISIBLE:
					meunBarInvisible();
					break;
					
				case MSG_MENU_BAR_VISIBLE:
					meunBarVisible();
					break;		
					
				case MSG_RELOAD_MENU_BAR:
					Log.d("Menu Bar", "Reload Menu bar Language");		
					homeBtn.setBackgroundDrawable(null);
					collectionsBtn.setBackgroundDrawable(null);
					bandBtn.setBackgroundDrawable(null);
					newsBtn.setBackgroundDrawable(null);
					storesBtn.setBackgroundDrawable(null);
					aftersaleBtn.setBackgroundDrawable(null);
					wallpaperBtn.setBackgroundDrawable(null);
					timeBtn.setBackgroundDrawable(null);
					
					homeBtn.setBackgroundResource(R.drawable.menubar_homebtn);
					collectionsBtn.setBackgroundResource(R.drawable.menubar_collectionsbtn);
					bandBtn.setBackgroundResource(R.drawable.menubar_thebrandbtn);
					newsBtn.setBackgroundResource(R.drawable.menubar_newsbtn);	
					storesBtn.setBackgroundResource(R.drawable.menubar_storesbtn);
					aftersaleBtn.setBackgroundResource(R.drawable.menubar_aftersalesbtn);
					wallpaperBtn.setBackgroundResource(R.drawable.menubar_wallpapersbtn);
					timeBtn.setBackgroundResource(R.drawable.menubar_timezonebtn);
					
					homeBtn.refreshDrawableState();
					homeBtn.invalidate();

					break;
					
				case MSG_SHOW_BACK_BTN:
					backBtn.setVisibility(View.VISIBLE);
					break;
					
				case MSG_HIDE_BACK_BTN:
					backBtn.setVisibility(View.INVISIBLE);
					break;
					
				case MSG_TOP_BAR_VISIBLE:
					topBar.setVisibility(View.VISIBLE);					
					topBarTitle.setText(subTitleString);				
					isShowTopBar = true;
					break;
					
				case MSG_TOP_BAR_INVISIBLE:
					topBar.setVisibility(View.INVISIBLE);
					isShowTopBar = false;
					break;
					
				case MSG_TOP_BAR_SHOW_TMP_IMGVIEW:
					tmpImageView.setVisibility(View.VISIBLE);
					break;
					
				case MSG_TOP_BAR_HIDE_TMP_IMGVIEW:
					tmpImageView.setVisibility(View.INVISIBLE);
					break;
					
			}
		}

		public void sleep(long delayMillis) {
			this.removeMessages(0);  
			sendMessageDelayed(obtainMessage(0), delayMillis);  
		}
	}
	
    class ResumeMenuBarFunction extends TimerTask{
    	public void run() {   		
    		isPlayingAnimation = false;
	   		resumeTask.cancel();
	   		resumeTask = null;    	
	   		if(isShowMenuBar){
	   			Log.d("Resume", "Visible");    			 
	   			myHandler.sendEmptyMessage(MSG_MENU_BAR_VISIBLE);
	   		}else{
	   			Log.d("Resume", "Gone");
	   			myHandler.sendEmptyMessage(MSG_MENU_BAR_INVISIBLE);
	   		}    		 
    	}   	
   }	
}

interface MenuBarResponser{
	void didPassMenuBarEvent(int _event);
	void didPressBackBtn();
}



