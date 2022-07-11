<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tech_info_terminology.aspx.cs" Inherits="ErnestBorel.tech_info_terminology" %>

<!DOCTYPE HTML>
<html>
<head>
<base href="<%=domain_cn %>/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content= "瑞士依波路手表类型及常用术语介绍，机械表主要由以下几个部分组成：(1)动力机构，即主发条及上条机构; (2)传动轮系;(3)调速机构，即擒纵系统和摆轮元件;(4)指针和校时机构……" >
<meta name="keywords" content="依波路,瑞士名表,自动手表,石英手表,计时码表,奢华腕表,高端品牌,腕表, 纽察图,瑞士,依波路使用须知">
<title>依波路手表技术信息_依波路官方网站</title>
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
				<h2>技术信息</h2>
				<h3>手表类型及常用术语 | <a href="tech_info_instruction">手表使用须知</a></h3>
				<div class="yui3-g">
					<div class="yui3-u-3-4">
						<div class="content">
							<div id="techinfo_term">
									<ol class="instruct">
										<li>
											<h5><a name="Mechanical Watches"></a>机械表</h5>
											机械表由以下零件组成：动力机制，即发条及上链机制；齿轮轮系，速度调节器，即擒纵系统和平衡摆轮组件；指针和计时机制。机械表包括手动或自动上链类型。两者都采用发条提供动力。手动上链机械表的发条需透过手动扭转表冠上紧发条，而全自动机械表，则透过腕表的自然摆动产生足够的能量来驱动上链机制，让发条收紧。
										</li>
										<li>
											<h5><a name="Quartz Watches"></a>石英表</h5>
											石英表分为指针丶电子或两者的组合类型。他们利用电力和石英的振荡器计时。指针式石英表使用电池供电让石英振荡，随後其 IC 板控制步进马达驱动齿轮轮系和转动指针。电子石英手表的时间则利用液晶显示萤幕显示。
										</li>
										<li>
											<h5><a name="Chronometer"></a>天文台表/精密手表</h5>
											天文台表是高精密与准确的表款。必须通过不同角度与温度的测试方可获得官方认证（COSC）。
										</li>
										<li>
											<h5><a name="Multifunction Watches"></a>多功能表</h5>
											多功能手表具有额外的功能，例如计时丶第二时区丶闹铃丶万年历及三问报时等。
										</li>
										<li>
											<h5><a name="Grande Complication"></a>复杂功能表</h5>
											复杂功能表至少有万年历丶三问报时和计时（双针秒）的功能，甚至有陀飞轮设计。
										</li>
										<li>
											<h5><a name="Minute Repeater"></a>三问表</h5>
											本义是以声音来报时的手表。随着侧边滑动发条的转动，有时丶刻和分的声音报时功能。
										</li>
										<li>
											<h5><a name="Chronograph"></a>计时码表</h5>
											这类的手表或怀表具有定时功能（计时丶停止和重设）。
										</li>
										<li>
											<h5><a name="Musical Watch"></a>音乐表</h5>
											是一种机械表，可以在每小时正点播放音乐。有磁盘或滚轮式机芯。
										</li>
										<li>
											<h5><a name="Water Resistance"></a>防水表</h5>
											被视为防水的表款，其表壳丶表冠和晶体必须防水与防尘，并在指定的水深中维持防水。此功能须定期检查。
										</li>
										<li>
											<h5><a name="Jumping Hours (or single-eyed)"></a>跳时（或单眼）</h5>
											手表的时针由1-12的数字表盘取代。表盘每隔一小时向前移动一个数字，即数字随着每小时而变化。表盘上设计一个小口来显示数字。
										</li>
										<li>
											<h5><a name="Antimagnetic Watches"></a>防磁表</h5>
											这类手表不受磁场影响，因为摆轮的发条由非磁性镍合金制成。
										</li>
										<li>
											<h5><a name="Diver's Watch"></a>潜水表</h5>
											专为深海潜水所设计，有一个螺旋式表冠，能承受20大气压（ATM），允许潜水至200米深。</li>
										<li>
											<h5><a name="Tourbillon"></a>陀飞轮</h5>
											是一种旋转式擒纵器的机械表。不受重力的影响，它可以保持机芯的精准度。除了一般机械表的擒纵器所有功能外，陀飞轮表能够围绕轴心持续360度旋转，以调整系统的位置和因重力引起的操作错误。
										</li>
										<li>
											<h5><a name="Rhodium Plating"></a>镀铑</h5>
											铑是一种昂贵的银白色贵金属。镀铑的过程很复杂，但能制作出坚硬丶耐磨和光亮的表面，大大提高防腐蚀和化学稳定性，因此也提高了手表的可靠性和使用寿命。
										</li>
										<li>
											<h5><a name="Geneva Seal"></a>日内瓦条纹</h5>
											镀铑之前，在显露机板和机芯的桥板上刻上优雅的装饰平行波纹，即日内瓦条纹。一般来说，他们只适用於高级手表。
										</li>
										<li>
											<h5><a name="Geneva Stripes"></a>月相</h5>
											月相腕表藉由旋转机芯的月亮面盘来运作，在表盘上的半圆形窗口显示月亮盈亏活动（新月丶下弦月和满月）。正确月亮盈亏活动周期的是29天12小时44分钟3秒。月亮面盘设计成两个相反的月亮，以一个齿轮运转，1周期为59天转59个锯齿，或2个阴历月。
										</li>
										<li>
											<h5><a name="Moon Phase"></a>格林威治标准时间（GMT）</h5>
											GMT 是格林威治标准时间的缩写，也称为世界时（UT）。格林威治天文台位於英国，这是世界经度的起点。
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
