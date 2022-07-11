<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="location_detail.aspx.cs" Inherits="ErnestBorel.api.location_detail" %>


<!DOCTYPE HTML>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Ernest Borel</title>
<meta name="viewport" content="width=device-width">
<link href="css/style.css?v=2018" rel="stylesheet" type="text/css">
<script src="<%=domain %>/js/jquery/jquery-1.6.4.min.js" type="text/javascript"></script>
<script src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>

<!-- Google Map Script -->
<script type="text/javascript">
    var info;
    var geocoder;
    var map;
    var marker;

    var lat;
    var lng;

    //dummy address
    lat = <%=lat %>;
    lng = <%=lng %>;;

    $(document).ready(function () { 
        initialize();
        codeAddress();
    });

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
        var latlng = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));

        geocoder.geocode({ 'location': latlng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                map.setCenter(results[0].geometry.location);

                var markerSize = new google.maps.Size(70, 62);
                var displaySize = new google.maps.Size(70, 62);
                var o_point = new google.maps.Point(0, 0);
                var a_point2 = new google.maps.Point(22, 60);

                //Specifiy images for pin
                var markerImg = new google.maps.MarkerImage("<%=domain %>/images/eb_pin.png", markerSize, o_point, a_point2, displaySize);
                marker = new google.maps.Marker({
                    position: results[0].geometry.location,
                    map: map,
                    title: "Ernest Borel",
                    icon: markerImg
                });
            } else {
                alert('Geocode was not successful for the following reason: ' + status);
            }
        });
    }

</script>
<style>

#mainWrapper, #topBar, #shopCity, #map_canvas, #shopInfo{
 	width: 100%;
 	color: #fff;
 }
 #topBar{
 	height: 56px;
 }
 #shopCity{
 	text-align: center;
 	font-weight: bold;
 	font-size: 20px;
 	height: 44px;
 	line-height: 44px;
 	overflow: hidden;
 	background: #000e21;
 }
 #map_canvas{
 	margin: 0 auto;
 	width: 320px;
 	height: 320px;
 }
 #shopInfo{
 	margin: 0 20px;
 	width: 320px;
 }
 #shopInfo p{
 	margin-right: 20px;
 }
 #shopContact{
 	background: url(images/store_addressicon.png);
 	padding-left: 28px;
 	background-repeat: no-repeat;
 }
 #shopName{
 	background: url(images/store_nameicon.png);
 	background-repeat: no-repeat;
 	padding-left: 28px;
 }

 </style>
</head>

<body>
<div id="mainWrapper">
	<!-- <div id="topBar"></div> -->
	<div id="shopCity"><%=title %></div>
	<div id="map_canvas"></div>
	<div id="shopInfo">
		<br>
		<p id="shopName"><span style="font-weight:bold;"><%=shopName %></span>
		<br>
		<%=shopAddress %> </p>
		<p id="shopContact"><%=shopContact %></p>	</div>
</div>
<script type="text/javascript">
    $('#map_canvas').css("width", $(window).width() + "px");
    $('#shopInfo').css("width", ($(window).width() - 20) + "px");
</script>
</body>
</html>




