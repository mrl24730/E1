package com.ernestborel.storeslocation;

import hk.SharedPreferencesSetting.UserProfile;

import com.ernestborel.R;

import android.annotation.SuppressLint;
import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.ViewGroup;
import android.view.View;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.widget.LinearLayout;

@SuppressLint("SetJavaScriptEnabled")
public class StoreDetailsPage {

	LinearLayout mainLayout;
	WebView myWebView;	
	
	public StoreDetailsPage(Context _context) {		
		LayoutInflater _layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		mainLayout = (LinearLayout)_layoutInflater.inflate(R.layout.layout_storedetails, null);
				
		myWebView = (WebView)mainLayout.findViewById(R.id.Store_DetailsMap);
		myWebView.loadUrl(UserProfile.getInstance().getStoreDetailsURL());	
		
		myWebView.setScrollBarStyle(View.SCROLLBARS_INSIDE_OVERLAY);
		WebSettings webSettings = myWebView.getSettings();
		webSettings.setJavaScriptEnabled(true);
		
		Log.d("Web View", "Load URl : " + UserProfile.getInstance().getStoreDetailsURL());
	}
	
	public ViewGroup getView(){
		return mainLayout;
	}
}
