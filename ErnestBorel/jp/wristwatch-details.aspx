﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../wristwatch-details.aspx.cs" Inherits="ErnestBorel.wristwatch_details1" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="Ernest Borel,swiss watch,automatic watch,quartz-movement watch,chrono watch,luxury watch,prestige watch,wrist watch,neuchatel,switzerland">
<meta name="description" content=" Romance in Heart Since 1856. Ernest Borel –  founded in Neuchatel, Switzerland. Exquisitely elegant, admirably designed automatic and quartz-movement wrist watches for men and women.">
<title><%=obj.title %></title>

<!-- For watch detail -->
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript" src="/js/jquery/jquery.elevateZoom.js"></script>
<script type="text/javascript" src="/js/watchdetails.js"></script>
<script type="text/javascript">
    nid = 1;
    subId = 1;
    type = "<%=obj.type%>";
	$(document).ready(function () {
	    watchDetailObj.init();
	});
</script>
</head>
<body>
<!-- panel start -->
<!--#include file ="panel.inc"-->
<!-- panel end -->
<div id="doc" class="watch-collection">
	<div id="docShadow">
	<div id="docWrapper">
		<!-- header start -->
        <!--#include file ="header.inc"-->
		<!-- header end -->
		
		<!-- nav start -->
		<!--#include file ="nav.inc"-->
		<!-- nav end -->
		<div class="main-content norepeatbg" id="watchDetailPage">
			<section>
                <span id="breadcrumbNav"><%=obj.breadcrumb %></span>	
				<div class="yui3-g">
					<h3><%=obj.col_name %></h3>
					<div class="yui3-u-1-3" id="infoWrapper">
						<h4>Model</h4>
						<p class="model">CS86518E-4858BK</p>
						<h4>製品の概要</h4>
						<ul class="spec">
							<li>&nbsp;</li>
						</ul>
						<div class="viewCouple">
							<h4>ペア</h4>
							<a href="javascript:void();" rel=""><img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+M9QDwADhgGAWjR9awAAAABJRU5ErkJggg==" alt=""></a>
							<a href="javascript:void();" rel=""><img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNk+M9QDwADhgGAWjR9awAAAABJRU5ErkJggg==" alt=""></a>
						</div>
					</div>
					<div class="yui3-u-2-3" id="photoWrapper">
						<img id="zoom_photo" src="/images/trans.gif" data-zoom-image="/images/trans.gif"/>
						<div id='photoController'>
							<a href='javascript:void(0)' id='prev' class='prev' title='prev'></a>
							<a href='javascript:void(0)' id='next' class='next' title='next'></a>
						</div>
					</div>
	            </div>
	            <div>
	            	<h4>このシリーズを検索</h4>
	            	<div id="watches">
		            	<div class="slider">
						    <ul>
						    	<asp:Repeater ID="thumbRepeater" runat="server">
								<ItemTemplate>
	                            <li class="thumbnail" rel="" id='thumb_<%#DataBinder.Eval(Container, "DataItem.url_model")%>' data-type="<%#DataBinder.Eval(Container, "DataItem.watch_type")%>">
	                            	<a href="#<%#DataBinder.Eval(Container, "DataItem.url_model")%>" rel="<%#DataBinder.Eval(Container, "DataItem.url_model")%>">
	                            		<img src="/images/watches/<%#DataBinder.Eval(Container, "DataItem.image")%>_t.png" alt="<%#DataBinder.Eval(Container, "DataItem.idx_watch")%>"/>
	                            	</a>
	                            </li>
	                        	</ItemTemplate>
	                            </asp:Repeater>
	                        </ul>
						</div>
						<a href="javascript:void(0);" class="btnPrev"><</a>
	            		<a href="javascript:void(0);" class="btnNext">></a>
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

<div class="hide">
<asp:Repeater ID="infoRepeater" runat="server">
<ItemTemplate>
<div id='<%#DataBinder.Eval(Container, "DataItem.url_model")%>'>
	<h4>Model</h4>
	<p class="model"><%#DataBinder.Eval(Container, "DataItem.idx_watch")%><%#DataBinder.Eval(Container, "DataItem.watch_oldmodel")%></p>
	<h4>製品の概要</h4>
	<ul class="spec">
		<li><%#DataBinder.Eval(Container, "DataItem.watch_spec")%></li>
	</ul>
	<div class="viewCouple">
		<h4>ペア</h4>
		<a class="<%#DataBinder.Eval(Container, "DataItem.matching_male_url")%>" href="javascript:void(0);" rel="<%#DataBinder.Eval(Container, "DataItem.matching_male_url")%>" >
			<img src="/images/watches/<%#DataBinder.Eval(Container, "DataItem.matching_male_url")%>_t.png" alt="<%#DataBinder.Eval(Container, "DataItem.matching_male")%>">
		</a>
		<a class="<%#DataBinder.Eval(Container, "DataItem.matching_female_url")%>" href="javascript:void(0);" rel="<%#DataBinder.Eval(Container, "DataItem.matching_female_url")%>" >
			<img src="/images/watches/<%#DataBinder.Eval(Container, "DataItem.matching_female_url")%>_t.png" alt="<%#DataBinder.Eval(Container, "DataItem.matching_female")%>">
		</a>
	</div>
	<div class="hide">
		<p class="image_url"><%#DataBinder.Eval(Container, "DataItem.image")%></p>
		<p class="matching"><%#DataBinder.Eval(Container, "DataItem.watch_matching")%></p>
	</div>
</div>
</ItemTemplate>
</asp:Repeater>
</div>
</body>
</html>

