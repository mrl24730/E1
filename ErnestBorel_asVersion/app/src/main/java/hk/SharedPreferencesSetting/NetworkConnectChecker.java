package hk.SharedPreferencesSetting;

import android.app.Activity;
import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.util.Log;

public class NetworkConnectChecker {
//	Activity activity;
	ConnectivityManager conMgr;
	private static NetworkConnectChecker instance = null;
	
	public static NetworkConnectChecker getInstance(){
		if( instance == null){
			instance = new NetworkConnectChecker();
		}
		return instance;
	}
	
	public void setUpManager(Activity _activity)
	{
//		activity = _activity;
		conMgr =  (ConnectivityManager)_activity.getSystemService(Context.CONNECTIVITY_SERVICE);
	}
	
	public Boolean getNetworkConnect()
	{
//		ConnectivityManager conMgr =  (ConnectivityManager)activity.getSystemService(Context.CONNECTIVITY_SERVICE);
		Log.d("ConnectivityManager", "ConnectivityManager 0:"+conMgr.getNetworkInfo(0).getState()+" 1:"+conMgr.getNetworkInfo(1).getState());
		if ( conMgr.getNetworkInfo(0).getState() == NetworkInfo.State.CONNECTED 
			    ||  conMgr.getNetworkInfo(1).getState() == NetworkInfo.State.CONNECTED  ) {
			    //notify user you are online
			return true;
		}
		else if ( conMgr.getNetworkInfo(0).getState() == NetworkInfo.State.DISCONNECTED 
			    ||  conMgr.getNetworkInfo(1).getState() == NetworkInfo.State.DISCONNECTED) {
			    //notify user you are not online
			return false;
		}
		return false;
	}
}
