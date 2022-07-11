
function getHashValue(key) {
  var matches = location.hash.match(new RegExp(key+'=([^&]*)'));
  return matches ? matches[1] : null;
}

function getQueryString(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return "";
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

var cur_lang = "en";
var arr_lang = ["tc","sc","en","jp","fr"];

$(document).ready(function(){
	var qs_lang = getQueryString("lang").toLowerCase();
	var isLangValid = $.inArray(qs_lang, arr_lang) > -1;
	if(isLangValid){cur_lang = qs_lang;}
})