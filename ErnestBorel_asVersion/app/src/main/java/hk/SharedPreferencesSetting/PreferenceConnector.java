package hk.SharedPreferencesSetting;

import java.util.Hashtable;
import java.util.Map;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
/**
 * The class PreferenceConnector is a class useful to
 * simplify you the interaction with your app preferences.
 * In fact it has methods that interact with the basical features
 * of SharedPreferences but still the possibility to obtain
 * preferences.
 * 
 * @author Simone Casagranda
 *
 */
public class PreferenceConnector{
	public static final String PREF_NAME = "ERNESTBOREL_LIST";
	public static final int MODE = Context.MODE_PRIVATE;
	
	public static final String NAME = "NAME";
	public static final String SURNAME = "SURNAME";
	public static final String AGE = "AGE";
	
	public static SharedPreferences preferences = null;  

	public static void writeBoolean(Context context, String key, boolean value) {
		getEditor(context).putBoolean(key, value).commit();
	}

	public static boolean readBoolean(Context context, String key, boolean defValue) {
		return getPreferences(context).getBoolean(key, defValue);
	}

	public static void writeInteger(Context context, String key, int value) {
		getEditor(context).putInt(key, value).commit();
	}

	public static int readInteger(Context context, String key, int defValue) {
		return getPreferences(context).getInt(key, defValue);
	}

	public static void writeString(Context context, String key, String value) {
		SharedPreferences.Editor _editor = getEditor(context);
		_editor.putString(key, value);
		_editor.commit();

	}
	
	public static String readString(Context context, String key, String defValue) {
		return getPreferences(context).getString(key, defValue);
	}
	
	public static void writeFloat(Context context, String key, float value) {
		getEditor(context).putFloat(key, value).commit();
	}

	public static float readFloat(Context context, String key, float defValue) {
		return getPreferences(context).getFloat(key, defValue);
	}
	
	public static void writeLong(Context context, String key, long value) {
		getEditor(context).putLong(key, value).commit();
	}

	public static long readLong(Context context, String key, long defValue) {
		return getPreferences(context).getLong(key, defValue);
	}

	public static SharedPreferences getPreferences(Context context) {
		if( preferences == null){
			preferences = context.getSharedPreferences(PREF_NAME, MODE);
		}
		return preferences;
	}

	
	public static Editor getEditor(Context context) {
		return getPreferences(context).edit();
	}

	public static Hashtable<String, String> getAll(Context context) {
		// TODO Auto-generated method stub
		Map<String, ?> _map = getPreferences(context).getAll();
		Hashtable<String, String> _hashtable = new Hashtable<String, String>();
		for (String _key : _map.keySet()) {
			_hashtable.put(_key, _map.get(_key).toString());
		}
		return _hashtable;
	}

}
