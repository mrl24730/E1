﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cs_aftersales.aspx.cs" Inherits="ErnestBorel.cs_aftersales" %>
<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="Ernest Borel,swiss watch,automatic watch,quartz-movement watch,chrono watch,luxury watch,prestige watch,wrist watch,neuchatel,switzerland">
<meta name="description" content=" Romance in Heart Since 1856. Ernest Borel –  founded in Neuchatel, Switzerland. Exquisitely elegant, admirably designed automatic and quartz-movement wrist watches for men and women.">
<title>Ernest Borel - サービスサイト</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<link rel="stylesheet" type="text/css" href="/css/jquery.jscrollpane.css">
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=geometry&key=AIzaSyCyjYF5YhIft1VpFoh3Fpr_MR-FfvMb2Dg"></script>
<script type="text/javascript" src="/js/markerclusterer.fix.js"></script>
<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=zHUMEGthxoNMIQupBjAg3GP7"></script>
<script type="text/javascript" src="http://api.map.baidu.com/library/TextIconOverlay/1.2/src/TextIconOverlay_min.js"></script>
<script type="text/javascript" src="http://api.map.baidu.com/library/MarkerClusterer/1.2/src/MarkerClusterer_min.js"></script>
<script type="text/javascript" src="/js/jquery/jquery.jscrollpane.js" ></script>
<script type="text/javascript">
    nid = 5;
    subId = 0;
    lang = "jp";
    langObj = { "tel": "電話番号:", "fax": "傳真番号:", "email": "メール: ", "web": "ウェブサイト: ", "back":"リストに戻る" };
    storeType = "network";
</script>
<script type="text/javascript" src="/js/location_map_baidu.js?v=39854"></script>
<script type="text/javascript" src="/js/location_map_google.js?v=39854"></script>
<script type="text/javascript" src="/js/location.js?v=39854"></script>
</head>

<body>
<!-- panel start -->
<!--#include file ="panel.inc"-->
<!-- panel end -->
<div id="doc">
	<div id="docShadow">
	<div id="docWrapper">
		<!-- header start -->
        <!--#include file ="header.inc"-->
		<!-- header end -->

		<!-- nav start -->
		<!--#include file ="nav.inc"-->
		<!-- nav end -->
		<div class="main-content" id="csPage">
			<section>
				<h2>サービスサイト</h2>
				<div>
					<p class="txt">
						寻找我们
						<span class="selector">
							<select id="region" data-placeholder="地域"></select> \
							<select id="country" data-placeholder="国"></select> \
							<select id="province" data-placeholder="州"></select><span class="province_seperator"> \</span>
							<select id="city" data-placeholder="都市"></select>
						</span>
					</p>
					<div id="map_canvas"></div>
				</div>
				<div style="text-align:center">
					<div class="shopWrapper">
						<h3>サービスサイト</h3>
						<div id="shopList" class="shopList"></div>
					</div>
				</div>
			</section>
		</div>
		<!-- footer start -->
		<!--#include file ="footer.inc"-->
		<!-- footer end -->
	</div>
	</div> 
</div>

<div class="hide">
	<div id="shopTemplate" class="shopCell">
		<h3 class="shop"></h3>
			<p class="address"></p>
			<p>
				<span class="tel"></span>
				<span class="fax"></span>
				<span class="email"></span>
				<span class="web"></span>
			</p>
		</div>
	</div>
</div>
</body>
</html>