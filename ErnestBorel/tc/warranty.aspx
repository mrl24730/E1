<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="/warranty.aspx.cs" Inherits="ErnestBorel.warranty" %>

<!DOCTYPE HTML>
<html lang="tc">
<head>
<base href="<%=domain_ch %>/tc/" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="description" content= "瑞士手表品牌—依波路，提供上市文件、公告、財務報告等投資者資訊以依波路為主題的不同分辨率手機壁紙，電腦壁紙的下載。" >
<meta name="keywords" content="依波路,瑞士名表,自動手表,石英手表,計時碼表,奢華腕表,高端品牌,腕表, 紐察圖,瑞士,投資者關係">
<title>保修登記_依波路表官方網站</title>
<!-- For watch detail -->
<!-- panel start -->
<!--#include file ="include.inc"-->
<!-- panel end -->
<link href="/js/jquery-ui-1.11.1/jquery-ui.min.css" rel="stylesheet">
<link href="/js/jquery-ui-1.11.1/jquery-ui.theme.min.css" rel="stylesheet">
<script type="text/javascript" src="/js/jquery-ui-1.11.1/jquery-ui.min.js"></script>
<script type="text/javascript" src="/js/jquery/jquery.cookie.js"></script>
<script type="text/javascript" src="/js/moment.js"></script>
<script type="text/javascript" src="/js/warranty.js?v=43859"></script>
<script type="text/javascript">
    nid = 7;
    subId = 0;
    var countryCityList = <%=warranty_country_list %> ;
</script>
</head>
<body>
	<!-- panel start -->
	<!--#include file ="panel.inc"-->
	<!-- panel end -->
	<div id="doc">
		<div id="docShadow">
			<div id="docWrapper">
				<!-- header start -->
                <!--#include file ="header.inc"-->
		        <!-- header end -->
		
		        <!-- nav start -->
		        <!--#include file ="nav.inc"-->
		        <!-- nav end -->
	<div class="main-content" id="warrantyPage">
        <section>
            <h2>保修登記</h2>
                <!-- Thank you section start -->
                <div id="success-submission">
                    <h3>恭喜您!</h3>
                    <h4>閣下的依波路腕表保修期已成功延長一年。</h4>

                    <div class="certificate">
                        <div class="col">
                            <p>產品資料</p>
                            <hr>
                            <p><span class="title">手表型號</span><span class="ModelNum"> - - - </span></p>
                            <p><span class="title">殼身編號</span><span class="CaseNum"> - - - </span></p>
                            <p><span class="title">保修卡編號</span><span class="WarrantyNum"> - - - </span></p>
                            <p><span class="title">延長後保修限期</span><span class="WarrantyDate"> - - - </span></p>
                        </div>

                        <div class="col right">
                            <p>個人資料</p>
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
                    <input type="button" name="btnSave" value="儲存" id="btnSave" class="btn timesNRoman" />
                    <p>&nbsp;</p>
                    <p>如有任何查詢，請致電中國客戶免費服務熱綫︰400 830 3865<br>
                        如有任何爭議，依波路(遠東)有限公司將保留最終決定權。</p>

                </div>
                <!-- Thank you section end -->


                <div id="step1">
                    完成注冊可延長1年保修期。
                </div>
                <div id="step2" class="hide">
                    請確認以下資料正確，然後提交。
                </div>
                <div id="warrantyForm-wrapper">
                    <form method="post" id="warrantyForm">
                    <div class="wrapper_warranty sep">
                        <div class="model_preview">
                            <img src="/images/watches/noimage/noimage_l.png" alt="Alternate Text" />
                        </div>
                        <h4>產品資料</h4>
                        <table class="tbl_warranty sep">
                            <tr>
                                <th>手表型號 <a class="hints" href="javascript:void(0);" rel="card1">?</a></th>
                                <td>
                                    <div class="editMode">
                                        <input type="text" name="ModelNum" id="ModelNum" value="" maxlength="30">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>殼身編號 <a class="hints" href="javascript:void(0);" rel="card2">?</a></th>
                                <td>
                                    <div class="editMode">
                                        <input type="text" name="CaseNum" id="CaseNum" value="" maxlength="30" placeholder="(可選填)">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>保修卡編號 <a class="hints" href="javascript:void(0);" rel="card3">?</a></th>
                                <td>
                                    <div class="editMode">
                                        <input type="text" name="WarrantyNum" id="WarrantyNum" value="" maxlength="30">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                        <h4>個人資料</h4>
                        <table class="tbl_warranty">
                            <tr>
                                <th>姓名</th>
                                <td>
                                    <div class="editMode">
                                        <input type="text" name="Name" id="Name" value="" class="m-size" maxlength="50">
                                        <input type="hidden" name="Title" id="Title" value="Mr." >
                                        <div class="radio-Gp" data-input="Title">
                                            <a href="javascript:void(0);" data-val="Mr." class="selected"><span></span>先生</a>
                                            <a href="javascript:void(0);" data-val="Mrs."><span></span>太太</a>
                                            <a href="javascript:void(0);" data-val="Ms."><span></span>小姐</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>聯絡電話</th>
                                <td>
                                    <div class="editMode">
                                        <div class="selection ccode">
                                            <select id="Ccode" name="Ccode" data-placeholder="Code" class="select2">
                                                <option value="+86">+86</option>
                                                <option value="+852">+852</option>
                                                <option value="+853">+853</option>
                                            </select>
                                        </div>
                                        <input type="text" name="Phone" id="Phone" value="" maxlength="20">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>電郵地址</th>
                                <td>
                                    <div class="editMode">
                                        <input type="text" name="Email" id="Email" value="" maxlength="50" placeholder="(可選填)">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>購買日期</th>
                                <td>
                                    <div class="editMode">
                                        <input type="text" name="Dop" id="Dop" value="" >
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>購買所在國家</th>
                                <td>
                                    <div class="editMode">
                                        <div class="selection">
                                        <select id="Country" name="Country" data-placeholder="Please select country" class="select2"></select>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>購買所在城市</th>
                                <td>
                                    <div class="editMode">
                                        <div class="selection">
                                        <select id="City" name="City" data-placeholder="Please select city" class="select2"></select>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr id="InvoiceRow">
                                <th>發票號碼</th>
                                <td>
                                    <div class="editMode">
                                        <input type="text" name="InvNum" id="InvNum" value="" maxlength="50" placeholder="(可選填)">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>驗證碼</th>
                                <td>
                                    <div class="editMode">
                                         <input type="text" name="Captcha" id="Captcha" value="" maxlength="20">
                                         <img src="/api/getCaptchaImg.ashx" id="CaptchaImg" />
                                         <img src="/images/warranty/reload.png" id="CaptchaReload" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>&nbsp;</th>
                                <td>
                                    <div class="editMode">
                                        <p><label><input type="checkbox" id="Subscribe" name="Subscribe" value="yes" checked> 我希望收到來自 ErnestBorel 的最新資料。</label></p>
                                        <p><label><input type="checkbox" id="Agree" name="Agree" value="yes"> 我同意收集個人資料相關之 <a href="legalNcopyright.aspx" target="_blank">《安全及私穩》</a> 條款。</label></p>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    <div class="btn-bar">
                        <input type="button" name="btnReset" value="重設" id="btnReset" class="btn timesNRoman" />
                        <input type="button" name="btnNext" value="繼續" id="btnNext" class="btn timesNRoman" />
                        <input type="button" name="btnBack" value="返回" id="btnBack" class="btn timesNRoman" />
                        <input id="btnSubmit" type="submit" name="btnSubmit" value="提交" class="btn" />
                    </div>
                    </form>
                </div>
        </section>
    </div>
	<!-- footer start -->
	<!--#include file ="footer.inc"-->
	<!-- footer end -->
            </div>
        </div> 
    </div>

    <div class="popup hide">
        <div class="card_frame card1 hide">
            <img src="/images/warranty/warranty_card_1.jpg">
            <img src="/images/warranty/arrow_left.png" class="arrow">
        </div>
        <div class="card_frame card2 hide">
            <img src="/images/warranty/warranty_card_2.jpg">
            <img src="/images/warranty/arrow_left.png" class="arrow">
        </div>
        <div class="card_frame card3 hide">
            <img src="/images/warranty/warranty_card_3.jpg">
            <img src="/images/warranty/arrow_left.png" class="arrow">
        </div>
    </div>
</body>
</html>
