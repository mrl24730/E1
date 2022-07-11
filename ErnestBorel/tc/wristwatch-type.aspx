<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../wristwatch-type.aspx.cs" Inherits="ErnestBorel.wristwatch_type" %>

<!DOCTYPE HTML>
<html lang="tc">
<head>
<base href="<%=domain_ch %>/tc/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content="瑞士手表品牌—依波路，推出最新高檔手表產品包括自動系列和石英系列，分別為男士、女士分別打造不同品味的高檔手表,情侶表等">
<meta name="keywords" content="<%=obj.metaKeyword %>">
<title>依波路官方網站 - 依波路手表最新產品</title>
<!-- For watch detail -->
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript">
	nid = 1;
	subId = 1;
	var ladyObj = <%=ladyObj %>;
    var coupleObj = <%=coupleObj %>;
    var casualObj = <%=casualObj %>;
</script>
<script type="text/javascript" src="/js/watchtype.js"></script>
</head>
<body>
<!-- panel start -->
<!--#include file ="panel.inc"-->
<!-- panel end -->
<div id="doc" class="watch-collection">
	<div id="docShadow">
	<div id="docWrapper">
		<!-- header start -->
        <!--#include file ="header.inc"-->
		<!-- header end -->

		<!-- nav start -->
		<!--#include file ="nav.inc"-->
		<!-- nav end -->
		<div class="main-content norepeatbg" id="watchTypePage">
			<section>
            	<div class="yui3-g">
	            	<div class="yui3-u-1-3 bannerWrapper">
						<h4>浪漫情侣</h4>
						<a href="wristwatch_collection/couple/"><img src="/images/0_watch_romantic_m.jpg" alt="" width="90%"></a>
                        <br />
                        <p class="desc">
                          ROMANTIC SERIES（浪漫情侶）承載品牌過百年的浪漫優雅，寓意世間愛情都能穿越時空，以對表傳情，方寸說愛。
                        </p>
					</div><!--
					--><div class="yui3-u-1-3 bannerWrapper">
						<h4>優雅淑女</h4>
						<a href="wristwatch_collection/lady/"><img src="/images/0_watch_feminine_m.jpg" alt="" width="90%"></a>
                        <br />
                        <p class="desc">
                          FEMININE SERIES（優雅淑女）意在透過多種唯美耀目的細節設計，把女性端莊高雅、時尚大方、簡樸商務等萬千風韻展現人前。
                        </p>
					</div><!--
					--><div class="yui3-u-1-3 bannerWrapper">
						<h4>瀟灑雅仕</h4>
						<a href="wristwatch_collection/casual/"><img src="/images/0_watch_causal_m.jpg" alt="" width="90%"></a>
                        <br />
                        <p class="desc">
                          CASUAL SERIES（瀟灑雅仕）意在帶給都市人一枚任何場合都適合配戴、風格百搭而時尚舒適的腕表，散發男士瀟灑不羈的雅仕風範。
                        </p>
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
	<div id="template" class="collection">
		<a href="wristwatch/automatic/0/" class="name">name</a>
	</div>
</div>
</body>
</html>
