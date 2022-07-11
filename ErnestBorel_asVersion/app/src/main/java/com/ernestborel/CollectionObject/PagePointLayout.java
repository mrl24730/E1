package com.ernestborel.CollectionObject;

import com.ernestborel.R;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

public class PagePointLayout extends LinearLayout {
	private Context context;
	private View view;
	private ImageView pointImage;
	
	public PagePointLayout(Context _context) {
		super(_context);
		context = _context;
		// TODO Auto-generated constructor stub
		LayoutInflater layoutInflater = (LayoutInflater)_context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		view = layoutInflater.inflate(R.layout.pagepoint_layout,this);
		
		pointImage = (ImageView) view.findViewById(R.id.Collection_Detail_PagePoint);
	}
	
	public void pointShow(Boolean _show)
	{
		if(_show)
		{
			pointImage.setBackgroundResource(R.drawable.general_redrot);
		}else
		{
			pointImage.setBackgroundResource(R.drawable.general_whiterot);
		}
	}
}
