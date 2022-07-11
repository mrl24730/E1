package hk.Util.Network;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;

import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;

import android.util.Log;

public class NetworkWorker implements Runnable{
	private NetworkJob networkJob;
	private int TIMEOUT = 10000;

	public NetworkWorker(NetworkJob _networkJob){
		networkJob = _networkJob;
	}

	@Override
	public void run() {
		if(!networkJob.url.startsWith("http://")){
			networkJob.url = "http://"+networkJob.url;
		}
		if(networkJob.requestMethod == NetworkJob.REQUEST_POST){
			httpPost();
		}else{
			httpGet();
		}
	}

	private void httpGet(){
		try
		{
			URL url = new URL(networkJob.url);
			HttpURLConnection conn = (HttpURLConnection) url.openConnection();
			conn.setDoInput(true);
			conn.setConnectTimeout(TIMEOUT);
			conn.setRequestMethod("GET");
			conn.setRequestProperty("accept", "*/*");
			//String location = conn.getRequestProperty("location");
			networkJob.responseCode = conn.getResponseCode();
			conn.connect();
			InputStream stream = conn.getInputStream();
			byte[]  data = new byte[102400];
			int length;
			ByteArrayOutputStream buffer = new ByteArrayOutputStream();

	        while ((length = stream.read(data, 0, data.length)) != -1) {
	        	  buffer.write(data, 0, length);
	        }
	        //buffer.flush();


			//int length=stream.read(data);
		//	String str=new String(buffer.toByteArray());  
			networkJob.receiveData = buffer.toByteArray();
			conn.disconnect();
			//System.out.println(str);
			stream.close();
			networkJob.networkInterface.didCompleteNetworkJob(networkJob);
		}
		catch(IOException ee)
		{
			Log.e("ee", "ee:"+ee.getMessage());
			networkJob.networkInterface.didFailNetworkJob(networkJob);
		}

	}

	private void httpPost(){
		try {
//			HttpClient client = new DefaultHttpClient();  
//		    HttpPost post = new HttpPost(networkJob.url);   
//		    post.setHeader("Content-type", "application/json");
//		    post.setHeader("Accept", "application/json");
//		    JSONObject obj = networkJob.postJSONObject;
//		    post.setEntity(new StringEntity(obj.toString(), "UTF-8"));
//		    HttpResponse response = client.execute(post); 
		    
			HttpParams httpParameters;
			httpParameters =  new  BasicHttpParams();
			HttpConnectionParams.setConnectionTimeout(httpParameters, TIMEOUT);
			HttpConnectionParams.setSoTimeout(httpParameters, TIMEOUT);
			HttpResponse response;		
			HttpClient httpclient = new DefaultHttpClient(httpParameters);
			HttpPost httppost = new HttpPost(networkJob.url);
			httppost.setEntity(new UrlEncodedFormEntity(networkJob.postParams));
			response=httpclient.execute(httppost);
			HttpEntity entity = response.getEntity();
			InputStream stream = entity.getContent();
			byte[]  data=new byte[102400];
			//int length=stream.read(data);
			//String str=new String(data,0,length);  
//			networkJob.receiveData = data;
			//System.out.println(str);
			int length;
			ByteArrayOutputStream buffer = new ByteArrayOutputStream();


	        while ((length = stream.read(data, 0, data.length)) != -1) {
	        	  buffer.write(data, 0, length);
	        }
	        networkJob.receiveData = buffer.toByteArray();
	        
			stream.close();
			networkJob.networkInterface.didCompleteNetworkJob(networkJob);
		} catch (Exception e) {
			e.printStackTrace();
			networkJob.networkInterface.didFailNetworkJob(networkJob);
		}
	}
}
