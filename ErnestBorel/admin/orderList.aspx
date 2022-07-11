<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderList.aspx.cs" Inherits="ErnestBorel.admin.orderList" %>
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
                            <h3 class="text-center">订单列表</h3>
                        </div>
                    </div>
                    <div class="search-wrapper">
                        <div class="row">
                          <div class="col-xs-12 text-left">
                            <table>
                                <tr>
                                    <td>从:<br>From:&nbsp;&nbsp;</td>
                                    <td><input value="" id="target-date-fr" />&nbsp;&nbsp;</td>
                                    <td>至:<br>To:&nbsp;</td>
                                    <td><input value="" id="target-date-to" />&nbsp;&nbsp;</td>
                                    <td><a href="#" id="btn-download" target="_blank"><span class="glyphicon glyphicon-download-alt" aria-hidden="true"></span>下載 Excel</a></td>
                                </tr>
                            </table>
                            <br><br>
                          </div>
                        </div>
                        <div class="row">
                          <div class="col-xs-12 text-left">
                            <label>电邮: </label><input id="target-email">
                            <br><br>
                          </div>
                        </div>
                        <div class="row">
                          <div class="col-xs-12 text-left">
                            <label>公司姓名: </label><input id="target-name">
                            <br><br>
                          </div>
                        </div>
                        <div class="row">
                          <div class="col-xs-12 text-left">
                            <button id="btn-search" name="btn-search">搜索</button>
                            <br><br>
                          </div>
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
                                <th>订单编号</th>
                                <th>电邮</th>
                                <th>公司姓名</th>
                                <th>姓名</th>
                                <th>总数量</th>
                                <th>全单总值(¥)</th>
                                <th>全单减免%</th>
                                <th>折后总值¥</th>
                                <th>日期</th>
                                <th>&nbsp;</th>
                            </tr>
                        </thead>
                        <tbody class="list"></tbody>
                    </table>
                </div>


                <div style="display: none;">
                    <!--
                    <div class="row cell" id="template">
                        <div class="col-xs-12 col-md-1 "><span class="visible-xs-block">訂單編號</span><span class="idx_order"></span></div>
                        <div class="col-xs-12 col-md-3 "><span class="visible-xs-block">電郵</span><span class="email"></span></div>
                        <div class="col-xs-12 col-md-3 "><span class="visible-xs-block">姓名</span><span class="customer_name"></span></div>
                        <div class="col-xs-12 col-md-2 "><span class="visible-xs-block">價格</span><span class="price"></span></div>
                        <div class="col-xs-12 col-md-2 "><span class="visible-xs-block">日期</span><span class="order_date"></span></div>
                        <div class="col-xs-12 col-md-1 "><a href="#" class="btn-detail">詳情</a></div>
                    </div>
                    -->
                    <table class="list-table">
                        <tr id="template">
                            <td class="idx_order">訂單編號</td>
                            <td class="email">電郵</td>
                            <td class="company_name">公司姓名</td>
                            <td class="customer_name">公司姓名</td>
                            <td class="qty">总数量</td>
                            <td class="price">全單總值 ¥</td>
                            <td class="d-price">全單減免 ¥</td>
                            <td class="f-price">折後總值 ¥</td>
                            <td class="order_date">日期</td>
                            <td><a href="#" class="btn-detail">詳情</a></td>
                        </tr>
                    </table>
                </div>
                <p class="no-record-msg text-center">暫時未有任何訂單</p>
                <div class="step detail-wrapper">
                    <div class="row">
                        <div class="col-xs-12">
                            <h3 class="text-center">訂單詳情 [<span class="idx_order"></span>]</h3>
                        </div>
                    </div>
                    <div class="cust-wrapper">
                        <div class="row">
                            <div class="col-xs-12 col-md-3 cust-title">公司資訊</div>
                            <div class="col-xs-12 col-md-9"><span class="company_name"></span></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-md-3 cust-title">客戶名稱</div>
                            <div class="col-xs-12 col-md-9"><span class="customer_name"></span></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-md-3 cust-title">郵箱地址</div>
                            <div class="col-xs-12 col-md-9"><span class="email"></span></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-md-3 cust-title">電話號碼</div>
                            <div class="col-xs-12 col-md-9"><span class="mobile"></span></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <table class="main-table" width="700" align="center" cellspacing="0" cellpadding="0">
                                <tr bgcolor="#cbcbcb">
                                    <td>
                                        <table width="100%" cellspacing="0" cellpadding="10">
                                            <tr>
                                                <td align="left" style="color:#808080; font-weight: bold;">订单编号. <span class="idx_order"></span> </td>
                                                <td align="right" style="color:#808080; font-weight: bold;"><span class="order_date"></span></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tbody class="itemList"></tbody>
                                <tr bgcolor="#FFF">
                                    <td><img src="http://www.ernestborel.cn/images/trans.gif" alt="" height="5" width="5"> </td>
                                </tr>
                                <tr bgcolor="#FFF">
                                    <td>
                                        <table class="summary-table" width="100%" cellspacing="0" cellpadding="10">
                                            <tr style="font-weight: bold; color: #808080">
                                                <td colspan="2" width="40%" align="left">
                                                    <span style="color:#000">订单总结</span>
                                                </td>
                                            </tr>
                                            <tr style="font-weight: bold; color: #808080">
                                                <td>总计</td>
                                                <td align="right">¥<span class="price"></span></td>
                                            </tr>
                                            <tr style="font-weight: bold; color: #808080">
                                                <td>总数量</td>
                                                <td align="right"><span class="total_qty"></span></td>
                                            </tr>
                                            <tr style="font-weight: bold; color: #808080">
                                                <td>折后总计</td>
                                                <td align="right" valign="top">
                                                    -<span class="discount">10</span>%<br>(-¥<span class="diff_price"></span>)
                                                </td>
                                            </tr>
                                        </table>
                                        <hr>
                                        <p align="right" style="font-size: 36px; font-weight: bold">¥<span class="d_price"></span></p>
                                    </td>
                                </tr>
                            </table>

                            <a href="javascript:void(0);" class="btn-back">返回</a>
                        </div>
                    </div>
                </div>
            </div>
            <div style="display: none;">
                <table>
                    <tbody>
                        <tr bgcolor="#FFF" id="temp_div">
                            <td><img src="http://www.ernestborel.cn/images/trans.gif" alt="" height="5" width="5"> </td>
                        </tr>
                        <tr bgcolor="#f0f0f0" id="temp_item">
                            <td>
                                <table width="100%" cellspacing="0" cellpadding="10">
                                    <tr>
                                        <td width="40%" align="right">
                                            <img class="img_path" src="" alt="" height="180" width="180">
                                        </td>
                                        <td style="color:#808080" align="left">
                                            <span class="idx_watch"></span>
                                            <br> 单价: ¥<span class="u_price"></span>
                                            <br> 数量: <span class="qty"></span>
                                            <br> 小计: ¥<span class="subtotal"></span>
                                            <br>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!-- Latest compiled and minified JavaScript -->
            <script src="https://code.jquery.com/jquery-1.12.4.min.js" integrity="sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=" crossorigin="anonymous"></script>
            <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
            <script src="js/common.js"></script>
            <script src="js/moment.js"></script>
            <script src="../js/uifn.js"></script>
            <script src="js/mobiscroll.custom-4.1.1.min.js"></script>
            <script src="js/orderlist.js"></script>
            <script src="js/jquery.tablesorter.min.js"></script>
        </body>

        </html>
