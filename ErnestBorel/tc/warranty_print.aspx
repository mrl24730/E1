<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="/warranty_print.aspx.cs" Inherits="ErnestBorel.warranty_print" %>

<!DOCTYPE HTML>
<html lang="tc">
<head>
<base href="<%=domain_ch%>/tc/" />
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
<script type="text/javascript" src="/js/moment.js"></script>
<script type="text/javascript" src="/js/warranty_print.js"></script>
</head>

<body>
	<div id="doc">
		<!-- Document Content Start -->
		<img src="/images/warranty/eb_logo.jpg" id="logo">
		<h3>恭喜您!</h3>
        <h4>閣下的依波路腕表保修期已成功延長一年。</h4>
		<div id="info" class="certificate">

            <div class="col">
                <p class="bold">產品資料</p>
                <hr>
                <p><span class="title">手表型號</span><span class="ModelNum"> - - - </span></p>
                <p><span class="title">殼身編號</span><span class="CaseNum"> - - - </span></p>
                <p><span class="title">保修卡編號</span><span class="WarrantyNum"> - - - </span></p>
                <p><span class="title">延長後保修限期</span><span class="WarrantyDate"> - - - </span></p>
            </div>

            <div class="col right">
                <p class="bold">個人資料</p>
                <hr>
                <p><span class="title">姓名</span><span class="Name"> - - - </span></p>
                <p><span class="title">聯絡電話</span><span class="Phone"> - - - </span></p>
                <p><span class="title">電郵地址</span><span class="Email"> - - - </span></p>
                <p><span class="title">購買日期</span><span class="Dop"> - - - </span></p>
                <p><span class="title">購買所在國家</span><span class="Country"> - - - </span></p>
                <p><span class="title">購買所在城市</span><span class="City"> - - - </span></p>
                <p><span class="title">發票號碼</span><span class="InvNum"> - - - </span></p>
            </div>
        </div>
        <p>&nbsp;</p>
        <p class="center">如有任何爭議，依波路(遠東)有限公司將保留最終決定權。</p>
		<!-- Document Content End -->
	</div>
</body>
</html>
