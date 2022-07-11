package com.Util.RealView;


import java.util.ArrayList;

import com.Util.RealView.HorizontalPager.OnScreenSwitchListener;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;

public class RealViewPagerController implements OnScreenSwitchListener {
	
	RealViewPagerControllerListener listener;
	HorizontalPager myRealViewer = null;
	int totalPage = 0;
	
	int previousPageIndex = 0;
	
	ArrayList<View> pages = new ArrayList<View>();
	
	public RealViewPagerController(Context _context, ViewGroup _attachToView, RealViewPagerControllerListener _listener) {	
		myRealViewer = new HorizontalPager(_context);		
		myRealViewer.setOnScreenSwitchListener(this);		
		listener = _listener;
		_attachToView.addView(myRealViewer);
	}
	
	public void addView(View _view){
		myRealViewer.addView(_view);		
		totalPage++;
		pages.add(_view);
		myRealViewer.setTotalPage(totalPage);
	}
	
	public void startRealViewController(){
		listener.loadPage(pages.get(0), 0);
		if( totalPage > 1){
			listener.loadPage(pages.get(1), 1);
		}
	}	
	
	public ViewGroup getRealViewer(){
		return myRealViewer;
	}
	
	public View getViewAt(int _index){
		return pages.get(_index);
	}	
	
	public void release(){
		for( int i = 0; i < pages.size(); i++){
			listener.releasePage(pages.get(i), i);			
		}		
		pages.removeAll(pages);
		pages = null;
		
		listener = null;
		
		totalPage = 0;
		myRealViewer.removeAllViews();		
		((ViewGroup)myRealViewer.getParent()).removeView(myRealViewer);
		myRealViewer = null;
		
		System.gc();
	}

	/*** OnScreenSwitchListener */
	@Override
	public void onScreenSwitched(int screen) {		
		if( screen > 0){
			listener.loadPage(pages.get( screen - 1 ), screen - 1);
			//_previous = pages.get(screen - 1); 
		}
		
		if( screen < totalPage - 1){
			listener.loadPage(pages.get( screen + 1 ), screen + 1);
			//_next = pages.get(screen + 1);
		}				
		listener.showCurrentPage(screen, pages.get(screen));
		
		if( screen > previousPageIndex){
			if( previousPageIndex - 1 >= 0){
				listener.hidePage(pages.get(previousPageIndex - 1), previousPageIndex - 1);
			}
			
		}else if( screen < previousPageIndex){
			if( previousPageIndex + 1 <= totalPage - 1){
				listener.hidePage(pages.get(previousPageIndex + 1), previousPageIndex + 1);
			}
		}		
		previousPageIndex = screen;
	}
	
	public void moveToPage(int _page)
	{
		myRealViewer.setCurrentScreen(_page, true);
		myRealViewer.setCanClick(false);
	}
	
	public void moveComplete()
	{
		myRealViewer.setCanClick(true);
	}
}
