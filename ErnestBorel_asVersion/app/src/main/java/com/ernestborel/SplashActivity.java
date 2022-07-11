package com.ernestborel;

import java.util.Timer;
import java.util.TimerTask;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.widget.ImageView;

@SuppressLint("HandlerLeak")
public class SplashActivity extends Activity{

	ImageView animatedImgView;
	AnimationDrawable frameAnimation;
	Timer timer = new Timer();
	
	private final int SPLASH_TOTAL_FRAME = 8;
	private final int SPLASH_DURATION_PER_FRAME = 90;
	private final int SPLASH_IDLE_TIME = 1500;
	private final int SPLASH_WAIT_TIME = 800;
	
	SplashHandler myHandler = new SplashHandler();
	
	 @Override	   
	 protected void onCreate(Bundle savedInstanceState) {
		 super.onCreate(savedInstanceState);
		 setContentView(R.layout.activity_splash);
		 
		 animatedImgView = (ImageView)findViewById(R.id.Opening_ImgView);		 
		 animatedImgView.setBackgroundResource(R.drawable.animation_splash);
		
		 frameAnimation = (AnimationDrawable) animatedImgView.getBackground();		 
		 
		 timer.schedule(new TimerTask() {
				
				@Override
				public void run() {
					frameAnimation.start();
				}
			}, SPLASH_WAIT_TIME);
		 
		 timer.schedule(new TimerTask() {
			
			@Override
			public void run() {
				frameAnimation.stop();
				myHandler.sendEmptyMessageDelayed(1, SPLASH_IDLE_TIME);
			}
		}, SPLASH_DURATION_PER_FRAME * SPLASH_TOTAL_FRAME + SPLASH_WAIT_TIME);

	 }
	 
	 class SplashHandler extends Handler{
		 @Override  
			public void handleMessage(Message msg) {  				
				Intent _intent = new Intent(SplashActivity.this, MainActivity.class);
				startActivity(_intent);
				finish();
				animatedImgView.setAnimation(null);			
				_intent = null;			
				frameAnimation = null;
				timer = null;				
			}
	 }
}
