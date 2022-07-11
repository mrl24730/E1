package com.ernestborel;

import hk.SharedPreferencesSetting.UserProfile;
import android.app.Activity;
import android.graphics.PixelFormat;
import android.media.MediaPlayer;
import android.media.MediaPlayer.OnCompletionListener;
import android.media.MediaPlayer.OnErrorListener;
import android.net.Uri;
import android.os.Bundle;

import android.widget.MediaController;
import android.widget.VideoView;

public class VideoActivity extends Activity{

	VideoView myVideoView;
	MediaController mediaController;
	   
	@Override	
	protected void onCreate(Bundle savedInstanceState) {			
		super.onCreate(savedInstanceState);	  
		UserProfile.isBackToBrand = true;
		
		setContentView(R.layout.activity_brand);
		getWindow().setFormat(PixelFormat.TRANSLUCENT);		
		
		myVideoView = (VideoView) findViewById(R.id.VideoView);
		

		if( UserProfile.getInstance().getCurrentLanguageIndex() == UserProfile.LANGUAGE_INDEX_EN){
			myVideoView.setVideoURI(Uri.parse("http://www.ernestborel.ch/video/2010_MV_en.mp4"));
					//("android.resource://" + getPackageName() + "/" + R.raw.mv_en));
		}else{
			myVideoView.setVideoURI(Uri.parse("http://www.ernestborel.ch/video/2010_MV.mp4"));
			//myVideoView.setVideoURI(Uri.parse("android.resource://" + getPackageName() + "/" + R.raw.mv));
		}			
		mediaController = new MediaController(this);
		myVideoView.setMediaController(mediaController);
		myVideoView.start();
				
		myVideoView.setOnErrorListener(new OnErrorListener() {			
			@Override
			public boolean onError(MediaPlayer mp, int what, int extra) {
				backToPreviousPage();
				return false;
			}
		});		

		myVideoView.setOnCompletionListener(new OnCompletionListener() {
			
			@Override
			public void onCompletion(MediaPlayer mp) {
				backToPreviousPage();
			}
		});
	}
	
	public void backToPreviousPage(){
		finish();
	}
	
	@Override
	public void onDestroy(){
		super.onDestroy();
		mediaController = null;

		myVideoView.setOnErrorListener(null);
		myVideoView.setOnCompletionListener(null);
	}	
}
