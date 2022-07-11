package com.ernestborel.CollectionObject;

import java.util.ArrayList;

import hk.SharedPreferencesSetting.NetworkConnectChecker;
import hk.SharedPreferencesSetting.UserProfile;
import hk.SharedPreferencesSetting.UserProfile.UserProfileHandler;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Typeface;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;

import android.widget.Button;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.Util.PageEventHandler;
import com.Util.RealView.RealViewPagerController;
import com.Util.RealView.RealViewPagerControllerListener;
import com.ernestborel.R;
//import com.ernestborel.wallpaper.WallpaperPage;

@SuppressLint({ "ViewConstructor", "HandlerLeak" })
public class CollectionDetailPage extends RelativeLayout implements RealViewPagerControllerListener, UserProfileHandler, OnClickListener{

	RefreshHandler handler = new RefreshHandler();
	CollectionDataListener collectionDataListener;
	private Context context;
	private View view;
	RealViewPagerController pagerController;
	
	private TextView watchNameText;
//	private PagePointLiner pagePointLiner;
	private TextView watchTotalText;
	private ViewGroup scrollView = null;
	private Button leftArrowBtn;
	private Button rightArrowBtn;
	
	private String detailId = null;
	private String category;
	private String watchName;
	private int page = 0;
	
	private JSONArray jsonArray = null;
	
	final int LOAD_JSON = 1;
	final int LOAD_MODEL = 2;
	final int SET_POSITION = 3;
	final int SHOW_LEFT_BTN = 4;
	final int HIDE_LEFT_BTN = 5;
	final int SHOW_RIGHT_BTN = 6;
	final int HIDE_RIGHT_BTN = 7;
	final int BUILD_PAGE_POINT = 8;
	final int API_LOADCOMPLETE = 9;
	
	ArrayList<CollectionDetailLayout> wallpaperList = new ArrayList<CollectionDetailLayout>();
	
	public CollectionDetailPage(Context _context, String _id, String _name, String _category, CollectionDataListener _collectionDataListener) {
		super(_context);
		context = _context;
		collectionDataListener = _collectionDataListener;
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.layout_collection_listview_detail_page,this);
		
//		Typeface face=Typeface.createFromAsset(_context.getAssets(), "fonts/times.ttf");
		watchTotalText = (TextView) view.findViewById(R.id.Collection_Detail_ScrollTotalName);
		ViewGroup _viewGroup = (ViewGroup) view.findViewById(R.id.Collection_Detail_PageLineLayout);
//		pagePointLiner = new PagePointLiner(_context, _viewGroup);
		scrollView = (ViewGroup) view.findViewById(R.id.Collection_Detail_ScrollLayout);
		watchNameText = (TextView) view.findViewById(R.id.Collection_Detail_WatchName);
		watchNameText.setText(_name);
//		watchTotalText.setTypeface(face);
		
		leftArrowBtn = (Button) view.findViewById(R.id.Collection_Detail_LeftArrow);
		rightArrowBtn = (Button) view.findViewById(R.id.Collection_Detail_RightArrow);
		leftArrowBtn.setOnClickListener(this);
		rightArrowBtn.setOnClickListener(this);
		
		watchName = _name;
		detailId = _id;
		category = _category;
		handler.sendEmptyMessage(LOAD_JSON);
	}
	
	public void onDestroy(){
		
		if(pagerController != null)
		{
			pagerController.release();
			pagerController = null;
		}
		wallpaperList.removeAll(wallpaperList);
		
		handler = null;
		context = null;
		view = null;
		detailId = null;
		category = null;
		watchName = null;
		jsonArray = null;
		collectionDataListener = null;
		
//		if(pagePointLiner != null)
//		{
//			pagePointLiner.onDestroy();
//			pagePointLiner = null;
//		}
 	}
	
	class RefreshHandler extends Handler {
    	@Override  
    	public void handleMessage(Message msg) {
    		switch (msg.what) {
			case LOAD_JSON:
				startLoadingJson();
				break;
				
			case LOAD_MODEL:
				buildModel();
				break;
				
			case SET_POSITION:
				setPosition();
				break;
				
			case SHOW_LEFT_BTN:
				leftBtn(true);
				break;
				
			case HIDE_LEFT_BTN:
				leftBtn(false);
				break;
				
			case SHOW_RIGHT_BTN:
				rightBtn(true);
				break;
				
			case HIDE_RIGHT_BTN:
				rightBtn(false);
				break;
				
			case BUILD_PAGE_POINT:
				buildPagePoint();
				break;
				
			case API_LOADCOMPLETE:
				apiBuildComplete();
			}
    	}
	}
	
	private void startLoadingJson() {
		UserProfile _userProfile = UserProfile.getInstance();
		_userProfile.setListener(this);
		_userProfile.loadCollection(detailId, category);
	}

	@Override
	public void apiLoadComplete() {
		// TODO Auto-generated method stub
		handler.sendEmptyMessage(API_LOADCOMPLETE);
	}

	public void apiBuildComplete()
	{
		UserProfile _userProfile = UserProfile.getInstance();
		String mainKey = _userProfile.buildKey(UserProfile.API_NAME_COLLECTION, "watch", detailId);
		String _jsonString = _userProfile.getData(mainKey);
		
		if(!(NetworkConnectChecker.getInstance().getNetworkConnect()) && _jsonString == null)
		{
			collectionDataListener.backToTable();
		}else /*if(_jsonString == null)
		{
			_userProfile.setTimeData(_userProfile.getUpdateTime(_userProfile.API_NAME_LOCAITON));
		}else*/
		{
			try {
				jsonArray = new JSONArray(_jsonString);
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			page = 0;
			//handler.sendEmptyMessageDelayed(LOAD_MODEL, PageEventHandler.PAGE_EVENT_ANIMATION_TIME);
			handler.sendEmptyMessage(LOAD_MODEL);
			handler.sendEmptyMessage(SET_POSITION);
		}
	}

	private void buildModel()
	{
		pagerController = new RealViewPagerController(context, scrollView, this);
		wallpaperList.removeAll(wallpaperList);
		
		for(int i = 0; i < jsonArray.length(); i++){
			JSONObject _infoArray = null;
			try {
				_infoArray = jsonArray.getJSONObject(i);
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			if(_infoArray.length() > 0)
			{
				CollectionDetailLayout _page = new CollectionDetailLayout(context, watchName, _infoArray, collectionDataListener);
				pagerController.addView(_page);
				wallpaperList.add(_page);
			}
		}
		
		pagerController.startRealViewController();
		if(wallpaperList.size() <= 1)
		{
			handler.sendEmptyMessage(HIDE_RIGHT_BTN);
		}
		handler.sendEmptyMessage(HIDE_LEFT_BTN);
		handler.sendEmptyMessage(BUILD_PAGE_POINT);
	}
	
//	public void hideWallpaper(){
//		for( int i =0; i < wallpaperList.size(); i++){
//			CollectionDetailLayout _page = wallpaperList.get(i);
//			_page.releasePage();
//		}
//
//	}
	
	@Override
	public void showCurrentPage(int _index, View _currentView) {
		// TODO Auto-generated method stub
		page = _index;
		handler.sendEmptyMessage(SET_POSITION);
		
		pagerController.moveComplete();
		if(wallpaperList.size() <= 1)
		{
			handler.sendEmptyMessage(HIDE_RIGHT_BTN);
			handler.sendEmptyMessage(HIDE_LEFT_BTN);
		}else if(page <= 0)
		{
			handler.sendEmptyMessage(HIDE_LEFT_BTN);
			handler.sendEmptyMessage(SHOW_RIGHT_BTN);
		}else if(page >= wallpaperList.size()-1)
		{
			handler.sendEmptyMessage(SHOW_LEFT_BTN);
			handler.sendEmptyMessage(HIDE_RIGHT_BTN);
		}else
		{
			handler.sendEmptyMessage(SHOW_LEFT_BTN);
			handler.sendEmptyMessage(SHOW_RIGHT_BTN);
		}
	}

	@Override
	public void releasePage(View _page, int _index) {
		// TODO Auto-generated method stub
		wallpaperList.get(_index).releasePage();
		//this.removeView(wallpaperList.get(_index));
	}

	@Override
	public void hidePage(View _page, int _index) {
		// TODO Auto-generated method stub
		Log.d("Wallpaper", "Wallpaper Hide");
		wallpaperList.get(_index).hidePage();
	}

	@Override
	public void loadPage(View _page, int _index) {
		// TODO Auto-generated method stub
		wallpaperList.get(_index).showPage();
	}
	
	public void setPosition()
	{
		watchTotalText.setText((page+1)+"/"+wallpaperList.size());
//		pagePointLiner.setPage(page);
	}

	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		if(v == leftArrowBtn)
		{
			moveToLeft();
		}else if(v == rightArrowBtn)
		{
			moveToRight();
		}
	}
	
	public void leftBtn(boolean _show)
	{
		if(_show)
		{
			leftArrowBtn.setVisibility(View.VISIBLE);
		}else
		{
			leftArrowBtn.setVisibility(View.GONE);
		}
	}
	
	public void rightBtn(boolean _show)
	{
		if(_show)
		{
			rightArrowBtn.setVisibility(View.VISIBLE);
		}else
		{
			rightArrowBtn.setVisibility(View.GONE);
		}
	}
	
	public void moveToLeft()
	{
		page -= 1;
		if(page < 0)
		{
			page = 0;
		}else
		{
			pagerController.moveToPage(page);
		}
	}
	
	public void moveToRight()
	{
		page += 1;
		if(page > wallpaperList.size()-1)
		{
			page = wallpaperList.size()-1;
		}else
		{
			pagerController.moveToPage(page);
		}
	}
	
	private void buildPagePoint()
	{
//		pagePointLiner.setTotalPage(wallpaperList.size());
	}
}
