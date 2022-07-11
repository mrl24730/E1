<%@ Page Language="C#" Inherits="ErnestBorel.Distributor.AfterLoginPage" %>
<!DOCTYPE HTML>
<html lang="en" id="CollecPage">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="viewport" content="width=device-width">
<meta name="keywords" content="keywords in here">
<meta name="description" content="Description in here">
<meta property="og:title" content="Collection | Ernest Borel">
<meta property="og:description" content="Description in here">
<title>Collection | Ernest Borel</title>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="../css/bootstrap.min.css">
<link rel="stylesheet" href="../css/swiper.min.css">
<link rel="stylesheet" href="../css/icomoon.css">
<link rel="stylesheet" href="../css/style.css?v=10007">
<link rel="stylesheet" href="../css/collection.css?v=10007">
</head>

<body>
    <div class="main-container">
        <div class="container-fluid px-0">
            <!-- header start -->
            <!--#include file ="header.inc"-->
            <!-- header end -->
        </div>

        <section>
            <!-- filter start -->
            <!--#include file ="filter.inc"-->
            <!-- filter end -->

            <div id="breadcrumb-nav" class="container">
                <ol class="breadcrumb">
                    <li>Home</li>
                    <li>Watches</li>
                    <li class="bc-section">Collection</li>
                    <li class="bc-search-result d-none">Search Result</li>
                </ol>
            </div>

            <div class="container">
                <h3 class="mini-title d-none"></h3>
                <div class="brand-desc text-lg-center d-none"></div>
                <div class="watch-container text-center" id="collContainer">
                    <div class="row">

                        <div class="col-lg-3 col-6 d-none" id="collTemp">
                            <a href="#"><img class="img-fluid product-img" src="../images/GB5630_4621_l.png"></a>
                            <p class="product-type m-0 pt-4">Automatic</p>
                            <p class="product-coll m-0">Heritage Collection</p>
                            <p class="product-model m-0">GS5690-211BK</p>
                            <p class="product-price m-0">$12,600</p>
                            <button class="btn btn-add mt-2">ADD TO CART</button>
                        </div>

                        <!-- <div class="col-lg-3 col-6">
                            <a href="#"><img class="img-fluid product-img" src="../images/GS5690_211BK_l.png"></a>
                            <p class="product-type m-0 pt-4">Automatic</p>
                            <p class="product-coll m-0">Heritage Collection</p>
                            <p class="product-model m-0">GS5690-211BK</p>
                            <p class="product-price m-0">$12,600</p>
                            <button class="btn mt-2">ADD TO CART</button>
                        </div>

                        <div class="col-lg-3 col-6">
                            <a href="#"><img class="img-fluid product-img" src="../images/GB5630_4621_l.png"></a>
                            <p class="product-type m-0 pt-4">Automatic</p>
                            <p class="product-coll m-0">Heritage Collection</p>
                            <p class="product-model m-0">GS5690-211BK</p>
                            <p class="product-price m-0">$12,600</p>
                            <button class="btn mt-2">ADD TO CART</button>
                        </div>

                        <div class="col-lg-3 col-6">
                            <a href="#"><img class="img-fluid product-img" src="../images/GS5690_211BK_l.png"></a>
                            <p class="product-type m-0 pt-4">Automatic</p>
                            <p class="product-coll m-0">Heritage Collection</p>
                            <p class="product-model m-0">GS5690-211BK</p>
                            <p class="product-price m-0">$12,600</p>
                            <button class="btn mt-2">ADD TO CART</button>
                        </div> -->

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
<script src="../js/swiper.min.js"></script>
<script src="../js/uifn.js?v=10007"></script>
<script src="../js/collection.js?v=10007"></script>
<script src="../js/filter.js?v=10007"></script>
</body>

</html>
