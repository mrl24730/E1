package com.Util;

import android.view.ViewGroup;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.view.animation.Animation.AnimationListener;
import android.view.animation.TranslateAnimation;

public class PageEventHandler {
	
	static PageEventHandler instance = null;
	
	Animation goToPage;
	Animation backToPage;	
	Animation selfNextPage;
	Animation selfBackPage;
	
	Animation fadeInAnimation;	
	Animation fadeOutAnimation;
	
	ViewGroup myViewGroup;
	
	public final static int PAGE_EVENT_ANIMATION_TIME = 300;
	
	public static PageEventHandler getInstance(int _width, int _height){
		if( instance == null){
			instance = new PageEventHandler(_width, _height);
		}
		return instance;
	}
	
	public static PageEventHandler getInstance(){		
		return instance;
	}
	
	public PageEventHandler(int _width, int _height) {
		goToPage = new TranslateAnimation(_width, 0, 0, 0);
		goToPage.setDuration(PAGE_EVENT_ANIMATION_TIME);		
		
		fadeInAnimation = new AlphaAnimation(0, 1);
		fadeInAnimation.setDuration(PAGE_EVENT_ANIMATION_TIME);
		fadeInAnimation.setFillAfter(true);
		
		fadeOutAnimation = new AlphaAnimation(1, 0.2f);
		fadeOutAnimation.setDuration(PAGE_EVENT_ANIMATION_TIME);
		fadeOutAnimation.setFillAfter(true);			
		
		backToPage = new TranslateAnimation(_width * -1, 0, 0, 0);
		backToPage.setDuration(PAGE_EVENT_ANIMATION_TIME);		
		
		selfNextPage = new TranslateAnimation(0, _width * -1, 0, 0);
		selfNextPage.setDuration(PAGE_EVENT_ANIMATION_TIME);
		
		selfBackPage = new TranslateAnimation(0, _width, 0, 0);
		selfBackPage.setDuration(PAGE_EVENT_ANIMATION_TIME);
	}
	
	public void changeCategory(ViewGroup _viewgroup){//, AnimationListener _listener){
		_viewgroup.startAnimation(fadeInAnimation);
		_viewgroup.bringToFront();		
		//fadeInAnimation.setAnimationListener(_listener);
	}	
	
	public void fadeOutAnimation(ViewGroup _viewgroup){
		_viewgroup.startAnimation(fadeOutAnimation);
		_viewgroup.bringToFront();		
	}
	
	public void fadeInAnimation(ViewGroup _viewgroup){
		_viewgroup.startAnimation(fadeInAnimation);
		_viewgroup.bringToFront();		
	}
	
	public void fadeOutAndIn(ViewGroup _viewGroup){
		myViewGroup = _viewGroup;
		
		fadeOutAnimation.setAnimationListener(new AnimationListener() {
			
			@Override
			public void onAnimationStart(Animation animation) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void onAnimationRepeat(Animation animation) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void onAnimationEnd(Animation animation) {
				myViewGroup.startAnimation(fadeInAnimation);	
				myViewGroup = null;
				fadeOutAnimation.setAnimationListener(null);
			}
		});
		_viewGroup.startAnimation(fadeOutAnimation);
		_viewGroup.bringToFront();
	}
	
	public void goToPage(ViewGroup _viewgroup){
		_viewgroup.startAnimation(goToPage);
		_viewgroup.bringToFront();		
	}
	
	public void backToPage(ViewGroup _viewgroup){
		_viewgroup.startAnimation(backToPage);
		_viewgroup.bringToFront();
	}
	
	public void selfNextPage(ViewGroup _viewgroup){
		_viewgroup.startAnimation(selfNextPage);
	}
	
	public void selfBackPage(ViewGroup _viewgroup){
		_viewgroup.startAnimation(selfBackPage);
	}
}
