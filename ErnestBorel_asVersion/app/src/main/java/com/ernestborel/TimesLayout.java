package com.ernestborel;

import hk.SharedPreferencesSetting.ScreenInformation;
import hk.SharedPreferencesSetting.UserProfile;
import hk.Util.Network.NetworkAddress;
import hk.Util.Network.NetworkInterface;
import hk.Util.Network.NetworkJob;
import hk.Util.Network.NetworkManager;

import java.util.Calendar;
import java.util.Date;
import java.util.TimeZone;
import java.util.Timer;

import org.json.JSONException;
import org.json.JSONObject;

import com.Util.CategoryListener;
import com.Util.Clock;
import com.Util.MainActivityListener;
import com.Util.Clock.ClockListener;
import com.ernestborel.times.TimeZoneData;
import com.ernestborel.times.TimeZoneTableLayout;
import com.ernestborel.times.TimeZoneTableLayout.TimeZoneTableListener;

import android.app.Activity;
import android.content.Context;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.TranslateAnimation;
import android.view.animation.Animation.AnimationListener;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;


public class TimesLayout implements ClockListener, CategoryListener, NetworkInterface, TimeZoneTableListener{
	RelativeLayout mainLayout;
	Activity activity;
	Context myContext;
	
	LinearLayout myLinearTable;
	Timer myTimer;
	
	RefreshHandler handler = new RefreshHandler();
	//int[] selectedCity = {-1, -1, -1};
	
	Animation deleteAnimation;
	Animation showCities;
	Animation hideCities;
	
	final int TOTAL_NUMBER_OF_CITY	= 3;
	
	private final int MSG_REMOVE_CELL = 4;
	private final int MSG_ADD_CELL = 5;
	private final int MSG_SHOW_TIMEZONE_TABLE	= 6;
	private final int MSG_HIDE_TIMEZONE_TABLE	= 7;
	private final int MSG_REFRESH_TIMEZONE_TABLE	= 8;
	private final int MSG_RELOAD_CELL = 9;
	
	private final int DELETE_CELL_ANIMATION_TIME	= 300;
	
	boolean isPlayingAnimation = false;
	
	TimeZoneTableLayout timeZoneTableLayout = null;
	
	int currentEditingCellIndex;
	
	MainActivityListener listener;
	
	public TimesLayout(Activity a, Context _context, MainActivityListener _listener){
		activity = a;
		myContext = _context;
		listener = _listener;
				
		LayoutInflater _layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		mainLayout = (RelativeLayout)_layoutInflater.inflate(R.layout.layout_times, null);
		
		myLinearTable = (LinearLayout)mainLayout.findViewById(R.id.Times_LinearTable);
		
		deleteAnimation = new TranslateAnimation(0, -ScreenInformation.getInstance().getscreenWidth(), 0, 0);
		deleteAnimation.setDuration(DELETE_CELL_ANIMATION_TIME);
		deleteAnimation.setFillAfter(true);
		
		showCities = new TranslateAnimation(0, 0, ScreenInformation.getInstance().getscreenHeight(), 0);
		showCities.setDuration(DELETE_CELL_ANIMATION_TIME);
		showCities.setFillAfter(true);
		showCities.setAnimationListener(new AnimationListener() {
			
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
				listener.didShowBackBtn();
				isPlayingAnimation = false;
			}
		});
		
		hideCities = new TranslateAnimation(0, 0, 0, ScreenInformation.getInstance().getscreenHeight());
		hideCities.setDuration(DELETE_CELL_ANIMATION_TIME);
		hideCities.setFillAfter(true);
		hideCities.setAnimationListener(new AnimationListener() {
			
			@Override
			public void onAnimationStart(Animation animation) {
				listener.didHideBackBtn();
			}
			
			@Override
			public void onAnimationRepeat(Animation animation) {
				// TODO Auto-generated method stub				
			}
			
			@Override
			public void onAnimationEnd(Animation animation) {					
				Message _message = new Message();
				_message.what = MSG_HIDE_TIMEZONE_TABLE;
				handler.sendMessage(_message);
			}
		});
	}
	
	public ViewGroup getView(){
		return mainLayout;
	}
	
	public void refreshView(){
		handler.sendEmptyMessage(MSG_RELOAD_CELL);		
	}
	
	public void showView(){
		myLinearTable.removeAllViews();				
		for(int i = 0; i < TOTAL_NUMBER_OF_CITY; i++){		
			LayoutInflater _layoutInflater = (LayoutInflater)myContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);		
			View _main = _layoutInflater.inflate(R.layout.cell_timesbase, null);			
			myLinearTable.addView(_main);
			_main.setTag("cell_" + i);
						
			if(UserProfile.getInstance().selectedCityIndex[i] == -1){
				readyForAddCity(_main, i);
			}else{
				preSetupCity(_main,i);				
			}			
		}
	}
	
	public void getTimestamp(int _index, int _tag){				
		String _zone = TimeZoneData.getInstance().getKey(_index);//"Australia/Melbourne";"Europe/Andorra";//
		NetworkJob _job = new NetworkJob();
		_job.networkInterface = this;
		_job.tag = _tag;
		_job.url = NetworkAddress.getTimeZone(_zone);
		NetworkManager _manger = NetworkManager.getInstance();
		_manger.addJob(_job);
	}
	
//	public void readyForAddCity(final int i){
//		
//		
//		readyForAddCity(_cell, i);
//	}
	
	public void readyForAddCity(View _view, final int i ){
		
		LayoutInflater _layoutInflater = (LayoutInflater)myContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);		
		View _cell = _layoutInflater.inflate(R.layout.cell_times, null);
		
		_cell.setTag("rootView");
		//_cell.setTag("cell_" + i);
		//myLinearTable.addView(_cell);
		
		Button _addBtn = (Button)_cell.findViewById(R.id.Time_AddCityBtn);
		FrameLayout.LayoutParams _params = (FrameLayout.LayoutParams)_addBtn.getLayoutParams();
		_params.width = _cell.getBackground().getIntrinsicWidth();
		_params.height = _cell.getBackground().getIntrinsicHeight();	
		
		_addBtn.setBackgroundDrawable(null);
		
		_addBtn.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				if(!isPlayingAnimation){
					addCity(i);
				}
			}
		});					
		((LinearLayout)_view.findViewById(R.id.Times_BaseCell)).addView(_cell);
	}
	
//	public void preSetupCity(final int i){
//		LayoutInflater _layoutInflater = (LayoutInflater)myContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
//		View _cell = _layoutInflater.inflate(com.ernestborel.R.layout.cell_timeswithcity, null);
//		preSetupCity(_cell, i);
//	}
	
	public void preSetupCity(View _view, final int i ){
		
		LayoutInflater _layoutInflater = (LayoutInflater)myContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View _cell = _layoutInflater.inflate(com.ernestborel.R.layout.cell_timeswithcity, null);
	
		Button _removeBtn = (Button)_cell.findViewById(R.id.Time_SubCityBtn);			
		_removeBtn.setOnClickListener(new OnClickListener() {		
			@Override
			public void onClick(View v) {
				if(!isPlayingAnimation){					
					removeCity(i);
				}
			}
		});	
		
				
		_cell.setTag("rootView");
		//myLinearTable.addView(_cell);		
		getTimestamp(UserProfile.getInstance().selectedCityIndex[i], i);
		
		TextView _cityName = (TextView)_cell.findViewById(R.id.Time_CityName);
		RelativeLayout.LayoutParams _params = (RelativeLayout.LayoutParams)_cityName.getLayoutParams();
		_params.width = ScreenInformation.getInstance().getscreenWidth() / 4;
				
		_cityName.setText(TimeZoneData.getInstance().getName(UserProfile.getInstance().selectedCityIndex[i]));				
		((LinearLayout)_view.findViewById(R.id.Times_BaseCell)).addView(_cell);
	}
	
	public void setupCity(int i, long _thisTimeStamp){
		View _main = myLinearTable.findViewWithTag("cell_" + i);
		
		View _cell = _main.findViewWithTag("rootView");
		
		RelativeLayout _bg = (RelativeLayout)_cell.findViewById(R.id.Time_CellWithCityBg);
		
		ImageView _clockBg = new ImageView(myContext);		
		_clockBg.setBackgroundResource(R.drawable.time_clock);
		_bg.addView(_clockBg);
		RelativeLayout.LayoutParams _params = (RelativeLayout.LayoutParams)_clockBg.getLayoutParams();
		_params.setMargins(
				(ScreenInformation.getInstance().getscreenWidth() - _clockBg.getBackground().getIntrinsicWidth() ) / 2,
				(_bg.getBackground().getIntrinsicHeight() - _clockBg.getBackground().getIntrinsicHeight() ) / 2,
				0, 0);
			
		Clock _clock = new Clock(myContext,  					
								_clockBg.getBackground().getIntrinsicWidth() / 2,
								_clockBg.getBackground().getIntrinsicHeight() / 2,										
								_clockBg.getBackground().getIntrinsicWidth() / 4, 
								i, this, _thisTimeStamp);				
		_bg.addView(_clock);		
		
		_params = (RelativeLayout.LayoutParams)_clock.getLayoutParams();
		_params.width = _clockBg.getBackground().getIntrinsicWidth();
		_params.height = _clockBg.getBackground().getIntrinsicHeight();
		_params.setMargins(
				(ScreenInformation.getInstance().getscreenWidth() - _clockBg.getBackground().getIntrinsicWidth() ) / 2,
				(_bg.getBackground().getIntrinsicHeight() - _clockBg.getBackground().getIntrinsicHeight() ) / 2,
				0, 0);		
		
		ImageView _dot = new ImageView(myContext);
		_dot.setBackgroundResource(R.drawable.time_clock_dot);
		_bg.addView(_dot);
		
		_params = (RelativeLayout.LayoutParams)_dot.getLayoutParams();
		_params.setMargins(
				(ScreenInformation.getInstance().getscreenWidth() - _dot.getBackground().getIntrinsicWidth() ) / 2,
				(_bg.getBackground().getIntrinsicHeight() - _dot.getBackground().getIntrinsicHeight() ) / 2,
				0, 0);	
		
		didUpdateTime(_thisTimeStamp, i);
		
		
	}
	
	public void addCity(int _tag){		
		currentEditingCellIndex = _tag;
		handler.sendEmptyMessage(MSG_SHOW_TIMEZONE_TABLE);				
		isPlayingAnimation = true;		
	}
	
	public void removeCity(final int _tag){	
		Log.d("Remove City", "Cell info Remove tag : " + _tag);
		deleteAnimation.setAnimationListener(new AnimationListener() {
			
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
				Message _msg = new Message();
				_msg.what = MSG_REMOVE_CELL ;
				_msg.obj = "" + _tag;
				handler.sendMessage(_msg);								
			}
		});	
		
		View _main = myLinearTable.findViewWithTag("cell_" +_tag);
		View _cell = _main.findViewWithTag("rootView");
		_cell.startAnimation(deleteAnimation);			
		isPlayingAnimation = true;
	}

	
	public void hideView(){
		handler.sendEmptyMessage(MSG_HIDE_TIMEZONE_TABLE);
	}
	
	class RefreshHandler extends Handler{
		@Override 
		public void handleMessage(Message msg) {
			
			if (msg.what < 3){
				try{
					View _cell = mainLayout.findViewWithTag("cell_" + msg.what);
					String[] _updateString = (String[])msg.obj;
					
					//Updates Times;						
					TextView _timeTxt = (TextView)_cell.findViewById(R.id.Time_Times);			
					_timeTxt.setText(_updateString[0]);								
					
					//Update Date											
					TextView _dateTxt = (TextView)_cell.findViewById(R.id.Time_Date);			
					_dateTxt.setText(_updateString[1]);
				}catch(Exception e){
					
				}
				Log.d("Update", "Update Clock~~~~~~~~");				
								
			}else if(msg.what == MSG_REMOVE_CELL){
				View _main = myLinearTable.findViewWithTag("cell_" + msg.obj);
				((LinearLayout)_main.findViewById(R.id.Times_BaseCell)).removeView(_main.findViewWithTag("rootView"));
				//((RelativeLayout)_cell.getRootView()).removeAllViews();
				readyForAddCity(_main, Integer.parseInt(msg.obj+""));						
				deleteAnimation.setAnimationListener(null);
				//Saving 
				UserProfile.getInstance().selectedCityIndex[ Integer.parseInt("" + msg.obj)] = -1;				
				UserProfile.getInstance().saveSelectedCity(myContext);
				isPlayingAnimation = false;
				
			}else if( msg.what == MSG_ADD_CELL){				
				long[] _content = (long [])msg.obj;				
				setupCity((int)_content[0], _content[1]);
				
			}else if( msg.what == MSG_SHOW_TIMEZONE_TABLE){
				timeZoneTableLayout = new TimeZoneTableLayout(activity, myContext, TimesLayout.this);
				mainLayout.addView(timeZoneTableLayout);	
				timeZoneTableLayout.startAnimation(showCities);
				
			}else if( msg.what == MSG_HIDE_TIMEZONE_TABLE){
				if(timeZoneTableLayout != null){
					timeZoneTableLayout.removeTimeZoneLayout();
					mainLayout.removeView(timeZoneTableLayout);
					timeZoneTableLayout = null;					
				}
				isPlayingAnimation = false;
				
			}else if(msg.what == MSG_RELOAD_CELL){
				handler.sendEmptyMessage(MSG_HIDE_TIMEZONE_TABLE);
				showView();
//				myLinearTable.removeView(myLinearTable.findViewWithTag("cell_" + currentEditingCellIndex));		
//				Log.d("Cell", "Cell info  Current EditingCellIndex : "  + currentEditingCellIndex);
//				preSetupCity(currentEditingCellIndex);
//				timeZoneTableLayout.startAnimation(hideCities);
			}
		}
	}
	
	
	/*** ClockListener Method **/
	@Override
	public void didUpdateTime(long _timeStamp, int _clockTag) {						
				
		Calendar _cal = Calendar.getInstance(TimeZone.getTimeZone("UTC"));
		Date _date = new Date();
		
		Log.d("TimeStamp", "Time Stamp : " + _timeStamp + "  vs Current : " + _date.getTime());
		
		_cal.setTimeInMillis(_timeStamp);
						
		String[] _updateString = new String[2];		
		_updateString[0] =  _cal.get(Calendar.HOUR_OF_DAY) + ":" + 
							((_cal.get(Calendar.MINUTE) < 10 ) ? "0" + _cal.get(Calendar.MINUTE) : _cal.get(Calendar.MINUTE) );
		
		int _cityDay = _cal.get(Calendar.DAY_OF_MONTH);
		Log.d("TimeStamp", "Time : " + _cal.get(Calendar.DAY_OF_MONTH) + ", " + _cal.get(Calendar.MONTH) + ", " + _cal.get(Calendar.YEAR) + " : " + _updateString[0]);
		
		_cal = Calendar.getInstance();
		_cal.setTimeInMillis(System.currentTimeMillis());
		int _today = _cal.get(Calendar.DAY_OF_MONTH);
				
		if (_today > _cityDay){
			if( _today - _cityDay > 1){
				_updateString[1] = myContext.getString(R.string.times_tomorrow);
			}else{
				_updateString[1] = myContext.getString(R.string.times_yesterday);
			}
		}else if( _today < _cityDay){
			if( _cityDay - _today > 1){
				_updateString[1] = myContext.getString(R.string.times_yesterday);
			}else{
				_updateString[1] = myContext.getString(R.string.times_tomorrow);
			}
		}else{
			_updateString[1] = myContext.getString(R.string.times_today);
		}
		
		Message _msg = new Message();
		_msg.what = _clockTag ;
		_msg.obj = _updateString;
		handler.sendMessage(_msg);	
	}

	/*** CategoryListener */ 
	@Override
	public void backEventRequest() {
		if( timeZoneTableLayout != null){			
			timeZoneTableLayout.startAnimation(hideCities);
		}
		// TODO Auto-generated method stub
		
	}

	/*** Networking Listener **/
	@Override
	public void didCompleteNetworkJob(NetworkJob networkJob) {
		int _tag = Integer.parseInt(networkJob.tag + "");	
		String _result = new String(networkJob.receiveData);		
		try {			
			JSONObject _jsonObj = new JSONObject(_result);

			long _timeStamp = Long.parseLong(_jsonObj.get("timestamp") + "") * 1000;

			long _content[] = { _tag, _timeStamp};			
			Log.d("Result", "Result : " + _result  + " TimeStamp : " + _timeStamp);
			Message _msg = new Message();
			_msg.what = MSG_ADD_CELL;
			_msg.obj = _content;
			handler.sendMessage(_msg);
			
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}		
	}

	@Override
	public void didFailNetworkJob(NetworkJob networkJob) {
		// TODO Auto-generated method stub
		
	}

	/*** TimeZoneTableListener */
	@Override
	public void didSelectedIndex(int _index) {
		// TODO Auto-generated method stub
		Log.d("Selected Cell", "Cell : " + _index);
		UserProfile.getInstance().selectedCityIndex[currentEditingCellIndex] = _index;
		UserProfile.getInstance().saveSelectedCity(myContext);	
		handler.sendEmptyMessage(MSG_RELOAD_CELL);
		listener.didHideBackBtn();
	}
}
