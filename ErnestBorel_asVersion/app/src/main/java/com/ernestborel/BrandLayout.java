package com.ernestborel;


import hk.SharedPreferencesSetting.UserProfile;
import hk.SharedPreferencesSetting.UserProfile.UserProfileHandler;

import com.ernestborel.storeslocation.LocationStoreDataListener;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Typeface;

import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;

import android.widget.Button;
import android.widget.FrameLayout;

import android.widget.ScrollView;
import android.widget.TextView;


public class BrandLayout implements UserProfileHandler{

	ScrollView myScrollView;
	FrameLayout mainLayout;
	TextView brandContent;
	TextView brandHeader;
	TextView brandCopyRight;
	
	Activity myActivity;
	
	Button playVideoBtn;
	
	UIHandler myHandler = new UIHandler();
	
	String contentString;
	
	public BrandLayout(Activity _act) {		
		myActivity = _act;		
		
		LayoutInflater _layoutInflater = (LayoutInflater)_act.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		mainLayout = (FrameLayout) _layoutInflater.inflate(R.layout.layout_brand, null);		
		myScrollView = (ScrollView)mainLayout.findViewById(R.id.Brand_scrollView);		
		myScrollView.setHorizontalScrollBarEnabled(false);
		myScrollView.setVerticalScrollBarEnabled(false);
		
		brandHeader = (TextView)myScrollView.findViewById(R.id.Brand_Header);
		brandCopyRight = (TextView)myScrollView.findViewById(R.id.Brand_CopyRight);
		brandContent = (TextView)myScrollView.findViewById(R.id.Brand_Content);	
		
		brandHeader.setText(myActivity.getString(R.string.brand_videoheader));
		brandCopyRight.setText(myActivity.getString(R.string.brand_videocopyright));
//		brandContent.setText(myActivity.getString(R.string.brand_content));
		   
		Log.d("ernest borel", "Reset ~~~in ~~~~");
		Log.d("ernest borel", "Reset Language  in brand : " + myActivity.getString(R.string.title_brand));
		Log.d("ernest borel", "Rest : " + myActivity.getString(R.string.brand_videoheader));
		
//		Typeface face=Typeface.createFromAsset(myActivity.getAssets(), "fonts/times.ttf"); 
//		brandHeader.setTypeface(face);
//		brandCopyRight.setTypeface(face);
//		brandContent.setTypeface(face);
		
		playVideoBtn = (Button)myScrollView.findViewById(R.id.Brand_MovieBtn);
		playVideoBtn.setOnClickListener(new OnClickListener() {			
			@Override
			public void onClick(View v) {								
				Intent _intent = new Intent(myActivity, VideoActivity.class);
				myActivity.startActivity(_intent);
				_intent = null;									
			}
		});	
		
		playVideoBtn.setClickable(false);
	}
	
	public void showBandView(){
		playVideoBtn.setClickable(true);
		
		contentString = "";
		brandContent.setText(contentString);
		UserProfile _userProfile = UserProfile.getInstance();
		_userProfile.setListener(this);
		_userProfile.loadHistory();
	}
	
	public void hideBandView(){
		playVideoBtn.setClickable(false);
	}
	
	public ViewGroup getView(){
		return mainLayout;
	}
	
	public void reloadView(){		
		myHandler.sendEmptyMessage(1);
	}
	
	class UIHandler extends Handler{
		
		@Override  
		public void handleMessage(Message msg) {  			
			Log.d("Reload View", "Band Page Reload la~~~");	
			//			brandContent.setText(myActivity.getString(R.string.brand_content));
			brandContent.setText(contentString);
			brandCopyRight.setText(myActivity.getString(R.string.brand_videocopyright));
			brandHeader.setText(myActivity.getString(R.string.brand_videoheader));
		}		
	}

	@Override
	public void apiLoadComplete() {
		// TODO Auto-generated method stub

		UserProfile _userProfile = UserProfile.getInstance();
		String mainKey = _userProfile.buildKey(UserProfile.API_NAME_HISTORY, "data");
		Log.d("BrandLayout", "BrandLayout BrandLayout:"+mainKey);
		String _jsonString = _userProfile.getData(mainKey);
		contentString = _jsonString;
		reloadView();
	}
}
