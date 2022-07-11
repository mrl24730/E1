//Defines the top level Class
function Class(){}Class.prototype.construct=function(){};Class.extend=function(e){var t=function(){if(arguments[0]!==Class){this.construct.apply(this,arguments)}};var n=new this(Class);var r=this.prototype;for(var i in e){var s=e[i];if(s instanceof Function)s.$=r;n[i]=s}t.prototype=n;t.extend=this.extend;return t}

//Baidu Map Class
var bMapClass = Class.extend({
	infoWin : null,
	data : null,
	ebMapClass : null,
	markers : new Array(),
	map : null,
	cur_city: "",

	construct   : function(){},
	initialize	: function(_ebMCls){
		
		//this.data = _data;
		this.ebMapClass = _ebMCls;
		this.infoWin =  new BMap.InfoWindow();


		this.map = new BMap.Map("map_canvas");    // 创建Map实例
		this.map.centerAndZoom(new BMap.Point(_ebMCls.d_lng,_ebMCls.d_lat), 7);  // 初始化地图,设置中心点坐标和地图级别
		this.map.addControl(new BMap.MapTypeControl());   //添加地图类型控件
		this.map.enableScrollWheelZoom();

		//this.recoords(0,Math.ceil(_data.length / 100));
		//this.recoordsFinish();
		
		var that = this;
		
		//map event listener
		this.map.addEventListener('moveend', function(){
		    // do something only the first time the map is loaded
		    that.showShopList();
		});
	},

	loadData: function(_data){
		this.data = _data;
		this.recoords(0,Math.ceil(_data.length / 100));
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


		/*
		var centerPt = this.map.getCenter();
		var minID,minDist;
		var haveCS;

		//show lng lat
		//console.log("lng: " + centerPt.lng());
		//console.log("lat: " + centerPt.lat());
		//console.log("zoom: " + this.map.getZoom());

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

		*/
	},

	panTo:function(lng, lat, zoom){
		var point = new BMap.Point(lng,lat);
		this.map.setZoom(zoom);
		this.map.setCenter(point);
	}

});
