package hk.SharedPreferencesSetting;

import hk.SharedPreferencesSetting.DataHandler.GetDataHandler;
import hk.SharedPreferencesSetting.DataHandler.RefreshHandler;
import hk.Util.Network.NetworkAddress;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;

import java.util.ArrayList;
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;

import org.json.JSONException;
import org.json.JSONObject;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.os.Handler;
import android.os.Message;
import android.telephony.TelephonyManager;
import android.util.Log;

public class UserProfile implements GetDataHandler{

	RefreshHandler refreshHandler = new RefreshHandler();
	public int SHOW_DIALOG = 0;
	public int HIDE_DIALOG = 1;
	
	public String language;
	public int currentLanguageIndex;
	
	public static boolean isBackToBrand = false;
	public boolean isFirstTime = false;
	
	public final String USING_LANG =  "lang";
	public final static String API_NAME_INIT = "init";
	public final static String API_NAME_LOCAITON	= "location";
	public final static String API_NAME_STORE	= "store";	
	public final static String API_NAME_AFTERSALE	= "aftersale";	
	public final static String API_NAME_COLLECTION	= "collection";
	public final static String API_NAME_NEWS	= "news";
	public final static String API_NAME_HISTORY	= "history";
	
	public final String LANGUAGE_TC =  "tc";
	public final String LANGUAGE_SC =  "sc";
	public final String LANGUAGE_ENG =  "en";
	
	public final static String ASSETS_KEY_IMAGE = "htmlimage";
	public final static String ASSETS_KEY_HTML = "html";
	
	public final static int LANGUAGE_INDEX_EN	 = 0;
	public final static int LANGUAGE_INDEX_ZHHK = 1;
	public final static int LANGUAGE_INDEX_ZHCN  = 2;

	public final static String COLLECTION_CATEGORY_AUTOMATIC = "automatic";
	public final static String COLLECTION_CATEGORY_QUARTZ = "quartz";
	
	public final static String TIME_ZONE_CITY_1 = "city_1";
	public final static String TIME_ZONE_CITY_2 = "city_2";
	public final static String TIME_ZONE_CITY_3 = "city_3";
	
	private String storeDetailsURL = "";
	
	private String urlID = "";
	
	private static UserProfile instance = null;
	ProgressDialog pdialog;
	Timer timer = new Timer(true);
	
	public Context context;	
	
	public int screenWidth = 0;
	public int screenHeight = 0;

	Hashtable<String, String> dataHashtable = null;//new Hashtable<String, String>();
	
	public int[] selectedCityIndex = new int[3];
	
	public static UserProfile getInstance(){
		if( instance == null){
			instance = new UserProfile();
		}
		return instance;
	}
	
	public static interface UserProfileHandler {
	    public void apiLoadComplete();
	}
	
	public static UserProfileHandler handler = null;
		
	public void setListener(UserProfileHandler _handler){
		handler = _handler;
	}
	
	public String getUDID(Activity _act){
		TelephonyManager mTelephonyMgr;
		mTelephonyMgr = (TelephonyManager)_act.getSystemService(Context.TELEPHONY_SERVICE);
		return  mTelephonyMgr.getDeviceId();
	}
	
	public void nextLanguage(){
		int _nextLanguage = currentLanguageIndex;
		_nextLanguage++;
		
		if( _nextLanguage > LANGUAGE_INDEX_ZHCN){
			_nextLanguage = LANGUAGE_INDEX_EN;
		}
		setLanguage(_nextLanguage);	
	}
	
	public boolean setLanguage(int _language){
		
			currentLanguageIndex = _language;
			switch (currentLanguageIndex) {
			case LANGUAGE_INDEX_EN:
				language = LANGUAGE_ENG;
				break;
			case LANGUAGE_INDEX_ZHCN:
				language = LANGUAGE_SC;
				break;
				
			case LANGUAGE_INDEX_ZHHK:
				language = LANGUAGE_TC;
				break;
			}
			return true;
		
	}
	
	public int getCurrentLanguageIndex(){
		return currentLanguageIndex;
	}
	
	public void saveSelectedCity(Context _context){
		PreferenceConnector.writeInteger(_context, TIME_ZONE_CITY_1, selectedCityIndex[0]);
		PreferenceConnector.writeInteger(_context, TIME_ZONE_CITY_2, selectedCityIndex[1]);
		PreferenceConnector.writeInteger(_context, TIME_ZONE_CITY_3, selectedCityIndex[2]);		
	}
	
	public void loadSelectedCity(Context _context){
		try{
			selectedCityIndex[0] = PreferenceConnector.readInteger(_context, TIME_ZONE_CITY_1, -1);
		}catch(Exception e){
			selectedCityIndex[0] = -1;
		}
		
		try{
			selectedCityIndex[1] = PreferenceConnector.readInteger(_context, TIME_ZONE_CITY_2, -1);
		}catch(Exception e){
			selectedCityIndex[1] = -1;
		}
		
		try{
			selectedCityIndex[2] = PreferenceConnector.readInteger(_context, TIME_ZONE_CITY_3, -1);
		}catch(Exception e){
			selectedCityIndex[2] = -1;
		}
	}
	
	public void save(Context _context){
		context = _context;
		PreferenceConnector.writeString(_context, USING_LANG, language);
		
//		try{
//			Log.d("Save", "Save Lanuage  " + language);		
//		}catch(Exception e){
//			Log.d("Error", "save Error Lanuage  ");				
//		}		
		
		Enumeration<String> e = dataHashtable.keys();
		while (e.hasMoreElements()) {
		    String _key = e.nextElement();
		    //Log.d("save", "save:"+_key);
		    PreferenceConnector.writeString(_context, _key, getData(_key));
		}
	    //Log.d("save", "save end");
		
	}
	
	public void load(Context _context){
		
		context = _context;
		language = PreferenceConnector.readString(_context, USING_LANG, LANGUAGE_ENG);
	
		Log.d("Load", "Load Lanuage  " + language);
		
		if( language.equals(LANGUAGE_ENG)){
			currentLanguageIndex = LANGUAGE_INDEX_EN;
			
		}else if( language.equals(LANGUAGE_SC)){
			currentLanguageIndex = LANGUAGE_INDEX_ZHCN;
			
		}else{
			currentLanguageIndex = LANGUAGE_INDEX_ZHHK;
		}
		
		dataHashtable = null;
		dataHashtable = PreferenceConnector.getAll(_context);
		//Log.d("dataHashtable", "dataHashtable:"+dataHashtable.size());
		if(dataHashtable.size() <= 0)
		{
			isFirstTime = true;
			refreshHandler.sendEmptyMessage(SHOW_DIALOG);
		}
		
		loadSelectedCity(_context);
	}
	
	public void setStoreDetailsURL(String _url, int _pageType){
		storeDetailsURL = NetworkAddress.DOMAIN_NAME + NetworkAddress.STORE_DETAILS_URL + "?id=" + _url + "&language=" + language + "&w=" + screenWidth;
		if(_pageType == NetworkAddress.LOCATION_PAGETYPE_AFTERSALE)
		{
			storeDetailsURL += "&type=network";
		}
	}
	
	public String getStoreDetailsURL(){
		return storeDetailsURL;
	}
	
	public class timerTask extends TimerTask
	{
		public void run()
	    {
			firstTime();
			refreshHandler.sendEmptyMessage(HIDE_DIALOG);
	    }
	};
	  
	public void firstTime()
	{
//		if(ScreenInformation.getInstance().getscreenWidth() > 480)
//		{
//			AssetsDataHandler.getInstance().copyAssets(context, ASSETS_KEY_IMAGE);
//		}
		
        String[] files = AssetsDataHandler.getInstance().getAssetsList(context, ASSETS_KEY_HTML);
        String nowLang = language;
	    for(int i = 0; i < files.length; i++)
	    {
	    	String text = AssetsDataHandler.getInstance().getAssetsTxtFile(context, ASSETS_KEY_HTML, files[i]);
	    	
	    	if(text != null)
	    	{

	    		String _fname = files[i].replace(".html", "");
	    		String[] names = _fname.split("-");
	    		if(names.length <= 2)
	    		{
	    			urlID = "";
	    		}else
	    		{
	    			urlID = names[2];
	    		}
	    		language = names[1];
	    		returnJson(text, names[0]);
	    	}
	    	
	    	text = null;
	    }
	    language = nowLang;
	}
	
	public void setTimeData(String _key)
	{
		dataHashtable.put(_key, "0");
	}
	
	public String getData(String _key)
	{
		return dataHashtable.get(_key);
	}
	
	public int getUpdateTime(String _key)
	{
		String jsonKey = buildKey(_key, "ts");
		String _jsonData = getData(jsonKey);
		int updateTime = 0;
		if(_jsonData != null)
		{
			updateTime =  Integer.valueOf(_jsonData);
		}
		return updateTime;
	}
	
	public int getUpdateTime(String _key, String _id)
	{
		String jsonKey = buildKey(_key, "ts", _id);
		String _jsonData = getData(jsonKey);
		int updateTime = 0;
		if(_jsonData != null)
		{
			updateTime =  Integer.valueOf(_jsonData);
		}
		return updateTime;
	}
	
	public void loadInit()
	{
		String _url = NetworkAddress.initUrl(getUpdateTime(API_NAME_INIT), language);
		loadUrl(_url, API_NAME_INIT);
	}
	
	public void loadHistory()
	{
		String _url = NetworkAddress.getHistory(language);
		loadUrl(_url, API_NAME_HISTORY);
	}
	
	public void loadLocationList(int _pageType){
		String _key = API_NAME_LOCAITON;
		if(_pageType == NetworkAddress.LOCATION_PAGETYPE_AFTERSALE)
		{
			_key = API_NAME_AFTERSALE+_key;
		}
		String _url = NetworkAddress.getLoactionList(getUpdateTime(_key), language, _pageType);
		loadUrl(_url, _key);
	}
	
	public void loadStoreList(String _cityID, int _pageType){
		String _url = NetworkAddress.getStoreURL(language, _cityID, _pageType);
		String _key = API_NAME_STORE;
		if(_pageType == NetworkAddress.LOCATION_PAGETYPE_AFTERSALE)
		{
			_key = API_NAME_AFTERSALE+_key;
		}
		loadUrl(_url, _key);
	}
	
	public void loadCollection(String _id, String _category){
		urlID = _id;
		String _url = NetworkAddress.getCollection(getUpdateTime(API_NAME_COLLECTION, _id), language, _id, _category);		
		loadUrl(_url, API_NAME_COLLECTION);
	}
	
	public void loadNews(String _id){
		urlID = _id;
		String _url = NetworkAddress.getNews(getUpdateTime(API_NAME_COLLECTION, _id), language, _id);		
		loadUrl(_url, API_NAME_NEWS);
	}
	
	public String buildKey(String _key, String _keyName)
	{
		return _key+"_"+language+"_"+_keyName;
	}
	
	public String buildKey(String _key, String _keyName, String _id)
	{
		return _key+"_"+language+"_"+_keyName+_id;
	}
	
	public void loadUrl(String _url, String _key)
	{
		Log.d("Start Load URL", "URL : " + _url);
		DataHandler dataHandler = DataHandler.getInstance();
		dataHandler.setListener(this);
		dataHandler.getDataByUrl(_url, context, _key);
	}

    public void returnJson(String _jsonData, String _key)
    {
    	if(!_key.equalsIgnoreCase(DataHandler.TAG_OFFLINE))
    	{
//    		String _fileName = _key+"-"+language;
//    		if(!(urlID.equalsIgnoreCase("")))
//    		{
//    			_fileName = _fileName+"-"+urlID;
//    		}
//    		_fileName += ".html";
//    		SaveTextHandler.getInstance().saveString(_jsonData, _fileName);
    		
    //		String meunJson = _jsonData;
            //Log.d("returnJson", "returnJson:"+meunJson);
            JSONObject _obj = null;
            try{
    			_obj = new JSONObject(_jsonData);
//    			_jsonStr = null;
    			
    		}catch (Exception e) {
    			return;
    			// TODO: handle exception
    		}
            Iterator<String> myIter = _obj.keys();
            List<String> sortKey = new ArrayList<String>();

            while(myIter.hasNext()){
                sortKey.add(myIter.next());
            }
            
            for (int i = 0; i < sortKey.size(); i++)
            {
            	String arrayKey = sortKey.get(i);
//            	String saveKey = buildKey(_key, arrayKey);
            	String saveKey = buildKey(_key, arrayKey, urlID);
            	try {
            		String _jsonString = _obj.getString(arrayKey);
                	dataHashtable.put(saveKey, _jsonString);
                	
        		} catch (JSONException e) {
        			// TODO Auto-generated catch block
        			e.printStackTrace();
        		}
            	
    		}
            save(context);
    	}
        
        if(handler != null)
        {
        	Log.d("apiLoadComplete", "apiLoadComplete");
        	handler.apiLoadComplete();
        }
        
        urlID = "";
        
        //Log.d("returnJson", "returnJson dataHashtable:"+dataHashtable);
        //Log.d("returnJson", "returnJson myIter:"+sortKey);
    }

	public int getScreenWidth() {
		return screenWidth;
	}

	public void setScreenWidth(int _screenWidth) {
		screenWidth = _screenWidth;
	}
	
	class RefreshHandler extends Handler {  
    	@Override  
    	public void handleMessage(Message msg) {
    		if(msg.what == SHOW_DIALOG)
    		{
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
    			
    			timer.schedule(new timerTask(), 1500);
    			
    		}else if(msg.what == HIDE_DIALOG)
    		{
    			if(pdialog != null)
    			{
    				pdialog.hide();
    				pdialog.dismiss();
    				pdialog = null;
    			}
    		}
    	}

    	public void sleep(long delayMillis) {  
    		this.removeMessages(0);  
    		sendMessageDelayed(obtainMessage(0), delayMillis);  
    	}
    }
}
