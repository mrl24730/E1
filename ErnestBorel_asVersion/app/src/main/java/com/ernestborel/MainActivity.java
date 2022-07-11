package com.ernestborel;


import hk.SharedPreferencesSetting.DataHandler;
import hk.SharedPreferencesSetting.LanguageHandler;
import hk.SharedPreferencesSetting.NetworkConnectChecker;
import hk.SharedPreferencesSetting.ScreenInformation;
import hk.SharedPreferencesSetting.UserProfile;
import hk.Util.Network.NetworkAddress;

import com.Util.CategoryListener;
import com.Util.MainActivityListener;
import com.Util.PageEventHandler;
import com.ernestborel.HomePageLayout.HomepageHandler;

import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.provider.SyncStateContract.Helpers;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;

import android.util.DisplayMetrics;
import android.util.Log;

import android.view.Display;
import android.view.KeyEvent;
import android.view.Menu;

import android.view.View;
import android.view.Window;

import android.view.ViewGroup;

import android.view.WindowManager;

import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.RelativeLayout;


@SuppressLint("HandlerLeak")
public class MainActivity extends Activity implements MenuBarResponser, MainActivityListener, HomepageHandler{
		
	
	FrameLayout mainActivityLayout;

	RefreshHandler handler = new RefreshHandler();
	
	MenuBar menuBar;
	RelativeLayout mainMenuTopBar;
	
	PageEventHandler pageEventHandler;
	CategoryListener categortListener;
	
	//Button mainMenuBtn;
	//private ViewGroup addPageView;
	
	//Page Indexing	 
	
	ImageView backgroundImgView;
	int currentPage;
	int previousPage;
	
	//Main View
	HomePageLayout homePage;	
	WallpaperLayout wallpaperPage;
	BrandLayout brandPage;
	StoresLocationLayout storeLocationPage;
	StoresLocationLayout aftersaleLocationPage;
	CollectionPageLayout collectionPageLayout;
	NewsPageLayout newsPageLayout;
	TimesLayout timesLayout;
	
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        this.requestWindowFeature(Window.FEATURE_NO_TITLE);
        
        setContentView(R.layout.activity_main);
        
        //Init switch page handler 
        WindowManager manager = (WindowManager) getSystemService(Context.WINDOW_SERVICE);
        Display display = manager.getDefaultDisplay();
        
        pageEventHandler = PageEventHandler.getInstance(display.getWidth(), display.getHeight());
        ScreenInformation.getInstance().presetScreenSize(this);
        NetworkConnectChecker.getInstance().setUpManager(this);
        
        display = null;
        manager = null;
        
        UserProfile _userProfile = UserProfile.getInstance();
		_userProfile.load(this);
	
		DisplayMetrics displaymetrics = new DisplayMetrics();
		getWindowManager().getDefaultDisplay().getMetrics(displaymetrics);		
		UserProfile.getInstance().screenWidth = displaymetrics.widthPixels;
		UserProfile.getInstance().screenHeight = displaymetrics.heightPixels;
		
        //Init UI
        mainActivityLayout = (FrameLayout)findViewById(R.id.MainActivity_layout);
        mainMenuTopBar = (RelativeLayout)findViewById(R.id.MainMenuTopBar);
        initMenuBar();
        
        backgroundImgView = (ImageView)findViewById(R.id.Main_BGImg);
        //Page Layout
        //addPageView = (ViewGroup)findViewById(R.id.AddPageLayout);

        initAllMainPage();
        
        if( UserProfile.isBackToBrand){
        	
        	Log.d("ernest borel", "Reset Language ");
        	mainActivityLayout.removeView(homePage.getView());
        	//didPassMenuBarEvent(PAGE_BAND_PAGE);
        	mainActivityLayout.addView(brandPage.getView());
        	
        	Log.d("ernest borel", "Reset Language : " + getString(R.string.title_brand));
        	menuBar.showTopBar();
        	menuBar.setTopBarTitle("");
        	menuBar.setTopBarTitle(getString(R.string.title_brand));
			pageEventHandler.changeCategory(brandPage.getView());
			currentPage = MenuBar.MENU_BAR_EVENT_BRAND;						
			menuBar.getView().bringToFront();
			menuBar.hideBackBtn();
			UserProfile.isBackToBrand = false;
        }
        
        Log.d("Child Count", "Count : " + mainActivityLayout.getChildCount());
     }
           
    
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
        	if( currentPage ==  MenuBar.MENU_BAR_EVENT_HOME){
                moveTaskToBack(true);
                return true;
        		
        	}else{
        		if( categortListener != null){
        			categortListener.backEventRequest();
        		}
        		return false;
        	}
        }
        return super.onKeyDown(keyCode, event);
    }
    
    @Override
    public void onResume(){    	
    	super.onResume();
    	if( currentPage ==  MenuBar.MENU_BAR_EVENT_TIME){
    		timesLayout.refreshView();
    	}    		
    }
    
    public void initMenuBar(){    	  
    	menuBar = new MenuBar(this, this);
    	
    	FrameLayout.LayoutParams _params = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.WRAP_CONTENT, ViewGroup.LayoutParams.WRAP_CONTENT);    	
    	mainActivityLayout.addView(menuBar.getView(), _params);    	        	    	    
    }
    
    public void initAllMainPage(){
                     
        wallpaperPage = new WallpaperLayout(this);
        //mainActivityLayout.addView(wallpaperPage.getView());
        
    	homePage = new HomePageLayout(this, this);        
    	mainActivityLayout.addView(homePage.getView());
    	
        brandPage = new BrandLayout(this);
        //mainActivityLayout.addView(brandPage.getView());
        Log.d("ernest borel", "Reset Language : " + getString(R.string.title_brand));
        
        
        storeLocationPage = new StoresLocationLayout(this, this);
        //mainActivityLayout.addView(storeLocationPage.getView());

        aftersaleLocationPage = new StoresLocationLayout(this, this);
    	
        collectionPageLayout = new CollectionPageLayout(this, this, this);
    	//mainActivityLayout.addView(collectionPageLayout);
    	
    	newsPageLayout = new NewsPageLayout(this, this, this);
    	//mainActivityLayout.addView(newsPageLayout);

    	timesLayout = new TimesLayout(this, this, this);
    	    	     	
        currentPage = MenuBar.MENU_BAR_EVENT_HOME;
        homePage.getView().bringToFront();
        
        mainMenuTopBar.bringToFront();
        menuBar.getView().bringToFront();           
        
    	//homePage.getView().setVisibility(View.GONE);
    	//collectionPageLayout.getView().setVisibility(View.GONE);
    	  
//    	if( !UserProfile.isBackToBrand){
//    		brandPage.getView().setVisibility(View.GONE);    	
//    	}
    	
//    	newsPageLayout.getView().setVisibility(View.GONE);
//    	storeLocationPage.getView().setVisibility(View.GONE);
//    	wallpaperPage.getView().setVisibility(View.GONE);
    }
    
    class RefreshHandler extends Handler {  
		@Override  
		public void handleMessage(Message msg) {  
			ViewGroup _viewGroup = null;	
			
			if(msg.what != MenuBar.MENU_BAR_UPDATE_TITLE){
				hideCurrentPage();
			}
			
			switch (msg.what) {
				case  MenuBar.MENU_BAR_EVENT_HIDE_BAR:				
					break;
					
				case MenuBar.MENU_BAR_EVENT_HOME:
					_viewGroup = homePage.getView();
					categortListener = null;
					break;
					
				case MenuBar.MENU_BAR_EVENT_COLLECTIONS:
					_viewGroup = collectionPageLayout.getView();
					menuBar.setTopBarTitle(getString(R.string.title_collection));
					collectionPageLayout.toFontView();
					categortListener = collectionPageLayout;
					break;
					
				case MenuBar.MENU_BAR_EVENT_BRAND:				
					_viewGroup = brandPage.getView();
					brandPage.showBandView();
					menuBar.setTopBarTitle(getString(R.string.title_brand));
					categortListener = null;
					break;
					
				case MenuBar.MENU_BAR_EVENT_NEWS:
					_viewGroup = newsPageLayout.getView();
					menuBar.setTopBarTitle(getString(R.string.title_news));
					newsPageLayout.toFontView();
					categortListener = newsPageLayout;
					break;
					
				case MenuBar.MENU_BAR_EVENT_STORES:
					_viewGroup = storeLocationPage.getView();
					storeLocationPage.pageType = NetworkAddress.LOCATION_PAGETYPE_STORES;
					storeLocationPage.showLocationStoreView();
					menuBar.setTopBarTitle(getString(R.string.title_stores));
					categortListener = storeLocationPage;
					break;
					
				case MenuBar.MENU_BAR_EVENT_AfterSale:
					_viewGroup = aftersaleLocationPage.getView();
					aftersaleLocationPage.pageType = NetworkAddress.LOCATION_PAGETYPE_AFTERSALE;
					aftersaleLocationPage.showLocationStoreView();
					menuBar.setTopBarTitle(getString(R.string.title_aftersale));
					categortListener = aftersaleLocationPage;
				break;
					
				case MenuBar.MENU_BAR_EVENT_WALLPAPERS:
					_viewGroup = wallpaperPage.getView();
					wallpaperPage.showWallpaper();
					menuBar.setTopBarTitle(getString(R.string.title_wallpapers));
					categortListener = null;
					break;
					
				case MenuBar.MENU_BAR_EVENT_TIME:
					_viewGroup = timesLayout.getView();
					timesLayout.showView();
					menuBar.setTopBarTitle(getString(R.string.title_time));
					categortListener = timesLayout;
					break;
					
				case MenuBar.MENU_BAR_UPDATE_TITLE:
					Log.d("ernest borel", "home page load Rest Main ~~~: " + getString(R.string.title_brand));
					if( currentPage == MenuBar.MENU_BAR_EVENT_BRAND){
						menuBar.setTopBarTitle(getString(R.string.title_brand));
					}
					break;					
				
				default:
					break;
			}
			
			if( _viewGroup != null){				
				_viewGroup.setVisibility(View.VISIBLE);
				mainActivityLayout.addView(_viewGroup);
				mainActivityLayout.bringChildToFront(_viewGroup);
				//pageEventHandler.changeCategory(_viewGroup); 	
				//_viewGroup.setVisibility(View.VISIBLE);
				_viewGroup.bringToFront();
				currentPage = msg.what;			
				
				menuBar.getView().bringToFront();
				menuBar.hideBackBtn();				
			}
		}

		public void sleep(long delayMillis) {
			this.removeMessages(0);  
			sendMessageDelayed(obtainMessage(0), delayMillis);  
		}
	}
    
    public void hideCurrentPage(){
    	//mainActivityLayout.bringChildToFront(backgroundImgView);
    	previousPage = currentPage;
    	
    	switch (previousPage) {
			case  MenuBar.MENU_BAR_EVENT_HIDE_BAR:				
				break;
				
			case MenuBar.MENU_BAR_EVENT_HOME:
				homePage.getView().setVisibility(View.INVISIBLE);
				mainActivityLayout.removeView(homePage.getView());
								
				break;
				
			case MenuBar.MENU_BAR_EVENT_COLLECTIONS:
				collectionPageLayout.hideWallpaper();
				collectionPageLayout.getView().setVisibility(View.INVISIBLE);
				mainActivityLayout.removeView(collectionPageLayout.getView());
				
				break;
				
			case MenuBar.MENU_BAR_EVENT_BRAND:
				brandPage.hideBandView();
				brandPage.getView().setVisibility(View.INVISIBLE);
				mainActivityLayout.removeView(brandPage.getView());

				break;
				
			case MenuBar.MENU_BAR_EVENT_NEWS:
				newsPageLayout.getView().setVisibility(View.INVISIBLE);
				mainActivityLayout.removeView(newsPageLayout.getView());
				break;
				
			case MenuBar.MENU_BAR_EVENT_STORES:
				storeLocationPage.getView().setVisibility(View.INVISIBLE);
				storeLocationPage.hideView();
				mainActivityLayout.removeView(storeLocationPage.getView());
				break;

			case MenuBar.MENU_BAR_EVENT_AfterSale:
				aftersaleLocationPage.getView().setVisibility(View.INVISIBLE);
				aftersaleLocationPage.hideView();
				mainActivityLayout.removeView(aftersaleLocationPage.getView());
				break;
				
			case MenuBar.MENU_BAR_EVENT_WALLPAPERS:
				wallpaperPage.hideWallpaper();		
				wallpaperPage.getView().setVisibility(View.INVISIBLE);
				mainActivityLayout.removeView(wallpaperPage.getView());
				break;
				
			case MenuBar.MENU_BAR_EVENT_TIME:	
				timesLayout.hideView();
				timesLayout.getView().setVisibility(View.INVISIBLE);
				mainActivityLayout.removeView(timesLayout.getView());
				break;
    	}
//    	mainActivityLayout.bringChildToFront(backgroundImgView);
    	
    	Log.d("Child Count", "Count : " + mainActivityLayout.getChildCount());
    }

/******  Menu Bar Listener *****/    
    
	@Override
	public void didPassMenuBarEvent(int _event) {
		// TODO Auto-generated method stub
		Log.d("Event", "Event Type : " + _event);	
		
		if(_event == currentPage){
			return;
		}
		
		if(!(NetworkConnectChecker.getInstance().getNetworkConnect()))
		{
			if(_event == MenuBar.MENU_BAR_EVENT_HIDE_BAR || _event == MenuBar.MENU_BAR_EVENT_HOME || _event == MenuBar.MENU_BAR_EVENT_COLLECTIONS)
			{
				handler.sendEmptyMessage(_event);
			}else
			{
				DataHandler.getInstance().showpopUp(LanguageHandler.getInstance().connectFail(), this);
			}
		}else
		{
			handler.sendEmptyMessage(_event);
		}
	}
	
	@Override
	public void didPressBackBtn() {
		categortListener.backEventRequest();
	}
	
/*** Main Activity Listener */
	@Override
	public void didShowTopBar() {
		// TODO Auto-generated method stub
		menuBar.showMeunBar();
	}

	@Override
	public void didHideTopBar() {
		// TODO Auto-generated method stub
		menuBar.hideMeunBar();
	}
	
	@Override
	public void didShowBackBtn() {
		menuBar.showBackBtn();
	}

	@Override
	public void didHideBackBtn() {
		menuBar.hideBackBtn();		
	}
	
	@Override
	public void setHeaderText(String _txt){
		menuBar.setTopBarTitle(_txt);
	}

/*** HomepageHandler */	
	@Override
	public void didRefreshLanguage() {
		brandPage.reloadView();
		menuBar.reloadMenuBar();
		if(currentPage == MenuBar.MENU_BAR_EVENT_BRAND){		
			Log.d("ernest borel", "home page load Rest Main : " + getString(R.string.brand_videoheader));
			menuBar.setTopBarTitle(getString(R.string.title_brand));
			handler.sendEmptyMessage(MenuBar.MENU_BAR_UPDATE_TITLE);
		}
	}
}
