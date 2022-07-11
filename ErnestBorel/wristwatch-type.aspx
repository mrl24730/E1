<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wristwatch-type.aspx.cs" Inherits="ErnestBorel.wristwatch_type" %>

<!DOCTYPE HTML>
<html>
<head>
<base href="<%=domain_cn %>/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content="瑞士手表品牌—依波路，推出最新高档手表产品包括自动系列和石英系列，分别为男士、女士分别打造不同品味的高档手表,情侣表等">
<meta name="keywords" content="<%=obj.metaKeyword %>">
<title>依波路官方网站 - 依波路手表最新产品</title>
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
						<a href="/wristwatch_collection/couple/"><img src="/images/0_watch_romantic_m.jpg" alt="" width="90%"></a>
                        <br />
                        <p class="desc">
                          ROMANTIC SERIES（浪漫情侣）承载品牌过百年的浪漫优雅，寓意世间爱情都能穿越时空，以对表传情，方寸说爱。
                        </p>
					</div><!--
					--><div class="yui3-u-1-3 bannerWrapper">
						<h4>优雅淑女</h4>
						<a href="/wristwatch_collection/lady/"><img src="/images/0_watch_feminine_m.jpg" alt="" width="90%"></a>
                        <br />
                        <p class="desc">
                          FEMININE SERIES（优雅淑女）意在透过多种唯美耀目的细节设计，把女性端庄高雅、时尚大方、简朴商务等万千风韵展现人前。
                        </p>
					</div><!--
					--><div class="yui3-u-1-3 bannerWrapper">
						<h4>潇洒雅仕</h4>
						<a href="/wristwatch_collection/casual/"><img src="/images/0_watch_causal_m.jpg" alt="" width="90%"></a>
                        <br />
                        <p class="desc">
                          CASUAL SERIES（潇洒雅仕）意在带给都市人一枚任何场合都适合配戴、风格百搭而时尚舒适的腕表，散发男士潇洒不羁的雅仕风范。
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
