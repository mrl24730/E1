<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="investor.aspx.cs" Inherits="ErnestBorel.investor" %>
<!DOCTYPE HTML>
<html>
<head>
    <base href="<%=domain_cn %>/" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<meta name="description" content= "瑞士手表品牌—依波路，提供上市文件、公告、财务报告等投资者资讯以依波路为主题的不同分辨率手机壁纸，电脑壁纸的下载。" >
	<meta name="keywords" content="依波路,瑞士名表,自动手表,石英手表,计时码表,奢华腕表,高端品牌,腕表, 纽察图,瑞士,投资者关系">
	<title>投资者关系_依波路表官方网站</title>
	<!-- panel start -->
	<!--#include file ="include.inc"-->
	<!-- panel end -->
	<script type="text/javascript" src="/js/investor.js"></script>
	<script type="text/javascript"> 
	    nid = 6; 
	    disableAutoCloseSubMenu();  
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
	<div class="main-content" id="irPage">
		<section>
			<h2>投资者关系</h2>
			<div class="yui3-g">
				<div class="yui3-u-3-4">
					<h3 class="subtitle">撮要</h3>
					<p style="text-align: justify;">依波路控股限公司于1856年在瑞士成立，为瑞士少有的百年独立名表制表商，以自有品牌依波路(Ernest Borel) 设计、制造、销售的名贵机械及石英男女装手表，以其「浪漫优雅」的形象闻名于世。依波路拥有庞大的经销网路，于全球拥有1000多个销售点，尤其专注中国市场、港澳地区及东南亚市场。集团的手表产品包含30多种不同的瑞士制机械及石英手表系列，超过250种款式，主要以高中等收入的终端客户为目标。<br><br></p>

					<h3 class="subtitle">最新公告</h3>

					<ul class="summary">
                        <asp:Repeater ID="announceList" runat="server">
                        <ItemTemplate>
						    <li><a href="pdf/<%# Eval("ir_file") %>" target="_blank">[<%# Eval("ir_releaseDate")%>]<br><%# Eval("ir_title") %></a></li>
                        </ItemTemplate>
						</asp:Repeater>
					</ul>

					<div class="yui3-g">
						<div class="yui3-u-3-4">
							<h3 class="subtitle">财务报告</h3>
							<ul class="summary">
								<li><a href="../pdf/tc/20170425_Annual_Report_2016_tc.pdf" target="_blank">[2017-04-25] 财务报表/环境、社会及管治资料 - [年报]二零一六年年报</a></li>
								<li><a href="pdf/tc/20160929_Interim_Report_tc.pdf" target="_blank">[2016-09-29] 财务报表/环境、社会及管治资料 - [中期/半年度报告]2016中期报告</a></li>
								<li><a href="pdf/tc/20160427_annual_report.pdf" target="_blank">[2016-04-27] 财务报表/环境、社会及管治资料 - [年报]二零一五年年报</a></li>
							</ul>
						</div>
						<div class="yui3-u-1-4">
							<h3 class="subtitle">重點提示</h3>
							<p><a href="investor_listing/">&gt; 上市文件</a></p>
							<p><a href="investor_announcement/">&gt; 公告</a></p>
							<p><a href="investor_report/">&gt; 財務報告</a></p>
							<p><a href="investor_enquiry/">&gt; 投資者查詢</a></p>
						</div>
					</div>

				</div>

				<div class="yui3-u-1-4">
					
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
</body>
</html>
