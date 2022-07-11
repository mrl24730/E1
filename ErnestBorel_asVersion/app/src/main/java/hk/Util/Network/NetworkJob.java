package hk.Util.Network;

import java.util.ArrayList;

import org.apache.http.NameValuePair;
import org.json.JSONObject;

public class NetworkJob {
	public final static int REQUEST_GET = 0;
	public final static int REQUEST_POST = 1;
	
	public String url;
	public byte[] receiveData;
	public Object tag;
	public int requestMethod;
	public NetworkInterface networkInterface;
	public int responseCode;
	public ArrayList <NameValuePair> postParams;
	public JSONObject postJSONObject;
	//Error
	public int failType;
	
	public NetworkJob(){
		requestMethod = NetworkJob.REQUEST_GET;
		postParams = new ArrayList <NameValuePair>();
		postJSONObject = new JSONObject();
	}
}
