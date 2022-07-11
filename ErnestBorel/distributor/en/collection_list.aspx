<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="../sc/collection_list.aspx.cs" Inherits="ErnestBorel.distributor.sc.collection_list" %>
<!DOCTYPE HTML>
<html lang="en" id="CollectionList">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<meta name="viewport" content="width=device-width">
<meta name="keywords" content="keywords in here">
<meta name="description" content="Description in here">
<meta property="og:title" content="Brand | Ernest Borel">
<meta property="og:description" content="Description in here">
<title>Brand | Ernest Borel</title>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="../css/bootstrap.min.css">
<link rel="stylesheet" href="../css/swiper.min.css">
<link rel="stylesheet" href="../css/icomoon.css">
<link rel="stylesheet" href="../css/style.css?v=10007">
<link rel="stylesheet" href="../css/collectionlist.css?v=10007">
</head>

<body>
    <div class="main-container">
        <div class="container-fluid px-0">
            <!-- header start -->
            <!--#include file ="header.inc"-->
            <!-- header end -->
        </div>

        <section>
            <div id="breadcrumb-nav" class="container">
                <ol class="breadcrumb">
                    <li>Home</li>
                    <li>Watches</li>
                    <li><%=familyName%></li>
                </ol>
            </div>

            <div class="container">
                <h3 class="mini-title"><%=familyName%></h3>
                <div class="mini-desc text-center px-3 pb-3">
                    <%=familyDesc%>
                </div>
                <div class="row collection-item">
                  <div class="col-6 col-md-4 d-none collection-template pb-3">
                    <a href="javascript:void(0);" class="collection-item">
                      <img src="" alt="" class="img-fluid collection-item__img">
                      <div class="text-center">
                        <span class="collection-item__name">Harmonic Collection</span><br>
                        <span class="collection-item__qtyTxt">
                          <span class="collection-item__qty">10</span>
                          WATCHES
                        </span>
                      </div>
                    </a>
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
<script src="../js/uifn.js?v=10007"></script>
<script src="../js/collectionList.js?v=10007"></script>
</body>

</html>
