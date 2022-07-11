<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../map.aspx.cs" Inherits="ErnestBorel.map" %>

<!DOCTYPE html>
<html lang="tc">
  <head>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
    <meta charset="utf-8">
    <title>DEV MAP</title>
    <style>
	html, body {
		height: 100%;
		margin: 0;
		padding: 0;
		background: transparent !important;
	}
	#content {width: 920px;overflow: hidden;}
	#map_canvas, #info {background:#133563; display: inline-block; *display:inline;*zoom:1; vertical-align:top;}
  #info{width:397px; height:340px; padding:30px;color:#af845d;}
  #name {font-size:25px;margin:10px 0;}
	#contact, #address{color:#fff;font-family: arial,​helvetica,​clean,​sans-serif;font-size: 13px;}
	
	#contact h5, #address h5{margin:0; font-size: 14px;}
	.map_link {display: none;}
	</style>
	<script src="<%=domain_ch %>/js/jquery/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>
    
  </head>
  <body style="background:yellow;">
	<div id="div-preview" onClick="hide();" style="position:absolute; top:0; left:0; width:100%; height:100%; display:none; background:url(<%=domain_ch %>/images/POS/background.png); z-index:99;">
		<img style="margin:20px auto; display:block;" id="pic" />
		<a href="javascript:hide();" style="background:url(<%=domain_ch %>/images/fancybox/fancybox.png) repeat -40px 0; position:absolute; top:10px; display:none; width:30px; height:30px; display;none;"></a>
	</div>
  	<div id="content">
    	<div id="map_canvas" style="height:400px;width:457px;"></div><!--
    	--><div id="info">
    			<div id="name"></div>
    			<div id="address"></div>
    			<div id="contact"></div>
				<div id="hk_images" style="display:none;">
					<a style="margin-right:23px;" href="javascript:largeImg('images/POS/HongKong_Shop_1.jpg');" ><img src="<%=domain_ch %>/images/POS/HongKong_Shop_1_s.jpg" /></a>
					<a href="javascript:largeImg('images/POS/HongKong_Shop_2.jpg');" ><img src="<%=domain_ch %>/images/POS/HongKong_Shop_2_s.jpg" /></a>
				</div>
				<div id="gz_images" style="display:none; position:absolute; bottom:56px;">
					<a style="margin-right:23px;" href="javascript:largeImg('images/POS/GuangZhou_Shop_1.jpg');" ><img src="<%=domain_ch %>/images/POS/GuangZhou_Shop_1_s.jpg" /></a>
					<a href="javascript:largeImg('images/POS/GuangZhou_Shop_2.jpg');" ><img src="<%=domain_ch %>/images/POS/GuangZhou_Shop_2_s.jpg" /></a>
				</div>
    	   </div>
	</div>
  <!-- Google Map Script -->
  <script>
    var info;
    var geocoder;
    var map;
    var marker;
	
	var lat;
	var lng;
	
    function initialize() {
      geocoder = new google.maps.Geocoder();
      var latlng = new google.maps.LatLng(48.8571084, 2.9092244999999366);
      var mapOptions = {
        zoom: 16,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
      }

      map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);

    }
    function codeAddress() {
      info = parent.getMapInfo();
    	var address = info.find(".address").html();

      
      lat = info.find(".lat").html();
      lng = info.find(".lng").html();
      var latlng = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));

      geocoder.geocode( { 'location': latlng}, function(results, status) {
      
	/*
      geocoder.geocode( { 'address': address}, function(results, status) {
	  */
        if (status == google.maps.GeocoderStatus.OK) {
          map.setCenter(results[0].geometry.location);
          
          /* ---- debug only ---- */
          //console.log("Xa: "+results[0].geometry.location.Xa, "  Ya: "+results[0].geometry.location.Ya);
          /* ---- debug only ---- */

          var markerSize = new google.maps.Size(70,60);
  	      var displaySize = new google.maps.Size(70,60);
  	      var o_point = new google.maps.Point(0,0);
  	      var a_point2 = new google.maps.Point(22,60);
        	//Specifiy images for pin
        	var  markerImg = new google.maps.MarkerImage("<%=domain_ch %>/images/eb_pin.png",markerSize, o_point, a_point2,displaySize);
        	marker = new google.maps.Marker({
        		  position: results[0].geometry.location, 
        		  map: map,
        		  title:"Ernest Borel",
        		  icon:	markerImg
        	});
        } else {
          alert('Geocode was not successful for the following reason: ' + status);
        }
          	
      });
    }
    initialize();
    codeAddress();
    
    var address_details = info.find(".addBox").html();
    document.getElementById("contact").innerHTML = (info.find(".conBox").html() == null) ? "" : info.find(".conBox").html();
    document.getElementById("contact").innerHTML = document.getElementById("contact").innerHTML.replace(/(\r\n|\n|\r)/gm,"<br/>");
    document.getElementById("address").innerHTML = address_details;
    document.getElementById("name").innerHTML	   = info.find("> h4").html();

	//for special pos, show more images
	//console.log(lng, lat);
	if((lng == 114.18532848358154 && lat == 22.28000529160012) || (lat == 22.280002 && lng == 114.1853284))
	{
		document.getElementById("hk_images").style.display = "block";
	}
	if(lng == 113.32683652639389 && lat == 23.1321342528128)
	{
		document.getElementById("gz_images").style.display = "block";
	}
	
	function largeImg(path)
	{
		$("#pic")[0].src = "<?php echo $chDomain ?>/"+path;
		$("#div-preview").fadeIn();
		
		setTimeout(function(){	
			var totalWidth = $("#div-preview").width();
			var picWidth = $("#pic").outerWidth();
			$("#div-preview a").css('right',(((totalWidth - picWidth)/2)-10));
			$("#div-preview a").show();
		},1000);
		
	}
	
	function hide()
	{
		$("#div-preview a").hide();
		$("#div-preview").fadeOut();
	}
  </script>


  </body>
</html>
