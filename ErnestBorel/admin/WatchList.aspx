<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WatchList.aspx.cs" Inherits="ErnestBorel.admin.WatchList" %>

<!DOCTYPE HTML>
<!--[if lt IE 7]> <html class="no-js lt-ie9 lt-ie8 lt-ie7" lang="en"> <![endif]-->
<!--[if IE 7]>    <html class="no-js lt-ie9 lt-ie8" lang="en"> <![endif]-->
<!--[if IE 8]>    <html class="no-js lt-ie9" lang="en"> <![endif]-->
<!--[if gt IE 8]><!--> <html class="no-js" lang="en"> <!--<![endif]-->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="keywords in here">
<meta name="description" content="Description in here">
<meta property="og:title" content="Watch List">
<meta property="og:description" content="Description in here">
<title>Watch List</title>
<link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/3.9.1/build/cssreset/cssreset-min.css" >
<style type="text/css">
html, body { font-family: "Microsoft JhengHei","SimHei", Arial; font-size: 18px;}
table{ width: 400px; margin: 0 auto;}
table td{ border: 1px solid #CCC; padding: 8px;}
table img{ width: 72px;}
.txt-center{ text-align: center;}
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

		<table>
			<tr>
				<td>ID</td>
				<td>Type</td>
				<td>Collection</td>
				<td>Model Number</td>
				<td class="txt-center">Image</td>
			</tr>
			<tbody id="list"></tbody>
		</table>

		<div style="display:none;">
			<table>
				<tr id="template">
					<td class="id">0</td>
					<td class="type">--</td>
					<td class="collection">--</td>
					<td class="model">--</td>
					<td class="image txt-center">
						<img src="../images/watches/noimage/noimage_l.png">
					</td>
				</tr>
			</table>
		</div>
		<!-- Document Content End -->
	</div>
	<script   src="https://code.jquery.com/jquery-1.12.4.min.js"   integrity="sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ="   crossorigin="anonymous"></script>
	<script type="text/javascript">
	var list = <%=output%>;
	
	$(document).ready(function(){
		for(var i = 0; i< list.length; i++){
			var cell = $("#template").clone();
			var type = (list[i].idx_collection == 1)? "automatic" : "quartz";
			cell.attr("id", "w"+i);
			cell.find(".id").text( (i+1) );
			cell.find(".type").text(type);
			cell.find(".collection").text(list[i].collection);
			cell.find(".model").text(list[i].id);
			var image = (list[i].id).replace("-", "_");
			image = "../images/watches/" + type + "/"+image+"_t.png";
			cell.find("img").attr("src", image);
			$("#list").append(cell);
		}
	});
	</script>
</body>
</html>
