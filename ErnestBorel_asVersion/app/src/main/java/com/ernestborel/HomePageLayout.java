package com.ernestborel;

import hk.ImageLoader.ImageLoader;
import hk.SharedPreferencesSetting.PreferenceConnector;
import hk.SharedPreferencesSetting.UserProfile;
import hk.SharedPreferencesSetting.UserProfile.UserProfileHandler;

import java.util.Locale;

import org.json.JSONArray;
import org.json.JSONException;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.res.Resources;
import android.graphics.Color;
import android.os.Handler;
import android.os.Message;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup.LayoutParams;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;

@SuppressLint("HandlerLeak")
public class HomePageLayout implements UserProfileHandler{

	HomepageHandler listener;
	RelativeLayout mainLayout;
	ImageView backgroundImg,HomePage_bg;
	Context myContext;
	Button myLanguageBtn;
	
	RefreshHandler handler = new RefreshHandler();
		
	public HomePageLayout(Context context, HomepageHandler _listener) {	
		myContext = context;
		listener = _listener;
		
		LayoutInflater _layoutInflater = (LayoutInflater)context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		mainLayout = (RelativeLayout)_layoutInflater.inflate(R.layout.layout_homepage, null);		
	   
		LinearLayout _btnList = (LinearLayout)mainLayout.findViewById(R.id.HomePage_BtnList);
		HomePage_bg = (ImageView)mainLayout.findViewById(R.id.HomePage_bg);
		myLanguageBtn = (Button)_btnList.findViewById(R.id.HomePage_LanguageBtn);
		myLanguageBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {	
				UserProfile.getInstance().nextLanguage();
				handler.sendEmptyMessage(1);
				//changeLanguage(UserProfile.LANGUAGE_INDEX_EN);				
			}
		});
		     				
		
		if(UserProfile.getInstance().isFirstTime){
			Log.d("Test", "Is First Time!!!!! Lanuage");			
			if( Locale.getDefault().getLanguage().toString().equals(Locale.SIMPLIFIED_CHINESE.toString())){
				UserProfile.getInstance().setLanguage(UserProfile.LANGUAGE_INDEX_ZHCN);
				handler.sendEmptyMessage(1);
				
			}else if( Locale.getDefault().getLanguage().toString().equals(Locale.CHINESE.toString())){
				UserProfile.getInstance().setLanguage(UserProfile.LANGUAGE_INDEX_ZHHK);
				handler.sendEmptyMessage(1);	
				
			}else{
				UserProfile.getInstance().setLanguage(UserProfile.LANGUAGE_INDEX_EN);
				handler.sendEmptyMessage(1);			
			}
		}else{
			UserProfile.getInstance().setLanguage(UserProfile.getInstance().currentLanguageIndex);
			handler.sendEmptyMessage(1);	
		}
//		Button _zhhk = (Button)_btnList.findViewById(R.id.Homepage_BtnZHHK);
//		_zhhk.setOnClickListener(new OnClickListener() {
//			
//			@Override
//			public void onClick(View v) {
//				changeLanguage(UserProfile.LANGUAGE_INDEX_ZHHK);				
//			}
//		});
//
//		Button _zhcn = (Button)_btnList.findViewById(R.id.Homepage_BtnZHCN);
//		_zhcn.setOnClickListener(new OnClickListener() {
//			
//			@Override
//			public void onClick(View v) {
//				changeLanguage(UserProfile.LANGUAGE_INDEX_ZHCN);				
//			}
//		});		
		
		backgroundImg = (ImageView)mainLayout.findViewById(R.id.HomePage_bg);
		if( UserProfile.getInstance().screenWidth == 720){
			backgroundImg.setBackgroundResource(R.drawable.homepage_bg_720);
		}
		loadApiLayoutBackgroundImg();
	}
	
	public void loadApiLayoutBackgroundImg() {		
		UserProfile _userProfile = UserProfile.getInstance();
		_userProfile.setListener(this);
		_userProfile.loadInit();
	}
	
	public String returnJsonBannerImg(String _jsonData) {
		// jsonString = _jsonData;
		Log.d("returnJson", "returnJson:" + _jsonData);
		// automaticArray = null;
		// quartzArray = null;
		String bannerStr = null;
		try {
			JSONArray jsonArray = new JSONArray(_jsonData);
			bannerStr = jsonArray.getString(0);
			Log.d("returnJson", "returnJson  returnJsonBannerImg :" + bannerStr);
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return bannerStr;
	}
		
	  public void changeLanguage(int _index){
	    	if( UserProfile.getInstance().setLanguage(_index)){
	    		 Resources res = myContext.getResources();
	    		
	    		 // Change locale settings in the app.    		  
	    		 DisplayMetrics dm = res.getDisplayMetrics();    		
	    		 android.content.res.Configuration conf = res.getConfiguration();    		

	    		 switch (UserProfile.getInstance().getCurrentLanguageIndex()) {
	    		 	case UserProfile.LANGUAGE_INDEX_EN:
	    		 		conf.locale = Locale.ENGLISH;
	    		 		break;
	    		 		
	    		 	case UserProfile.LANGUAGE_INDEX_ZHCN:
	    		 		conf.locale = Locale.SIMPLIFIED_CHINESE;
	    		 		break;
	    		 		
	    		 	case UserProfile.LANGUAGE_INDEX_ZHHK:
	    		 		conf.locale = Locale.CHINESE;
	    		 		break;
					default:
						break;
	    		 }    		 
 		 
	    		 Log.d("Reload View", "Band Page Reload la~~~");
	    		 listener.didRefreshLanguage();
	    		 res.updateConfiguration(conf, dm);	    	
	    		 	    		 
	    	}    	
	    	
//	    	 mainLayout.setDrawingCacheEnabled(true);   	 
//	    	 Bitmap screenCapture = mainLayout.getDrawingCache();   
//	    	tmpImgView.setImageBitmap(screenCapture);
//	    	tmpImgView.invalidate();	    	
	    	
	    }
	
	public ViewGroup getView(){
		return mainLayout;		
	}	
	
	interface HomepageHandler{
		void didRefreshLanguage();
	}
	
	class RefreshHandler extends Handler{
		@Override  
		public void handleMessage(Message msg) {  
									
			Resources res = myContext.getResources();			 
			DisplayMetrics dm = res.getDisplayMetrics();    		    		
			android.content.res.Configuration conf = res.getConfiguration();    		
						 
			myLanguageBtn.setBackgroundDrawable(null);
			switch(UserProfile.getInstance().getCurrentLanguageIndex()){
				case UserProfile.LANGUAGE_INDEX_EN:
					conf.locale = Locale.ENGLISH;
					myLanguageBtn.setBackgroundResource(R.drawable.homepage_enbtn);
					break;					
					
				case UserProfile.LANGUAGE_INDEX_ZHHK:
					conf.locale = Locale.CHINESE;
					myLanguageBtn.setBackgroundResource(R.drawable.homepage_zhbtn);
					break;
					
				case UserProfile.LANGUAGE_INDEX_ZHCN:
					conf.locale = Locale.SIMPLIFIED_CHINESE;
					myLanguageBtn.setBackgroundResource(R.drawable.homepage_chbtn);
					break;
			}			    		 
   		 	listener.didRefreshLanguage();
   		 	res.updateConfiguration(conf, dm);   			
   		 	PreferenceConnector.writeString(myContext, UserProfile.getInstance().USING_LANG, UserProfile.getInstance().language);   		   
   		 	Log.d("Load", "load in home page : " + UserProfile.getInstance().getCurrentLanguageIndex());
   		 	Log.d("ernest borel", "home page load Rest : " + myContext.getString(R.string.brand_videoheader));
		}
	}

	@Override
	public void apiLoadComplete() {
		// TODO Auto-generated method stub
		UserProfile _userProfile = UserProfile.getInstance();
		String mainKey = _userProfile.buildKey(UserProfile.API_NAME_INIT,
				"banner");
		
		String _jsonString = _userProfile.getData(mainKey);
		
		if(_jsonString==null||_jsonString.indexOf("http")<0){
			return;
		}
		final String bannerStr = returnJsonBannerImg(_jsonString);
		handler.post(new Runnable() {
			@Override
			public void run() {
				HomePage_bg.setBackgroundColor(Color.BLACK);
				
				ImageLoader imageLoader=new ImageLoader(myContext,R.drawable.collection03);
//				HomePage_bg.setBackgroundColor(Color.parseColor("#000000"));
				imageLoader.DisplayImage(bannerStr, HomePage_bg);
			}
		});
		

	}
}
