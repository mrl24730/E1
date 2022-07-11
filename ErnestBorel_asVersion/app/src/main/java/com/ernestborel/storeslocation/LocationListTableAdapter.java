package com.ernestborel.storeslocation;

import hk.SharedPreferencesSetting.UserProfile;

import java.util.ArrayList;

import com.ernestborel.R;

import android.content.Context;
import android.graphics.Paint;
import android.graphics.Rect;
import android.graphics.Typeface;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class LocationListTableAdapter extends BaseAdapter{
	
	private final int NUMBER_OF_TEXT_TO_NEXT_LINE	= 42;
	//private static LayoutInflater inflater = null;
	private LayoutInflater inflater = null;
	
	ArrayList<String> tableData = new ArrayList<String>();
	
	ArrayList<ArrayList<String>> storeData = new ArrayList<ArrayList<String>>();
	int currentType;
	
	int currentSelectedIndex = -1;
	
	public final static int CELL_TYPE_SIMPLE = 1;
	public final static int CELL_TYPE_NORMAL = 2;
	public final static int CELL_TYPE_EMPTY = 3;
	
	Context myContext;
	
	public LocationListTableAdapter(Context _ctx){		
		myContext = _ctx;
		inflater = (LayoutInflater)_ctx.getSystemService(Context.LAYOUT_INFLATER_SERVICE);		
	}
	
	public void setCurrentArray(ArrayList<String> _ary){
		tableData = null;
		tableData = _ary;				
	}
	
	public ArrayList<String> getTableData(){
		return tableData;
	}
	
	public void setNoramlStyleArray(ArrayList<ArrayList<String>> _ary){
		storeData = null;
		storeData = _ary;
		
		for( int i = 0; i < storeData.size(); i++){
			Log.d("Table Data ", "Data : " + storeData.get(i));
		}
	}
	
	public void setCurrentType(int _type){
		currentType = _type;				
	}
	
	public String getStoreID(int _index){
		String _str = "";
		try{
			_str = storeData.get(_index).get(5); 
		}catch(Exception e){
			
		}
		return _str; 
	}
	
	public void setCurrentSelected(int _index){
		currentSelectedIndex = _index;
	}

	@Override
	public int getCount() {
		if( currentType == CELL_TYPE_SIMPLE){
			return tableData.size();
			
		}else if( currentType == CELL_TYPE_EMPTY){
			return 0;
			
		}else {
			return storeData.size();
		}
	}

	@Override
	public Object getItem(int position) {
		if( currentType == CELL_TYPE_SIMPLE){
			return tableData.get(position);
		}else {
			return storeData.get(position);
		}
	}

	@Override
	public long getItemId(int position) {
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View _cell = convertView;					
//		Typeface face=Typeface.createFromAsset(myContext.getAssets(), "fonts/times.ttf");
		
		if( currentType == CELL_TYPE_SIMPLE){
			TextView _title = null;			
			String _txt = tableData.get(position);
			
			try {
				_title = (TextView)_cell.findViewById(R.id.Location_CellTitle);				
				_title.setText(_txt);
//				_title.setTypeface(face);
				
			} catch (Exception e) {
				_cell = null;
				_cell = inflater.inflate(com.ernestborel.R.layout.cell_locationstore, null);
				_title = (TextView)_cell.findViewById(R.id.Location_CellTitle);				
				_title.setText(_txt);
//				_title.setTypeface(face);
			}			
			
			if(currentSelectedIndex == position){
				_cell.setBackgroundResource(R.drawable.collection_tablebg_selected);			
			}else{
				_cell.setBackgroundResource(R.drawable.collection_tablebg);
			}
			
		}else{
			String _txt = storeData.get(position).get(0);
			TextView _title = null;
			
			try{
				_title = (TextView)_cell.findViewById(R.id.Store_CellTitle);
				_title.setText(_txt);
//				_title.setTypeface(face);
				
			}catch(Exception e){
				_cell = null;
				_cell = inflater.inflate(com.ernestborel.R.layout.cell_store, null);
				_title = (TextView)_cell.findViewById(R.id.Store_CellTitle);
				_title.setText(_txt);
//				_title.setTypeface(face);
			}				
			
			TextView _subTitle =  (TextView)_cell.findViewById(R.id.Store_CellSubTitle);
			String _subTxt = storeData.get(position).get(1);			
			Rect bounds = new Rect();
			
			Paint textPaint = _subTitle.getPaint();
			textPaint.getTextBounds(_subTxt,0,_subTxt.length(),bounds);
					
			if(bounds.width() >  UserProfile.getInstance().screenWidth * 0.9){
				_subTitle.setLines(2);
			}else{
				_subTitle.setLines(1);
			}
			
			
//			if(UserProfile.getInstance().currentLanguageIndex == UserProfile.LANGUAGE_INDEX_EN ){
//				if( _subTxt.length() > NUMBER_OF_TEXT_TO_NEXT_LINE){
//					_subTxt = _subTxt.substring(0, 42) + "\n" + _subTxt.substring(42);
//					_subTitle.setLines(2);
//				}else{
//					_subTitle.setLines(1);
//				}
//			}else{
//				if( _subTxt.length() > NUMBER_OF_TEXT_TO_NEXT_LINE /2 ){
//					_subTxt = _subTxt.substring(0, 42) + "\n" + _subTxt.substring(42);
//					_subTitle.setLines(2);
//				}else{
//					_subTitle.setLines(1);
//				}				
//			}
			_subTitle.setText(_subTxt);
			
			if(currentSelectedIndex == position){
				_cell.setBackgroundResource(R.drawable.store_cellbar_selected);			
			}else{
				_cell.setBackgroundResource(R.drawable.store_cellbar);
			}
		}			
							
		return _cell;
	}

}
