<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../investor.aspx.cs" Inherits="ErnestBorel.investor" %>
<!DOCTYPE HTML>
<html lang="tc">
<head>
    <base href="<%=domain_ch %>/tc/" />
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<meta name="description" content= "瑞士手表品牌—依波路，提供上市文件、公告、財務報告等投資者資訊以依波路為主題的不同分辨率手機壁紙，電腦壁紙的下載。" >
	<meta name="keywords" content="依波路,瑞士名表,自動手表,石英手表,計時碼表,奢華腕表,高端品牌,腕表, 紐察圖,瑞士,投資者關係">
	<title>投資者關係_依波路表官方網站</title>
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
			<h2>投資者關係</h2>
			<div class="yui3-g">
				<div class="yui3-u-3-4">
					<h3 class="subtitle">撮要</h3>
					<p style="text-align: justify;">依波路控股限公司於1856年在瑞士成立，為瑞士少有的百年獨立名表製表商，以自有品牌依波路(Ernest Borel) 設計、製造、銷售的名貴機械及石英男女裝手表，以其「浪漫優雅」的形象聞名於世。依波路擁有龐大的經銷網路，於全球擁有1000多個銷售點，尤其專注中國市場、港澳地區及東南亞市場。集團的手表產品包含30多種不同的瑞士製機械及石英手表系列，超過250種款式，主要以高中等收入的終端客戶為目標。<br><br></p>

					<h3 class="subtitle">最新公告</h3>

					<ul class="summary">
                        <asp:Repeater ID="announceList" runat="server">
                        <ItemTemplate>
						    <li><a href="../pdf/<%# Eval("ir_file") %>" target="_blank">[<%# Eval("ir_releaseDate")%>]<br><%# Eval("ir_title") %></a></li>
                        </ItemTemplate>
						</asp:Repeater>
					</ul>

					<div class="yui3-g">
						<div class="yui3-u-3-4">
							<h3 class="subtitle">財務報告</h3>
							<ul class="summary">
								<li><a href="../pdf/tc/20170425_Annual_Report_2016_tc.pdf" target="_blank">[2017-04-25] 財務報表/環境、社會及管治資料 - [年報]二零一六年年報</a></li>
								<li><a href="pdf/tc/20160929_Interim_Report_tc.pdf" target="_blank">[2016-09-29] 財務報表/環境、社會及管治資料 - [中期/半年度報告]2016中期報告</a></li>
								<li><a href="pdf/tc/20160427_annual_report.pdf" target="_blank">[2016-04-27] 環境、社會及管治資料 - [年報] 二零一五年年報</a></li>
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
