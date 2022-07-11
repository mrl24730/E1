<header>
    <nav class="navbar navbar-expand-lg p-0 bg-blue">
        <div class="container">
            <a href="filter.aspx?search=1" class="d-lg-none navbar-btn-search"><span class="icon-search"></span></a>
            <a href="#"><img class="img-fluid d-none w-50 order-1 d-lg-inline-block" src="../images/0_search_icon.png"></a>
            <button class="navbar-toggler collapsed order-3" type="button" data-toggle="collapse" data-target="#navbarMobile" aria-controls="navbarMobile"
                aria-expanded="false" aria-label="Toggle navigation">
                <span class="icon-bar top-bar"></span>
                <span class="icon-bar middle-bar"></span>
                <span class="icon-bar bottom-bar"></span>
            </button>

            <!-- Desktop Nav -->
            <div class="navbar-collapse w-50 d-none d-lg-inline-block" id="navbarDesktop">
                <ul class="navbar-nav mr-auto ">

                    <li class="nav-item">
                        <a class="nav-link dropdown-toggle" href="#" id="menuDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">MENU</a>
                        <div id="nav-menu" class="dropdown-menu" aria-labelledby="menuDropdown">
                            <div class="container">
                                <div class="row">
                                    <div class="col-4">
                                        <h5>MY ERNEST BOREL</h5>
                                        <a class="dropdown-item " href="cart.aspx">MY CART</a>
                                        <a class="dropdown-item" href="order.aspx">MY ORDER</a>
                                    </div>
                                    <div class="col-4">
                                        <h5>ERNEST BOREL WORLD</h5>
                                        <a class="dropdown-item" href="brand.aspx">OUR BRAND</a>
                                        <a class="dropdown-item" href="ambassador.aspx">BRAND AMBRASSADOR</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>

                    <li class="nav-item">
                        <a class="nav-link dropdown-toggle" href="#" id="watchesDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">WATCHES</a>
                        <div class="watches-submenu dropdown-menu" aria-labelledby="watchesDropdown">
                            <div class="container">
                                <div class="row nav-collection">
                                    <div class="col-4">
                                        <a href="collection_list.aspx?f=couple">
                                          <img class="img-fluid" src="../images/0_watch_romantic_m.jpg">
                                          <p>ROMANTIC SERIES</p>
                                        </a>
                                    </div>
                                    <div class="col-4">
                                      <a href="collection_list.aspx?f=lady">
                                        <img class="img-fluid" src="../images/0_watch_feminine_m.jpg">
                                        <p>FEMININE SERIES</p>
                                      </a>
                                    </div>
                                    <div class="col-4">
                                      <a href="collection_list.aspx?f=casual">
                                        <img class="img-fluid" src="../images/0_watch_causal_m.jpg">
                                        <p>CASUAL SERIES</p>
                                      </a>
                                    </div>
                                </div>
                                <div class="row no-gutters">
                                    <div class="col-3 offset-3"><a class="btn" href="filter.aspx">SHOW ALL WATCHES</a></div>
                                    <div class="col-3"><a class="btn" href="home.aspx#new-product">NEW PRODUCTS</a></div>
                                </div>
                            </div>
                        </div>
                    </li>

                </ul>
            </div>

            <div class="mx-auto d-block text-center order-0 order-md-1 w-25">
            <!-- <div class="text-center" style="position: absolute; width: 100%; height: 100%; top: 0; left: 0; z-index: -1;"> -->
                <a class="navbar-brand mx-auto" href="home.aspx"><img class="img-logo img-fluid" src="../images/0_logo.png"></a>
            </div>

            <div class="navbar-collapse collapse w-50 order-2">
            <!-- <div class="navbar-collapse collapse w-100"> -->
                <div class="navbar-nav ml-auto">
                    <ul class="list-inline">
                        <li class="list-inline-item">
                            <a class="cart" href="cart.aspx">
                                <span class="icon icon-shopping-cart">
                                    <span class="cart-count">0</span>
                                </span>
                            </a>
                        </li>
                        <li class="list-inline-item"><a class="btn-lang lang" data-lang="tc" href="javascript:void(0);">繁</a></li>
                        <li class="list-inline-item"><a class="btn-lang lang" data-lang="sc" href="javascript:void(0);">简</a></li>
                        <li class="list-inline-item"><a class="btn-lang lang active" data-lang="en" href="javascript:void(0);">EN</a></li>
                    </ul>

                </div>
            </div>
        </div>
    </nav>
     <!-- Mobile Nav -->
    <div class="d-lg-none collapse" id="navbarMobile">
        <ul class="navbar-nav">
            <li class="nav-item">
                <a class="collapsed nav-link" href="#" role="button" data-target=".watches-submenu" data-toggle="collapse" aria-haspopup="true" aria-expanded="false">
                  <img class="nav-link__icon" src="../images/0_menu_watch_icon_m.png" alt="WATCHES">WATCHES
                </a>
                <div class="watches-submenu collapse show">
                    <ul class="navbar-sub">
                        <li><a class="navsub-link" href="home.aspx#new-product">NEW PRODUCTS</a></li>
                        <li><a class="navsub-link" href="collection_list.aspx?f=couple">ROMANTIC SERIES</a></li>
                        <li><a class="navsub-link" href="collection_list.aspx?f=lady">FEMININE SERIES</a></li>
                        <li><a class="navsub-link" href="collection_list.aspx?f=casual">CASUAL SERIES</a></li>
                        <li><a class="navsub-link" href="filter.aspx">ALL WATCHES</a></li>
                    </ul>
                </div>
            </li>
            <li class="nav-item">
                <a class="collapsed nav-link" href="#"  role="button" data-target=".my-ernest-borel-submenu" data-toggle="collapse" aria-haspopup="true" aria-expanded="false">
                  <img class="nav-link__icon" src="../images/0_menu_cart_icon_m.png" alt="MY ERNEST BOREL">MY ERNEST BOREL
                </a>
                <div class="my-ernest-borel-submenu collapse">
                    <ul class="navbar-sub">
                        <li><a class="navsub-link" href="cart.aspx">MY CART</a></li>
                        <li><a class="navsub-link" href="order.aspx">MY ORDER</a></li>
                    </ul>
                </div>
            </li>
            <li class="nav-item">
                <a class="collapsed nav-link" href="#"  role="button" data-target=".ernest-borel-world-submenu" data-toggle="collapse" aria-haspopup="true" aria-expanded="false">
                  <img class="nav-link__icon" src="../images/0_menu_eb_icon_m.png" alt="ERNEST BOREL WORLD">ERNEST BOREL WORLD
                </a>
                <div class="ernest-borel-world-submenu collapse">
                    <ul class="navbar-sub">
                        <li><a class="navsub-link" href="brand.aspx">OUR BRAND</a></li>
                        <li><a class="navsub-link" href="ambassador.aspx">BRAND AMBASSADOR</a></li>
                    </ul>
                </div>
            </li>
            <li class="nav-item">
                <a class="collapsed nav-link" href="#"  role="button" data-target=".lang-submenu" data-toggle="collapse" aria-haspopup="true" aria-expanded="false">
                  <img class="nav-link__icon" src="../images/0_menu_language_icon_m.png" alt="LANGUAGE">LANGUAGE
                </a>
                <div class="lang-submenu collapse">
                    <ul class="navbar-sub">
                        <li><a class="navsub-link btn-lang" data-lang="tc"  href="/distributor/tc/">繁體</a></li>
                        <li><a class="navsub-link btn-lang" data-lang="sc"  href="/distributor/sc/">简体</a></li>
                        <li><a class="navsub-link btn-lang" data-lang="en"  href="/distributor/en/">ENGLISH</a></li>
                    </ul>
                </div>
            </li>
        </ul>
        <div class="navbarMobile__overlay"></div>
    </div>
</header>

<div id="nav__overlay"></div>
<div id="ajax-overlay"></div>

<!-- Modal -->
<div class="modal fade" id="watchAddedModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body text-center">
        Product has been added to your cart.
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <a href="cart.aspx" class="btn btn-primary tocart-btn">Go To My Cart</a>
      </div>
    </div>
  </div>
</div>
