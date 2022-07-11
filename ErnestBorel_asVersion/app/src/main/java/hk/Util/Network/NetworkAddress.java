package hk.Util.Network;

import hk.SharedPreferencesSetting.UserProfile;

public class NetworkAddress {

	 public static String DOMAIN_NAME = "http://www.ernestborel.cn/api/";
//	public static String DOMAIN_NAME = "http://revampcn.kitchen-digital.com/api/";

	public static String INIT_URL = "init.ashx?ts=%tt&lang=%ll&device=2";
	public static String COLLECTION_URL = "getCollection.ashx";
	public static String NEWS_URL = "getNews.ashx";
	public static String LOCATION_URL = "getLocation.ashx";
	public static String STORE_URL = "getStore.ashx";
	public static String HISTORY_URL = "getHistory.ashx";

	public static String STORE_DETAILS_URL = "location_detail.aspx";

	public static String TIME_ZONE_URL = "http://api.timezonedb.com/?zone=";

	public final static int LOCATION_PAGETYPE_STORES	= 0;
	public final static int LOCATION_PAGETYPE_AFTERSALE	= 1;
	
	public static String initUrl(int _ts, String _lang) {
		return DOMAIN_NAME
				+ INIT_URL.replaceAll("%tt", _ts + "").replaceAll("%ll", _lang)
				+ "&w=" + UserProfile.getInstance().getScreenWidth();
	}

	public static String getLoactionList(int _ts, String _lang, int _type) {
		String _url = DOMAIN_NAME + LOCATION_URL + "?ts=" + _ts + "&lang=" + _lang;
		if(_type == LOCATION_PAGETYPE_AFTERSALE)
		{
			_url += "&type=network";
		}
		return _url;
	}

	public static String getStoreURL(String _lang, String _id, int _type) {
		String _url = DOMAIN_NAME + STORE_URL + "?lang=" + _lang + "&id=" + _id;
		if(_type == LOCATION_PAGETYPE_AFTERSALE)
		{
			_url += "&type=network";
		}
		return _url;
	}

	public static String getCollection(int _ts, String _lang, String _id,
			String _category) {
		return DOMAIN_NAME + COLLECTION_URL + "?ts=" + _ts + "&lang=" + _lang
				+ "&id=" + _id + "&category=" + _category + "&w="
				+ UserProfile.getInstance().getScreenWidth();
	}

	public static String getNews(int _ts, String _lang, String _id) {
		return DOMAIN_NAME + NEWS_URL + "?ts=" + _ts + "&lang=" + _lang
				+ "&id=" + _id + "&w="
				+ UserProfile.getInstance().getScreenWidth();
	}

	public static String getTimeZone(String _zone) {
		return TIME_ZONE_URL + _zone + "&key=89ZFW3SHPJ66&format=json";
	}

	public static String getHistory(String _lang) {
		String _url = DOMAIN_NAME + HISTORY_URL + "?lang=" + _lang;
		return _url;
	}
}
