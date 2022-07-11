<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../tech_info_instruction.aspx.cs" Inherits="ErnestBorel.tech_info_instruction" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="Ernest Borel,swiss watch,automatic watch,quartz-movement watch,chrono watch,luxury watch,prestige watch,wrist watch,neuchatel,switzerland">
<meta name="description" content=" Romance in Heart Since 1856. Ernest Borel –  founded in Neuchatel, Switzerland. Exquisitely elegant, admirably designed automatic and quartz-movement wrist watches for men and women.">
<title>Ernest Borel - 技術情報</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript">
    nid = 1;
    subId = 3;
    $(document).ready(function () {
        $(".watch-menu .w3").addClass("selected");
    });
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
		<div class="main-content" id="tisPage">
			<section>
				<h2>技術情報</h2>
				<h3><a href="tech_info_terminology">專門用語</a> | 取扱說明書</h3>
				<div class="infocontwrapper" >
					<ol class="instruct">
						<li>腕時計のモデルによって防水レベルが異なりますので、着用するに当たって防水レベルをあらかじめ確認しておいてください。</li>
						<li>腕時計のベルトまたはブレスレットの長さを調整したいときは、専門家におまかせください。</li>
						<li>サファイアクリスタルフェースはモード硬度9で耐摩耗性がとても高いのですが、ダイヤモンド、コランダム、またはこれらに類する物質など、モース硬度10以上の物質による衝撃を与えないでください。</li>
						<li>リューズを操作した後は、原点まで押し戻し、ねじ込み式リューズの場合は最後までしっかりと締め付けてください。</li>
						<li>水中でリューズを操作しないでください。</li>
						<li>腕時計を落下させたり、ほかの物体と激しく衝突させないでください。</li>
						<li>腕時計は、スピーカー、冷蔵庫など、強い磁界から遠ざけて保管してください。</li>
						<li>浴室、サウナなどの場所で腕時計を着用しないでください。</li>
						<li>腕時計が海水に触れた場合、ただちに水道水で洗い、軟らかい布で水分を拭き取ってください。</li>
						<li>汗、または酸性/アルカリ性の強い物質との長時間にわたる接触は避けてください。</li>
						<li>金属製ウオッチケースまたはブレスレットは、歯ブラシを用いて薄い石鹸水で軽く磨き、軟らかい布で水分を拭き取ってください。</li>
						<li>革製ベルトを、長時間にわたって水や直射日光に当てないでください。</li>
						<li>腕時計を、溶剤、洗浄剤、香水などの化学物質に直接接触させないでください。</li>
						<li>腕時計を、60<sup>o</sup>c 以上または零下10<sup>o</sup>c 以下の環境に、長時間にわたって放置しないでください。</li>
						<li>使用環境に従って防水レベルを定期的にチェックしてください。</li>
						<li>クォーツ時計が止まったら、ムーブメントが液漏れによるダメージを受けないようにするため、すぐに電池を交換してください。三針クォーツ時計の場合、電池の電圧が低くなると、秒針が4秒ごとにジャンプして電池切れを警告する機能があります。二針クォーツ時計の場合には、この機能がないので、時計が遅れはじめたり、止まったりしたら、すぐに電池を交換してください。</li>
					</ol>
				</div>
			</section>
			<!--<script>$("ol.instruct li").each(function(i){$(this).css("opacity",0).delay(300*i).animate({opacity:1},1000);});</script>-->
		</div>
		<!-- footer start -->
		<!--#include file ="footer.inc"-->
		<!-- footer end -->
	</div>
	</div> 
</div>
</body>
</html>
