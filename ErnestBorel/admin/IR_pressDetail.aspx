<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IR_pressDetail.aspx.cs" Inherits="ErnestBorel.admin.IR_pressDetail" %>
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
<link rel="stylesheet" href="/js/jquery-ui-1.11.1/jquery-ui.min.css" />
<link rel="stylesheet" href="css/admin.css" />
<script>
    document.documentElement.className = document.documentElement.className.replace(/\bno-js\b/g, '') + ' js ';
</script>
<script type="text/javascript" src="../js/jquery/jquery-1.7.1.min.js"></script>
<script type="text/javascript" src="../js/jquery-ui-1.11.1/jquery-ui.min.js"></script>
<script type="text/javascript" src="js/detail.js"></script>
<script type="text/javascript" src="js/moment.js"></script>
<script type="text/javascript" src="js/common.js"></script>
<script type="text/javascript">
    isNew = ("<%=isNew %>" == "True")? true: false;
    isWithdrawn = ("<%=isWithdrawn %>" == "True")? true: false;
    recordID = "<%=masterRec_idx%>";
    obj = <%=outputString%>;   
    system_time = "<%=SystemTime %>";
</script>
</head>
<body>
<div class="ui-widget mainFrame" id="DetailPage">
    <div id="header">
        <h1>Ernest Borel admin panel </h1>
        <span class="nav"> System time: <span class="system_time">0000</span> | <a href="IR_pressList.aspx">Investor Relation</a> | <a href="logout.aspx">Logout</a></span>
    </div>
    <h2>Investor Relation - New Announcement</h2>

    <div>
        <input type="button" value="Save" id="btnSave" style="display:none;"/>
        <input type="button" value="Edit" id="btnEdit"/>
        <input type="button" value="Reactivate" id="btnReactivate"/>
        <input type="button" value="Withdraw" id="btnWithdraw"/>
        <input type="button" value="Back" id="btnCancel" onclick="window.location='IR_pressList.aspx'"/>
    </div>

    <form id="frm_detail" name="frm_detail" action="ir_pressDetail.aspx" method="post" enctype="multipart/form-data">
    <input type="hidden" id="input_recIdx" name="input_recIdx" value="0" />
    <input type="hidden" id="input_withdraw" name="input_withdraw" value="0" />
    <input type="hidden" id="input_reactivate" name="input_reactivate" value="0" />
    <input type="hidden" id="isNewRec" name="isNewRec" value="new" />
    <div id="main">
        <h3>Basic info:</h3>
        <table id="ir_detail">
            <tr>
                <td>Release Date: </td>
                <td>
                    <div id="edit_releaseDate" style="display:none;">
                        <input type="text" id="input_releaseDate" name="input_releaseDate" onclick="$('#input_releaseDate').datepicker({ dateFormat: 'yy-mm-dd' });$('#input_releaseDate').datepicker('show');" value="<%=str_releaseDate %>" />&nbsp;
                        <input type="text" id="spinnerHH" name="spinnerHH" value="<%=ir_masterRec.ir_releaseDate.ToString("HH")%>" />:
                        <input type="text" id="spinnerMM" name="spinnerMM" value="<%=ir_masterRec.ir_releaseDate.ToString("mm")%>" />
                    </div>
                    <div id="disp_releaseDate" style="display:block;"></div>
                </td>
            </tr>
            <tr>
                <td>Status: </td>
                <td>
                    <span id="ir_status"></span>
              </td>
            </tr>
            <tr>
                <td>Last Updated: </td>
                <td><span id="ir_lastUpdated"></span></td>
            </tr>
        </table>
        
        <h3>Document list:</h3>
        <div class="toolbar">
            <div>
            <select id="sel_lang">
                <option value="">--Select Language--</option>
                <option value="en">English</option>
                <option value="tc">Traditional Chinese</option>
                <option value="sc">Simplified Chinese</option>
                <option value="fr">French</option>
                <option value="jp">Japanese</option>
            </select>
            </div>
            <div><input type="button" value="&#9668; Add document for this language" id="btnAddLang"/></div>
        </div>
        <div id="submitFrm"></div>
    </div>
    </form>
</div>



<!-- base input form -->
<div style="display:none;">
    <div id="baseFrm" class="recordCell">
        <input type="hidden" id="rec_idx"  name="rec_idx" value=""/>
        <table id="releaseDetail" class="releaseDetail">
        <tr><td>Language: </td><td><input id="input_lang" name="input_lang" type="hidden" value=""/><span id="span_lang"></span></td></tr>
        <tr><td>Release Title: </td><td><input id="input_title" name="input_title" type="text" value="" placeholder="Enter Title Here" style="width:50em;"/></td></tr>
        <tr><td>Description: </td><td><input id="input_desc" name="input_desc" type="text" value="" placeholder="Enter Description Here" style="width:50em;"/></td></tr>
        <tr><td>PDF File: </td><td><input id="input_file" name="input_file" type="file" /></td></tr>
        </table>
    </div>

    <div id="recordTemplate" class="recordCell">
        <input type="hidden" id="rec_idx"  name="rec_idx" value=""/>
        <table id="releaseDetail" class="releaseDetail">
            <tr>
                <td>Language: </td>
                <td><input id="input_lang" name="input_lang" type="hidden" value=""/><span id="span_lang"></span></td>
            </tr>
            <tr>
                <td>Release Title: </td>
                <td>
                    <div id="disp_title"></div>
                    <div id="edit_title" style="display:none;">
                        <input id="input_title" name="input_title" type="text" value="" placeholder="Enter Title Here" style="width:50em;"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td>Description: </td>
                <td>
                    <div id="disp_desc"></div>
                    <div id="edit_desc" style="display:none;"><input id="input_desc" name="input_desc" type="text" value="" placeholder="Enter Description Here" style="width:50em;"/></div>
                </td>
            </tr>
            <tr>
                <td>PDF File: </td>
                <td>
                    <div><a href="/pdf/" id="disp_path"></a></div>
                    <div id="edit_file" style="display:none;" ><input id="input_file" name="input_file" type="file" value="" /></div>
                    <input id="org_filename" name="org_filename" type="hidden" value="" />
                    <input id="org_filesize" name="org_filesize" type="hidden" value="" />
                </td>
            </tr>
        </table>
    </div>
</div>

</body>
</html>

