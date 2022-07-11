package com.ernestborel.CollectionObject;

import java.util.ArrayList;
import java.util.Hashtable;

import hk.ImageLoader.ImageLoader;
import hk.SharedPreferencesSetting.ScreenInformation;
import hk.SharedPreferencesSetting.UserProfile;

import com.ernestborel.R;

import android.app.Activity;
import android.content.Context;
import android.graphics.Paint;
import android.graphics.Rect;
import android.graphics.Typeface;
import android.graphics.drawable.BitmapDrawable;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

public class CollectionListTableItem extends BaseAdapter{
	private Activity activity;
//	private JSONArray jsonArray;
	ArrayList<Hashtable<String, String>> jsonArray;
	
	private static LayoutInflater inflater = null;
	public ImageLoader imageLoader;
	private String myId = null;
	private int needPostion;
	
	public CollectionListTableItem(Activity a, Context _context, ArrayList<Hashtable<String, String>> _jsonArray, int _needPostion){//JSONArray _meunArray) {
		super();
		activity = a;
		jsonArray = _jsonArray;
		inflater = (LayoutInflater)a.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		imageLoader = new ImageLoader(a.getApplicationContext(), R.drawable.collection_tableicon);
		needPostion = _needPostion;
	}
	
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View rowView = convertView;
	    if(convertView==null)
	    {
	    	rowView = inflater.inflate(com.ernestborel.R.layout.layout_collection_listview_item, parent, false);
	    }
	    
	    Hashtable<String, String> subItem = jsonArray.get(position);
	    
	    
	    
		ImageView imageView = (ImageView) rowView.findViewById(R.id.Collection_TitleImage);
		int h=ScreenInformation.getInstance().getscreenHeight()/8;
		int w=ScreenInformation.getInstance().getscreenWidth()/6;
//		Log.d("", "getscreenHeight:"+h);
//		Log.d("", "getscreenWidth:"+w);
		imageView.getLayoutParams().width=w;
		imageView.getLayoutParams().height=h;

		ImageView bgImageView = (ImageView) rowView.findViewById(R.id.Collection_CellBg_HighLight);
		if(needPostion == position)
		{
			bgImageView.setVisibility(View.VISIBLE);
		}else
		{
			bgImageView.setVisibility(View.GONE);
		}
		
//		Typeface face=Typeface.createFromAsset(activity.getAssets(), "fonts/times.ttf");
		
	    TextView titleTextView = (TextView) rowView.findViewById(R.id.Collection_Item_SubTitle);	    
//	    titleTextView.setTypeface(face);
	    	   
		String _subTxt = subItem.get("name");
		
		Rect bounds = new Rect();		
		Paint textPaint = titleTextView.getPaint();
		textPaint.getTextBounds(_subTxt,0,_subTxt.length(),bounds);
				
		if(bounds.width() >  UserProfile.getInstance().screenWidth * 0.75){
			titleTextView.setLines(2);
		}else{
			titleTextView.setLines(1);
		}
	    
	    titleTextView.setText(subItem.get("name"));
	    myId = subItem.get("id");
	    String imageName = subItem.get("img");
	    
	    
//	    JSONObject subItem = null;
//		try {
//			subItem = jsonArray.getJSONObject(position);
//		} catch (JSONException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
//	    
//	    
//	    try {
//	    	titleTextView.setText(subItem.getString("name"));
//		} catch (JSONException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
//	    
//	    try {
//	    	myId = subItem.getString("id");
//		} catch (JSONException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
//	    
//	    String imageName = null;
//		try {
//			imageName = subItem.getString("img");
//		} catch (JSONException e1) {
//			// TODO Auto-generated catch block
//			e1.printStackTrace();
//		}
	    
		imageLoader.DisplayImage(imageName, imageView);
		
		BitmapDrawable bd = (BitmapDrawable) activity.getResources().getDrawable(R.drawable.collection_tablebg);
		int height = bd.getBitmap().getHeight();
		
		((RelativeLayout.LayoutParams)imageView.getLayoutParams()).height = height;
		
	    //Log.d("position", "position:"+position);
	    
		return rowView;
	}

	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return jsonArray.size();//jsonArray.length();
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
	
	public String getMyId()
	{
		return myId;
	}
	
	public void releaseView()
	{
		imageLoader.clearCache();
		imageLoader = null;
		jsonArray = null;
		inflater = null;
		myId = null;
		activity = null;
	}
}
