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


var cur_region = "";
var cur_country = "";
var cur_city = "";
var isDropDownChange = false;
var isGoogle = true;

var ebMapClass = Class.extend({
	mapObj : null,
	data : null,
	d_lat : 0,
	d_lng : 0,
	infoHTML: "<div class='gbubble'><p class='name'>{shop}</p><p class='address'>{address}</p><p><span class='tel'>{tel}</span><span class='fax'>{fax}</span><span class='email'>{email}</span><span class='web'>{web}</span></div>",
	construct   : function(){},
	initialize	: function(_data,lat,lng){
		this.data = _data;
		
		this.d_lat = lat;
		this.d_lng = lng;

		//Google map detection
		if(typeof(google) == "object"){
			this.mapObj = new gMapClass();
		}else{
			this.mapObj = new bMapClass();
			isGoogle = false;
		}
		
		this.mapObj.initialize(this,_data);


		for(var i = 0; i < _data.length; i++){
			var id = _data[i].idx_shop
			var is_pos = _data[i].is_pos;
			var is_aftersales = _data[i].is_aftersales;
			var tel = (_data[i].shop_tel.trim() != "")? langObj.tel + _data[i].shop_tel + "<br>" : "";
			var fax = (_data[i].shop_fax.trim() != "")? langObj.fax + _data[i].shop_fax + "<br>" : "";
			var email = (_data[i].shop_email.trim() != "")? langObj.email + _data[i].shop_email + "<br>" : "";
			var web = (_data[i].shop_web.trim() != "")? langObj.web + _data[i].shop_web + "<br>" : "";

		    var shop = $("#shopTemplate").clone(true, true);
		    if(shopSelected == i) shop.addClass("selected");
		    shop.addClass("s"+i+" d"+id).removeAttr("id");
		    shop.attr({"data-id":i, "data-city":_data[i].idx_city});
		    shop.find(".shop").html(_data[i].shop_name);
		    shop.find(".address").html(_data[i].shop_address);
		    shop.find(".tel").html(tel);
		    shop.find(".fax").html(fax);
		    shop.find(".email").html(email);
		    shop.find(".web").html(web);

		    if(is_aftersales == "1"){
		    	$("#csList").append(shop);
		    }

		    if(is_pos == "1"){
		    	var shopShadow = shop.clone(true,true);
		    	$("#posList").append(shopShadow);
		    }
		}

	},

	getShopInfo : function(idx){
		shopSelected = idx;
		
		var shopType = this.data[idx].shop_type;
		var name = this.data[idx].shop_name.trim();
		var address = this.data[idx].shop_address.trim();
		var tel = (this.data[idx].shop_tel.trim() != "")? langObj.tel + this.data[idx].shop_tel + "<br>" : "";
		var fax = (this.data[idx].shop_fax.trim() != "")? langObj.fax + this.data[idx].shop_fax + "<br>" : "";
		var email = (this.data[idx].shop_email.trim() != "")? langObj.email + this.data[idx].shop_email + "<br>" : "";
		var web = (this.data[idx].shop_web.trim() != "")? langObj.web + this.data[idx].shop_web + "<br>" : "";
		
		var shopContent = this.infoHTML;
		shopContent = shopContent.replace("{shop}", name);
		shopContent = shopContent.replace("{address}", address);
		shopContent = shopContent.replace("{tel}", tel );
		shopContent = shopContent.replace("{fax}", fax);
		shopContent = shopContent.replace("{email}", email);
		shopContent = shopContent.replace("{web}", web);

		return shopContent;
	},

	showInfoWin:function(ref){

		var idx = (ref.idx)? ref.idx : $(ref).attr("data-id");
		this.mapObj.showInfoWin(this.getShopInfo(idx),ref);
	},

	panTo:function(lng, lat, zoom){
		this.mapObj.panTo(lng,lat,zoom);
	}
});


//Google Map Class
var gMapClass = Class.extend({
	infoWin : null,
	data : null,
	ebMapClass : null,
	markers : new Array(),
	map : null,

	construct   : function(){},
	initialize	: function(_ebMCls,_data){
		
		this.data = _data;
		this.ebMapClass = _ebMCls;
		this.infoWin =  new google.maps.InfoWindow();

		var mapOptions = {
			center: new google.maps.LatLng(_ebMCls.d_lat, _ebMCls.d_lng),
		 	zoom: 12,
			mapTypeId: google.maps.MapTypeId.ROADMAP,
			navigationControlOptions : {style:google.maps.NavigationControlStyle.ZOOM_PAN},
		};
		this.map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);


		this.setMarkers();

		//init marker cluster for grouping
		var markerCluster = new MarkerClusterer(this.map, this.markers, {styles:style, maxZoom:18});//16

		
		var that = this;

		//map event listener
		google.maps.event.addListener(this.map, 'idle', function(){
		    // do something only the first time the map is loaded
		    that.showShopList();
		});

	},

	setMarkers : function(){
		var that = this;

		for(var i = 0; i < this.data.length; i++){
			var shop = this.data[i];
			
			var marker = new google.maps.Marker({
			  //position: locationArray[i],
			  position: new google.maps.LatLng(shop.shop_lat, shop.shop_lng),
			  map: that.map,
			  title: shop.shop_name,
			  icon: new google.maps.MarkerImage ("/images/POS/map_pin.png")
			});

			marker.idx = i;
			this.markers.push(marker);

			google.maps.event.addListener(marker, 'click', function(e) {
				that.ebMapClass.showInfoWin(this);
			});
		}
	},
	showInfoWin : function(_html,ref){
		ref = (ref.idx) ? ref : this.markers[ref.attr("data-id")];
		var idx = ref.idx;
		this.infoWin.setContent(_html);
		this.infoWin.setZIndex(1);
		this.infoWin.open(this.map,this.markers[idx]);
		//this.infoWin.setPosition(this.markers[idx].getPosition());
		//this.infoWin.setOptions({pixelOffset:{width:0,height:-50}});
		//this.map.setCenter(this.markers[idx].getPosition());

		//scroll to and highlight selected shop
		//if(this.data[idx].shop_type == 0 || this.data[idx].shop_type == 2){
		if(this.data[idx].is_aftersales == "1"){
			apiCs.scrollToElement($(".s"+shopSelected));
		}

		//if(this.data[idx].shop_type > 0){
		if(this.data[idx].is_pos == "1"){
			apiPos.scrollToElement($(".s"+shopSelected));
		}
		$(".shopCell").removeClass("selected");
		$(".s"+shopSelected).addClass("selected");
	},

	showShopList:function(){

		var that = this;
		//show shop list according to current map boundary
		var isSelectedInBound = false;
		var currentBounds = this.map.getBounds() // get bounds of the map object's viewport

		var centerPt = this.map.getCenter();
		var minID = null;
		var minDist = 999999;
		var haveCS = false;
		$(".shopCell").hide();
		
		
		console.log("lng: " + centerPt.lng());
		console.log("lat: " + centerPt.lat());
		console.log("zoom: " + this.map.getZoom());
		
		
		for(var i = 0; i < that.data.length; i++){
			//load data

			var point = new google.maps.LatLng(that.data[i].shop_lat, that.data[i].shop_lng);
			var shop_city = $(".s"+i).attr("data-city");

			if(currentBounds.contains(point) || (shop_city == cur_city && isDropDownChange)){
			    // your location is inside your map object's viewport
				$(".s"+i).show();
				//if(that.data[i].shop_type == 0 ||that.data[i].shop_type == 2) haveCS = true;
				if (that.data[i].is_aftersales == "1") haveCS = true;
			}

			//if((that.data[i].shop_type == 0 || that.data[i].shop_type == 2) && !haveCS){
			if((that.data[i].is_aftersales == "1") && !haveCS){
				var csPt = new google.maps.LatLng(that.data[i].shop_lat, that.data[i].shop_lng);
				var dist = google.maps.geometry.spherical.computeDistanceBetween (csPt, centerPt);

				if( (minID== null) || dist < minDist){
					minID = that.data[i].idx_shop;
					minDist = dist;
				}
			}

		}//end for loop
		isDropDownChange = false;

		//If no CS on screen
		if(!haveCS){

			$("#csList .shopCell").hide();

			var defaultID = minID
			var idx_country = cur_country;

			for(var j = 0; j< country.length ; j++){
				if(country[j].idx == idx_country && country[j].default_cs != 0){
					defaultID = country[j].default_cs;
					break;
				}
			}

			$("#csList .d"+defaultID).show();
			//show nearest / default CS store in here
		}

		$("#posList").jScrollPane();
		apiPos = $("#posList").data('jsp');
		$("#csList").jScrollPane();
		apiCs = $("#csList").data('jsp');
		
	},

	panTo:function(lng, lat, zoom){
		var point = new google.maps.LatLng(lat,lng)
		this.map.setZoom(zoom);
		this.map.setCenter(point);
	}
});

//Baidu Map Class
var bMapClass = Class.extend({
	infoWin : null,
	data : null,
	ebMapClass : null,
	markers : new Array(),
	map : null,

	construct   : function(){},
	initialize	: function(_ebMCls,_data){
		
		this.data = _data;
		this.ebMapClass = _ebMCls;
		this.infoWin =  new BMap.InfoWindow();


		this.map = new BMap.Map("map_canvas");    // 创建Map实例
		this.map.centerAndZoom(new BMap.Point(_ebMCls.d_lng,_ebMCls.d_lat), 7);  // 初始化地图,设置中心点坐标和地图级别
		this.map.addControl(new BMap.MapTypeControl());   //添加地图类型控件
		this.map.enableScrollWheelZoom();

		this.recoords(0,Math.ceil(_data.length / 100));
		//this.recoordsFinish();
		
		var that = this;
		
		//map event listener
		this.map.addEventListener('moveend', function(){
		    // do something only the first time the map is loaded
		    that.showShopList();
		});
		

	},

	recoords : function(idx_start, ttl_call){
		//console.log("recoords",idx_start,ttl_call);
		var that = this;

		var pts = [];
		for(var i= idx_start * 100; i < (idx_start+1) * 100; i++ ){
			//no more
			if(i == this.data.length -1 ) break;
			
			pts.push(this.data[i].shop_lng +"," + this.data[i].shop_lat);
		}
		$.ajax({
			url:"http://api.map.baidu.com/geoconv/v1/?ak=zHUMEGthxoNMIQupBjAg3GP7&from=1&to=5&coords="	+ pts.join(";"),
			 jsonp: "callback",
    		// tell jQuery we're expecting JSONP
    		dataType: "jsonp",
    		success:function(response){
    			
    			for(var i= idx_start * 100,k=0; k < response.result.length; k++,i++ ){
    				that.data[i].shop_lng = response.result[k].x;
    				that.data[i].shop_lat = response.result[k].y;
    			}

    			idx_start++;
    			if(ttl_call-1 < idx_start ){
    				//Finish
    				that.recoordsFinish();
    			}else{
    				that.recoords(idx_start,ttl_call);
    			}
    		}
		})

	},

	recoordsFinish: function(){

		this.setMarkers();


		var bdStyle = $.extend([],style);
		for(var i = 0; i < bdStyle.length; i++){
			bdStyle[i].textColor = "white";
			bdStyle[i].offset = new BMap.Size(0,5);
			bdStyle[i].size = new BMap.Size(35, 50);
			// bdStyle[i].textSize = 18;
		}
		
		//init marker cluster for grouping
		var markerClusterer = new BMapLib.MarkerClusterer(this.map, {
			markers:this.markers,
			maxZoom:16,
			styles:bdStyle
		});

		this.showShopList();
		
	},

	setMarkers : function(){
		var that = this;
		for(var i = 0; i < this.data.length; i++){

			var shop = this.data[i];
			var myIcon = new BMap.Icon("/images/POS/map_pin.png", new BMap.Size(70, 60), {  
				imageSize: new BMap.Size(35, 50), // 指定定位位置   
				imageOffset: new BMap.Size(0, 0)   // 设置图片偏移    
			 });

			 var marker = new BMap.Marker(new BMap.Point(shop.shop_lng,shop.shop_lat),
			 {
			 	icon: myIcon
			 });

			marker.idx = i;
			//this.map.addOverlay(marker);
			
			marker.addEventListener("click",function(e){
				that.ebMapClass.showInfoWin(e.currentTarget);
			});

			this.markers.push(marker);
		}
	},
	showInfoWin : function(_html,ref){

		ref = (ref.idx) ? ref : this.markers[ref.attr("data-id")];
		var idx = ref.idx;

		//console.log(_html,ref);
		this.infoWindow = new BMap.InfoWindow(_html,{
			offset : new BMap.Size(-27, -37),
			enableMessage : false
		});

		this.map.openInfoWindow(this.infoWindow,ref.getPosition());

		//scroll to and highlight selected shop
		
		//if(this.data[idx].shop_type == 0 || this.data[idx].shop_type == 2){
		if(this.data[idx].is_aftersales == "1"){
			apiCs.scrollToElement($(".s"+shopSelected));
		}

		//if(this.data[idx].shop_type > 2){
		if(this.data[idx].is_pos == "1"){
			apiPos.scrollToElement($(".s"+shopSelected));
		}
		$(".shopCell").removeClass("selected");
		$(".s"+shopSelected).addClass("selected");
	},

	showShopList:function(){

		var that = this;
		//show shop list according to current map boundary
		var isSelectedInBound = false;
		var currentBounds = this.map.getBounds() // get bounds of the map object's viewport

		var centerPt = this.map.getCenter();
		var minID,minDist;
		var haveCS;


		//console.log("lng: " + centerPt.lng());
		//console.log("lat: " + centerPt.lat());
		console.log("zoom: " + this.map.getZoom());

		$(".shopCell").hide();
		for(var i = 0; i < that.data.length; i++){
			//load data

			var point = new BMap.Point(that.data[i].shop_lng,that.data[i].shop_lat)
			var shop_city = $(".s"+i).attr("data-city");

			if(currentBounds.containsPoint(point) || (shop_city == cur_city && isDropDownChange)){
			    // your location is inside your map object's viewport
				$(".s"+i).show();

				//if(that.data[i].shop_type == 0 || that.data[i].shop_type == 2) haveCS = true;
				if (that.data[i].is_aftersales == "1") haveCS = true;
			}

			if((that.data[i].is_aftersales == "1") && !haveCS){
				var csPt = new BMap.Point(that.data[i].shop_lng, that.data[i].shop_lat);
				var dist = this.map.getDistance(csPt, centerPt);

				if(!minID || dist < minDist){
					minID = that.data[i].idx_shop;
					minDist = dist;
				}
			}

		}//end for loop

		//If no CS on screen
		if(!haveCS){
			$("#csList .shopCell").hide();

			var defaultID = minID
			var idx_country = cur_country;

			for(var j = 0; j< country.length ; j++){
				if(country[j].idx == idx_country && country[j].default_cs != 0){
					defaultID = country[j].default_cs;
					break;
				}
			}

			$("#csList .d"+defaultID).show();
			//show nearest / default CS store in here
		}

		$("#posList").jScrollPane();
		apiPos = $("#posList").data('jsp');
		$("#csList").jScrollPane();
		apiCs = $("#csList").data('jsp');
	},

	panTo:function(lng, lat, zoom){
		var point = new BMap.Point(lng,lat);
		this.map.setZoom(zoom);
		this.map.setCenter(point);
	}

});


var pageObj = {

	ebMapClass : null,
	initialize:function(_ebMCls){

		this.ebMapClass = _ebMCls;
		
		//generate region / country / city selection
		pageObj.loadSelection(region,$("#region"), "");
		pageObj.loadSelection(country,$("#country"),"ER");
		pageObj.loadSelection(city,$("#city"),"CH");

		//Default select Switzerland
		$("#region").select2("val", "ER"); 
		$("#country").select2("val", "CH"); 
		$("#city").select2("val", "geneva"); 
		

		var that = this;
		//page element event listener
		$(".shopCell").click(function(){
			$(".shopCell").removeClass("selected");
			var id = $(this).attr("data-id");
			//console.log("id",this);
			//that.ebMapClass.panTo(locationData[id].shop_lng ,locationData[id].shop_lat,18);
			that.ebMapClass.showInfoWin($(this));

		});

		$("#region").change(function(){
			var p = $(this).val();
			cur_region = p;
			pageObj.loadSelection(country,$("#country"),p);
			$("#country").change();
		});

		$("#country").change(function(){
			var p = $(this).val();
			cur_country = p;
			pageObj.loadSelection(city,$("#city"),p);
			$("#city").change();
		});
		
		$("#city").change(function(){
			var p = $(this).val();
			cur_city = p;
			isDropDownChange = true;
			var lng = $(this).find("."+p).attr("data-lng");
			var lat = $(this).find("."+p).attr("data-lat");
			var zoom = $(this).find("."+p).attr("data-zoom");
			zoom = parseInt(zoom,10);
			that.ebMapClass.panTo(lng, lat, zoom);
		});
	},

	loadSelection: function(data,target,parent){
		selected = "";
		parent = (parent == undefined)?"":parent;
		target.html("");
		for(var i = 0; i< data.length; i++){
			if( parent == data[i].idx_parent ){
				selected = (selected == "")? data[i].idx : selected;
	    		var opt = $("<option/>");
	    		var zoom = (isGoogle) ? data[i].zoom : data[i].zoom+data[i].zoom_baidu;
	    		opt.val(data[i].idx).text(data[i].name).addClass(data[i].idx);
	    		opt.attr({"data-lng":data[i].lng, "data-lat":data[i].lat, "data-zoom":zoom});
	    		target.append(opt);
			}
		}
		$(target).select2();

		if(target.attr("id") == "region"){
			pageObj.loadSelection(city,$("#city"),selected,"city");
		}
	}
}



$(document).ready(function(){
	
	//load google / baidu map 
	ebMap = new ebMapClass();

	var google = (isGoogle)?1:0;
	$.ajax({
		type: "POST",
		url: "/api/getLocationFull.ashx",
		data: { "lang": lang, "google": google},
		dataType: "json"
	}).done(function(evt) {
		locationData = evt.data;
		region = evt.region;
		country = evt.country;
		city = evt.city;
		ebMap.initialize(locationData,46.2024573,6.1440117);
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
