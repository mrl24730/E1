//Defines the top level Class
function Class(){}Class.prototype.construct=function(){};Class.extend=function(e){var t=function(){if(arguments[0]!==Class){this.construct.apply(this,arguments)}};var n=new this(Class);var r=this.prototype;for(var i in e){var s=e[i];if(s instanceof Function)s.$=r;n[i]=s}t.prototype=n;t.extend=this.extend;return t}

//Google Map Class
var gMapClass = Class.extend({
	infoWin : null,
	data : null,
	ebMapClass : null,
	markers : new Array(),
	map : null,
	markerCluster:null,

	construct   : function(){},
	initialize	: function(_ebMCls){
		var that = this
;
		this.ebMapClass = _ebMCls;
		this.infoWin =  new google.maps.InfoWindow();

		var mapOptions = {
			center: new google.maps.LatLng(_ebMCls.d_lat, _ebMCls.d_lng),
		 	zoom: 12,
			mapTypeId: google.maps.MapTypeId.ROADMAP,
			navigationControlOptions : {style:google.maps.NavigationControlStyle.ZOOM_PAN},
		};
		this.map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
		this.map.setOptions({draggable: true, zoomControl: true, scrollwheel: false, disableDoubleClickZoom: true});
		
		//map event listener
		google.maps.event.addListener(this.map, 'idle', function(){
		    // do something only the first time the map is loaded
		    //that.showShopList();
		    that.showCenterPoint();
		});

	},

	loadData: function(_data){
		this.data = _data;
		this.setMarkers();
	},

	setMarkers : function(){
		var that = this;
		removeMarkers();

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

		//init marker cluster for grouping
		this.markerCluster = new MarkerClusterer(this.map, this.markers, {styles:style, maxZoom:18});//16


		function removeMarkers(){

			for(i=0; i<that.markers.length; i++){
			    that.markers[i].setMap(null);
			}

			//console.log(that.markerCluster);
			if(that.markerCluster != null){
				//console.log("clear cluster")
				that.markerCluster.setMap(null);
			}

			that.markers = new Array();
		}
	},

	showInfoWin : function(_html,ref){
		ref = (ref.idx != undefined) ? ref : this.markers[ref.attr("data-id")];
		var idx = ref.idx;
		this.infoWin.setContent(_html);
		this.infoWin.setZIndex(1);
		this.infoWin.open(this.map,this.markers[idx]);
		
		//this.infoWin.setPosition(this.markers[idx].getPosition());
		//this.infoWin.setOptions({pixelOffset:{width:0,height:-50}});
		//this.map.setCenter(this.markers[idx].getPosition());

		//highlight selected shop
		//$(".shopCell").removeClass("selected");
		//$(".s"+shopSelected).addClass("selected");
	},

	showCenterPoint:function(){
		//show lng lat
		var centerPt = this.map.getCenter();
		console.log("---------------" );
		console.log("lng: " + centerPt.lng());
		console.log("lat: " + centerPt.lat());
		console.log("zoom: " + this.map.getZoom());
		
	},

	showShopList:function(){

		var that = this;
		//show shop list according to current map boundary
		var isSelectedInBound = false;
		var currentBounds = this.map.getBounds() // get bounds of the map object's viewport
		
		/*
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

		apiPos = $("#shopList").data('jsp');
		*/
	},

	panTo:function(lng, lat, zoom){
		var point = new google.maps.LatLng(lat,lng)
		this.map.setZoom(zoom);
		this.map.setCenter(point);
	}
});
