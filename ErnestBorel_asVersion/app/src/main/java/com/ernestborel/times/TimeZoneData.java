package com.ernestborel.times;

import hk.SharedPreferencesSetting.UserProfile;

import java.util.ArrayList;

import android.content.Context;
import android.util.Log;

import com.ernestborel.R;

public class TimeZoneData{
	private static TimeZoneData instance = null;
	private int totalCell = 109;
	String[] timeKey = new String[totalCell];
	int[] timeName = new int[totalCell];
	int[] timeUTC = new int[totalCell];
	int[] timeUTCH = new int[totalCell];
	int[] timeUTCM = new int[totalCell];
	int[] timeEN = new int[totalCell];
	int[] timeZHHK = new int[totalCell];
	int[] timeZHCN = new int[totalCell];
	
	int TIME_ADD = 0;
	int TIME_MINUS = 1;
	
	public static TimeZoneData getInstance(){
		if( instance == null){
			instance = new TimeZoneData();
		}
		return instance;
	}
	
	public TimeZoneData()
	{
		setUplist(108, "Europe/Vienna", R.string.timezone_name_switzerland, TIME_ADD, 1, 0, 95, 87, 93);
		setUplist(0, "Europe/Vienna", R.string.timezone_name_amsterdam, TIME_ADD, 1, 0, 4, 42, 59);
		setUplist(1, "Europe/Vienna", R.string.timezone_name_vienna, TIME_ADD, 1, 0, 105, 98, 107);
		setUplist(2, "Europe/Vienna", R.string.timezone_name_brussels, TIME_ADD, 1, 0, 18, 17, 27);
		setUplist(3, "Europe/Prague", R.string.timezone_name_prague, TIME_ADD, 1, 0, 82, 16, 26);
		setUplist(4, "Europe/Copenhagen", R.string.timezone_name_copenhagen, TIME_ADD, 1, 0, 29, 54, 70);
		setUplist(5, "Europe/Paris", R.string.timezone_name_paris, TIME_ADD, 1, 0, 77, 7, 12);
		setUplist(6, "Europe/Berlin", R.string.timezone_name_berlin, TIME_ADD, 1, 0, 13, 47, 64);
		setUplist(7, "Europe/Vatican", R.string.timezone_name_vatican, TIME_ADD, 1, 0, 104, 69, 78);
		setUplist(8, "Europe/Rome", R.string.timezone_name_rome, TIME_ADD, 1, 0, 84, 108, 53);
		setUplist(9, "Europe/Monaco", R.string.timezone_name_monaco, TIME_ADD, 1, 0, 66, 104, 100);
		setUplist(10, "Europe/Oslo", R.string.timezone_name_oslo, TIME_ADD, 1, 0, 75, 82, 84);
		setUplist(11, "Europe/Warsaw", R.string.timezone_name_warsaw, TIME_ADD, 1, 0, 106, 75, 37);
		setUplist(12, "Europe/Madrid", R.string.timezone_name_madrid, TIME_ADD, 1, 0, 58, 66, 7);
		setUplist(13, "Europe/Stockholm", R.string.timezone_name_stockholm, TIME_ADD, 1, 0, 93, 74, 85);
		setUplist(14, "Europe/Stockholm", R.string.timezone_name_berne, TIME_ADD, 1, 0, 14, 28, 44);
		setUplist(15, "Africa/Cairo", R.string.timezone_name_cairo, TIME_ADD, 2, 0, 21, 77, 13);
		setUplist(16, "Europe/Helsinki", R.string.timezone_name_helsinki, TIME_ADD, 2, 0, 42, 100, 96);
		setUplist(17, "Europe/Athens", R.string.timezone_name_athens, TIME_ADD, 2, 0, 8, 79, 88);
		setUplist(18, "Europe/Bucharest", R.string.timezone_name_bucharest, TIME_ADD, 2, 0, 19, 13, 23);
		setUplist(19, "Europe/Bucharest", R.string.timezone_name_ankara, TIME_ADD, 2, 0, 5, 27, 43);
		setUplist(20, "Europe/Kiev", R.string.timezone_name_kiev, TIME_ADD, 2, 0, 49, 58, 103);
		setUplist(21, "Europe/Istanbul", R.string.timezone_name_istanbul, TIME_ADD, 2, 0, 46, 21, 35);
		setUplist(22, "Asia/Kuwait", R.string.timezone_name_kuwait, TIME_ADD, 3, 0, 52, 49, 66);
		setUplist(23, "Indian/Antananarivo", R.string.timezone_name_antananarivo, TIME_ADD, 3, 0, 6, 80, 89);
		setUplist(24, "Asia/Tehran", R.string.timezone_name_tehran, TIME_ADD, 3, 0, 98, 103, 99);
		setUplist(25, "Asia/Muscat", R.string.timezone_name_muscat, TIME_ADD, 3, 0, 68, 65, 6);
		setUplist(26, "Asia/Qatar", R.string.timezone_name_doha, TIME_ADD, 3, 0, 33, 25, 41);
		setUplist(27, "Asia/Riyadh", R.string.timezone_name_riyadh, TIME_ADD, 3, 0, 83, 29, 45);
		setUplist(28, "Europe/Moscow", R.string.timezone_name_moscow, TIME_ADD, 4, 0, 67, 70, 79);
		setUplist(29, "Asia/Dubai", R.string.timezone_name_abudhabi, TIME_ADD, 4, 0, 1, 41, 58);
		setUplist(30, "Asia/Dubai", R.string.timezone_name_portlouis, TIME_ADD, 4, 0, 80, 91, 94);
		setUplist(31, "Asia/Kabul", R.string.timezone_name_kabul, TIME_ADD, 4, 30, 48, 72, 81);
		setUplist(32, "Indian/Maldives", R.string.timezone_name_male, TIME_ADD, 5, 0, 60, 64, 5);
		setUplist(33, "Asia/Karachi", R.string.timezone_name_islamabad, TIME_ADD, 5, 0, 45, 22, 36);
		setUplist(34, "Asia/Colombo", R.string.timezone_name_newdelhi, TIME_ADD, 5, 30, 71, 85, 92);
		setUplist(35, "Asia/Colombo", R.string.timezone_name_colombo, TIME_ADD, 5, 30, 28, 11, 18);
		setUplist(36, "Asia/Thimphu", R.string.timezone_name_thimphu, TIME_ADD, 6, 0, 99, 31, 47);
		setUplist(37, "Asia/Dhaka", R.string.timezone_name_dhaka, TIME_ADD, 6, 0, 32, 92, 48);
		setUplist(38, "Asia/Rangoon", R.string.timezone_name_yangon, TIME_ADD, 6, 30, 109, 20, 34);
		setUplist(39, "Asia/Phnom_Penh", R.string.timezone_name_phnompenh, TIME_ADD, 7, 0, 78, 39, 55);
		setUplist(40, "Asia/Jakarta", R.string.timezone_name_jakarta, TIME_ADD, 7, 0, 47, 78, 87);
		setUplist(41, "Asia/Bangkok", R.string.timezone_name_bangkok, TIME_ADD, 7, 0, 11, 68, 77);
		setUplist(42, "Asia/Phnom_Penh", R.string.timezone_name_hanoi, TIME_ADD, 7, 0, 40, 36, 52);
		setUplist(43, "Asia/Chongqing", R.string.timezone_name_beijing, TIME_ADD, 8, 0, 12, 9, 16);
		setUplist(44, "Asia/Shanghai", R.string.timezone_name_shanghai, TIME_ADD, 8, 0, 90, 1, 1);
		setUplist(45, "Asia/Macau", R.string.timezone_name_macau, TIME_ADD, 8, 0, 57, 105, 101);
		setUplist(46, "Asia/Kuala_Lumpur", R.string.timezone_name_kualalumpur, TIME_ADD, 8, 0, 51, 24, 40);
		setUplist(47, "Asia/Hong_Kong", R.string.timezone_name_ulanbator, TIME_ADD, 8, 0, 102, 59, 104);
		setUplist(48, "Asia/Manila", R.string.timezone_name_manila, TIME_ADD, 8, 0, 61, 63, 4);
		setUplist(49, "Asia/Singapore", R.string.timezone_name_singapore, TIME_ADD, 8, 0, 91, 83, 90);
		setUplist(50, "Asia/Taipei", R.string.timezone_name_taipei, TIME_ADD, 8, 0, 97, 12, 19);
		setUplist(51, "Asia/Hong_Kong", R.string.timezone_name_hongkong, TIME_ADD, 8, 0, 43, 52, 69);
		setUplist(52, "Asia/Chongqing", R.string.timezone_name_chongqing, TIME_ADD, 8, 0, 27, 51, 68);
		setUplist(53, "Asia/Tokyo", R.string.timezone_name_tokyo, TIME_ADD, 9, 0, 100, 35, 14);
		setUplist(54, "Asia/Tokyo", R.string.timezone_name_osaka, TIME_ADD, 9, 0, 74, 2, 2);
		setUplist(55, "Asia/Seoul", R.string.timezone_name_seoul, TIME_ADD, 9, 0, 89, 95, 28);
		setUplist(56, "Australia/Adelaide", R.string.timezone_name_adelaide, TIME_ADD, 9, 30, 2, 44, 61);
		setUplist(57, "Australia/Darwin", R.string.timezone_name_darwin, TIME_ADD, 9, 30, 31, 94, 49);
		setUplist(58, "Australia/Brisbane", R.string.timezone_name_brisbane, TIME_ADD, 10, 0, 16, 14, 24);
		setUplist(59, "Pacific/Guam", R.string.timezone_name_hagatna, TIME_ADD, 10, 0, 38, 40, 57);
		setUplist(60, "Australia/Melbourne", R.string.timezone_name_melbourne, TIME_ADD, 10, 0, 62, 102, 97);
		setUplist(61, "Australia/Sydney", R.string.timezone_name_canberra, TIME_ADD, 10, 0, 22, 73, 82);
		setUplist(62, "Australia/Sydney", R.string.timezone_name_sydney, TIME_ADD, 10, 0, 96, 67, 76);
		setUplist(63, "Pacific/Guadalcanal", R.string.timezone_name_solomonislands, TIME_ADD, 11, 0, 92, 61, 74);
		setUplist(64, "Pacific/Guadalcanal", R.string.timezone_name_newcaledonia, TIME_ADD, 11, 0, 70, 84, 91);
		setUplist(65, "Australia/Brisbane", R.string.timezone_name_portvila, TIME_ADD, 10, 0, 81, 99, 108);
		setUplist(66, "Pacific/Fiji", R.string.timezone_name_suva, TIME_ADD, 12, 0, 94, 109, 75);
		setUplist(67, "Asia/Magadan", R.string.timezone_name_magadan, TIME_ADD, 12, 0, 59, 62, 3);
		setUplist(68, "Pacific/Auckland", R.string.timezone_name_auckland, TIME_ADD, 12, 0, 9, 81, 83);
		setUplist(69, "Pacific/Auckland", R.string.timezone_name_wellington, TIME_ADD, 12, 0, 108, 46, 63);
		setUplist(70, "Pacific/Apia", R.string.timezone_name_samoa, TIME_ADD, 13, 0, 85, 107, 109);
		setUplist(71, "Pacific/Tongatapu", R.string.timezone_name_nuku, TIME_ADD, 13, 0, 73, 30, 46);
		setUplist(72, "Europe/Dublin", R.string.timezone_name_dublin, TIME_ADD, 0, 0, 34, 71, 80);
		setUplist(73, "Europe/Lisbon", R.string.timezone_name_lisbon, TIME_ADD, 0, 0, 54, 32, 51);
		setUplist(74, "Europe/London", R.string.timezone_name_london, TIME_ADD, 0, 0, 55, 53, 105);
		setUplist(75, "Atlantic/Azores", R.string.timezone_name_azores, TIME_MINUS, 1, 0, 10, 34, 33);
		setUplist(76, "Africa/Dakar", R.string.timezone_name_dakar, TIME_MINUS, 1, 0, 30, 93, 50);
		setUplist(77, "America/Montevideo", R.string.timezone_name_brasilia, TIME_MINUS, 2, 0, 15, 5, 10);
		setUplist(78, "America/Montevideo", R.string.timezone_name_midatlantic, TIME_MINUS, 2, 0, 64, 3, 8);
		setUplist(79, "America/Fortaleza", R.string.timezone_name_greenland, TIME_MINUS, 3, 0, 37, 57, 73);
		setUplist(80, "America/Fortaleza", R.string.timezone_name_fortaleza, TIME_MINUS, 3, 0, 36, 97, 95);
		setUplist(81, "America/Argentina/Cordoba", R.string.timezone_name_buenosaires, TIME_MINUS, 3, 0, 20, 15, 25);
		setUplist(82, "America/Santiago", R.string.timezone_name_santiago, TIME_MINUS, 3, 0, 88, 88, 20);
		setUplist(83, "America/Halifax", R.string.timezone_name_newfoundland, TIME_MINUS, 3, 30, 72, 60, 106);
		setUplist(84, "Atlantic/Bermuda", R.string.timezone_name_hamiltonbermuda, TIME_MINUS, 4, 0, 39, 96, 29);
		setUplist(85, "America/Tortola", R.string.timezone_name_britishvirginislands, TIME_MINUS, 4, 0, 17, 50, 67);
		setUplist(86, "America/Argentina/San_Juan", R.string.timezone_name_sanjuanpuertorico, TIME_MINUS, 4, 0, 87, 90, 22);
		setUplist(87, "America/St_Lucia", R.string.timezone_name_castries, TIME_MINUS, 4, 0, 24, 10, 17);
		setUplist(88, "America/Caracas", R.string.timezone_name_caracas, TIME_MINUS, 4, 30, 23, 8, 15);
		setUplist(89, "America/Nassau", R.string.timezone_name_nassaubahamas, TIME_MINUS, 5, 0, 69, 56, 72);
		setUplist(90, "America/Toronto", R.string.timezone_name_toronto, TIME_MINUS, 5, 0, 101, 26, 42);
		setUplist(91, "America/Havana", R.string.timezone_name_havana, TIME_MINUS, 5, 0, 41, 45, 62);
		setUplist(92, "America/Jamaica", R.string.timezone_name_kingston, TIME_MINUS, 5, 0, 50, 38, 56);
		setUplist(93, "America/Panama", R.string.timezone_name_panamacity, TIME_MINUS, 5, 0, 76, 6, 11);
		setUplist(94, "America/New_York", R.string.timezone_name_washington, TIME_MINUS, 5, 0, 107, 76, 38);
		setUplist(95, "America/New_York", R.string.timezone_name_sanjose, TIME_MINUS, 6, 0, 86, 89, 21);
		setUplist(96, "America/Mexico_City", R.string.timezone_name_mexicocity, TIME_MINUS, 6, 0, 63, 101, 98);
		setUplist(97, "America/Chicago", R.string.timezone_name_chicago, TIME_MINUS, 6, 0, 25, 37, 54);
		setUplist(98, "America/Chihuahua", R.string.timezone_name_chihuahua, TIME_MINUS, 7, 0, 26, 23, 39);
		setUplist(99, "America/Phoenix", R.string.timezone_name_arizona, TIME_MINUS, 7, 0, 7, 33, 32);
		setUplist(100, "America/Los_Angeles", R.string.timezone_name_losangeles, TIME_MINUS, 8, 0, 56, 48, 65);
		setUplist(101, "Pacific/Pitcairn", R.string.timezone_name_pitcairnislands, TIME_MINUS, 8, 0, 79, 19, 31);
		setUplist(102, "America/Vancouver", R.string.timezone_name_vancouver, TIME_MINUS, 8, 0, 103, 86, 86);
		setUplist(103, "America/Juneau", R.string.timezone_name_alaska, TIME_MINUS, 9, 0, 3, 43, 60);
		setUplist(104, "Pacific/Honolulu", R.string.timezone_name_honoluluhawaii, TIME_MINUS, 10, 0, 44, 106, 102);
		setUplist(105, "Pacific/Midway", R.string.timezone_name_midwayatoll, TIME_MINUS, 11, 0, 65, 4, 9);
		setUplist(106, "Pacific/Enderbury", R.string.timezone_name_enderburyisland, TIME_MINUS, 11, 0, 35, 55, 71);
		setUplist(107, "Pacific/Kwajalein", R.string.timezone_name_kwajaleinatoll, TIME_MINUS, 12, 0, 53, 18, 30);
	}
	
	public void setUplist(int _index, String _key, int _name, int _utc,  int _utcH, int _utcM, int _indexEn, int _indexZhhk, int _indexZhcn)
	{
		timeKey[_index] = _key;
		timeName[_index] = _name;
		timeUTC[_index] = _utc;
		timeUTCH[_index] = _utcH;
		timeUTCM[_index] = _utcM;
		timeEN[_index] = _indexEn;
		timeZHHK[_index] = _indexZhhk;
		timeZHCN[_index] = _indexZhcn;
	}
	
	public int[] getlist(int _noNeed1, int _noNeed2, int _noNeed3)
	{
		int _lang = UserProfile.getInstance().currentLanguageIndex;
		int[] timelang = new int[totalCell];
		for(int i = 0; i < totalCell; i++)
		{
			Log.d("getlist", "getlist timelang:"+timelang.length+" timeEN:"+timeEN.length+" i:"+i);
			int sortNum = 0;
			switch (_lang) {
				case UserProfile.LANGUAGE_INDEX_EN:
					sortNum = timeEN[i];
					break;
					
				case UserProfile.LANGUAGE_INDEX_ZHHK:
					sortNum = timeZHHK[i];
					break;
					
				case UserProfile.LANGUAGE_INDEX_ZHCN:
					sortNum = timeZHCN[i];
					break;
			}
			timelang[(sortNum-1)] = i;
		}
		
		ArrayList<String> array_sort= new ArrayList<String>();
		for(int i = 0; i < totalCell; i++)
		{
			if(i != _noNeed1 && i != _noNeed2 && i != _noNeed3)
			{
//				
				array_sort.add(timelang[i]+"");
			}
		}
		
		int[] timelist = new int[array_sort.size()];
		for (int i = 0; i < array_sort.size(); i++) {
			timelist[i] = Integer.valueOf(array_sort.get(i));
		}
		array_sort.removeAll(array_sort);
		array_sort = null;
		return timelist;
	}
	
	public int[] searchlist(int _noNeed1, int _noNeed2, int _noNeed3, Context _context, String _searchString)
	{
		ArrayList<String> array_sort= new ArrayList<String>();
		for(int i = 0; i < totalCell; i++)
		{
			if(i != _noNeed1 && i != _noNeed2 && i != _noNeed3)
			{
//				if(_searchString.toLowerCase().contains(_context.getResources().getString(getName(i)).toLowerCase()))
//				{
//					array_sort.add(i+"");
//				}
//				Log.d("searchlist", "searchlist:"+_context.getResources().getString(getName(i))+" length:"+_searchString.length());
				String _checkString = _context.getResources().getString(getName(i));
				if(_checkString.length() >= _searchString.length())
				{
					if(_searchString.equalsIgnoreCase((String) _checkString.subSequence(0, _searchString.length())))
					{
						array_sort.add(i+"");
					}
				}
			}
		}
//		if(array_sort.size() > 0)
//		{
			int[] timelist = new int[array_sort.size()];
			for (int i = 0; i < array_sort.size(); i++) {
				timelist[i] = Integer.valueOf(array_sort.get(i));
			}
			array_sort.removeAll(array_sort);
			array_sort = null;
			return timelist;
//		}
//		return getlist(_noNeed1, _noNeed2, _noNeed3);
	}
	
	public String getKey(int _index)
	{
		return timeKey[_index];
	}
	
	public int getName(int _index)
	{
		return timeName[_index];
	}
	
	public String getUTC(int _index)
	{
		String _utc = "(UTC ";
		if(timeUTC[_index] == TIME_ADD)
		{
			_utc += "+";
		}else if(timeUTC[_index] == TIME_MINUS)
		{
			_utc += "-";
		}
		_utc += getNumText(timeUTCH[_index]) + ":" + getNumText(timeUTCM[_index]) + ")";
		return _utc;
	}
	
	public String getNumText(int _num)
	{
		if(_num >= 10)
		{
			return _num+"";
		}else
		{
			return "0"+_num;
		}
	}
}