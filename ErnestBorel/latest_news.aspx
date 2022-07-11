<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="latest_news.aspx.cs" Inherits="ErnestBorel.latest_news" %>
<!DOCTYPE HTML>
<html>
<head>
<base href="<%=domain_cn %>/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content= "<%=metadesc %>" >
<meta name="keywords" content="依波路,瑞士名表,自动手表,石英手表,计时码表,奢华腕表,高端品牌,腕表, 纽察图,瑞士,依波路新闻">
<title>依波路官方网站 - 依波路世界 - 企业新闻</title>
<link rel="stylesheet" type="text/css" href="/css/pagination.css">
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/jquery/jquery.pagination.js"></script>
<script type="text/javascript" src="/js/jquery/jquery.ellipsis.js"></script>
<script type="text/javascript" src="/js/news.js"></script>
<script>
nid = 3; subId = 1;
var totalResult = <%=newsTotal %>;
var itemPerPage = <%=pageItem %>;
var currentPage = <%=pageNow %> - 1;
</script>
</head>

<body>
<!-- panel start -->
<!--#include file ="panel.inc"-->
<!-- panel end -->
<div id="doc" class="News">
	<div id="docShadow">
	<div id="docWrapper">
		<!-- header start -->
        <!--#include file ="header.inc"-->
		<!-- header end -->
		
		<!-- nav start -->
		<!--#include file ="nav.inc"-->
		<!-- nav end -->
		<div class="main-content">
			<section>
				<h2>企业新闻</h2>
				<div class="content">
                    <asp:Repeater id="newsList" runat="server">
                    <ItemTemplate>
                        <div class='newsblock'>
				            <div class='imgWrapper'>
					            <a href='/news_details/<%#DataBinder.Eval(Container, "DataItem.news_ref")%>' title='<%#DataBinder.Eval(Container, "DataItem.news_title")%>'><img src='/images/latest_news/<%#DataBinder.Eval(Container, "DataItem.img")%>' alt='<%#DataBinder.Eval(Container, "DataItem.news_title")%>'></a>
				            </div>
				            <div class='contWrapper'>
					            <h3><a class='newsTitle' href='/news_details/<%#DataBinder.Eval(Container, "DataItem.news_ref")%>'> <%#DataBinder.Eval(Container, "DataItem.news_title")%> </a></h3>
					            <span class='newsDate'><%#DataBinder.Eval(Container, "DataItem.date")%></span>
					            <p class='desc'><%#DataBinder.Eval(Container, "DataItem.news_desc")%></p>
				            </div>
				            <a class='more_btn' href='/news_details/<%#DataBinder.Eval(Container, "DataItem.news_ref")%>'>More <span class='nav_arrow'>&#9658;</span></a>
			            </div>
                    </ItemTemplate>
                    </asp:Repeater>
					<div id='pagination' class="right"></div>
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
