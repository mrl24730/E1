package hk.SharedPreferencesSetting;

public class LanguageHandler {
	public String TXT_CONNECT_ZH = "請檢查網絡連線";
	public String TXT_CONNECT_EN = "Please check connection";
	
	private static LanguageHandler instance = null;
	UserProfile userProfile;
	public static LanguageHandler getInstance(){
		if( instance == null){
			instance = new LanguageHandler();
		}
		return instance;
	}
	
	public LanguageHandler(){
		userProfile = UserProfile.getInstance();
	}
	
	public String connectFail()
	{
		/*if(userProfile.language.equalsIgnoreCase(userProfile.LANGUAGE_ZH))
		{
			return TXT_CONNECT_ZH;
		}else if(userProfile.language.equalsIgnoreCase(userProfile.LANGUAGE_ENG))
		{
			return TXT_CONNECT_EN;
		}*/
		return TXT_CONNECT_ZH;
	}
}
