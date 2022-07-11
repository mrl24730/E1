package hk.SharedPreferencesSetting;

import java.util.Hashtable;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;

import android.os.Handler;
import android.os.Message;
import android.util.Log;

import hk.Util.Network.NetworkInterface;
import hk.Util.Network.NetworkJob;
import hk.Util.Network.NetworkManager;

public class DataHandler implements NetworkInterface{
	private static DataHandler instance = null;
	RefreshHandler refreshHandler = new RefreshHandler();
	Hashtable<String, String> urlHashtable = new Hashtable<String, String>();
	
	ProgressDialog pdialog;
	Context context;
	
	public int NETWORK_FAIL = 0;
	public int HIDE_DIALOG = 1;
	public int RETURN_JSON = 2;
	
	public String TAG_JSON = "JSON";
	public String TAG_IMAGE = "IMAGE";
	public static String TAG_OFFLINE = "OFFLINE";
	
	public String tagUrl = null;
	
	public static DataHandler getInstance(){
		if( instance == null){
			instance = new DataHandler();
		}
		return instance;
	}
	
	public static interface GetDataHandler {
	    public void returnJson(String _jsonData, String _key);
	}
	
	public static GetDataHandler handler = null;
		
	public void setListener(GetDataHandler _handler){
		handler = _handler;
	}
	
//	public void getDataByUrl(String _url) {
//		String _jsonData = urlHashtable.get(_url);
//		if(tagUrl != null)
//		{
//			tagUrl = null;
//		}
//		tagUrl = _url;
//		if(_jsonData != null)
//		{
//			handler.returnJson(_jsonData);
//		}else
//		{
//			if(context != null)
//			{
//				context = null;
//			}
//			if(pdialog != null)
//			{
//				pdialog.hide();
//				pdialog.dismiss();
//				pdialog = null;
//			}
//			loadUrl(_url, TAG_JSON);
//		}
//	}
	
	public void getDataByUrl(String _url, Context _context, String _key) {
		String _jsonData = urlHashtable.get(_url);
		if(tagUrl != null)
		{
			tagUrl = null;
		}
		tagUrl = _url;
		if(_jsonData != null)
		{
			handler.returnJson(_jsonData, _key);
		}else
		{
			if(context != null)
			{
				context = null;
			}
			context = _context;
			if(pdialog != null)
			{
				pdialog.hide();
				pdialog.dismiss();
				pdialog = null;
			}
			pdialog = new ProgressDialog(context);		
			pdialog.setCancelable(false);
			pdialog.setMessage("Loading ....");
			pdialog.show();
			loadUrl(_url, _key);
		}
	}
	
	public Boolean getImageByUrl(String _imageName, NetworkInterface _networkInterface)
	{
//		NetworkJob _job = new NetworkJob();
//		_job.networkInterface = _networkInterface;
//		_job.tag = _imageName;
//		_job.url = NetworkAddress.imageUrl(_imageName);
//		Log.d("_job.url", "_job.url:"+_job.url);
//		NetworkManager _manger = NetworkManager.getInstance();
//		_manger.addJob(_job);
		return false;
	}

	public void loadUrl(String _url, String _tag)
	{
		NetworkJob _job = new NetworkJob();
		_job.networkInterface = this;
		_job.tag = _tag;
		_job.url = _url;
		NetworkManager _manger = NetworkManager.getInstance();
		_manger.addJob(_job);
	}
	
	@Override
	public void didCompleteNetworkJob(NetworkJob networkJob) {
		refreshHandler.sendEmptyMessage(HIDE_DIALOG);
		// TODO Auto-generated method stub
		String _tag = networkJob.tag.toString();
		
		String _jsonStr = new String(networkJob.receiveData);
		Log.d("","didCompleteNetworkJob: _jsonStr"+_jsonStr);
		try{
			urlHashtable.put(tagUrl, _jsonStr);
			handler.returnJson(urlHashtable.get(tagUrl), _tag);
//			if(_tag.equalsIgnoreCase(TAG_JSON) && tagUrl != null)
//			{
//				
////				refreshHandler.sendEmptyMessage(RETURN_JSON);
//			}
		}catch (Exception e) {
			// TODO: handle exception
			Log.e("ee", "ee:"+e);
		}
		_jsonStr = null;
	}

	@Override
	public void didFailNetworkJob(NetworkJob networkJob) {
		// TODO Auto-generated method stub
		refreshHandler.sendEmptyMessage(NETWORK_FAIL);
		refreshHandler.sendEmptyMessage(HIDE_DIALOG);
	}
	
	public void showpopUp(String _string, Context _context)
	{
		if(_context != null)
		{
			AlertDialog alertGoDialog = new AlertDialog.Builder(_context).create();
			alertGoDialog.setButton("OK", new DialogInterface.OnClickListener() {
				public void onClick(DialogInterface dialog, int which) {
					return;
				} });
			alertGoDialog.setMessage(_string);
			alertGoDialog.show();
		}
	}
	
	public void reSetAllThing()
	{
		urlHashtable.clear();
	}
	
	class RefreshHandler extends Handler {  
    	@Override  
    	public void handleMessage(Message msg) {
    		if(msg.what == NETWORK_FAIL)
    		{
//    			showpopUp(LanguageHandler.getInstance().connectFail(), context);
    			handler.returnJson(null, TAG_OFFLINE);
    		}else if(msg.what == HIDE_DIALOG)
    		{
    			if(pdialog != null)
    			{
    				pdialog.hide();
    				pdialog.dismiss();
    				pdialog = null;
    			}
    			if(context != null)
    			{
    				context = null;
    			}
    		}else if(msg.what == RETURN_JSON)
    		{
//    			handler.returnJson(urlHashtable.get(tagUrl));
    		}
    	}

    	public void sleep(long delayMillis) {  
    		this.removeMessages(0);  
    		sendMessageDelayed(obtainMessage(0), delayMillis);  
    	}
    }
}
