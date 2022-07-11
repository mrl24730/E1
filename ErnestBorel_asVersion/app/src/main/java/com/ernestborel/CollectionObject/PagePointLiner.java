package com.ernestborel.CollectionObject;

import java.util.ArrayList;

import android.content.Context;
import android.view.ViewGroup;

public class PagePointLiner {
	private ViewGroup pageView;
	private Context context;
	private int totalPage = 0;
	ArrayList<PagePointLayout> pagePointList = new ArrayList<PagePointLayout>();
	
	public PagePointLiner(Context _context, ViewGroup _view)
	{
		context = _context;
		pageView = _view;
	}
	
	public void onDestroy()
	{
		pagePointList.removeAll(pagePointList);
		pagePointList = null;
 	}
	
	public void setTotalPage(int _page)
	{
		totalPage = _page;
		buildPage();
	}
	
	public ViewGroup getView(){
		return pageView;
	}
	
	public void buildPage()
	{
		pageView.removeAllViews();
		pagePointList.removeAll(pagePointList);
		for (int i = 0; i < totalPage; i++) {
			PagePointLayout _pagePointLayout = new PagePointLayout(context);
			pageView.addView(_pagePointLayout);
			pagePointList.add(_pagePointLayout);
		}
		setPage(0);
	}
	
	public void setPage(int _page){
		for (int i = 0; i < totalPage; i++) 
		{
			PagePointLayout _pagePointLayout = pagePointList.get(i);
			if(i == _page)
			{
				_pagePointLayout.pointShow(true);
			}else
			{
				_pagePointLayout.pointShow(false);
			}
		}
	}
}
