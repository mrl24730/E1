package com.ernestborel.NewsObject;

import hk.ImageLoader.ImageLoader;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.ernestborel.R;

import android.app.Activity;
import android.content.Context;
import android.graphics.Typeface;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class NewsTableItem extends BaseAdapter{
	private JSONArray jsonArray;
	private LayoutInflater inflater = null;
	public ImageLoader imageLoader;
	public int position;
	
	Context myContext;
	
	public NewsTableItem(Activity a, Context _context, JSONArray _meunArray, int _position) {
		super();
		myContext = _context;
		jsonArray = _meunArray;
		position = _position;
		inflater = (LayoutInflater)a.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		imageLoader = new ImageLoader(a.getApplicationContext(), R.drawable.news_table_image);
	}
	
	public void releaseTableItem(){
		imageLoader.clearCache();
		imageLoader = null;
		jsonArray = null;
		inflater = null;
		myContext = null;
	}
	
	@Override
	public View getView(int _position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View rowView = convertView;
	    if(convertView==null)
	    {
	    	rowView = inflater.inflate(com.ernestborel.R.layout.cell_news, parent, false);
	    }
	    
	    JSONObject subItem = null;
		try {
			subItem = jsonArray.getJSONObject(_position);
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	    
	    ImageView imageView = (ImageView) rowView.findViewById(R.id.News_Table_Image);
	    ViewGroup textView = (ViewGroup) rowView.findViewById(R.id.News_Table_Cell);
	    
//	    Typeface face=Typeface.createFromAsset(myContext.getAssets(), "fonts/times.ttf");
	    
	    TextView titleTextView = (TextView) textView.findViewById(R.id.News_Table_Title);
	    TextView dateTextView = (TextView) textView.findViewById(R.id.News_Table_Date);
//	    titleTextView.setTypeface(face);
//	    dateTextView.setTypeface(face);
	    
	    try {
	    	dateTextView.setText(subItem.getString("date"));
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

	    try {
	    	titleTextView.setText(subItem.getString("title"));
	    	titleTextView.setAutoLinkMask(3);
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			
			e.printStackTrace();
		}
	    
	    String imageName = null;
		try {
			imageName = subItem.getString("file");
		} catch (JSONException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		imageLoader.DisplayImage(imageName, imageView);
	    
		ImageView bgImageView = (ImageView) rowView.findViewById(R.id.News_Table_ImageBg);
		if(position == _position)
		{
			bgImageView.setBackgroundResource(R.drawable.news_table_bg_selected);
		}else
		{
			bgImageView.setBackgroundResource(R.drawable.news_table_bg);
		}
		
		return rowView;
	}
	
	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return jsonArray.length();
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
