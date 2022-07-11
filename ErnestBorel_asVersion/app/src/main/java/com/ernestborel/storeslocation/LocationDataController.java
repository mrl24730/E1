package com.ernestborel.storeslocation;

import java.util.ArrayList;
import java.util.Hashtable;

import android.util.Log;
import hk.SharedPreferencesSetting.UserProfile;
import hk.SharedPreferencesSetting.UserProfile.UserProfileHandler;
import hk.Util.Network.NetworkAddress;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class LocationDataController implements UserProfileHandler{
	
	ArrayList<String> regionList = new ArrayList<String>();
	ArrayList<ArrayList<String>> country = new ArrayList<ArrayList<String>>();
	ArrayList<ArrayList<ArrayList<Hashtable<String, String>>>> city = new ArrayList<ArrayList<ArrayList<Hashtable<String,String>>>>();
	ArrayList<ArrayList<String>> shop = new ArrayList<ArrayList<String>>();
	
	LocationStoreDataListener listener;
	
	int loadingData;
	int pageType;
	
	public static final int LOADING_DATA_LOCATION_LIST = 0;
	public static final int LOADING_DATA_STORE = 1;
	
	public LocationDataController(LocationStoreDataListener _listener) {
		listener = _listener;
	}
	
	public void reloadLocationData(int _pageType){
		loadingData = LOADING_DATA_LOCATION_LIST;
		pageType = _pageType;
		
		UserProfile _userProfile = UserProfile.getInstance();
		_userProfile.setListener(this);
		_userProfile.loadLocationList(_pageType);			
	}
	
	public void loadStoreInformation(String _cityID, int _pageType){
		loadingData = LOADING_DATA_STORE;
		pageType = _pageType;
		
		UserProfile _userProfile = UserProfile.getInstance();
		_userProfile.setListener(this);
		_userProfile.loadStoreList(_cityID, _pageType);
	}
	
	/*** Get Data ***/
	public ArrayList<String> getRegionData(){
		return regionList;
	}
	
	public ArrayList<String> getCountryData(int _regionIndex){
		return country.get(_regionIndex);
	}
	
	public ArrayList<String> getCity(int _regionIndex, int _countryIndex){
		ArrayList<Hashtable<String, String>> _tableList = city.get(_regionIndex).get(_countryIndex);	
		ArrayList<String> _city  = new ArrayList<String>();
		
		for( int i = 0; i < _tableList.size(); i++){
			Hashtable<String, String> _table = _tableList.get(i);
			String _name = _table.get("name");
			_city.add(_name);			
		}		
		return _city;
	}	

	public String getCityID(int _regionIndex, int _countryIndex, int _cityIndex){
		ArrayList<Hashtable<String, String>> _tableList = city.get(_regionIndex).get(_countryIndex);	
		return _tableList.get(_cityIndex).get("id");
	}
	
	public ArrayList<ArrayList<String>> getShopData(){
		return shop;
	}
	
	/***  UserProfileHandler ***/
	@Override
	public void apiLoadComplete() {
		UserProfile _userProfile = UserProfile.getInstance();
		
		if(loadingData == LOADING_DATA_LOCATION_LIST){
			String _key = UserProfile.API_NAME_LOCAITON;
			if(pageType == NetworkAddress.LOCATION_PAGETYPE_AFTERSALE)
			{
				_key = UserProfile.API_NAME_AFTERSALE+_key;
			}
			
			String mainKey = _userProfile.buildKey(_key, "region");
			String _jsonString = _userProfile.getData(mainKey);
			
			if( _jsonString == null || _jsonString.length() < 10){
				//No Need to update location store informations
				return;
			}
			
			//Clear Data
			regionList.removeAll(regionList);
			for( int i = 0; i < country.size(); i++){
				ArrayList<String> _ary = country.get(i);
				_ary.removeAll(_ary);
			}
			country.removeAll(country);
			for( int i = 0; i < city.size(); i++){
				ArrayList<ArrayList<Hashtable<String, String>>> _ary = city.get(i);
				for( int k = 0; k < _ary.size(); k++){
					ArrayList<Hashtable<String, String>> _ary2 = _ary.get(k);
					for( int z = 0; z < _ary2.size(); z++){
						Hashtable<String, String> _table = _ary2.get(z);
						_table.clear();
						_table = null;
					}
					_ary2.removeAll(_ary2);
				}
				_ary.removeAll(_ary);
			}
			city.removeAll(city);
				
			
			//Update Location Data
			try {
				//Get Region List
				JSONArray _list = new JSONArray(_jsonString);			
				for( int i = 0; i < _list.length(); i++){
					JSONObject _jsonRegion = _list.getJSONObject(i);				
					String _regionName = _jsonRegion.getString("name");
					regionList.add(_regionName);
					
					//Get Country List
					ArrayList<String> _countryList = new ArrayList<String>();		
					ArrayList<ArrayList<Hashtable<String, String>>> _cityTablePerRegion = new ArrayList<ArrayList<Hashtable<String,String>>>();
									
					JSONArray _jsonCountryList = _jsonRegion.getJSONArray("country");
					for( int k = 0; k < _jsonCountryList.length(); k++){
						ArrayList<Hashtable<String, String>> _cityTablePerCountry = new ArrayList<Hashtable<String,String>>();
						
						JSONObject _jsonCountry = _jsonCountryList.getJSONObject(k);
						String _country = _jsonCountry.getString("name");
						_countryList.add(_country);					
						
						//Get City List					
						JSONArray _jsonCityList = _jsonCountry.getJSONArray("city");
						for( int z = 0; z < _jsonCityList.length(); z++){
							Hashtable<String, String> _cityTable = new Hashtable<String, String>();
							
							JSONObject _jsonCity = _jsonCityList.getJSONObject(z);
							String _id = _jsonCity.getString("id");
							String _name = _jsonCity.getString("name");
													
							_cityTable.put("id", _id);
							_cityTable.put("name", _name);
							_cityTablePerCountry.add(_cityTable);
						}
						_cityTablePerRegion.add(_cityTablePerCountry);
					}	
					city.add(_cityTablePerRegion);
					country.add(_countryList);			
								
				}				
			} catch (JSONException e) {
				Log.e("Json Parser", "Parse Jason Error!!!");
			}	
		}else if( loadingData == LOADING_DATA_STORE){
			String _key = UserProfile.API_NAME_STORE;
			if(pageType == NetworkAddress.LOCATION_PAGETYPE_AFTERSALE)
			{
				_key = UserProfile.API_NAME_AFTERSALE+_key;
			}
			
			String mainKey = _userProfile.buildKey(_key, "shop");
			String _jsonString = _userProfile.getData(mainKey);
			
			//clear data
			for( int i = 0; i < shop.size(); i++){
				ArrayList<String> _shopInfo = shop.get(i);
				_shopInfo.removeAll(_shopInfo);
			}
			shop.removeAll(shop);
			
			try {
				//Get Region List
				JSONArray _shopList = new JSONArray(_jsonString);
				for( int i = 0; i < _shopList.length(); i++){
					
					ArrayList<String> _storeInfo = new ArrayList<String>();
					JSONArray _shop = _shopList.getJSONArray(i);
					for( int k = 0; k < _shop.length(); k++){
						String _info = _shop.getString(k);
						_storeInfo.add(_info);
					}	
					shop.add(_storeInfo);
				}				
			}catch(Exception e){
				Log.e("Json Parser", "Parse Jason Error!!!");
			}
		}					
		listener.locationDataFinishUpdate(loadingData);
	}
}



