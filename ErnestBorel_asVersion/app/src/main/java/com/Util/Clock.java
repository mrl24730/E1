package com.Util;

import java.util.Calendar;
import java.util.TimeZone;
import java.util.Timer;
import java.util.TimerTask;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;


public class Clock extends View{
	
	Paint paint = new Paint();
	DrawHandler myHandler = new DrawHandler();
	
	int centerPointX;
	int centerPointY;
	int clockLength;
	
	float hoursRotation;
	float minutesRotation;
	float secondRotation;
	
	float currentTime;
	int clockTag;
	
	ClockListener myListener;
	long updatedTimeStamp;
	
	Timer timer;

	public Clock(Context context, int _posX, int _posY, int _length, int _clockTag, ClockListener _listener, long timeStamp) {		
		super(context);		
		
		Log.d("timeStamp", "time Update TimeStamp : " + timeStamp);
		updatedTimeStamp = timeStamp;
		centerPointX = _posX;
		centerPointY = _posY;
		clockLength = _length;
		currentTime = 0;
		clockTag = _clockTag;
				
		hoursRotation = minutesRotation = secondRotation = -90;
		Calendar _cal = Calendar.getInstance(TimeZone.getTimeZone("UTC"));
		_cal.setTimeInMillis(timeStamp);
		
		secondRotation += 360 / 60 * _cal.get(Calendar.SECOND); 
		minutesRotation += 360 / 60 * _cal.get(Calendar.MINUTE);
		hoursRotation += 360 / 12 * _cal.get(Calendar.HOUR) + ((360 / 12 ) / 60.0f ) * _cal.get(Calendar.MINUTE);
		currentTime = _cal.get(Calendar.SECOND);
		
		paint.setAntiAlias(true);		
		setWillNotDraw(false);
		
		timer = new Timer();
		timer.schedule(new TimerTask() {			
			@Override
			public void run() {
				myHandler.sendEmptyMessage(0);				
			}
		}, 0, 1000);
		setBackgroundColor(Color.TRANSPARENT);
	
		
		myListener = _listener;
	}
	
	@Override 
	public void onDetachedFromWindow(){		
		super.onDetachedFromWindow();
		myHandler = null;
		paint = null;
		timer.cancel();
		timer = null;
	}

	@Override
	public void onDraw(Canvas canvas){
		super.onDraw(canvas);

		canvas.drawColor(Color.TRANSPARENT);
		
		//draw seconds	
		paint.setColor(0xff8B7F65);						
		paint.setStrokeWidth(1);		
		float _endX = (float)(centerPointX + clockLength * Math.cos(secondRotation * Math.PI / 180));			
		float _endY = (float)(centerPointY + clockLength * Math.sin(secondRotation * Math.PI / 180));
		canvas.drawLine(centerPointX, centerPointY, _endX, _endY, paint);
		
		//draw minutes				
		paint.setColor(Color.WHITE);
		paint.setStrokeWidth(4);		
		_endX = (float)(centerPointX + clockLength / 10 * 9 * Math.cos(minutesRotation * Math.PI / 180));			
		_endY = (float)(centerPointY + clockLength / 10 * 9 * Math.sin(minutesRotation * Math.PI / 180));
		canvas.drawLine(centerPointX, centerPointY, _endX, _endY, paint);
		
		//draw hours
		paint.setColor(Color.WHITE);
		paint.setStrokeWidth(4);
		_endX = (float)(centerPointX + clockLength / 10 * 6 * Math.cos(hoursRotation * Math.PI / 180));			
		_endY = (float)(centerPointY + clockLength / 10 * 6 * Math.sin(hoursRotation * Math.PI / 180));
		canvas.drawLine(centerPointX, centerPointY, _endX, _endY, paint);
	}
	
	class DrawHandler extends Handler{
		@Override  
		public void handleMessage(Message msg) {  
			currentTime += 1;					
			updatedTimeStamp += 1000;			
			
			secondRotation += 360 / 60;
			
			if( secondRotation == 270){
				secondRotation = -90;
				minutesRotation += 360 / 60;						
				hoursRotation += 360 / 12 / 60.0f;
				
				myListener.didUpdateTime(updatedTimeStamp, clockTag);
			}			
			invalidate();
		}
	}
	
	
	public interface ClockListener{
		public void didUpdateTime(long _timeStamp, int _clockTag);		
	}
}
