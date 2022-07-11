package com.ernestborel.CollectionObject;


public interface CollectionDataListener {
	void zoomWatch(String _imageUrl);
	void closeZoom();
	void backToTable();
	void tryWatch(String _imageUrl);
	void closeTry();
}
