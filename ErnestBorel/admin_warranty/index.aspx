<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ErnestBorel.admin_warranty.index" %>

<!DOCTYPE HTML>
<!--[if lt IE 7]> <html class="no-js lt-ie9 lt-ie8 lt-ie7" lang="en"> <![endif]-->
<!--[if IE 7]>    <html class="no-js lt-ie9 lt-ie8" lang="en"> <![endif]-->
<!--[if IE 8]>    <html class="no-js lt-ie9" lang="en"> <![endif]-->
<!--[if gt IE 8]><!--> <html class="no-js" lang="en"> <!--<![endif]-->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="keywords" content="keywords in here">
<meta name="description" content="Description in here">
<meta property="og:title" content="Ernest Borel - Admin">
<meta property="og:description" content="Description in here">
<title>Ernest Borel - Admin</title>
<link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/3.9.1/build/cssreset/cssreset-min.css" >
<!--[if lt IE 9]>
    <script src="//html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<link rel="stylesheet" href="../js/jquery-ui-1.11.1/jquery-ui.min.css" />
<link rel="stylesheet" href="../admin/css/admin.css" />
<script type="text/javascript" src="../js/jquery/jquery-1.7.1.min.js"></script>
<script type="text/javascript" src="../js/jquery-ui-1.11.1/jquery-ui.min.js"></script>
<script>
    document.documentElement.className = document.documentElement.className.replace(/\bno-js\b/g, '') + ' js ';
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnSubmit").button();
    });
</script>
</head>

<body>
    <div class="ui-widget mainFrame" id="Login">
        <h1>依波路保修管理平台<br>Ernest Borel Warranty Panel</h1>
        <form id="frmlogin" action="index.aspx?<%=qstring%>" method="post">
            <div class="formline">
                <div class="lbl"><label for="frmUsername">用户名<br>Username: </label></div><div><input id="frmUsername" type="text" name="frmUsername"/></div>
            </div>
            <div class="formline">
                <div class="lbl"><label for="frmPassword">密码<br>Password: </label></div><div><input id="frmPassword" type="password" name="frmPassword"/></div>
            </div>
            <div class="formline">
                <div class="lbl"><input type="submit" value="登录 Login" id="btnSubmit" name="btnSubmit"/></div>
                <div id="msg" style="color:#ff0000;"><%=errmsg%></div>
            </div>
        </form>
    </div>
</body>
</html>
