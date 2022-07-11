package hk.SharedPreferencesSetting;

import android.app.Activity;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.Display;

public class ScreenInformation {
	public static ScreenInformation instance = null;
	private int screenWidth = 0;
	private int screenHeight = 0;
	private float dp_px_H = 0;
	private float dp_px_W = 0;
	
	private String deviceID;
	
	public static ScreenInformation getInstance(){
		if( instance == null){
			instance = new ScreenInformation();
		}
		
		return instance;
	}
	
	public void presetScreenSize(Activity _act){
		Display display = _act.getWindowManager().getDefaultDisplay(); 
		screenWidth = display.getWidth();  // deprecated
		screenHeight = display.getHeight();
		Log.d("Screen Size", "screenWidth: "+screenWidth+" screenHeight: "+screenHeight);
		
		DisplayMetrics displaymetrics = new DisplayMetrics();
		_act.getWindowManager().getDefaultDisplay().getMetrics(displaymetrics);
		dp_px_H = displaymetrics.ydpi;
		dp_px_W = displaymetrics.xdpi;
		Log.d("Screen Size", "screen Dp Width: "+dp_px_W+" Dp Height: "+dp_px_H);
		
		deviceID = android.provider.Settings.System.getString(
				_act.getContentResolver(),
				android.provider.Settings.Secure.ANDROID_ID);
		Log.d("deviceID", "deviceID:"+deviceID);
		
	}

	public int getscreenWidth()
	{
		return screenWidth;
	}
	public int getscreenHeight()
	{
		return screenHeight;
	}
	
	public float getscreenDpWidth()
	{
		return dp_px_W;
	}
	public float getscreenDpHeight()
	{
		return dp_px_H;
	}
	
	public String getdeviceID()
	{
		return deviceID;
	}
}
