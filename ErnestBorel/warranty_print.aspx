<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="warranty_print.aspx.cs" Inherits="ErnestBorel.warranty_print" %>

<!DOCTYPE HTML>
<html lang="sc">
<head>
<base href="<%=domain_cn%>/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="keywords in here">
<meta name="description" content="Description in here">
<meta property="og:title" content="Ernest Borel - Warranty Registration">
<meta property="og:description" content="Description in here">
<title>Ernest Borel - Warranty Registration</title>
<link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/3.9.1/build/cssreset/cssreset-min.css" >
<link rel="stylesheet" type="text/css" href="/css/warranty_print.css" >
<!--[if lt IE 9]>
	<script src="//html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<script>
	document.documentElement.className = document.documentElement.className.replace(/\bno-js\b/g, '') + ' js ';
</script>
<script type="text/javascript" src="/js/jquery/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="/js/jquery/jquery.cookie.js"></script>
<script type="text/javascript" src="/js/warranty_print.js"></script>
<script type="text/javascript" src="/js/moment.js"></script>
</head>

<body>
	<div id="doc">
		<!-- Document Content Start -->
		<img src="/images/warranty/eb_logo.jpg" id="logo">
		<h3>恭喜您!</h3>
        <h4>阁下的依波路腕表保修期已成功延长一年。</h4>
		<div id="info" class="certificate">
            <div class="col">
                <p class="bold">产品资料</p>
                <hr>
                <p><span class="title">手表型号</span><span class="ModelNum"> - - - </span></p>
                <p><span class="title">壳身编号</span><span class="CaseNum"> - - - </span></p>
                <p><span class="title">保修卡编号</span><span class="WarrantyNum"> - - - </span></p>
                <p><span class="title">延长后保修限期</span><span class="WarrantyDate"> - - - </span></p>
            </div>

            <div class="col right">
                <p class="bold">个人资料</p>
                <hr>
                <p><span class="title">姓名</span><span class="Name"> - - - </span></p>
                <p><span class="title">联络电话</span><span class="Phone"> - - - </span></p>
                <p><span class="title">电邮地址</span><span class="Email"> - - - </span></p>
                <p><span class="title">购买日期</span><span class="Dop"> - - - </span></p>
                <p><span class="title">购买所在国家</span><span class="Country"> - - - </span></p>
                <p><span class="title">购买所在城市</span><span class="City"> - - - </span></p>
                <p><span class="title">发票号码</span><span class="InvNum"> - - - </span></p>
            </div>
        </div>
        <p>&nbsp;</p>
        <p class="center">如有任何争议，依波路（远东）有限公司将保留最终决定权。</p>
		<!-- Document Content End -->
	</div>
</body>
</html>
