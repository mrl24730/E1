package com.ernestborel.CollectionObject;

import java.util.ArrayList;
import java.util.Hashtable;

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
public class CollectionListTableLayout extends RelativeLayout {
	RefreshHandler handler = new RefreshHandler();
	Activity activity;
	Context context;
	private View view;
//	private JSONArray jsonArray = null;
	ArrayList<Hashtable<String, String>> jsonArray = null;
	private ListView listView;
	private int toPostion;
	
	CollectionListTableItem collectionListTableItem = null;
	
	final int LOADTABLE = 0;
	
	public CollectionListTableLayout(Activity a, Context _context, ArrayList<Hashtable<String, String>> _jsonArray, int _postion){//JSONArray _jsonArray) {
		super(_context);
		activity = a;
		context = _context;
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.layout_collection_listview,this);
		jsonArray = _jsonArray;
		
		collectionListTableItem = new CollectionListTableItem(activity, context, jsonArray, _postion);
		
		toPostion = _postion;
		listView = (ListView) view.findViewById(R.id.Collection_ListView);
		
		handler.sendEmptyMessage(LOADTABLE);
	}
	
	public void onDestroy(){
		collectionListTableItem.releaseView();
		collectionListTableItem = null;
		
		handler = null;
		activity = null;
		context = null;
		view = null;
		jsonArray = null;
 	}
	
	public static interface CollectionListTableHandler {
	    public void loadID(String _id, String _name, int _position);
	}
	
	public static CollectionListTableHandler collectionHandler = null;
		
	public void setListener(CollectionListTableHandler _handler){
		collectionHandler = _handler;
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
//		Log.d("loadTable", "loadTable:"+jsonArray.toString());
		listView.setAdapter(collectionListTableItem);
		listView.setTextFilterEnabled(true);
		listView.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				// When clicked, show a toast with the TextView text
//				Toast.makeText(getApplicationContext(), ((TextView) view).getText(), Toast.LENGTH_SHORT).show();
//				handler.sendEmptyMessage(HIDE_TABLE);
				
				Hashtable<String, String> subItem = jsonArray.get(position);
				String _myId = subItem.get("id");
				String _myName = subItem.get("name");
				if(collectionHandler != null)
				{
					collectionHandler.loadID(_myId, _myName, position);
				}
			}
		});
		if(toPostion >= 0)
		{
//			listView.setSelectionFromTop(toPostion, toPostion);
			listView.setSelection(toPostion);
		}
 	}
}
