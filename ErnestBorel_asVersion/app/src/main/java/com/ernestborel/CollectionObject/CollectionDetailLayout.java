package com.ernestborel.CollectionObject;

import hk.ImageLoader.ImageLoader;
import hk.SharedPreferencesSetting.NetworkConnectChecker;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.ernestborel.R;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.ScrollView;
import android.widget.TextView;

@SuppressLint({ "ViewConstructor", "HandlerLeak" })
public class CollectionDetailLayout extends RelativeLayout implements OnClickListener {
	RefreshHandler handler = new RefreshHandler();
	CollectionDataListener collectionDataListener = null;
	private Context context;
	private View view;
	private ImageView watchImageView;
	private TextView watchNumberTextView;
	private Button zoomBtn;
	private Button infoBtn;
	private Button tryMeBtn;
	
	//Detail
	private ViewGroup detailView = null;
	private TextView detail_TitelTextView;
	private TextView detail_InfoTextView;
	private Button detail_CloseBtn;
//	private ViewGroup largeView = null;
	
//	CollectionDetailLargeLayout collectionDetailLargeLayout;
	
	public ImageLoader imageLoader;
	
	private JSONObject infoArray = null;
	
	final int BUILD_PAGE = 0;
	final int HIDE_PAGE = 1;
	final int KILL_PAGE = 2;
	final int SHOW_INFO = 3;
	final int HIDE_INFO = 4;
	final int BUILD_LARGE = 5;
	final int REMOVE_LARGE = 6;
	final int BUILD_TRY = 7;
	
	public CollectionDetailLayout(Context _context, String _name, JSONObject _infoArray, CollectionDataListener _collectionDataListener) {
		super(_context);
		context = _context;
		collectionDataListener = _collectionDataListener;
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.layout_collection_listview_detail,this);
		imageLoader = new ImageLoader(_context, R.drawable.collection_detail_watch);
		
		infoArray = _infoArray;
		
//		Typeface face=Typeface.createFromAsset(_context.getAssets(), "fonts/times.ttf");
		
		watchImageView = (ImageView) view.findViewById(R.id.Collection_Detail_WatchImage);
		//watchImageView.setOnClickListener(this);
		
		watchNumberTextView = (TextView) view.findViewById(R.id.Collection_Detail_WatchNum);
//		watchNumberTextView.setTypeface(face);
		
		zoomBtn = (Button) view.findViewById(R.id.Collection_Detail_Zoom);
		infoBtn = (Button) view.findViewById(R.id.Collection_Detail_Info);
		tryMeBtn = (Button) view.findViewById(R.id.Collection_Detail_TryMe);
		zoomBtn.setOnClickListener(this);
		infoBtn.setOnClickListener(this);
		tryMeBtn.setOnClickListener(this);
//		tryMeBtn.setVisibility(View.GONE);
		
		zoomBtn.setVisibility(View.INVISIBLE);
		tryMeBtn.setVisibility(View.INVISIBLE);
		
//		if(NetworkConnectChecker.getInstance().getNetworkConnect())
//		{
//			zoomBtn.setVisibility(View.VISIBLE);
//			tryMeBtn.setVisibility(View.VISIBLE);
//		}else
//		{
//			zoomBtn.setVisibility(View.INVISIBLE);
//			tryMeBtn.setVisibility(View.INVISIBLE);
//		}
		
		detailView = (ViewGroup) view.findViewById(R.id.Collection_Detail_InfoLayout);
		ScrollView _scrollView = (ScrollView) detailView.findViewById(R.id.Collection_Detail_Info_ScrollView);
		ViewGroup _linerView = (ViewGroup) _scrollView.findViewById(R.id.Collection_Detail_Info_ScrollView_LinearLayout);
		detail_TitelTextView = (TextView) _linerView.findViewById(R.id.Collection_Detail_InfoTitel);
//		detail_TitelTextView.setTypeface(face);
		detail_TitelTextView.setTextColor(Color.rgb(200,0,0));
		
		detail_InfoTextView = (TextView) _linerView.findViewById(R.id.Collection_Detail_InfoText);
//		detail_InfoTextView.setTypeface(face);
		
		detail_CloseBtn = (Button) detailView.findViewById(R.id.Collection_Detail_InfoClose);
		detail_CloseBtn.setOnClickListener(this);
		
//		largeView = (ViewGroup) view.findViewById(R.id.Collection_Detail_LargeLayout);
//		largeView.setOnClickListener(this);
	}
	
	public void showPage(){
		handler.sendEmptyMessage(BUILD_PAGE);
	}
	
	public void hidePage(){
		imageLoader.clearCache();
		handler.sendEmptyMessage(HIDE_PAGE);
	}
	
	public void releasePage(){
		handler.sendEmptyMessage(KILL_PAGE);
	}
	
	public void onDestroy(){
 	}
	
	class RefreshHandler extends Handler {
    	@Override  
    	public void handleMessage(Message msg) {
    		switch (msg.what) {
			case BUILD_PAGE:
				loadModel();
				break;
				
			case HIDE_PAGE:
				hideModel();
				break;
				
			case KILL_PAGE:
				killModel();
				break;
				
			case SHOW_INFO:
				showInfo(true);
				break;
				
			case HIDE_INFO:
				showInfo(false);
				break;
				
			case BUILD_LARGE:
				buildLarge();
				break;
				
			case REMOVE_LARGE:
				removeLarge();
				break;
				
			case BUILD_TRY:
				buildTry();
				break;
			}
    	}

    	public void sleep(long delayMillis) {
    		this.removeMessages(0);
    		sendMessageDelayed(obtainMessage(0), delayMillis);
    	}
    }
	
	private void loadModel()
	{
		if(infoArray != null)
		{
			String imageName = null;
			try {
				imageName = infoArray.getString("img");
			} catch (JSONException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
			Log.d("loadModel", "loadModel:"+imageName);
			imageLoader.DisplayImage(imageName, watchImageView);
//			noimage_s.png
			if(NetworkConnectChecker.getInstance().getNetworkConnect() && !(imageLoader.getImageName(imageName).equalsIgnoreCase("noimage_s.png")))
			{
				zoomBtn.setVisibility(View.VISIBLE);
				tryMeBtn.setVisibility(View.VISIBLE);
			}
			
			try {
				watchNumberTextView.setText(infoArray.getString("model"));
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		setUpInfo();
	}
	
	private void hideModel()
	{
		watchImageView.setImageResource(R.drawable.empty_px);			
		Log.d("Hide", "Hide Model ~~~~~~~~~~~~~~~~~~~~");
		System.gc();
	}
	
	private void killModel()
	{
		watchImageView.setImageResource(R.drawable.empty_px);		
		
		handler = null;
		context = null;
		view = null;
		if(imageLoader != null)
		{
			imageLoader.clearCache();
			imageLoader = null;
		}
		infoArray = null;
		collectionDataListener = null;
		System.gc();
	}
	
	@Override 
	public void onClick(View v) {
		// TODO Auto-generated method stub
		if(v == zoomBtn )//|| v == watchImageView)
		{
			handler.sendEmptyMessage(BUILD_LARGE);
		}else/* if(v == largeView)
		{
			handler.sendEmptyMessage(REMOVE_LARGE);
		}else*/ if(v == infoBtn)
		{
			handler.sendEmptyMessage(SHOW_INFO);
		}else if(v == detail_CloseBtn)
		{
			handler.sendEmptyMessage(HIDE_INFO);
		}else if(v == tryMeBtn)
		{
			handler.sendEmptyMessage(BUILD_TRY);
		}
	}
	
	private void setUpInfo()
	{
		String showText = null;
		JSONArray textArray = null;
		try {
			textArray = infoArray.getJSONArray("spec");
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
//		Log.d("setUpInfo", "setUpInfo:"+textArray);
		for (int i = 0; i < textArray.length(); i++) {
			if(i == 0)
			{
				try {
					showText = textArray.getString(i) + "\n";
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}else
			{
				try {
					showText += textArray.getString(i) + "\n";
				} catch (JSONException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		}
		
		detail_InfoTextView.setText(showText);
	}
	
	private void showInfo(Boolean _show)
	{
		if(_show)
		{
			detailView.setVisibility(View.VISIBLE);
		}else
		{
			detailView.setVisibility(View.GONE);
		}
	}
	
	private void buildLarge()
	{
		String imageUrl = null;
		try {
			imageUrl = infoArray.getString("large");
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		collectionDataListener.zoomWatch(imageUrl);
//		collectionDetailLargeLayout = new CollectionDetailLargeLayout(context, imageUrl);
//		largeView.addView(collectionDetailLargeLayout);
//		largeView.setVisibility(View.VISIBLE);
	}
	
	private void removeLarge()
	{
//		collectionDetailLargeLayout.releaseView();
//		collectionDetailLargeLayout = null;
//		largeView.removeAllViews();
//		largeView.setVisibility(View.GONE);
	}
	
	private void buildTry()
	{
		String imageUrl = null;
		try {
			imageUrl = infoArray.getString("large");
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		collectionDataListener.tryWatch(imageUrl);
	}
}
