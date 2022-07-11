package com.ernestborel;

import hk.SharedPreferencesSetting.UserProfile;
import hk.SharedPreferencesSetting.UserProfile.UserProfileHandler;

import org.json.JSONArray;
import org.json.JSONException;

import com.Util.CategoryListener;
import com.Util.MainActivityListener;
import com.Util.PageEventHandler;
import com.ernestborel.NewsObject.NewsDataListener;
import com.ernestborel.NewsObject.NewsInformationLayout;
import com.ernestborel.NewsObject.NewsTableLayout;

import android.app.Activity;
import android.content.Context;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;

import android.view.ViewGroup;
import android.widget.LinearLayout;


public class NewsPageLayout extends LinearLayout implements CategoryListener, UserProfileHandler, NewsDataListener{
	RefreshHandler handler = new RefreshHandler();
	Activity activity;
	Context context;
	MainActivityListener listener;
	//PageEventHandler pageEventHandler;
	
	private ViewGroup view;
	private ViewGroup mainView = null;
	private JSONArray jsonArray = null;
	
	private NewsTableLayout newsTableLayout = null;
	private NewsInformationLayout newsInformationLayout = null;
	private int position = -1;
	
	final int LOAD_JSON = 0;
	final int BUILD_TABLE = 1;
	final int BUILD_INFO = 2;
	
	public NewsPageLayout(Activity a, Context _context, MainActivityListener _listener) {
		super(_context);
		activity = a;
		context = _context;
		listener = _listener;
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = (ViewGroup)layoutInflater.inflate(R.layout.layout_news_listview, this);
		ViewGroup listLinearView = (ViewGroup) view.findViewById(R.id.News_ListLinearLayout);
		mainView = (ViewGroup) listLinearView.findViewById(R.id.News_ListLayout);
		
		//pageEventHandler = PageEventHandler.getInstance();
	}
	
	public void onDestroy(){
 	}
	
	public ViewGroup getView(){
		return view;		
	}
	
	class RefreshHandler extends Handler {
    	@Override  
    	public void handleMessage(Message msg) {
    		switch (msg.what) {
			case LOAD_JSON:
    			loadJson();
				break;
				
			case BUILD_TABLE:
				buildTable();
				break;
				
			case BUILD_INFO:
				buildInfo();
				break;
			}
    	}

    	public void sleep(long delayMillis) {  
    		this.removeMessages(0);  
    		sendMessageDelayed(obtainMessage(0), delayMillis);  
    	}
    }
	
	private void loadJson()
	{
		clearView();
		UserProfile _userProfile = UserProfile.getInstance();
		_userProfile.setListener(this);
		_userProfile.loadInit();
	}

	@Override
	public void backEventRequest() {
		// TODO Auto-generated method stub
		handler.sendEmptyMessage(BUILD_TABLE);
	}

	@Override
	public void apiLoadComplete() {
		// TODO Auto-generated method stub
		Log.d("News", "News Api load finish!!!!!!");
		
		UserProfile _userProfile = UserProfile.getInstance();
		String mainKey = _userProfile.buildKey(UserProfile.API_NAME_INIT, "news");
		String _jsonString = _userProfile.getData(mainKey);
		
		jsonArray = null;
		try {
			jsonArray = new JSONArray(_jsonString);
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		handler.sendEmptyMessage(BUILD_TABLE);
	}
	
	public void clearView()
	{
		mainView.removeAllViews();
		if(newsTableLayout != null)
		{
			newsTableLayout.releaseNewsTableLayout();
			newsTableLayout = null;
		}
		
		if( newsInformationLayout != null){
			newsInformationLayout.releaseNew();
			newsInformationLayout.removeAllViews();
			newsInformationLayout = null;
		}
	}
	
	public void buildTable()
	{
		listener.didHideBackBtn();
		newsTableLayout = new NewsTableLayout(activity, context, jsonArray, this, position);
		mainView.addView(newsTableLayout);
		PageEventHandler.getInstance().fadeOutAndIn(mainView);
	}
	
	public void buildInfo()
	{
		listener.didShowBackBtn();
		clearView();
		newsInformationLayout = new NewsInformationLayout(context, position, jsonArray, this);
		mainView.addView(newsInformationLayout);
		PageEventHandler.getInstance().fadeOutAndIn(newsInformationLayout);
		
	}
	
	public void toFontView()
	{
		handler.sendEmptyMessage(LOAD_JSON);
//		handler.sendEmptyMessage(BUILD_TABLE);
		//handler.sendEmptyMessage(SHOW_PAGE_AUTOMATIC);
	}
	
	@Override
	public void loadID(int _position) {
		// TODO Auto-generated method stub
		position = _position;
		handler.sendEmptyMessage(BUILD_INFO);
	}

	@Override
	public void nowPage(int _page) {
		// TODO Auto-generated method stub
		position = _page;
	}
}
