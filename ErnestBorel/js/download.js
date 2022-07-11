nid = 3; 
subId = 3; 
var android_url = "https://play.google.com/store/apps/details?id=com.ernestborel&feature=search_result#?t=W251bGwsMSwyLDEsImNvbS5lcm5lc3Rib3JlbCJd";

if(typeof(google) != "object"){
	android_url = "/downloads/ErnestBorel.apk";
}

$(document).ready(function(){
	$(".android").attr("href", android_url);	
});