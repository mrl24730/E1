package com.Util.RealView;

import android.view.View;

public interface RealViewPagerControllerListener {

	void showCurrentPage(int _index, View _currentView);
	void releasePage(View _page, int _index);
	void hidePage(View _page, int _index);
	void loadPage(View _page, int _index);
	
}
