<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../tech_info_terminology.aspx.cs" Inherits="ErnestBorel.tech_info_terminology" %>

<!DOCTYPE HTML>
<html lang="tc">
<head>
<base href="<%=domain_ch %>/tc/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content= "瑞士依波路手表類型及常用術語介紹，機械表主要由以下幾個部分組成：(1)動力機構，即主發條及上條機構; (2)傳動輪系;(3)調速機構，即擒縱系統和擺輪元件;(4)指針和校時機構……" >
<meta name="keywords" content="依波路,瑞士名表,自動手表,石英手表,計時碼表,奢華腕表,高端品牌,腕表, 紐察圖,瑞士,依波路使用須知">
<title>依波路手表技術信息_依波路官方網站</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/jquery/jquery.scrollfollow.modForJq164.js"></script>
<script type="text/javascript" src="/js/jquery/jquery.ui.core.js" ></script>
<script type="text/javascript" src="/js/jquery/jquery.ui.widget.js" ></script>
<script type="text/javascript" src="/js/jquery/jquery.ui.accordion.js" ></script>
<script>
nid = 1;
subId = 3;
$(document).ready(function(){
	$(".watch-menu .w3").addClass("selected");

	//accordion, float index
	/*
	$("#floatCont").height($(".yui3-u-3-4 .content").height())
	$(".floatbox").scrollFollow({container:'termfloat',speed:1150});
	*/

	$(".termIndex").accordion({
		autoHeight: false,
		navigation: true
	});
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
		<div class="main-content" id="tech_info">
		<section>
				<h2>技術信息</h2>
				<h3>手表類型及常用術語 | <a href="tech_info_instruction">手表使用須知</a></h3>
				<div class="yui3-g">
					<div class="yui3-u-3-4">
						<div class="content">
							<div id="techinfo_term">
									<ol class="instruct">
										<li>
											<h5><a name="Mechanical Watches"></a>機械表</h5>
											機械表由以下零件組成：動力機制，即發條及上鍊機制；齒輪輪系，速度調節器，即擒縱系統和平衡擺輪組件；指針和計時機制。機械表包括手動或自動上鍊類型。兩者都採用發條提供動力。手動上鍊機械表的發條需透過手動扭轉表冠上緊發條，而全自動機械表，則透過腕表的自然擺動產生足夠的能量來驅動上鍊機制，讓發條收緊。
										</li>
										<li>
											<h5><a name="Quartz Watches"></a>石英表</h5>
											石英表分為指針丶電子或兩者的組合類型。他們利用電力和石英的振盪器計時。指針式石英表使用電池供電讓石英振盪，隨後其IC 板控制步進馬達驅動齒輪輪系和轉動指針。電子石英手表的時間則利用液晶顯示螢幕顯示。
										</li>
										<li>
											<h5><a name="Chronometer"></a>天文台表/精密手表</h5>
											天文台表是高精密與準確的表款。必須通過不同角度與溫度的測試方可獲得官方認證（COSC）。
										</li>
										<li>
											<h5><a name="Multifunction Watches"></a>多功能表</h5>
											多功能手表具有額外的功能，例如計時丶第二時區丶鬧鈴丶萬年曆及三問報時等。
										</li>
										<li>
											<h5><a name="Grande Complication"></a>複雜功能表</h5>
											複雜功能表至少有萬年曆丶三問報時和計時（雙針秒）的功能，甚至有陀飛輪設計。
										</li>
										<li>
											<h5><a name="Minute Repeater"></a>三問表</h5>
											本義是以聲音來報時的手表。隨著側邊滑動發條的轉動，有時丶刻和分的聲音報時功能。
										</li>
										<li>
											<h5><a name="Chronograph"></a>計時碼表</h5>
											這類的手表或懷表具有定時功能（計時丶停止和重設）。
										</li>
										<li>
											<h5><a name="Musical Watch"></a>音樂表</h5>
											是一種機械表，可以在每小時正點播放音樂。有磁盤或滾輪式機芯。
										</li>
										<li>
											<h5><a name="Water Resistance"></a>防水表</h5>
											被視為防水的表款，其表殼丶表冠和晶體必須防水與防塵，並在指定的水深中維持防水。此功能須定期檢查。
										</li>
										<li>
											<h5><a name="Jumping Hours (or single-eyed)"></a>跳時（或單眼）</h5>
											手表的時針由1-12的數字表盤取代。表盤每隔一小時向前移動一個數字，即數字隨著每小時而變化。表盤上設計一個小口來顯示數字。
										</li>
										<li>
											<h5><a name="Antimagnetic Watches"></a>防磁表</h5>
											這類手表不受磁場影響，因為擺輪的發條由非磁性鎳合金製成。
										</li>
										<li>
											<h5><a name="Diver's Watch"></a>潛水表</h5>
											專為深海潛水所設計，有一個螺旋式表冠，能承受20大氣壓（ATM），允許潛水至200米深。</li>
										<li>
											<h5><a name="Tourbillon"></a>陀飛輪</h5>
											是一種旋轉式擒縱器的機械表。不受重力的影響，它可以保持機芯的精準度。除了一般機械表的擒縱器所有功能外，陀飛輪表能夠圍繞軸心持續360度旋轉，以調整系統的位置和因重力引起的操作錯誤。
										</li>
										<li>
											<h5><a name="Rhodium Plating"></a>鍍銠</h5>
											銠是一種昂貴的銀白色貴金屬。鍍銠的過程很複雜，但能製作出堅硬丶耐磨和光亮的表面，大大提高防腐蝕和化學穩定性，因此也提高了手表的可靠性和使用壽命。
										</li>
										<li>
											<h5><a name="Geneva Seal"></a>日內瓦條紋</h5>
											鍍銠之前，在顯露機板和機芯的橋板上刻上優雅的裝飾平行波紋，即日內瓦條紋。一般來說，他們只適用於高級手表。
										</li>
										<li>
											<h5><a name="Geneva Stripes"></a>月相</h5>
											月相腕表藉由旋轉機芯的月亮面盤來運作，在表盤上的半圓形窗口顯示月亮盈虧活動（新月丶下弦月和滿月）。正確月亮盈虧活動週期的是29天12小時44分鐘3秒。月亮面盤設計成兩個相反的月亮，以一個齒輪運轉，1週期為59天轉59個鋸齒，或2個陰曆月。
										</li>
										<li>
											<h5><a name="Moon Phase"></a>格林威治標準時間（GMT）</h5>
											GMT 是格林威治標準時間的縮寫，也稱為世界時（UT）。格林威治天文台位於英國，這是世界經度的起點。
										</li>
									</ol>
								</div>
							</div>
						</div>
								
						<!-- floating index start -->
						<!--
						<div class="yui3-u-1-4">
							<div id="floatCont" class="termfloat">
								<div class="floatbox">
									<div class="sideBox termIndex">
										<h5>三划</h5>
										<ul class="capital_A">
											<li><a href="#Grande Complication">大复杂表</a></li>
											<li><a href="#Minute Repeater">三问表</a></li>
										</ul>
										<h5>四划</h5>
										<ul class="capital_A">
											<li><a href="#Chronometer">天文台表</a></li>
											<li><a href="#Chronograph">计时码表</a></li>
											<li><a href="#Geneva Seal">日内瓦印记</a></li>
											<li><a href="#Geneva Stripes">日内瓦条纹</a></li>
											<li><a href="#Moon Phase">月相</a></li>
										</ul>
										<h5>五划</h5>
										<ul class="capital_A">
											<li><a href="#Quartz Watches">石英表</a></li>
										</ul>
										<h5>六划</h5>
										<ul class="capital_A">
											<li><a href="#Mechanical Watches">机械表</a></li>
											<li><a href="#Multifunction Watches">多功能表</a></li>
										</ul>
										<h5>七划</h5>
										<ul class="capital_A">
											<li><a href="#Tourbillon">陀飞轮</a></li>
											<li><a href="#Water Resistance">防水表</a></li>
											<li><a href="#Antimagnetic Watches">防磁表</a></li>
											<li><a href="#Water Resistance">防水表</a></li>
										</ul>
										<h5>十划</h5>
										<ul class="capital_A">
											<li><a href="#Greenwich Mean Time (GMT)">格林威治平均时间(GMT)</a></li>
										</ul>
										<h5>十三划</h5>
										<ul class="capital_A">
											<li><a href="#Jumping Hours (or single-eyed)">跳时表 (或称为独眼龙)</a></li>
										</ul>
										<h5>十四划</h5>
										<ul class="capital_A">
											<li><a href="#Rhodium Plating">镀铑处理</a></li>
										</ul>
										<h5>十五划</h5>
										<ul class="capital_A">
											<li><a href="#Diver's Watch">潜水表</a></li>
										</ul>
								</div>
								<a class="totop" href="#"></a> </div>
							</div>
						</div>
						-->
						<!-- floating index end -->
					</div>
				</section>

		</div>
		<!-- footer start -->
        <!--#include file ="footer.inc"-->
		<!-- footer end -->
	</div>
	</div> 
</div>
</body>
</html>
