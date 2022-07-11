package com.ernestborel.NewsObject;


import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import hk.ImageLoader.ImageLoader;
import hk.SharedPreferencesSetting.UserProfile;
import hk.SharedPreferencesSetting.UserProfile.UserProfileHandler;

import com.Util.PageEventHandler;
import com.ernestborel.R;

import android.content.Context;
//import android.graphics.Typeface;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.ScrollView;
import android.widget.TextView;

public class NewsInformationLayout extends RelativeLayout implements UserProfileHandler, OnClickListener{
	RefreshHandler handler = new RefreshHandler();
	//private Context context;
	private View view;
	public ImageLoader imageLoader;
	public NewsDataListener newsDataListener = null;
	
//	PageEventHandler pageEventHandler;
	private JSONArray jsonArray = null;
	private ScrollView scrollView;
	private ImageView infoImage;
	private TextView infoTitleTextView;
	private TextView infoDateTextView;
	private TextView infoDetailTextView;
	private Button newerBtn;
	private Button olderBtn;
	
	private int page;
	private int pageAction;
	private String date;
	private String myID;
	
	final int BUILD_PAGE = 0;
	final int NEWER_PAGE = 1;
	final int OLDER_PAGE = 2;
	final int SETUP_PAGE = 3;
		
	public NewsInformationLayout(Context _context, int _position, JSONArray _jsonArray, NewsDataListener _newsDataListener) {
		super(_context);
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.layout_news_listview_info,this);
		imageLoader = new ImageLoader(_context, R.drawable.news_info_image);
		newsDataListener = _newsDataListener;
//		pageEventHandler = PageEventHandler.getInstance();
		jsonArray = _jsonArray;
		page = _position;
		
		scrollView = (ScrollView) view.findViewById(R.id.News_ScrollView);
		ViewGroup _linearLayout = (ViewGroup) scrollView.findViewById(R.id.News_ScrollView_LinearLayout);
		infoImage = (ImageView) _linearLayout.findViewById(R.id.News_Image);
		
//		Typeface face=Typeface.createFromAsset(_context.getAssets(), "fonts/times.ttf");		
		
		infoTitleTextView = (TextView) _linearLayout.findViewById(R.id.News_Detail_Title);
		infoDateTextView = (TextView) _linearLayout.findViewById(R.id.News_Detail_Date);
		infoDetailTextView = (TextView) _linearLayout.findViewById(R.id.News_Detail_Desc);
		
//		infoTitleTextView.setTypeface(face);
//		infoDateTextView.setTypeface(face);
//		infoDetailTextView.setTypeface(face);
		
		ViewGroup _topBarLayout = (ViewGroup) view.findViewById(R.id.News_BtnLayout);
		newerBtn = (Button) _topBarLayout.findViewById(R.id.News_NewBtn);
		olderBtn = (Button) _topBarLayout.findViewById(R.id.News_OldBtn);
		newerBtn.setOnClickListener(this);
		olderBtn.setOnClickListener(this);
		
		handler.sendEmptyMessageDelayed(BUILD_PAGE, PageEventHandler.PAGE_EVENT_ANIMATION_TIME);
		
	}
	
	public void releaseNew(){
		handler = null;
		view = null;
		imageLoader.clearCache();
		imageLoader = null;
		
		jsonArray = null;	
		scrollView = null;
		infoImage = null;
		newsDataListener = null;
	}
	
	public void onDestroy(){
 	}
	
	class RefreshHandler extends Handler {
    	@Override  
    	public void handleMessage(Message msg) {
    		pageAction = msg.what;
    		switch (msg.what) {
			case BUILD_PAGE:
				buildPage(page);
				break;
				
			case NEWER_PAGE:
				page -= 1;
				backPage();
				handler.sendEmptyMessageDelayed(BUILD_PAGE, PageEventHandler.PAGE_EVENT_ANIMATION_TIME);
				//buildPage((page-1));
				break;
				
			case OLDER_PAGE:
				page += 1;
				nextPage();
				handler.sendEmptyMessageDelayed(BUILD_PAGE, PageEventHandler.PAGE_EVENT_ANIMATION_TIME);
				//buildPage((page+1));
				break;
				
			case SETUP_PAGE:
				setUpPage();
				break;
				
			}
    	}

    	public void sleep(long delayMillis) {  
    		this.removeMessages(0);  
    		sendMessageDelayed(obtainMessage(0), delayMillis);  
    	}
    }
	
	private void nextPage()
	{
		PageEventHandler.getInstance().fadeOutAndIn(this);
	}
	
	private void backPage()
	{
		PageEventHandler.getInstance().fadeOutAndIn(this);
	}
	
	private void buildPage(int _page)
	{
		if(_page >= 0 && _page < jsonArray.length())
		{
			infoImage.setImageBitmap(null);
			infoImage.setBackgroundDrawable(null);
			infoImage.setBackgroundResource(R.drawable.news_info_image);
			
			infoTitleTextView.setText("");
			infoDateTextView.setText("");
			infoDetailTextView.setText("");
			
			page = _page;
			if(newsDataListener != null)
			{
				newsDataListener.nowPage(_page);
			}
			Log.d("buildPage", "buildPage:"+page);
			JSONObject subItem = null;
			try {
				subItem = jsonArray.getJSONObject(page);
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			date = null;
			try {
				date = subItem.getString("date");
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			myID = null;
			try {
				myID = subItem.getString("id");
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			UserProfile _userProfile = UserProfile.getInstance();
			_userProfile.setListener(this);
			_userProfile.loadNews(myID);
			
//			if(pageAction == NEXT_PAGE)
//			{
//				backPage();
//			}else if(pageAction == OLD_PAGE)
//			{
//				nextPage();
//			}
			
			scrollView.fullScroll(ScrollView.FOCUS_UP);
			
			if(newerBtn.getVisibility() != View.VISIBLE)
			{
				newerBtn.setVisibility(View.VISIBLE);
			}
			if(olderBtn.getVisibility() != View.VISIBLE)
			{
				olderBtn.setVisibility(View.VISIBLE);
			}
			
			if(page == 0)
			{
				if(UserProfile.getInstance().screenWidth == 720){
					newerBtn.setBackgroundResource(R.drawable.news_newerbtn_selected_720);
				}else{
					newerBtn.setBackgroundResource(R.drawable.news_newerbtn_selected);	
				}
				
				//newerBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.news_newerbtn_selected));
			}else
			{
				if(UserProfile.getInstance().screenWidth == 720){
					newerBtn.setBackgroundResource(R.drawable.news_newerbtn_720);
				}else{
					newerBtn.setBackgroundResource(R.drawable.news_newerbtn);	
				}				
				//newerBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.news_newerbtn));
			}
			
			if(page >= jsonArray.length()-1)
			{
				if(UserProfile.getInstance().screenWidth == 720){
					olderBtn.setBackgroundResource(R.drawable.news_olderbtn_selected_720);
				}else{
					olderBtn.setBackgroundResource(R.drawable.news_olderbtn_selected);	
				}
				//olderBtn.setBackgroundDrawable(null);
				
				//olderBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.news_olderbtn_selected));
			}else
			{
				//olderBtn.setBackgroundDrawable(null);
				if(UserProfile.getInstance().screenWidth == 720){
					olderBtn.setBackgroundResource(R.drawable.news_olderbtn_720);
				}else{
					olderBtn.setBackgroundResource(R.drawable.news_olderbtn);
				}
				
				//olderBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.news_olderbtn));
			}
		}
	}

	@Override
	public void apiLoadComplete() {
		// TODO Auto-generated method stub
		handler.sendEmptyMessage(SETUP_PAGE);
	}
	
	private void setUpPage()
	{
		UserProfile _userProfile = UserProfile.getInstance();
		String mainKey = _userProfile.buildKey(UserProfile.API_NAME_NEWS, "img", myID);
		String _img = _userProfile.getData(mainKey);
		imageLoader.DisplayImage(_img, infoImage);
		
		mainKey = _userProfile.buildKey(UserProfile.API_NAME_NEWS, "title", myID);
		String _title = _userProfile.getData(mainKey);
		infoTitleTextView.setText(_title);
		
		mainKey = _userProfile.buildKey(UserProfile.API_NAME_NEWS, "source", myID);
		String _source = _userProfile.getData(mainKey);
		mainKey = _userProfile.buildKey(UserProfile.API_NAME_NEWS, "author", myID);
		String _author = _userProfile.getData(mainKey);
		mainKey = _userProfile.buildKey(UserProfile.API_NAME_NEWS, "date", myID);
		String _date = _userProfile.getData(mainKey);
		infoDateTextView.setText(_source+"\n"+_author+"\n"+_date);
		
		mainKey = _userProfile.buildKey(UserProfile.API_NAME_NEWS, "desc", myID);
		String _desc = _userProfile.getData(mainKey);
		infoDetailTextView.setText(_desc);
	}

	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		if(v == olderBtn && page < jsonArray.length() - 1){
			handler.sendEmptyMessage(OLDER_PAGE);
			
		}else if(v == newerBtn && page > 0)
		{
			handler.sendEmptyMessage(NEWER_PAGE);
		}
	}
}
