<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../default.aspx.cs" Inherits="ErnestBorel._default" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="Ernest Borel,swiss watch,automatic watch,quartz-movement watch,chrono watch,luxury watch,prestige watch,wrist watch,neuchatel,switzerland">
<meta name="description" content=" Romance in Heart Since 1856. Ernest Borel –  founded in Neuchatel, Switzerland. Exquisitely elegant, admirably designed automatic and quartz-movement wrist watches for men and women.">
<title>Ernest Borel - メインページ</title>
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
						<li><a href="javascript:void(0);"><img src="/images/home/home_theme_20190730_en_1.jpg"></a></li>
                        <li><a href="javascript:void(0);"><img src="/images/home/home_theme_20190730_en_2.jpg"></a></li>
                        <li><a href="javascript:void(0);"><img src="/images/home/home_theme_20190730_en_3.jpg"></a></li>
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
						<p>検索する</p>
						<input type="text" id="keyword" name="keyword" placeholder="キーワードまたはモデル番号を入力してください" /><input type="submit" id="btnSearch" name="btnSearch" value="検索する"/>
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
