package com.ernestborel;

import hk.SharedPreferencesSetting.UserProfile;


import java.util.ArrayList;
import java.util.Timer;
import java.util.TimerTask;

import com.Util.CategoryListener;
import com.Util.MainActivityListener;
import com.Util.PageEventHandler;
import com.ernestborel.storeslocation.LocationDataController;
import com.ernestborel.storeslocation.LocationListTableAdapter;
import com.ernestborel.storeslocation.LocationStoreDataListener;
import com.ernestborel.storeslocation.StoreDetailsPage;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Typeface;

import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;

import android.widget.AdapterView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.RelativeLayout;
import android.widget.TextView;

@SuppressLint("HandlerLeak")
public class StoresLocationLayout implements LocationStoreDataListener, CategoryListener{
	
	LinearLayout mainLayout;
	LinearLayout tableFrame;
	LocationDataController mylocationData = new LocationDataController(this);
	LocationListTableAdapter myDataAdapter;
	
	StoreDetailsPage detailsPage = null;
	
	Context myContext;
	
	MainActivityListener listener;
	
	LinearLayout movingTable;
	ImageView listTmpImg;
	
	Timer myTimer = new Timer();
	
	RelativeLayout subTopBar;
	TextView subTitle;
	
	String subTitleTxt = "";
	int selectedRegionIndex;
	int selectedCountryIndex;
	int selectedCityIndex;
	int selectedStoreIndex;
		
	int currentLocationLayoutIndex;
	
	int currentPage; 
	private final int LOCATION_PAGING_REGION	= 0;
	private final int LOCATION_PAGING_COUNTRY	= 1;
	private final int LOCATION_PAGING_CITY		= 2;
	private final int LOCATION_PAGING_STORE		= 3;	
	private final int LOCATION_PAGING_CITY_DETAILS	= 4;
	
	private final int MSG_RELOAD_TABLE	= 1;
	private final int MSG_NEXT_PAGE	= 2;
	private final int MSG_PREVIOUS_PAGE	= 3;
	private final int MSG_UPDATE_SUBTITLE_BAR = 4;
	private final int MSG_EMPTY_TABLE	= 5;
	
	String selectedStoreID;
	
	ListView myListView;	
	
	UIHandler myHandler = new UIHandler();
	
	Animation prepareForNext;
	Animation prepareForPrevious;
	
	Animation nextTableAnim;	
	Animation previousTableAnim;

	public int pageType; 
	
	public StoresLocationLayout(Context _context, MainActivityListener _listener) {
		LayoutInflater _layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		mainLayout = (LinearLayout)_layoutInflater.inflate(R.layout.layout_location, null);
		
		myContext = _context;
		
		//movingTable = (LinearLayout)mainLayout.findViewById(R.id.Location_MovingTable);
		//listTmpImg = (ImageView)movingTable.findViewById(R.id.Location_ListImg);
		
		tableFrame = (LinearLayout)mainLayout.findViewById(R.id.Location_TableFrame);
		subTopBar = (RelativeLayout)tableFrame.findViewById(R.id.Location_SubTitleBG);
		
//		Typeface face=Typeface.createFromAsset(myContext.getAssets(), "fonts/times.ttf");
		subTitle = (TextView)subTopBar.findViewById(R.id.Location_SubTitle);
//		subTitle.setTypeface(face);
		
		myDataAdapter = new LocationListTableAdapter(_context);
		
		myListView = (ListView)tableFrame.findViewById(R.id.Location_Table);
		myListView.setAdapter(myDataAdapter);
		myListView.setTextFilterEnabled(true);
		
		listener = _listener;		
		
		myListView.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				Log.d("My List View", "Position : " + position + " ID : " + id);
				didSelectCell(position);
			}
		});
//	
//		nextTableAnim = new TranslateAnimation(0, UserProfile.getInstance().screenWidth * -1, 0, 0);
//		nextTableAnim.setDuration(2000);
//		nextTableAnim.setFillAfter(false);		
//		
//		prepareForNext = new 
	}
	
	public void didSelectCell(int _position){		
				
		switch (currentPage) {
			case LOCATION_PAGING_REGION:
				selectedRegionIndex = _position;
				loadTable(currentPage + 1);				
				break;
				
			case LOCATION_PAGING_COUNTRY:
				selectedCountryIndex = _position;
				ArrayList<String> _city = mylocationData.getCity(selectedRegionIndex, selectedCountryIndex);
				Log.d("City Size", "City Size : " + _city.size());
				if( _city.size() == 1){
					currentPage += 1;
					subTitleTxt = mylocationData.getCountryData(selectedRegionIndex).get(selectedCountryIndex);
					mylocationData.loadStoreInformation(mylocationData.getCityID(selectedRegionIndex, selectedCountryIndex, 0), pageType);
				}else{
					loadTable(currentPage + 1);	
				}								
				break;
								
			case LOCATION_PAGING_CITY:
				selectedCityIndex = _position;		
				subTitleTxt = myDataAdapter.getTableData().get(selectedCityIndex);									
				mylocationData.loadStoreInformation(mylocationData.getCityID(selectedRegionIndex, selectedCountryIndex, _position), pageType);
				break;
				
			case LOCATION_PAGING_STORE:						
				selectedStoreIndex = _position;
				selectedStoreID = myDataAdapter.getStoreID(selectedStoreIndex);
				loadTable(currentPage + 1);				
				break;				
			
			default:
				break;
		}				
	}
	
	public void loadTable(int _toPage){					
		if( currentPage > _toPage){
			myHandler.sendEmptyMessage(MSG_PREVIOUS_PAGE);
		}else{
			myHandler.sendEmptyMessage(MSG_NEXT_PAGE);
		}
		setCurrentPage(_toPage);
	}
		
	public void showLocationStoreView(){
		setCurrentPage(LOCATION_PAGING_REGION);		
		mylocationData.reloadLocationData(pageType);				
	}
	
	public ViewGroup getView(){
		return mainLayout;
	}
	
	public void setCurrentPage(int _page){
		currentPage = _page;
		
		myTimer.schedule(new TimerTask() {
			@Override
			public void run() {
				myHandler.sendEmptyMessage(MSG_UPDATE_SUBTITLE_BAR);				
			}
		}, PageEventHandler.PAGE_EVENT_ANIMATION_TIME);			
	}
	
	public void hideView(){
		myDataAdapter.setCurrentSelected(-1);
		myHandler.sendEmptyMessage(MSG_EMPTY_TABLE);
	}

	/*** LocationStoreDataListener method */
	@Override
	public void locationDataFinishUpdate(int _type){		
		Log.d("Loading Data", "updated");
		if( _type == LocationDataController.LOADING_DATA_LOCATION_LIST){			
			setCurrentPage(LOCATION_PAGING_REGION);
			myHandler.sendEmptyMessage(MSG_RELOAD_TABLE);			
		}else{
			loadTable(currentPage + 1);
		}
	}
	
	class UIHandler extends Handler{
		@Override  
		public void handleMessage(Message msg) {  			
			PageEventHandler _handler = PageEventHandler.getInstance();			
									
			switch( msg.what){			
				case MSG_RELOAD_TABLE:
					selectedRegionIndex = selectedCountryIndex = selectedCityIndex = selectedStoreIndex = -1; 
					myDataAdapter.setCurrentSelected(-1);
					tableFrame.setVisibility(View.VISIBLE);
					myDataAdapter.setCurrentType(LocationListTableAdapter.CELL_TYPE_SIMPLE);
					myDataAdapter.setCurrentArray(mylocationData.getRegionData());
					
					subTopBar.setVisibility(View.GONE);	
					myDataAdapter.notifyDataSetChanged();
					break;
					
				case MSG_NEXT_PAGE:						
					_handler.fadeOutAndIn(tableFrame);
					
					break;
				
				case MSG_PREVIOUS_PAGE:
					_handler.fadeOutAndIn(tableFrame);					
					break;
					
				case MSG_UPDATE_SUBTITLE_BAR:
					if(detailsPage != null){
						detailsPage.getView().removeAllViews();
						mainLayout.removeView(detailsPage.getView());
						detailsPage = null;
					}
					
					switch (currentPage) {
						case LOCATION_PAGING_REGION:				
							myDataAdapter.setCurrentSelected(selectedRegionIndex);
							tableFrame.setVisibility(View.VISIBLE);
							myDataAdapter.setCurrentType(LocationListTableAdapter.CELL_TYPE_SIMPLE);
							myDataAdapter.setCurrentArray(mylocationData.getRegionData());							
							break;
							
						case LOCATION_PAGING_COUNTRY:
							myDataAdapter.setCurrentSelected(selectedCountryIndex);
							subTitleTxt = mylocationData.getRegionData().get(selectedRegionIndex);
							tableFrame.setVisibility(View.VISIBLE);			
							myDataAdapter.setCurrentType(LocationListTableAdapter.CELL_TYPE_SIMPLE);
							myDataAdapter.setCurrentArray(mylocationData.getCountryData(selectedRegionIndex));
							break;
							
						case LOCATION_PAGING_CITY:
							myDataAdapter.setCurrentSelected(selectedCityIndex);
							subTitleTxt = mylocationData.getCountryData(selectedRegionIndex).get(selectedCountryIndex);
							tableFrame.setVisibility(View.VISIBLE);		
							myDataAdapter.setCurrentType(LocationListTableAdapter.CELL_TYPE_SIMPLE);
							myDataAdapter.setCurrentArray(mylocationData.getCity(selectedRegionIndex, selectedCountryIndex));
							break;
							
						case LOCATION_PAGING_STORE:			
							myDataAdapter.setCurrentSelected(selectedStoreIndex);
							tableFrame.setVisibility(View.VISIBLE);		
							myDataAdapter.setCurrentType(LocationListTableAdapter.CELL_TYPE_NORMAL);
							myDataAdapter.setNoramlStyleArray(mylocationData.getShopData());
							break;
							
						case LOCATION_PAGING_CITY_DETAILS:
							UserProfile.getInstance().setStoreDetailsURL(selectedStoreID, pageType);
							myDataAdapter.setCurrentSelected(selectedStoreIndex);
							tableFrame.setVisibility(View.GONE);							
							detailsPage = new StoreDetailsPage(myContext);	
							mainLayout.addView(detailsPage.getView());							
							break;
				
						default:
							break;
					}
				
					if( currentPage != LOCATION_PAGING_REGION){
						subTopBar.setVisibility(View.VISIBLE);
						subTitle.setText(subTitleTxt);
						listener.didShowBackBtn();
					}else{
						subTopBar.setVisibility(View.GONE);
						listener.didHideBackBtn();
					}
					myDataAdapter.notifyDataSetChanged();		
					break;
										
				case MSG_EMPTY_TABLE:
					subTopBar.setVisibility(View.GONE);	
					myDataAdapter.setCurrentType(LocationListTableAdapter.CELL_TYPE_EMPTY);
					myDataAdapter.notifyDataSetChanged();
					break;
			}	
			_handler = null;
		}
	}
	
	//Category Listener
	@Override
	public void backEventRequest() {
		if( currentPage > 0){
			if( currentPage == LOCATION_PAGING_STORE){
				ArrayList<String> _city = mylocationData.getCity(selectedRegionIndex, selectedCountryIndex);
				if( _city.size() == 1){
					subTitleTxt = mylocationData.getCountryData(selectedRegionIndex).get(selectedCountryIndex);
					loadTable(currentPage - 2);
				}else{
					loadTable(currentPage - 1);	
				}
			}else{
				loadTable(currentPage - 1);	
			}											
		}		
	}
}
