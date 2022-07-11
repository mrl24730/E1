﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../default.aspx.cs" Inherits="ErnestBorel._default" %>

<!DOCTYPE HTML>
<html lang="tc">
<head>
<base href="<%=domain_ch %>/tc/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content="源自1856年的浪漫經典瑞士手表品牌—依波路,依波路手表發源自「鐘表王國」瑞士紐察圖,打造出款式設計典雅精緻的奢華高檔手表,包括:情侶表,女士名牌手表,男士名牌手表">
<meta name="keywords" content="依波路手表,依波路價格,情侶表,高檔手表,女士名牌手表,男士名牌手表">
<title>瑞士手表品牌—依波路表官方網站</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/home.js"></script>
<script>
    nid = 0;
</script>
<link rel="shortcut icon" type="image/x-icon" href="/favicon.ico?v=2">
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
		<div id="mainPage" class="main-content">
			<section>
				<div id="main-theme">
					<ul id="themeWrapper">
						<li><a href="javascript:void(0);"><img src="/images/home/home_theme_20200730_tc_1.jpg"></a></li>
                        <li><a href="javascript:void(0);"><img src="/images/home/home_theme_20200730_tc_2.jpg"></a></li>
                        <li><a href="javascript:void(0);"><img src="/images/home/home_theme_20200730_tc_3.jpg"></a></li>
                        <li><a href="javascript:void(0);"><img src="/images/home/home_theme_20200730_tc_4.jpg"></a></li>
					</ul>

					<div id="hero-banner" class="row-sep">
						<p>最新消息</p>
						<div id="slides">
							<ul id="bannerWrapper">
								<asp:Repeater id="newsList" runat="server">
                                    <ItemTemplate>
								    <li data-caption="<%#DataBinder.Eval(Container, "DataItem.news_title")%>'>">
                                        <a href="news_details/<%#DataBinder.Eval(Container, "DataItem.news_ref")%>">
                                            <img src="/images/latest_news/<%#DataBinder.Eval(Container, "DataItem.img")%>" alt="<%#DataBinder.Eval(Container, "DataItem.news_title")%>">
                                        </a>
								    </li>
                                    </ItemTemplate>
                                </asp:Repeater>
							</ul>
							<a href="javascript:void(0);" class="btn-prev"></a>
							<a href="javascript:void(0);" class="btn-next"></a>
							<a href="javascript:void(0);" class="btn-pause"></a>
							<a href="javascript:void(0);" class="btn-resume"></a>
						</div>
						<div id="caption">&nbsp;</div>
					</div>

					<div id="searchWrapper">
						<form id="formSearch" name="formSearch" action="wristwatch_selector" method="POST">
						<p>腕表搜尋</p>
						<input type="text" id="keyword" name="keyword" placeholder="輸入關鍵字或產品型號" /><input type="submit" id="btnSearch" name="btnSearch" value="搜尋"/>
						</form>
					</div>

				</div>

			</section>
		</div>
		<!-- footer start -->
        <!--#include file ="footer.inc"-->
		<!-- footer end -->
	</div>

</div>
</body>
</html>
