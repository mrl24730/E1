<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../wristwatch-type.aspx.cs" Inherits="ErnestBorel.wristwatch_type" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content=" Romance in Heart Since 1856. Ernest Borel –  founded in Neuchatel, Switzerland. Exquisitely elegant, admirably designed automatic and quartz-movement wrist watches for men and women.">
<meta name="keywords" content="<%=obj.metaKeyword %>">
<title>Ernest Borel - 最新製品</title>
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
						<h4>ROMANTIC SERIES<br>(ロマンチックな恋人たち)</h4>
						<a href="wristwatch_collection/couple/"><img src="/images/0_watch_romantic_m.jpg" alt="" width="90%"></a>
                        <br />
                        <p class="desc">
                          ROMANTIC SERIES(ロマンチックな恋人たち)はブランドの100年のロマンチックなエレガンスさを受け継いでいます。世の愛情はみな時空をこえられるという意味合いが秘められており、ペアウォッチで心を伝え、いかなる時も愛の時を刻みます。
                        </p>
					</div><!--
					--><div class="yui3-u-1-3 bannerWrapper">
						<h4>FEMININE SERIES<br>(フェミニンな淑女)</h4>
						<a href="wristwatch_collection/lady/"><img src="/images/0_watch_feminine_m.jpg" alt="" width="90%"></a>
                        <br />
                        <p class="desc">
                          FEMININE SERIES (フェミニンな淑女)は多種多様な美しさの中心をなす輝くような美しさのきめ細やかなデザインを通して、女性の端正でエレガント、ファッショナブルな気前のよさ、質素なビジネスシーンなど、様々な優雅な姿を人々の前で表現しています。
                        </p>
					</div><!--
					--><div class="yui3-u-1-3 bannerWrapper">
						<h4>CASUAL SERIES<br>(カジュアルな紳士)</h4>
						<a href="wristwatch_collection/casual/"><img src="/images/0_watch_causal_m.jpg" alt="" width="90%"></a>
                        <br />
                        <p class="desc">
                          CASUAL SERIESは都市に生きる人々のいかなるシーンにもマッチする、どんなスタイルにも合うファッショナブルで快適な腕時計です。都会の男性のおしゃれで束縛されないカジュアルな風采です。
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
