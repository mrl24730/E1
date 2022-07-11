<%@ Page Language="C#" Inherits="ErnestBorel.Distributor.AfterLoginPage" %>
<!DOCTYPE HTML>
<html lang="en" id="HomePage">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="viewport" content="width=device-width">
<meta name="keywords" content="keywords in here">
<meta name="description" content="Description in here">
<meta property="og:title" content="Home | Ernest Borel">
<meta property="og:description" content="Description in here">
<title>Home | Ernest Borel</title>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="../css/bootstrap.min.css">
<link rel="stylesheet" href="../css/swiper.min.css">
<link rel="stylesheet" href="../css/icomoon.css">
<link rel="stylesheet" href="../css/style.css?v=10007">
<link rel="stylesheet" href="../css/home.css?v=10007">
</head>

<body>
    <div class="main-container ">
        <div class="container-fluid px-0">
            <!-- header start -->
            <!--#include file ="header.inc"-->
            <!-- header end -->

            <div id="home__hero-wrapper" class="swiper-container">
                <div class="swiper-wrapper">
                    <div class="swiper-slide">
                        <a href="#">
                          <img class="img-fluid d-none d-md-block" src="../images/2_index_banner_1.jpg">
                          <img class="img-fluid d-md-none" src="http://via.placeholder.com/750x500">
                        </a>
                    </div>
                    <div class="swiper-slide">
                      <a href="#">
                        <img class="img-fluid d-none d-md-block" src="../images/2_index_banner_1.jpg">
                        <img class="img-fluid d-md-none" src="http://via.placeholder.com/750x500">
                      </a>
                    </div>
                    <div class="swiper-slide">
                      <a href="#">
                        <img class="img-fluid d-none d-md-block" src="../images/2_index_banner_1.jpg">
                        <img class="img-fluid d-md-none" src="http://via.placeholder.com/750x500">
                      </a>
                    </div>
                </div>
            </div>
        </div>

        <div id="bannerBullets" class="swiper-pagination banner-pagination"></div>

        <div id="collection" class="container">
            <div class="row">
                <div class="col-md-4">
                  <a href="collection_list.aspx?f=couple">
                    <img class="img-fluid" src="../images/0_watch_romantic_m.jpg">
                    <p class="text-center text-lg-left pt-2">ROMANTIC SERIES</p>
                  </a>
                </div>
                <div class="col-md-4">
                  <a href="collection_list.aspx?f=lady">
                    <img class="img-fluid" src="../images/0_watch_feminine_m.jpg">
                    <p class="text-center text-lg-left pt-2">FEMININE SERIES</p>
                  </a>
                </div>
                <div class="col-md-4">
                  <a href="collection_list.aspx?f=casual">
                    <img class="img-fluid" src="../images/0_watch_causal_m.jpg">
                    <p class="text-center text-lg-left pt-2">CASUAL SERIES</p>
                  </a>
                </div>
            </div>
        </div>

        <div class="swiper-slide d-none" id="swiperSlideTemp">
            <a href="#"><img class="img-fluid product-img" src=""></a>
            <p class="product-type"></p>
            <p class="product-coll"></p>
            <p class="product-model"></p>
            <p class="product-price"></p>
            <button class="btn d-none d-md-block mt-2">ADD TO CART</button>
        </div>

        <section id="new-product">
            <div class="container text-center" >
                <h2>NEW PRODUCTS</h2>
                <div id="home__product-wrapper" class="swiper-container">
                    <div class="swiper-wrapper">

                    </div>
                </div>
                <div id="productBullets" class="swiper-pagination"></div>
                <div class="swiper-button-prev"></div>
                <div class="swiper-button-next"></div>
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
<script src="../js/swiper.min.js"></script>
<script src="../js/uifn.js?v=10007"></script>
<script src="../js/home.js?v=10007"></script>
</body>

</html>
