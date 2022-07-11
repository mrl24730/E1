<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../investor_announcement.aspx.cs" Inherits="ErnestBorel.investor_announcement" %>

<!DOCTYPE HTML>
<html lang="tc">
<head>
<base href="<%=domain_ch %>/tc/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content= "瑞士手表品牌—依波路，提供上市文件、公告、財務報告等投資者資訊以依波路為主題的不同分辨率手機壁紙，電腦壁紙的下載。" >
<meta name="keywords" content="依波路,瑞士名表,自動手表,石英手表,計時碼表,奢華腕表,高端品牌,腕表, 紐察圖,瑞士,投資者關係">
<title>投資者關係_公告_依波路表官方網站</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/investor.js"></script>
<script>
	nid = 6; 
	subId = 3;
    totalResult = <%=totalResult %>;
    itemPerPage = <%=itemPerPage %>;
    currentPage = <%=page %> - 1;
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
					<h2>公告</h2>
					<div class="yui3-g investor">
						<div class="yui3-u-1">

		                    <asp:Repeater ID="rptInvestorAnnounce" runat="server">
		                        <ItemTemplate>
		                        <div class="block line">
								    <h4>[<%# Eval("ir_releaseDate")%>] <%# Eval("ir_title") %></h4>
								    <p><a href="/pdf/<%# Eval("ir_file") %>" target="_blank"><%# Eval("ir_desc")%></a> (<%# Eval("ir_filesize")%>KB, PDF) </p>
							    </div>
		                        </ItemTemplate>
		                    </asp:Repeater>
							<div id='pagination'></div>	
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
