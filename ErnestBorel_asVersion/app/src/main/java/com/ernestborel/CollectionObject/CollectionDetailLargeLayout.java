package com.ernestborel.CollectionObject;

import hk.ImageLoader.ImageLoader;

import com.ernestborel.R;

import android.content.Context;
import android.graphics.Matrix;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnTouchListener;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;

public class CollectionDetailLargeLayout extends RelativeLayout implements OnClickListener, OnTouchListener {
	private View view;
	CollectionDataListener collectionDataListener = null;
	ImageLoader imageLoader;
//	TouchImageView largeImageView;
	ImageView largeImageView;
	Button closeBtn;
	
	TouchViewControl touchViewControl = null;
	
	public CollectionDetailLargeLayout(Context _context, String _url, CollectionDataListener _collectionDataListener) {
		super(_context);
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.layout_collection_listview_largeimage,this);
		
		collectionDataListener = _collectionDataListener;
		imageLoader = new ImageLoader(_context, R.drawable.collection_detail_largeimage_loading);
		largeImageView = (ImageView) view.findViewById(R.id.Collection_Detail_LargeImage);
//		largeImageView = (TouchImageView) view.findViewById(R.id.Collection_Detail_LargeImage);
//		largeImageView.setMaxZoom(4f);
		
		imageLoader.DisplayImage(_url, largeImageView);
		largeImageView.setOnTouchListener(this);
		touchViewControl = new TouchViewControl(_context, largeImageView);
		
		closeBtn = (Button) view.findViewById(R.id.Collection_Detail_LargeClose);
		closeBtn.setOnClickListener(this);
	}
	
	public void releaseView()
	{
		largeImageView.setBackgroundDrawable(null);
		imageLoader.clearCache();
		imageLoader = null;
		closeBtn.setOnClickListener(null);
		collectionDataListener = null;
		if(touchViewControl != null)
		{
			touchViewControl.onDestroy();
			touchViewControl = null;
		}
	}

	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		if(v == closeBtn)
		{
			collectionDataListener.closeZoom();
		}
	}

	@Override
	public boolean onTouch(View v, MotionEvent event) {
		// TODO Auto-generated method stub
		return touchViewControl.onTouch(v, event);
//		return true;
	}
}
