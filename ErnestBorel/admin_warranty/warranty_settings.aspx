<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="warranty_settings.aspx.cs" Inherits="ErnestBorel.admin_warranty.warranty_settings" %>

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
<link rel="stylesheet" href="../js/jquery-ui-1.11.1/jquery-ui.css" />
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
        <span class="nav">| <a href="warranty_search.aspx">搜索保修记录 Search Warranty Record</a> | <a href="logout.aspx">登出 Logout</a></span>
    </div>
    <h2>高级设置<br>Advance Settings</h2>
    <div>
        <div>
            <h3>下载登记备案记录<br>Download Registration Record</h3>
            <table>
                <tr>
                    <td>从:<br>From:&nbsp;&nbsp;</td>
                    <td><input type="text" name="txtFrom" value="" id="txtFrom" class="datepicker" />&nbsp;&nbsp;</td>
                    <td>至:<br>To:&nbsp;</td>
                    <td><input type="text" name="txtTo" value="" id="txtTo" class="datepicker" />&nbsp;&nbsp;</td>
                    <td><a href="api/getWarrantyRegistration.ashx?From=2014-10-30&To=2015-12-01" class="btn btnDownload">下载Download</a></td>
                </tr>
            </table>
        </div>
        <div>
            <h3>导入保修数据<br>Import Warranty Data</h3>
            <form method="post" id="FormUpload">
                <div id="SettingsGroup">
                    <table>
                        <tr>
                            <td>请选择文件类型：<br>Please select file type: &nbsp;&nbsp;</td>
                            <td><input type="radio" name="SettingType" id="CountryCity" value="CountryCity" /><label for="CountryCity">国家及城市<br>Country / City</label></td>
                            <td><input type="radio" name="SettingType" id="ModelNum" value="ModelNum" /><label for="ModelNum">手表型号<br>Model No</label></td>
                            <td><input type="radio" name="SettingType" id="CaseNum" value="CaseNum" /><label for="CaseNum">壳身编号<br>Case No</label></td>
                            <td><input type="radio" name="SettingType" id="WarrantyNum" value="WarrantyNum" /><label for="WarrantyNum">保修卡编号<br>Warranty Card No</label></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <input type="file" name="FileUpload" id="FileUpload" />
                    <input type="submit" name="btnSubmit" value="上传 Upload" />
                </div>
            </form>
            <div id="consolePanel"></div>
        </div>
    </div>
    <div class="template">
        <div id="dialog-confirm" title="">
          <p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span><span class="txt"></span></p>
        </div>
    </div>
</div>
</body>
</html>
