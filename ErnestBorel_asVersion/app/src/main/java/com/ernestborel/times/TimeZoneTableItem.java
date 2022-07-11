package com.ernestborel.times;

import com.ernestborel.R;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

public class TimeZoneTableItem extends BaseAdapter {
	private LayoutInflater inflater = null;
	int[] cellInfo;
	int myid;

	private TextView nameText = null;
	private TextView utcText = null;
	
	public TimeZoneTableItem(Activity a, int[] _cellInfo) {
		super();
		cellInfo = _cellInfo;
		inflater = (LayoutInflater)a.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
	}
	
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		
		View rowView = convertView;
	    if(convertView==null)
	    {
	    	rowView = inflater.inflate(com.ernestborel.R.layout.cell_timezone, parent, false);
	    }
	    
	    myid = cellInfo[position];	   
	    nameText = (TextView) rowView.findViewById(R.id.Time_Table_Name);
		utcText = (TextView) rowView.findViewById(R.id.Time_Table_UTC);
		nameText.setText(TimeZoneData.getInstance().getName(myid));
		utcText.setText(TimeZoneData.getInstance().getUTC(myid));
	    
		return rowView;
	}
	
	public int[] getCellsID(){
		return cellInfo;
	}
	
	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return cellInfo.length;
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return position;
	}
}
