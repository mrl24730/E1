//Defines the top level Class
function Class(){}Class.prototype.construct=function(){};Class.extend=function(e){var t=function(){if(arguments[0]!==Class){this.construct.apply(this,arguments)}};var n=new this(Class);var r=this.prototype;for(var i in e){var s=e[i];if(s instanceof Function)s.$=r;n[i]=s}t.prototype=n;t.extend=this.extend;return t}

var shopSelected = 0;
var apiPos, apiCs;
var style = [{
    url: '/images/POS/map_group.png',
    height: 50,
    width: 35,
    anchor: [8, 70],
    textColor: '#FFF',
    textSize: 16
  }, {
    url: '/images/POS/map_group.png',
    height: 50,
    width: 35,
    anchor: [10, 68],
    textColor: '#FFF',
    textSize: 14
  }, {
    url: '/images/POS/map_group.png',
    height: 50,
    width: 35,
    anchor: [11, 65],
    textColor: '#FFF',
    textSize: 12
  }];


var cur_region = 1;
var cur_country = 1;
var cur_province = 0;
var cur_city = (storeType=="pos")?0:0;
var isDropDownChange = false;
var isGoogle = true;
var locationData;
var shopData;


var ebMapClass = Class.extend({
	mapObj : null,
	data : null,
	d_lat : 0,
	d_lng : 0,
	infoHTML: "<div class='gbubble'><p class='name'>{shop}</p><p class='address'>{address}</p><p><span class='tel'>{tel}</span><span class='fax'>{fax}</span><span class='email'>{email}</span><span class='web'>{web}</span></p><p class='btnBack'><a href='javascript:void(0);' onclick='infoWinBackClicked({idx_shop})'>{back}</a></p></div>",
	construct   : function(){},
	initialize	: function( lat,lng){
		
		this.d_lat = lat;
		this.d_lng = lng;

		var forcebaidu = this.getUrlVars("baidu");
		forcebaidu = (forcebaidu == undefined) ? false:true;

		//Google map detection

		if(typeof(google) == "object" && !forcebaidu){
			this.mapObj = new gMapClass();
		}else{
			this.mapObj = new bMapClass();
			isGoogle = false;
		}
		
		this.mapObj.initialize(this);
	},

	// Read a page's GET URL variables and return them as an associative array.
	getUrlVars: function(query)
	{
	    var vars = [], hash;
	    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
	    for(var i = 0; i < hashes.length; i++)
	    {
	        hash = hashes[i].split('=');
	        vars.push(hash[0]);
	        vars[hash[0]] = hash[1];
	    }
	    return vars[query];
	},

	loadData:function(_data){
		this.data = _data;

		$("#shopList").html("");
		for(var i = 0; i < _data.length; i++){
			var id = _data[i].idx_shop;
			var is_pos = _data[i].is_pos;
			var is_aftersales = _data[i].is_aftersales;
			var tel = (_data[i].shop_tel.trim() != "")? langObj.tel + _data[i].shop_tel + "<br>" : "";
			var fax = (_data[i].shop_fax.trim() != "")? langObj.fax + _data[i].shop_fax + "<br>" : "";
			var email = (_data[i].shop_email.trim() != "")? langObj.email + _data[i].shop_email + "<br>" : "";
			var web = (_data[i].shop_web.trim() != "")? langObj.web + _data[i].shop_web + "<br>" : "";

		    var shop = $("#shopTemplate").clone(true, true);
		    //if(shopSelected == i) shop.addClass("selected");
		    shop.addClass("s"+i+" d"+id).removeAttr("id");
		    shop.attr("id", "shop_"+id);
		    shop.attr({"data-id":i, "data-city":_data[i].idx_city});
		    shop.find(".shop").html(_data[i].shop_name);
		    shop.find(".address").html(_data[i].shop_address);
		    shop.find(".tel").html(tel);
		    shop.find(".fax").html(fax);
		    shop.find(".email").html(email);
		    shop.find(".web").html(web);
		    $("#shopList").append(shop);
		}
	},

	showShopList : function(){
		var that = this;
		var idx_city = "";

		if(locationData.list[cur_region].list[cur_country].id == "CN"){
			idx_city = locationData.list[cur_region].list[cur_country].list[cur_province].list[cur_city].id;
		}else{
			idx_city = locationData.list[cur_region].list[cur_country].list[cur_city].id;
		}

		$.ajax({
			type: "POST",
			url: "/api/getLocationFull.ashx",
			data: { "lang": lang, "id": idx_city, "type":storeType},
			dataType: "json"
		}).done(function(evt) {
			shopData = evt.data;
			that.loadData(evt.data);
			that.mapObj.loadData(evt.data);
		});
	},

	getShopInfo : function(idx){
		shopSelected = idx;
		var name = this.data[idx].shop_name.trim();
		var address = this.data[idx].shop_address.trim();
		var tel = (this.data[idx].shop_tel.trim() != "")? langObj.tel + this.data[idx].shop_tel + "<br>" : "";
		var fax = (this.data[idx].shop_fax.trim() != "")? langObj.fax + this.data[idx].shop_fax + "<br>" : "";
		var email = (this.data[idx].shop_email.trim() != "")? langObj.email + this.data[idx].shop_email + "<br>" : "";
		var web = (this.data[idx].shop_web.trim() != "")? langObj.web + this.data[idx].shop_web + "<br>" : "";
		var idx_shop = this.data[idx].idx_shop;

		var shopContent = this.infoHTML;
		shopContent = shopContent.replace("{shop}", name);
		shopContent = shopContent.replace("{address}", address);
		shopContent = shopContent.replace("{tel}", tel );
		shopContent = shopContent.replace("{fax}", fax);
		shopContent = shopContent.replace("{email}", email);
		shopContent = shopContent.replace("{web}", web);
		shopContent = shopContent.replace("{back}", langObj.back);
		shopContent = shopContent.replace("{idx_shop}", idx_shop);
		
		return shopContent;
	},

	showInfoWin:function(ref){
		var idx = (ref.idx!=undefined)? ref.idx : $(ref).attr("data-id");
		this.mapObj.showInfoWin(this.getShopInfo(idx),ref);
		var idx_shop = this.data[idx].idx_shop;
		$(".shopCell").removeClass("selected");
		$("#shop_"+idx_shop).addClass("selected");
	},

	panTo:function(lng, lat, zoom){
		this.mapObj.panTo(lng,lat,zoom);
	}
});


var pageObj = {

	ebMapClass : null,
	initialize:function(_ebMCls){

		var that = this;

		this.ebMapClass = _ebMCls;
		
		//generate region / country / city selection
		this.loadSelection("region",$("#region"));
		this.loadSelection("country",$("#country"));
		this.loadSelection("city",$("#city"));
		$("#province").hide();
		$(".province_seperator").hide();

		//Default select Switzerland
		$("#region").select2("val", cur_region); 
		$("#country").select2("val", cur_country); 
		$("#city").select2("val", cur_city);
		
		this.ebMapClass.showShopList();

		//page element event listener
		$(".shopCell").click(function(){
			$(".shopCell").removeClass("selected");
			$(this).addClass("selected")
			var id = $(this).attr("data-id");
			that.ebMapClass.panTo(shopData[id].shop_lng ,shopData[id].shop_lat,18);
			that.ebMapClass.showInfoWin($(this));
			scrollToElm($("#csPage"), 500);
		});

		$("#region").change(function(){
			var p = $(this).val();
			cur_region = p;
			pageObj.loadSelection("country",$("#country"));
			$("#country").change();
		});

		$("#country").change(function(){
			var p = $(this).val();
			cur_country = p;
			if(locationData.list[cur_region].list[cur_country].id == "CN"){
				$("#province").show();
				$(".province_seperator").show();
				pageObj.loadSelection("province",$("#province"));
				$("#province").change();
			}else{
				$("#province").hide();
				$(".province_seperator").hide();
				$("#s2id_province").hide();
				pageObj.loadSelection("city",$("#city"));
				$("#city").change();
			}
		});

		$("#province").change(function(){
			var p = $(this).val();
			cur_province = p;
			pageObj.loadSelection("city",$("#city"));
			$("#city").change();
		});
		
		$("#city").change(function(){
			var p = $(this).val();
			cur_city = p;
			isDropDownChange = true;
			var lng = $(this).find(".c"+p).attr("data-lng");
			var lat = $(this).find(".c"+p).attr("data-lat");
			var zoom = $(this).find(".c"+p).attr("data-zoom");
			zoom = parseInt(zoom,10);
			that.ebMapClass.panTo(lng, lat, zoom);
			that.ebMapClass.showShopList();
		});

	},

	loadSelection: function(type,target){
		var data;
		target.html("");
		
		if(type == "region"){
			data = locationData.list;
		}else if(type == "country"){
			data = locationData.list[cur_region].list;
		}else if(type == "province"){
			data = locationData.list[cur_region].list[cur_country].list;
		}else if(type == "city" && locationData.list[cur_region].list[cur_country].id == "CN"){
			data = locationData.list[cur_region].list[cur_country].list[cur_province].list;
		}else{
			data = locationData.list[cur_region].list[cur_country].list;
		}

		for(var i = 0; i< data.length; i++){
			
			//selected = (selected == "")? data[i].id : selected;
    		var opt = $("<option/>");
    		var zoom = (isGoogle) ? data[i].zoom : data[i].zoom+data[i].zoom_baidu;
    		opt.val(i).text(data[i].name).addClass("c"+i);

    		if(type == "city"){
    			opt.attr({"data-lng":data[i].lng, "data-lat":data[i].lat, "data-zoom":zoom});
    		}

    		target.append(opt);
		}

		$(target).select2();

	}
}



$(document).ready(function(){
	
	//load google / baidu map 
	ebMap = new ebMapClass();

	var google = (isGoogle)?1:0;

	$.ajax({
		type: "POST",
		url: "/api/getCityList.ashx",
		data: { "lang": lang, "google": google, "type":storeType},
		dataType: "json"
	}).done(function(evt) {

		locationData = evt;
		//locationData = evt.data;
		//region = evt.region;
		//country = evt.country;
		//city = evt.city;

		var initLat = locationData.list[cur_region].list[cur_country].list[cur_city].lat;
		var initLng = locationData.list[cur_region].list[cur_country].list[cur_city].lng;
		ebMap.initialize(initLat, initLng);
		pageObj.initialize(ebMap);
	});
});

/*
jQuery.fn.scrollTo = function(elem, speed) { 
    $(this).animate({
        scrollTop:  $(this).scrollTop() - $(this).offset().top + $(elem).offset().top 
    }, speed == undefined ? 1000 : speed); 
    return this; 
};
*/



function scrollToElm(elm, time, offset) {
    //var x = $(elm).offset().top - 100; // 100 provides buffer in viewport
    var x = $(elm).offset().top;
    if(time == undefined){
        time = 500;
    }

    if(offset != undefined){
        x += offset;
    }
    
   $('html,body').animate({scrollTop: x}, time);
}


function scrollToX(x, time) {
   if(time == undefined)
        time = 500;
   $('html,body').animate({scrollTop: x}, time);
}



function infoWinBackClicked(elm){
	scrollToElm("#shop_"+elm, 500, -50);
}