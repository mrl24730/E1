<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IR_pressList.aspx.cs" Inherits="ErnestBorel.admin.IR_pressList" %>
<%@ Import Namespace="ErnestBorel.admin" %>  

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
<link rel="stylesheet" type="text/css" href="css/pagination.css">
<link rel="stylesheet" href="css/admin.css" />
<script type="text/javascript" src="/js/jquery/jquery-1.7.1.min.js"></script>
<script type="text/javascript" src="/js/jquery-ui-1.11.1/jquery-ui.min.js"></script>
<script type="text/javascript" src="/js/jquery/jquery.pagination.js"></script>
<script>
    document.documentElement.className = document.documentElement.className.replace(/\bno-js\b/g, '') + ' js ';
</script>
<script type="text/javascript" src="js/moment.js"></script>
<script type="text/javascript" src="js/common.js"></script>
<script type="text/javascript" src="js/list.js"></script>
<script type="text/javascript">
    fullList = <%=output%>;
    obj = fullList;
    ttl = obj.length;
    status_all = <%=cntAllReleases%>;
    status_published = <%=cntPublished%>;
    status_pending = <%=cntPending%>;
    status_withheld = <%=cntWithheld%>;
    system_time = "<%=SystemTime %>";
</script>
</head>
<body>
<div class="ui-widget mainFrame" id="ListPage">
    <div id="header">
        <h1>Ernest Borel admin panel </h1>
        <span class="nav"> System time: <span class="system_time">0000</span> | <a href="IR_pressList.aspx">Investor Relation</a> | <a href="logout.aspx">Logout</a></span>
    </div>
    <h2>Investor Relation - Announcement management</h2>
    <div id="toolbar">
        <div>
            <select id="selectmenu">
    	        <option value="all" id="all" selected>All</option>
    	        <option value="published" id="published">Published</option>
                <option value="pending" id="pending">Pending</option>
    	        <option value="withheld" id="withheld">Withheld</option>
            </select>
        </div>
        <div>
            <input type="button" value="Create New" id="btnCreate" name="btnCreate" />
        </div>
    </div>

    <table id="tbl">
        <thead>
            <td>#</td>
            <td>Release Date</td>
            <td>Status</td>
            <td style="width:400px">Title</td>
            <td>Language</td>
            <td>Last Updated</th>
        </thead>
        <tbody id="content"></tbody>
    </table>

    <div id="pagination"></div>
</div>

<div class="display:none">
    <table>
        <tr title="Click to view detail" id="template" data-id="0" class="irRecord" >
            <td class="no"></td>
            <td class="release"></td>
            <td class="status"></td>
            <td class="title"></td>
            <td class="lang"></td>
            <td class="update"></td>
        </tr>
    </table>
</div>
</body>
</html>
