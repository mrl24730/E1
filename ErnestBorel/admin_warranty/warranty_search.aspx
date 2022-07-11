<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="warranty_search.aspx.cs" Inherits="ErnestBorel.admin_warranty.warranty_search" %>

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
<link rel="stylesheet" href="css/admin_warranty.css" />
<script type="text/javascript" src="../js/jquery/jquery-1.7.1.min.js"></script>
<script type="text/javascript" src="../js/jquery-ui-1.11.1/jquery-ui.min.js"></script>
<script type="text/javascript" src="js/admin_warranty_uifn.js"></script>
<script>
    document.documentElement.className = document.documentElement.className.replace(/\bno-js\b/g, '') + ' js ';
</script>
</head>
<body>
<div class="ui-widget mainFrame WarrantyPage" id="ListPage">
    <div id="header">
        <h2>依波路保修管理平台<br>Ernest Borel admin panel </h2>
        <span class="nav"> <% if(isAdmin) { %> | <a href="warranty_settings.aspx">高级设置 Advance Setting</a><% } %> | <a href="logout.aspx">登出 Logout</a></span>
    </div>
    <h2>搜索保修记录<br>Search Warranty Record</h2>
    <form method="post" id="FormSearch">
    <table class="tbl_search">
        <tr>
            <th>保修卡编号<br>Warranty Card No.</th>
            <td><input type="text" name="WarrantyNum" id="WarrantyNum" /></td>
        </tr>
        <tr>
            <th>壳身编号<br>Case No.</th>
            <td><input type="text" name="CaseNum" id="CaseNum" /></td>
        </tr>
            <th>联络电话<br>Phone No.</th>
            <td><input type="text" name="Phone" id="Phone" /></td>
        <tr>
            <th>&nbsp;</th>
            <td><input type="submit" value="搜索Search" /></td>
        </tr>
    </table>

    </form>
    <div class="result_wrapper">
        
        <table class="tbl_searchResult">
            <thead>
            <tr>
                <th>手表型号<br>Model No.</th>
                <th>壳身编号<br>Case No.</th>
                <th>保修卡编号<br>Warranty Card No.</th>
                <th>姓名<br>Name</th>
                <th>联络电话<br>Phone</th>
                <th>电邮地址<br>Email</th>
                <th>购买日期<br>Date of purchase</th>
                <th>发票号码<br>Invoice No.</th>
                <th>国家及城市<br>Country / City</th>
                <th>延长后保修限期<br>Extended Warranty</th>
                <th>登记日期<br>Date Online Registration </th>
            </tr>
            </thead>
            <tbody>

            </tbody>
        </table>
        <div id="message"></div>
    </div>
    <div class="template">
        <table>
            <tr>
                <td class="ResModelNum"></td>
                <td class="ResCaseNum"></td>
                <td class="ResWarrantyNum"></td>
                <td class="ResName"></td>
                <td class="ResPhone"></td>
                <td class="ResEmail"></td>
                <td class="ResDop"></td>
                <td class="ResInvNum"></td>
                <td class="ResCountryCity"></td>
                <td class="ResExtendedDate"></td>
                <td class="ResCreateDate"></td>
            </tr>
        </table>
    </div>
</div>
</body>
</html>