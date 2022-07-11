<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customerList.aspx.cs" Inherits="ErnestBorel.admin.customerList" %>
<%@ Import Namespace="ErnestBorel.admin" %>
        <!DOCTYPE HTML>
        <html lang="en">

        <head>
            <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
            <meta name="keywords" content="keywords in here">
            <meta name="description" content="Description in here">
            <meta property="og:title" content="Ernest Borel - 管理員系統">
            <meta property="og:description" content="Description in here">
            <title>Ernest Borel - 管理員系統</title>
            <!-- Latest compiled and minified CSS -->
            <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
            <link rel="stylesheet" href="css/admin.css" />
            <link rel="stylesheet" href="css/mobiscroll.custom-4.1.1.min.css">
            <link rel="stylesheet" href="css/order.css" />
            <script>
            var system_time = "<%=SystemTime %>";
            </script>
        </head>

        <body>
            <div class="container main-container">
                <div class="row">
                    <div class="col-xs-12">
                        <div id="header">
                            <h1 style="display: block; width: 100%">Ernest Borel 管理员系统 </h1>
                            <span class="nav" style="display: block; width: 100%; text-align: right"><a href="customerList.aspx">客戶列表</a> | <a href="orderList.aspx">订单列表</a> | <a href="logout.aspx">登出</a></span>
                            <p style="font-size: 13px; text-align: right;">系统时间: <span class="system_time"></span></p>
                        </div>
                    </div>
                </div>
                <div class="step list-wrapper active">
                    <div class="row">
                        <div class="col-xs-12">
                            <h3 class="text-center">客戶列表</h3>
                        </div>
                        <div class="col-xs-12">
                            <a href="/admin/api/downloadcustomerlist.ashx" id="btn-download" target="_blank"><span class="glyphicon glyphicon-download-alt" aria-hidden="true"></span>下載 Excel</a>
                            <br><br>
                        </div>
                    </div>
                    
                    <!--
                    <div class="row table-head hidden-xs">
                        <div class="col-xs-12 col-md-1">訂單編號</div>
                        <div class="col-xs-12 col-md-3">電郵</div>
                        <div class="col-xs-12 col-md-3">姓名</div>
                        <div class="col-xs-12 col-md-3">折扣</div>
                        <div class="col-xs-12 col-md-2">價格</div>
                        <div class="col-xs-12 col-md-2">日期</div>
                        <div class="col-xs-12 col-md-1">&nbsp;</div>
                    </div>
                    <div class="list"></div>
                    -->
                    <table class="list-table" width="100%" id="sorting-table">
                        <thead>
                            <tr>
                                <th>公司</th>
                                <th>姓名</th>
                                <th>电邮</th>
                                <th>電話</th>
                                <th>登記日期</th>
                            </tr>
                        </thead>
                        <tbody class="list"></tbody>
                    </table>
                </div>


                <div style="display: none;">
                    <table class="list-table">
                        <tr id="template">
                            <td class="company_name"></td>
                            <td class="customer_name"></td>
                            <td class="email"></td>
                            <td class="mobile"></td>
                            <td class="created_at"></td>
                        </tr>
                    </table>
                </div>
                <p class="no-record-msg text-center">暫時未有任何客戶登記</p>
            </div>
            
            <!-- Latest compiled and minified JavaScript -->
            <script src="https://code.jquery.com/jquery-1.12.4.min.js" integrity="sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=" crossorigin="anonymous"></script>
            <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
            <script src="js/common.js"></script>
            <script src="js/moment.js"></script>
            <script src="../js/uifn.js"></script>
            <script src="js/mobiscroll.custom-4.1.1.min.js"></script>
            <script src="js/jquery.tablesorter.min.js"></script>
            <script src="js/customerlist.js?v=3823"></script>
        </body>

        </html>
