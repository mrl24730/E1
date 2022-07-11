package com.ernestborel;

import java.util.ArrayList;
import java.util.Hashtable;

import hk.SharedPreferencesSetting.DataHandler;
import hk.SharedPreferencesSetting.LanguageHandler;
import hk.SharedPreferencesSetting.UserProfile;
import hk.SharedPreferencesSetting.UserProfile.UserProfileHandler;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.Util.CategoryListener;
import com.Util.MainActivityListener;
import com.Util.PageEventHandler;

import com.ernestborel.CollectionObject.CollectionDataListener;
import com.ernestborel.CollectionObject.CollectionDetailLargeLayout;
import com.ernestborel.CollectionObject.CollectionDetailPage;
import com.ernestborel.CollectionObject.CollectionListTableLayout;
import com.ernestborel.CollectionObject.CollectionListTableLayout.CollectionListTableHandler;
import com.ernestborel.CollectionObject.TryMeCameraView;

import android.annotation.SuppressLint;
import android.app.Activity;

import android.content.Context;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.RelativeLayout;

@SuppressLint({ "HandlerLeak", "ViewConstructor" })
public class CollectionPageLayout extends RelativeLayout implements OnClickListener, UserProfileHandler, CollectionListTableHandler, CategoryListener, CollectionDataListener{
	RefreshHandler handler = new RefreshHandler();
	Activity activity;
	Context context;
//	PageEventHandler pageEventHandler;
	MainActivityListener listener;
	
//	ProgressDialog pdialog;
	private View view;
	private ViewGroup mainView = null;
	private ViewGroup largeView = null;
	private Button automaticBtn;
	private Button quartzBtn;
	
	private CollectionListTableLayout automaticList = null;
//	private CollectionListTableLayout quartzList = null;
	private CollectionDetailPage collectionDetailPage = null;
	private CollectionDetailLargeLayout collectionDetailLargeLayout = null;
	private TryMeCameraView tryMeCameraView = null;
	
	private String imageUrl;
	
	final int SHOW_PAGE_AUTOMATIC = 1;
	final int SHOW_PAGE_QUARTZ = 2;
	final int SHOW_DETAIL_AUTOMATIC = 3;
	final int SHOW_DETAIL_QUARTZ = 4;
	final int LOAD_JSON = 5;
	final int LOAD_LARGE = 6;
	final int KILL_LARGE = 7;
	final int LOAD_TRY = 8;
	final int KILL_TRY = 9;
	
	int page = 999;
	int autoWatchCount;
	int clickPostion = -1;
	final int PAGE_AUTOMATIC = 0;
	final int PAGE_QUARTZ = 1;
	final int PAGE_AUTOMATIC_DETAIL = 2;
	final int PAGE_QUARTZ_DETAIL = 3;
	
	private String jsonString;
//	private JSONArray automaticArray = null;
//	private JSONArray quartzArray = null;
	ArrayList<Hashtable<String, String>> automaticArray = null;
//	ArrayList<Hashtable<String, String>> quartzArray = null;
	
	private String detailID = null;
	private String detailName = null;
	
	public CollectionPageLayout(Activity a, Context _context, MainActivityListener _listener) {
		super(_context);
		activity = a;
		context = _context;
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.layout_collection,this);
		
		//pageEventHandler = PageEventHandler.getInstance();
		listener = _listener;
		
		mainView = (ViewGroup) view.findViewById(R.id.Collection_ListLayout);
		largeView = (ViewGroup) view.findViewById(R.id.Collection_Detail_LargeLayout);
		automaticBtn = (Button) view.findViewById(R.id.Collection_AutomaticBtn);
		quartzBtn = (Button) view.findViewById(R.id.Collection_QuartzBtn);
		automaticBtn.setOnClickListener(this);
		quartzBtn.setOnClickListener(this);
	}
	
	public ViewGroup getView(){
		return this;		
	}
	
	public void onDestroy(){
 	}
	
	class RefreshHandler extends Handler {
    	@Override  
    	public void handleMessage(Message msg) {
    		switch (msg.what) {
			case SHOW_PAGE_AUTOMATIC:
    			setAutomatic(true);
				break;
				
			case SHOW_DETAIL_AUTOMATIC:
    			showDetail(UserProfile.COLLECTION_CATEGORY_AUTOMATIC);
				break;
				
			case SHOW_DETAIL_QUARTZ:
    			showDetail(UserProfile.COLLECTION_CATEGORY_QUARTZ);
				break;

			case SHOW_PAGE_QUARTZ:
    			setQuartz(true);
				break;

			case LOAD_JSON:
    			startLoadingJson();
				break;
				
			case LOAD_LARGE:
				buildLargePage();
				break;
				
			case KILL_LARGE:
				killLargePage();
				break;
				
			case LOAD_TRY:
				showCameraView();
				break;
				
			case KILL_TRY:
				killLargePage();
				break;
				
			default:
				break;
			}
    	}

    	public void sleep(long delayMillis) {  
    		this.removeMessages(0);  
    		sendMessageDelayed(obtainMessage(0), delayMillis);  
    	}
    }
	
	private void setAutomatic(Boolean _show)
	{
		if(automaticBtn.getVisibility() != View.VISIBLE)
		{
			automaticBtn.setVisibility(View.VISIBLE);
		}
		if(quartzBtn.getVisibility() != View.VISIBLE)
		{
			quartzBtn.setVisibility(View.VISIBLE);
		}
		
		page = PAGE_AUTOMATIC;
		removeAllView();
		if(automaticList == null && automaticArray != null)
		{
			automaticList = new CollectionListTableLayout(activity, context, automaticArray, clickPostion);
			mainView.addView(automaticList);
			automaticList.setListener(this);
//			if(quartzList != null){
//				PageEventHandler.getInstance().fadeOutAnimation(quartzList);
//			}
			PageEventHandler.getInstance().fadeOutAndIn(automaticList);
		}
		automaticBtn.setBackgroundDrawable(null);
		quartzBtn.setBackgroundDrawable(null);
		
		if(UserProfile.getInstance().screenWidth == 720){
			automaticBtn.setBackgroundResource(R.drawable.collection_auto_selected_720);
			quartzBtn.setBackgroundResource(R.drawable.collection_quartz_720);	
		}else{
			automaticBtn.setBackgroundResource(R.drawable.collection_auto_selected);
			quartzBtn.setBackgroundResource(R.drawable.collection_quartz);
		}
		
		
//		automaticBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.collection_auto_selected));
//		quartzBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.collection_quartz));
//		closePdialog();
	}
	
	private void setQuartz(Boolean _show)
	{
		page = PAGE_QUARTZ;
		removeAllView();
//		if(quartzList == null && quartzArray != null)
//		{
//			quartzList = new CollectionListTableLayout(activity, context, quartzArray, clickPostion);
//			mainView.addView(quartzList);
//			quartzList.setListener(this);
//			if( automaticList != null){
//				PageEventHandler.getInstance().fadeOutAnimation(automaticList);
//			}
//			PageEventHandler.getInstance().fadeOutAndIn(quartzList);
//		}
		automaticBtn.setBackgroundDrawable(null);
		quartzBtn.setBackgroundDrawable(null);
		
		if(UserProfile.getInstance().screenWidth == 720){
			automaticBtn.setBackgroundResource(R.drawable.collection_auto_720);
			quartzBtn.setBackgroundResource(R.drawable.collection_quartz_selected_720);
		}else{
			automaticBtn.setBackgroundResource(R.drawable.collection_auto);
			quartzBtn.setBackgroundResource(R.drawable.collection_quartz_selected);
		}
//		automaticBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.collection_auto));
//		quartzBtn.setBackgroundDrawable(getResources().getDrawable(R.drawable.collection_quartz_selected));
//		closePdialog();
	}
	
	private void showDetail(String _category)
	{
		removeAllView();
		collectionDetailPage = new CollectionDetailPage(context, detailID, detailName, _category, this);
		mainView.addView(collectionDetailPage);
		PageEventHandler.getInstance().fadeOutAndIn(collectionDetailPage);
//		closePdialog();
	}
	
	private void removeAllView()
	{
//		hideWallpaper();
//		if(collectionDetailPage != null)
//		{
//			collectionDetailPage.hideWallpaper();
//		}
		mainView.removeAllViews();
		largeView.removeAllViews();
		if(automaticList != null)
		{
			automaticList.onDestroy();
			automaticList = null;
		}
		
//		if(quartzList != null)
//		{
//			quartzList.onDestroy();
//			quartzList = null;
//		}
		
		if(collectionDetailPage != null)
		{
			collectionDetailPage.onDestroy();
			collectionDetailPage = null;
		}
		
		if(collectionDetailLargeLayout != null)
		{
			collectionDetailLargeLayout.releaseView();
			collectionDetailLargeLayout = null;
		}
		
		if(tryMeCameraView != null)
		{
			tryMeCameraView.onDestroy();
			tryMeCameraView = null;
		}
	}
	
	private void startLoadingJson() {
		UserProfile _userProfile = UserProfile.getInstance();
		_userProfile.setListener(this);
		_userProfile.loadInit();

		automaticBtn.setBackgroundResource(R.drawable.collection_auto_selected);
		quartzBtn.setBackgroundResource(R.drawable.collection_quartz_selected);
//		DataHandler dataHandler = DataHandler.getInstance();
//		dataHandler.setListener(this);
//		String _url = NetworkAddress.initUrl();
//		dataHandler.getDataByUrl(_url, context);
	}
	
	public void apiLoadComplete()
	{
		UserProfile _userProfile = UserProfile.getInstance();
		String mainKey = _userProfile.buildKey(UserProfile.API_NAME_INIT, "watches");
		String _jsonString = _userProfile.getData(mainKey);
		returnJson(_jsonString);
	}
	
    public void returnJson(String _jsonData)
    {
    	jsonString = _jsonData;
//        Log.d("returnJson", "returnJson collect:"+jsonString);
    	automaticArray = null;
    	//quartzArray = null;
    	
    	JSONObject watchesArray = null;
		try {
			watchesArray = new JSONObject(jsonString);
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		try {
//			automaticArray = watchesArray.getJSONArray("automatic");
			automaticArray = jsonToArray(watchesArray.getJSONArray("automatic"));
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		autoWatchCount  = automaticArray.size();
		
		ArrayList<Hashtable<String, String>> quartzArray;
		try {
//			quartzArray = watchesArray.getJSONArray("quartz");
			quartzArray = jsonToArray(watchesArray.getJSONArray("quartz"));			
			automaticArray.addAll(quartzArray);			
			
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		page = PAGE_AUTOMATIC;
		handler.sendEmptyMessage(SHOW_PAGE_AUTOMATIC);
    }
    
    public ArrayList<Hashtable<String, String>> jsonToArray(JSONArray _object)
    {
    	ArrayList<Hashtable<String, String>> _array = new ArrayList<Hashtable<String,String>>();
    	for (int i = 0; i < _object.length(); i++) {
    		Hashtable<String, String> _hashtable = new Hashtable<String, String>();
    		JSONObject subItem = null;
    		try {
    			subItem = _object.getJSONObject(i);
    		} catch (JSONException e) {
    			// TODO Auto-generated catch block
    			e.printStackTrace();
    		}
    		
    		try {
    			
    			
//    				Log.d("", "retro_collection :"+subItem.getString("name")+"|"+subItem.getString("img"));
    			
        		_hashtable.put("name", subItem.getString("name"));
    		} catch (JSONException e) {
    			// TODO Auto-generated catch block
    			e.printStackTrace();
    		}
    		
    		try {
        		_hashtable.put("id", subItem.getString("id"));
    		} catch (JSONException e) {
    			// TODO Auto-generated catch block
    			e.printStackTrace();
    		}
    		
    		try {
        		_hashtable.put("img", subItem.getString("img"));
    		} catch (JSONException e) {
    			// TODO Auto-generated catch block
    			e.printStackTrace();
    		}
    		_array.add(_hashtable);
		}
    	return _array;
    }
    
    @Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
//    	showPdialog();
		if(v == automaticBtn)
		{
			if(page != PAGE_AUTOMATIC)
			{
				if(page != PAGE_AUTOMATIC_DETAIL)
				{
					clickPostion = -1;
				}
				page = PAGE_AUTOMATIC;
				handler.sendEmptyMessage(SHOW_PAGE_AUTOMATIC);
				listener.didHideBackBtn();
			}
		}else if(v == quartzBtn)
		{
			if(page != PAGE_QUARTZ)
			{
				if(page != PAGE_QUARTZ_DETAIL)
				{
					clickPostion = -1;
				}
				page = PAGE_QUARTZ;
				handler.sendEmptyMessage(SHOW_PAGE_QUARTZ);
				listener.didHideBackBtn();
			}
		}
    }

	@Override
	public void loadID(String _id, String _name, int _position) {
		// TODO Auto-generated method stub
		//Log.d("loadID", "loadID:"+_id);
		detailID = _id;
		detailName = _name;
		clickPostion = _position;
		
		int _message ;
		if ( _position >= autoWatchCount){
			page = PAGE_QUARTZ_DETAIL;
			_message = SHOW_DETAIL_QUARTZ;
		}else{
			page = PAGE_AUTOMATIC_DETAIL;
			_message = SHOW_DETAIL_AUTOMATIC;
		}
		handler.sendEmptyMessage(_message);
		listener.didShowBackBtn();

	}
	
	public void toFontView()
	{
//		removeAllView();
//		showPdialog();
		handler.sendEmptyMessage(LOAD_JSON);
//		startLoadingJson();
//		handler.sendEmptyMessage(SHOW_PAGE_AUTOMATIC);
	}

	@Override
	public void backEventRequest() {
		// TODO Auto-generated method stub
//		showPdialog();
		if(tryMeCameraView != null)
		{
			closeTry();
		}else if(page == PAGE_AUTOMATIC_DETAIL || (page == PAGE_QUARTZ_DETAIL))
		{
			handler.sendEmptyMessage(SHOW_PAGE_AUTOMATIC);
			listener.didHideBackBtn();
//		}else if(page == PAGE_QUARTZ_DETAIL)
//		{
//			handler.sendEmptyMessage(SHOW_PAGE_QUARTZ);
//			listener.didHideBackBtn();
		}
	}
	
	public void hideWallpaper()
	{
//		if(collectionDetailPage != null)
//		{
//			collectionDetailPage.hideWallpaper();
//		}

		automaticBtn.setVisibility(View.INVISIBLE);
		quartzBtn.setVisibility(View.INVISIBLE);
		removeAllView();
	}
	
	public void buildLargePage()
	{
		listener.didHideTopBar();
		collectionDetailLargeLayout = new CollectionDetailLargeLayout(context, imageUrl, this);
		largeView.addView(collectionDetailLargeLayout);
		largeView.setVisibility(View.VISIBLE);
		collectionDetailLargeLayout.setClickable(true);
	}
	
	private void showCameraView()
	{
		listener.didShowTopBar();
		listener.didShowBackBtn();
		tryMeCameraView = new TryMeCameraView(context, this, imageUrl);
		largeView.addView(tryMeCameraView);
		largeView.setVisibility(View.VISIBLE);
		tryMeCameraView.setClickable(true);
	}
	
	public void killLargePage()
	{
		listener.didShowTopBar();
		largeView.removeAllViews();
		largeView.setVisibility(View.GONE);
		if(collectionDetailLargeLayout != null)
		{
			collectionDetailLargeLayout.releaseView();
			collectionDetailLargeLayout = null;
		}
		if(tryMeCameraView != null)
		{
			tryMeCameraView.onDestroy();
			tryMeCameraView = null;
		}
	}
	
	@Override
	public void zoomWatch(String _imageUrl) {
		// TODO Auto-generated method stub
		imageUrl = _imageUrl;
		handler.sendEmptyMessage(LOAD_LARGE);
	}

	@Override
	public void closeZoom() {
		// TODO Auto-generated method stub
		handler.sendEmptyMessage(KILL_LARGE);
	}

	@Override
	public void backToTable() {
		// TODO Auto-generated method stub
		backEventRequest();
		DataHandler.getInstance().showpopUp(LanguageHandler.getInstance().connectFail(), context);
	}

	@Override
	public void tryWatch(String _imageUrl) {
		// TODO Auto-generated method stub
		imageUrl = _imageUrl;
		handler.sendEmptyMessage(LOAD_TRY);
	}

	@Override
	public void closeTry() {
		// TODO Auto-generated method stub
		handler.sendEmptyMessage(KILL_TRY);
	}
	
//	public void showPdialog()
//	{
//		if(pdialog != null)
//		{
//			pdialog.hide();
//			pdialog.dismiss();
//			pdialog = null;
//		}
//		pdialog = new ProgressDialog(context);		
//		pdialog.setCancelable(false);
//		pdialog.setMessage("Loading ....");
//		pdialog.show();
//	}
//	
//	public void closePdialog()
//	{
//		if(pdialog != null)
//		{
//			pdialog.hide();
//			pdialog.dismiss();
//			pdialog = null;
//		}
//	}
}
