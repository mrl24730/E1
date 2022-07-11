<%@ Page Language="C#" Inherits="ErnestBorel.Distributor.AfterLoginPage" %>
<!DOCTYPE HTML>
<html lang="en" id="CartPage" class="">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="viewport" content="width=device-width">
    <meta name="keywords" content="keywords in here">
    <meta name="description" content="Description in here">
    <meta property="og:title" content="Brand | Ernest Borel">
    <meta property="og:description" content="Description in here">
    <title>Cart | Ernest Borel</title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="../css/bootstrap.min.css">
    <link rel="stylesheet" href="../css/swiper.min.css">
    <link rel="stylesheet" href="../css/icomoon.css">
    <link rel="stylesheet" href="../css/style.css?v=10003">
    <link rel="stylesheet" href="../css/customer-info.css?v=10003">
    <link rel="stylesheet" href="../css/cart.css?v=10003">
</head>

<body>
    <div class="main-container ">
        <div class="container-fluid px-0">
            <!-- header start -->
            <!--#include file ="header.inc"-->
            <!-- header end -->
        </div>

        <section>
            <div id="breadcrumb-nav" class="container">
                <ol class="breadcrumb">
                    <li>Home</li>
                    <li>My Ernest Borel</li>
                    <li>My Cart</li>
                </ol>
            </div>

            <div class="container customer-container">
                <h3 class="mini-title">CUSTOMER INFORMATION</h3>

                <div class="customer-info__form">
                    <div class="row">
                        <div class="col">
                            <form id="customerForm" action="">
                                <p class="d-block w-100 text-right p-0 customer-info__txt-remark">* All fields are required</p>
                                <input type="text" class="d-block w-100 p-2 my-1" id="company_name" name="company_name" placeholder="Company Name" maxlength="50">
                                <div class="error err_company_empty">Please input</div>
                                <input type="text" class="d-block w-100 p-2 my-1" id="customer_name" name="customer_name" placeholder="Your Name" maxlength="50">
                                <div class="error err_name_empty">Please input</div>
                                <input type="email" class="d-block w-100 p-2 my-1" id="email" name="email" placeholder="Email Address" maxlength="50">
                                <div class="error err_email_empty">Please input</div>
                                <div class="error err_email_wrong">Email address invalid</div>
                                <input type="tel" class="d-block w-100 p-2 my-1" id="mobile" name="mobile" placeholder="Mobile No." maxlength="20">
                                <div class="error err_mobile_empty">Please input</div>
                            </form>
                        </div>
                    </div>
                    <div class="row justify-content-center my-5 customer-btngrp">
                        <div class="col text-center">
                            <a href="javascript:void(0);" class="customer-info__btn-back d-inline-block my-2">BACK TO PREVIOUS STEP</a>
                            <a href="javascript:void(0);" class="customer-info__btn-next d-inline-block my-2">NEXT</a>
                        </div>
                    </div>
                </div>
            </div>


            <div class="container cart-container active">
                <h3 class="mini-title cart-title">MY CART</h3>
                <h3 class="mini-title confirm-title">YOUR ORDER</h3>
                <h3 class="mini-title thx-title">THANK YOU FOR YOUR ORDER</h3>
                <div class="row">
                    <div class="col-lg-8 order-lg-1" >
                        <div class="text-left p-2 mb-2 order-info">
                            INVOICE NO. <span class="invoice-no">123456</span>
                            <span class="float-right">2018-09-14 14:39</span>
                        </div>
                        <div id="itemContainer">
                            <div class="alert item-card mb-3 d-none depth-1" id="itemCardTemp">
                                <button type="button" class="close float-right" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <div class="row align-items-center depth-2">
                                    <div class="col-lg-3 col-md-3 col-sm-4 col-4 img-container text-center">
                                        <a href="#"><img class="img-fluid" src="../images/LS5690_211BK_l.png"></a>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-8 col-8 item-detail">
                                        <p class="order-coll"></p>
                                        <p class="order-model"></p>
                                        <p class="m-0 order-price"></p>
                                        <div class="row py-2">
                                            <div class="col-lg-1 col-md-1 col-sm-1 col-2 order-qty">
                                                Qty:
                                            </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 col-6 ml-1">
                                                <!-- <select class="qty-select"></select> -->
                                                <input type="number" class="qty-select text-center" name="qty-select" max="999" min="0" maxlength="3">
                                            </div>
                                        </div>
                                        <p class="order-subtotal">SUB-TOTAL: <span class="itemTotal"></span></p>
                                    </div>
                                </div>
                            </div>

                            <p class="px-2 empty-cart__desc d-none">You don't have anything in your cart, keep shopping!</p>
                        </div>

                    </div>

                    <div class="col-lg-4 order-lg-2 px-0" id="totalContainer">
                      <div class="total-card pb-2 mb-3">
                        <p class="total-title m-0">Order Summary</p>
                        <form id="totalCardTemp">
                            <div class="form-group d-flex justify-content-between total-subtotal">
                                <label class="col-form-label">Total</label>
                                <div class="w-50">
                                    <input type="text" class="form-control-plaintext text-right" id='subTotal' value="$0" readonly>
                                </div>
                            </div>
                            <div class="form-group d-flex justify-content-between total-discount">
                                <label class="col-form-label" for="discount">Discount</label>
                                <div class="text-right">
                                    <span class="total-discount"><input type="text" maxlength="2" name="discount" id="discount" placeholder="Example: 20" value="0"><span id='testing'>&nbsp;%</span></span>
                                    <span class="confirm-discount">(<span class="confirm-discount__p"></span>%) off<br><span class="confirm-discount__amount"></span></span>
                                </div>
                            </div>
                            <div class="form-group total-discounted">
                                <p class="text-right"></p>
                            </div>
                            <hr>
                            <p class="total text-right" id="total"></p>

                            <div class="px-2">
                                <p class="total-decor">*Please refer to the contract discount rate signed between you and Ernest Borel</p>
                                <div class="cart-btngrp">
                                  <a href="javascript:void(0);" class="d-block text-center save-btn"><span class="save active">SAVE FOR LATER</span><span class="saving">SAVING</span><span class="saved">SAVED!</span></a>
                                  <a href="javascript:void(0);" class="d-block text-center next-btn">NEXT</a>
                                </div>
                                <div class="confirm-btngrp">
                                  <a href="javascript:void(0);" class="d-block text-center prev-btn">BACK TO PREVIOUS STEP</a>
                                  <a href="javascript:void(0);" class="d-block text-center confirm-btn">CONFIRM YOUR ORDER</a>
                                </div>
                                <div class="thx-btngrp">
                                  <a href="home.aspx" class="d-block text-center prev-btn">BACK TO HOME</a>
                                </div>
                            </div>
                        </form>
                      </div>
                    </div>

                </div>

            </div>

        </section>

        <!-- header start -->
        <!--#include file ="footer.inc"-->
        <!-- header end -->

    </div>

    <!-- Latest compiled and minified JavaScript -->
    <script src="../js/jquery-3.3.1.min.js"></script>
    <script src="../js/popper.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/uifn.js?v=10003"></script>
    <script src="../js/cart.js?v=10003"></script>
</body>

</html>
