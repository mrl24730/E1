package hk.SharedPreferencesSetting;

import java.io.File;
import java.io.FileOutputStream;

import android.content.Context;
import android.graphics.Bitmap;
import android.os.Environment;

public class SaveTextHandler {

	private static SaveTextHandler instance = null;
	
	public static SaveTextHandler getInstance(){
		if( instance == null){
			instance = new SaveTextHandler();
		}
		return instance;
	}
	
	public File getSaveFname(String _fname)
    {
    	String root = Environment.getExternalStorageDirectory().toString();
//    	String root = context.getPackageResourcePath();
//    	String root = context.getFilesDir().getAbsolutePath();
    	File myDir = new File(root + "/ErnestBorel/html");
    	myDir.mkdirs();
    	File file = new File (myDir, _fname);
    	return file;
    }
	
	public File getSaveAlbume(String _fname)
    {
    	String root = Environment.getExternalStorageDirectory().toString();
//    	String root = context.getPackageResourcePath();
//    	String root = context.getFilesDir().getAbsolutePath();
    	File myDir = new File(root + "/ErnestBorel_TryMe");
    	myDir.mkdirs();
    	File file = new File (myDir, _fname);
    	return file;
    }
	
	public void saveString(String _string, String _fname)
	{
		File file = getSaveFname(_fname);
    	if (file.exists ()) file.delete (); 
    	try
    	{
    		FileOutputStream out = new FileOutputStream(file);
    		out.write(_string.getBytes());
    		out.flush();
    		out.close();
    	} catch (Exception e) {
    		e.printStackTrace();
    	}
	}
	
	public String getRoot()
	{
    	String root = Environment.getExternalStorageDirectory().toString();
    	return root + "/ErnestBorel/html";
	}
}
