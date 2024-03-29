﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cs_store.aspx.cs" Inherits="ErnestBorel.cs_store" %>
<!DOCTYPE HTML>
<html>
<head>
<base href="<%=domain_cn %>/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content= "瑞士依波路品牌专业维修中心地址:广州市林和西路3-15号耀中广场620室,联系方式:电话：(86-20) 3888 0791" >
<meta name="keywords" content="依波路售后服务" >
<title>依波路手表零售点_依波路官网</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<link rel="stylesheet" type="text/css" href="/css/jquery.jscrollpane.css">
<script type="text/javascript" src="/js/jquery/jquery.jscrollpane.js" ></script>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false&libraries=geometry&key=AIzaSyCyjYF5YhIft1VpFoh3Fpr_MR-FfvMb2Dg"></script>
<script type="text/javascript" src="/js/markerclusterer.fix.js"></script>
<script type="text/javascript" src="https://api.map.baidu.com/api?v=2.0&ak=zHUMEGthxoNMIQupBjAg3GP7"></script>
<script type="text/javascript" src="https://api.map.baidu.com/library/TextIconOverlay/1.2/src/TextIconOverlay_min.js"></script>
<script type="text/javascript" src="https://api.map.baidu.com/library/MarkerClusterer/1.2/src/MarkerClusterer_min.js"></script>
<script type="text/javascript">
    nid = 4;
    subId = 0;
    lang = "sc";
    langObj = {"tel":"电话︰", "fax":"传真︰", "email":"电邮︰", "web":"网址︰", "back":"返回列表"};
    storeType = "pos";
</script>
<script type="text/javascript" src="/js/location_map_baidu.js?v=243346"></script>
<script type="text/javascript" src="/js/location_map_google.js?v=243346"></script>
<script type="text/javascript" src="/js/location.js?v=243346"></script>
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
				<h2>零售点</h2>
				<div>
					<p class="txt">
						寻找我们
						<span class="selector">
							<select id="region" data-placeholder="地區"></select> \
							<select id="country" data-placeholder="國家"></select> \
							<select id="province" data-placeholder="省"></select><span class="province_seperator"> \</span>
							<select id="city" data-placeholder="城市"></select>
						</span>
					</p>
					<div id="map_canvas"></div>
				</div>
				<div style="text-align:center">
					<div class="shopWrapper">
						<h3>零售点</h3>
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
