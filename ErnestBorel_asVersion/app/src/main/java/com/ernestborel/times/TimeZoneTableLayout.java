package com.ernestborel.times;

import hk.SharedPreferencesSetting.UserProfile;

import com.ernestborel.R;

import android.app.Activity;
import android.content.Context;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.AdapterView.OnItemClickListener;

public class TimeZoneTableLayout extends RelativeLayout implements TextWatcher {
	private View view;
	Activity activity;
	Context context;
	EditText inputBox;
	ListView citiesList;
	TimeZoneTableItem timeZoneTableItem;
	
	TimeZoneTableListener listener;
	
	public TimeZoneTableLayout(Activity a, Context _context, TimeZoneTableListener _listener) {
		super(_context);
		activity = a;
		context = _context;
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.layout_timezone,this);
		
		inputBox = (EditText)view.findViewById(R.id.Time_SearchBox);
		inputBox.addTextChangedListener(this);
		
		listener = _listener;
		
		Button _searchBtn = (Button)view.findViewById(R.id.Time_SearchBtn);
		
		_searchBtn.setOnClickListener(new OnClickListener() {			
			@Override
			public void onClick(View v) {
				startSearching();
			}
		});
		citiesList = (ListView)view.findViewById(R.id.TimeZone_ListView);
		timeZoneTableItem = new TimeZoneTableItem(activity, TimeZoneData.getInstance().getlist(UserProfile.getInstance().selectedCityIndex[0], UserProfile.getInstance().selectedCityIndex[1], UserProfile.getInstance().selectedCityIndex[2]));
		buildTable();
	}
	
	public void startSearching(){
		InputMethodManager imm = (InputMethodManager)context.getSystemService(Context.INPUT_METHOD_SERVICE);
		imm.hideSoftInputFromWindow(inputBox.getWindowToken(), 0);
	}
	
	
	public void removeTimeZoneLayout(){
		timeZoneTableItem = null;

		InputMethodManager imm = (InputMethodManager)context.getSystemService(Context.INPUT_METHOD_SERVICE);
		imm.hideSoftInputFromWindow(inputBox.getWindowToken(), 0);
	}
	
	public void buildTable()
	{
		citiesList.setAdapter(timeZoneTableItem);
		citiesList.setTextFilterEnabled(true);
		citiesList.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				listener.didSelectedIndex(timeZoneTableItem.getCellsID()[position]);
			}
		});
	}

	@Override
	public void afterTextChanged(Editable arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void beforeTextChanged(CharSequence s, int start, int count,
			int after) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void onTextChanged(CharSequence s, int start, int before, int count) {
		// TODO Auto-generated method stub
		if(timeZoneTableItem != null)
		{
			timeZoneTableItem = null;
		}
		timeZoneTableItem = new TimeZoneTableItem(activity, TimeZoneData.getInstance().searchlist(UserProfile.getInstance().selectedCityIndex[0], UserProfile.getInstance().selectedCityIndex[1], UserProfile.getInstance().selectedCityIndex[2], context, inputBox.getText().toString()));
		buildTable();
	}
	
	
	public interface TimeZoneTableListener{
		void didSelectedIndex(int _index);
	}

}
