﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../search.aspx.cs" Inherits="ErnestBorel.search" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="Ernest Borel,swiss watch,automatic watch,quartz-movement watch,chrono watch,luxury watch,prestige watch,wrist watch,neuchatel,switzerland">
<meta name="description" content=" Romance in Heart Since 1856. Ernest Borel –  founded in Neuchatel, Switzerland. Exquisitely elegant, admirably designed automatic and quartz-movement wrist watches for men and women.">
<title>Ernest Borel - 検索する</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/search.js"></script>
<script type="text/javascript">
    result = <%=output %>;
</script>
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
			<div class="main-content" id="searchPage">
				<section>
					<h2>見つかりました</h2>
					<div id="resultList"></div>
					<div id='pagination'></div>
					<div id="noresult" class="hide">0個見つかりました</div>
				</section>
			</div>
			<!-- footer start -->
			<!--#include file ="footer.inc"-->
			<!-- footer end -->
		</div>
	</div> 
</div>

<div class="hide">
	<div id='resultCell' class="resultblock">
        <div class='imgWrapper'>
            <a class="url" href='/wristwatch/col_movement/col_ref/#img' title='col_name'><img src='/images/watches/col_movement/img_t.png'></a>
        </div>
        <div class='contWrapper'>
            <h3><a class='url' href='/wristwatch/col_movement/col_ref/#img'><span class="col_name">col_name</span></a></h3>
            <p class='desc'>idx_watch</p>
        </div>
        <a class='more_btn url' href='/wristwatch/col_movement/col_ref/#img'>More <span class='nav_arrow'>&#9658;</span></a>
    </div>
</div>
</body>
</html>
