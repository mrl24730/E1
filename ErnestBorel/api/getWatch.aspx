<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getWatch.aspx.cs" Inherits="ErnestBorel.api.getWatch" %>

<!DOCTYPE HTML>
<!--[if lt IE 7]> <html class="no-js lt-ie9 lt-ie8 lt-ie7" lang="en"> <![endif]-->
<!--[if IE 7]>    <html class="no-js lt-ie9 lt-ie8" lang="en"> <![endif]-->
<!--[if IE 8]>    <html class="no-js lt-ie9" lang="en"> <![endif]-->
<!--[if gt IE 8]><!--> <html class="no-js" lang="en"> <!--<![endif]-->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="keywords in here">
<meta name="description" content="Description in here">
<meta property="og:title" content="title">
<meta property="og:description" content="Description in here">
<title>title</title>
<link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/3.9.1/build/cssreset/cssreset-min.css" >
<style type="text/css">
.tbl td{ padding: 3px; border: 1px solid #CCC;}
</style>
<!--[if lt IE 9]>
	<script src="//html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<script>
	document.documentElement.className = document.documentElement.className.replace(/\bno-js\b/g, '') + ' js ';
</script>
</head>

<body>
	<div id="doc">
		<!-- Document Content Start -->

		<table class="tbl">
			<tr>
				<td>Collection</td>
                <td>Model No.</td>
				<td>spec</td>
			</tr>

            <asp:Repeater ID="repeatSpec" runat="server">
            <ItemTemplate>
            <tr>
                <td><%#DataBinder.Eval(Container, "DataItem.idx_collection")%></td>
				<td><%#DataBinder.Eval(Container, "DataItem.idx_watch")%></td>
				<td><%#DataBinder.Eval(Container, "DataItem.watch_spec")%></td>
			</tr>
            </ItemTemplate>
            </asp:Repeater>
		</table>
		<!-- Document Content End -->
	</div>
</body>
</html>