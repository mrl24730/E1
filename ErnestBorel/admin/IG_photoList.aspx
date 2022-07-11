<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IG_photoList.aspx.cs" Inherits="ErnestBorel.admin.IG_photoList" %>

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
<link rel="stylesheet" href="/js/jquery-ui-1.11.1/jquery-ui.css" />
<link rel="stylesheet" href="/css/magnific-popup.css" />

<link rel="stylesheet" href="css/admin.css" />
<script type="text/javascript" src="/js/jquery/jquery-1.7.1.min.js"></script>
<script type="text/javascript" src="/js/jquery-ui-1.11.1/jquery-ui.min.js"></script>
<script type="text/javascript" src="/js/imagesloaded.pkgd.min.js"></script>
<script type="text/javascript" src="/js/jquery/jquery.magnific-popup.min.js"></script>
<script>
    document.documentElement.className = document.documentElement.className.replace(/\bno-js\b/g, '') + ' js ';
</script>
<script type="text/javascript" src="js/instagram.admin.class.js"></script>
<script type="text/javascript" src="js/moment.js"></script>
<script type="text/javascript" src="js/common.js"></script>
<script>
    var adminMode = "<%=adminMode%>";
    var system_time = "<%=SystemTime %>";
    window.instagram = new instagramClass();
    $(document).ready(function () {
        instagram.initUI($("#gallery-wrap"));
    });

</script>

</head>
<body>
<div class="ui-widget mainFrame" id="InstagramPage">
    <div id="header">
        <h1>Ernest Borel admin panel </h1>
        <span class="nav"> System time: <span class="system_time">0000</span> | <a href="IR_pressList.aspx">Investor Relation</a> | <a href="IG_photoList.aspx">Instagram</a> | <a href="logout.aspx">Logout</a></span>
    </div>
    <h2>Instagram - Approval management</h2>
    <div id="gallery-wrap" class="clearfix"></div>
    <div id="popup" class="white-popup mfp-hide">
      <div class="img-wrapper"></div>
      Hello...
    </div>
</div>
</body>
</html>
