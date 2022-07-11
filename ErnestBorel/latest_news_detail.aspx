<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="latest_news_detail.aspx.cs" Inherits="ErnestBorel.latest_news_detail" %>
<!DOCTYPE HTML>
<html>
<head>
<base href="<%=domain_cn %>/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content="<%=obj.meta %>">
<meta name="keywords" content="<%=obj.title %>">
<title><%=obj.title %>_依波路官网</title>
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script src="/js/jquery.scrollfollow.modForJq164.js" type="text/javascript"></script>
<!--[if lte IE 6]>
	<link rel="stylesheet" type="text/css" href="../css/ie6.css" />
<![endif]-->
<script type="text/javascript">
	nid = 3; subId = 1;
	var imgTotal = <%=obj.imageURL.Count%>;
	var videoURL =  "<%=obj.videoURL%>";
	var captionArray = <%=obj.captionString %>;
	var title = "";

	$(document).ready(function(){

		if($(".btnPrev").find("a").attr("title") == "") $(".btnPrev").hide();
		if($(".btnNext").find("a").attr("title") == "") $(".btnNext").hide();
		
		if(videoURL != ""){
			$(".colorbox").attr("title", "Click to play");
			$(".colorbox").colorbox({
				href: videoURL,
				iframe: true,
				innerWidth:560, 
				innerHeight:315
			});
		} else {
			$(".colorbox").colorbox();
		}


		$(".relatedNews").scrollFollow({ container: 'outer', offset:121});
		
		if(imgTotal > 1){
			var cycleObj = new CycleProgress({container:".imgWrapper"});
			$(".newsblock_dt .imgWrapper").css("border-bottom","none");
			$("#imgCaption").cycle({timeout:0});
		}

	});
</script>
<style>


</style>
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
		<div id="outer" class="main-content">
			<section>
				<h2>企业新闻</h2>
                <span id="breadcrumbNav"><a href='/' >依波路</a> &gt; <a href='/newspage' >企业新闻 </a> &gt; 正文</span>

				<div class="yui3-g">
					<div class="yui3-u-3-4">
						<div class="content newsblock_dt">
							<h3><%=obj.title %></h3>
							<span class="newsDate"><%=obj.displayDate %></span>
							<div class="imgWrapper">
                            <asp:Repeater id="imageList" runat="server">
                                <ItemTemplate>
                                <a class="colorbox" href="../images/latest_news/<%# Container.DataItem %>.jpg" title="Click to enlarge photo">
		                            <img src="../images/latest_news/<%# Container.DataItem %>_h.jpg" alt="title">
		                            <img src="../images/icon_magnifier.png" class="icon_enlarge" alt="Click to enlarge photo">
	                            </a>
                                </ItemTemplate>
                            </asp:Repeater>
                            </div>
							<div id="imgCaption"></div>

							<span id="articleInfo">
								来源：<a rel='author'>瑞士依波路官網 </a><br />
								作者：<cite>瑞士依波路 </cite> <br />
								文章發布时间：<time><%=obj.displayDate %></time>
							</span>

							<div class="descFull"><p><%=obj.desc %></p></div>
							
							<div class="funcPanel">
								
								<div style='float:left;'  class="btnPrev">
	                                <div style='margin-left:21px;'>
	                                <span style='display:block;margin-bottom:3px;'>&lt; 上一篇</span>
	                                <a href='/news_details/<%=obj.prev.news_ref%>' class='' title='<%=obj.prev.news_title%>'><%=obj.prev.news_title%></a>
	                                </div>
	                            </div>

	                            <div style='float:right; text-align:right;' class="btnNext">
	                                <div style='margin-right:21px;'>
	                                <span style='display:block;margin-bottom:3px;'>下一篇 &gt;</span>
	                                <a href='/news_details/<%=obj.next.news_ref%>' class='' title='<%=obj.next.news_title%>'><%=obj.next.news_title%></a>
	                                </div>
	                            </div>

							</div>
						</div>
					</div>
					
				</div>
			</section>
			<div class="relatedNews">
				<h4>相关新闻</h4>
				<ul>
                    <asp:Repeater id="RelatedNewList" runat="server">
                    <ItemTemplate>
					<li><a href="/news_details/<%#DataBinder.Eval(Container, "DataItem.Url")%>"><%#DataBinder.Eval(Container, "DataItem.Title")%></a></li>
                    </ItemTemplate>
                    </asp:Repeater>
				</ul>
			</div>
		</div>
		<!-- footer start -->
        <!--#include file ="footer.inc"-->
		<!-- footer end -->
	</div>
	</div>
</div>

</body>
</html>
