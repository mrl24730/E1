package com.ernestborel.NewsObject;

import org.json.JSONArray;

import com.Util.PageEventHandler;
import com.ernestborel.R;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.AdapterView.OnItemClickListener;

@SuppressLint({ "HandlerLeak", "ViewConstructor" })
public class NewsTableLayout extends RelativeLayout {
	RefreshHandler handler = new RefreshHandler();
	Activity activity;
	Context context;
	public NewsDataListener newsDataListener = null;
	
	private View view;
	private JSONArray jsonArray = null;
	private ListView listView;
	private int position;
	
	NewsTableItem newsTableItem;
	
	final int LOADTABLE = 0;

	public NewsTableLayout(Activity a, Context _context, JSONArray _jsonArray, NewsDataListener _handler, int _position) {
		super(_context);
		// TODO Auto-generated constructor stub
		activity = a;
		context = _context;
		position = _position;
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.layout_news_listview_table,this);
		jsonArray = _jsonArray;
		newsDataListener = _handler;
		
		listView = (ListView) view.findViewById(R.id.News_ListView);
		
		newsTableItem = new NewsTableItem(activity, context, jsonArray, _position);
		
		handler.sendEmptyMessageDelayed(LOADTABLE, PageEventHandler.PAGE_EVENT_ANIMATION_TIME + 10);
	}
	
	public void releaseNewsTableLayout(){
		newsTableItem.releaseTableItem();
		newsTableItem = null;
		
		handler = null;
		activity = null;
		context = null;
		view = null;
		jsonArray = null;
		listView = null;
		newsDataListener = null;
		
	}
	
	class RefreshHandler extends Handler {
    	@Override  
    	public void handleMessage(Message msg) {
    		switch (msg.what) {
			case LOADTABLE:
    			loadTable();
				break;
			}
    	}

    	public void sleep(long delayMillis) {  
    		this.removeMessages(0);  
    		sendMessageDelayed(obtainMessage(0), delayMillis);  
    	}
    }
	
	private void loadTable()
	{
		Log.d("loadTable", "loadTable:"+jsonArray.toString());		
		
		listView.setAdapter(newsTableItem);
		listView.setTextFilterEnabled(true);
		listView.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				if(newsDataListener != null)
				{
					newsDataListener.loadID(position);
				}
				
			}
		});
		
		if(position >= 0)
		{
			listView.setSelection(position);
		}
	}
}
