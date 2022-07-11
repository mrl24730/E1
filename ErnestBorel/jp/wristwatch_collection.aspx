<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../wristwatch_collection.aspx.cs" Inherits="ErnestBorel.wristwatch_collection" %>

<!DOCTYPE HTML>
<html lang="jp">
<head>
<base href="<%=domain_ch %>/jp/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="Ernest Borel,swiss watch,automatic watch,quartz-movement watch,chrono watch,luxury watch,prestige watch,wrist watch,neuchatel,switzerland">
    <meta name="description" content=" Romance in Heart Since 1856. Ernest Borel –  founded in Neuchatel, Switzerland. Exquisitely elegant, admirably designed automatic and quartz-movement wrist watches for men and women.">
<title>Ernest Borel - 最新製品</title>
<!-- For watch detail -->
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<script type="text/javascript">
    nid = 1;
    subId = 1;
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
		<div class="header-content">
        	<%=collectionDetails.hero_image %>
        	<p><%=collectionDetails.name %></p>
        </div>
		<div class="main-content norepeatbg" id="watchCollectionPage">
			<section>
			<div class="yui3-g">
				<div class="yui3-u-1-3" id="collectionWrapper">
					<h3><%=collectionDetails.type_name%></h3>
                    <asp:Repeater ID="colRepeater" runat="server">
                    <ItemTemplate>
                        <p><a href="wristwatch_collection/<%=type %>/<%#DataBinder.Eval(Container, "DataItem.col_ref")%>/"><%#DataBinder.Eval(Container, "DataItem.col_name")%></a></p>
                    </ItemTemplate>
                    </asp:Repeater>
				</div>
				<div class="yui3-u-2-3">
					<h3><%=collectionDetails.name %></h3>
					<p><%=collectionDetails.desc %></p>
					<div class="yui3-g">
						<asp:Repeater ID="watchRepeater" runat="server">
						<ItemTemplate>
						<div class="watch yui3-u-1-2">
							<a href="wristwatch/<%=type %>/<%=col_ref%>/#<%#DataBinder.Eval(Container, "DataItem.url_model")%>"><img src="/images/watches/<%#DataBinder.Eval(Container, "DataItem.image")%>_s.png"></a>
							<span><%#DataBinder.Eval(Container, "DataItem.idx_watch")%></span>
						</div>
						</ItemTemplate>
                    	</asp:Repeater>
					</div>
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
	<div id="template" class="collection">
		<a href="wristwatch/automatic/0/" class="name">name</a>
	</div>
</div>
</body>
</html>
