<div class="container-fluid finder-inline">
    <ul class="container nav">

        <li class="nav-item">
            <a class="nav-link dropdown-toggle filter-title" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">COLLECTION</a>
            <div class="dropdown-menu pt-3">
                <div class="container">
                    <div class="row text-center">
                        <div class="col-12 py-2" data-ftitle="collection">
                            <p class="dropdown-item d-inline mx-2" data-filter="casual">Casual</p>
                            <p class="dropdown-item d-inline mx-2" data-filter="lady">Lady</p>
                            <p class="dropdown-item d-inline mx-2" data-filter="couple">Couple</p>
                        </div>
                    </div>
                    <hr>
                    <div class="row justify-content-center">
                        <div class="col-5 text-center">
                            <button class="btn dFilter-all">SHOW ALL WATCHES</button>
                            <button class="show-btn dFilter-close">SHOW <span class="filterd-num">0</span> WATCHES</button>
                        </div>
                    </div>
                </div>
            </div>
        </li>

        <li class="nav-item">
            <a class="nav-link dropdown-toggle filter-title" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">MOVEMENT</a>
            <div class="dropdown-menu pt-3">
                <div class="container">
                    <div class="row text-center">
                        <div class="col-12 py-2" data-ftitle="movement">
                            <p class="dropdown-item d-inline mx-2" data-filter="automatic">Automatic</p>
                            <p class="dropdown-item d-inline mx-2" data-filter="quartz">Quartz</p>
                        </div>
                    </div>
                    <hr>
                    <div class="row justify-content-center">
                        <div class="col-5 text-center">
                            <button class="btn dFilter-all">SHOW ALL WATCHES</button>
                            <button class="show-btn dFilter-close">SHOW <span class="filterd-num">0</span> WATCHES</button>
                        </div>
                    </div>
                </div>
            </div>
        </li>

        <li class="nav-item">
            <a class="nav-link dropdown-toggle filter-title" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">GENDER</a>
            <div class="dropdown-menu pt-3">
                <div class="container">
                    <div class="row text-center">
                        <div class="col-12 py-2" data-ftitle="gender">
                            <p class="dropdown-item d-inline mx-2" data-filter="M">Male</p>
                            <p class="dropdown-item d-inline mx-2" data-filter="F">Female</p>
                            <p class="dropdown-item d-inline mx-2" data-filter="N">Neutral</p>
                        </div>
                    </div>
                    <hr>
                    <div class="row justify-content-center">
                        <div class="col-5 text-center">
                            <button class="btn dFilter-all">SHOW ALL WATCHES</button>
                            <button class="show-btn dFilter-close">SHOW <span class="filterd-num">0</span> WATCHES</button>
                        </div>
                    </div>
                </div>
            </div>
        </li>

        <li class="nav-item">
            <a class="nav-link dropdown-toggle" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">DESIGN</a>
            <div class="dropdown-menu design-filter">

                <div class="container">
                    <div class="row text-center">
                        <div class="col-3" data-ftitle="band">
                            <h5 class="filter-title">BAND</h5>
                            <p data-filter="TPU">TPU Strap</p>
                            <p data-filter="STEEL">Stainless Steel Bracelet</p>
                            <p data-filter="LEATHER">Leather Strap</p>
                        </div>
                        <div class="col-3" data-ftitle="shape">
                            <h5 class="filter-title">CASE SHAPE</h5>
                            <p data-filter="ROUND">Round</p>
                            <p data-filter="SQUARE">Square-arc</p>
                            <p data-filter="RECTANGULAR">Rectangular-arc</p>
                            <p data-filter="SPECIAL">Special</p>
                        </div>
                        <div class="col-3" data-ftitle="material">
                            <h5 class="filter-title">CASE MATERIAL</h5>
                            <p data-filter="KGOLD">Solid Gold</p>
                            <p data-filter="STEEL+KGOLD">Stainless Steel + Solid Gold</p>
                            <p data-filter="STEEL">Stainless Steel</p>
                            <p data-filter="STEEL+PLATING">PDV and Stainless Steel</p>
                        </div>
                        <div class="col-3" data-ftitle="dial">
                            <h5 class="filter-title">DIAL DECO</h5>
                            <p data-filter="DIAMOND">Diamond</p>
                            <p data-filter="SCALE">Scale</p>
                            <p data-filter="SHELL">Shell</p>
                            <p data-filter="HYDRAULIC">Hydraulic</p>
                        </div>
                    </div>
                    <hr>
                    <div class="row justify-content-center">
                        <div class="col-5 text-center">
                            <button class="btn dFilter-all">SHOW ALL WATCHES</button>
                            <button class="show-btn dFilter-close">SHOW <span class="filterd-num">0</span> WATCHES</button>
                        </div>
                    </div>
                </div>
            </div>
        </li>

        <form class="form-inline ml-auto">
            <input class="form-control mr-sm-2" type="search" id="deskSearch" placeholder="Keyword or Model Number">
            <a href="#" id="deskSearchBtn"><img class="img-fluid" src="../images/0_search_icon.png"></a>
        </form>

    </ul>
</div>
<div id="filter-overlay"></div>

<!-- Mobile filter bar -->
<nav class="d-lg-none navbar navbar-expand-lg navbar-light bg-light w-100 text-center justify-content-center" id="mobileFilter">
    <a class="navbar-brand" href="#" data-toggle="collapse" data-target="#mobFlterNav">WATCH SELECTOR <span class="icon-sliders"></span></a>
</nav>

<div class="d-lg-none collapse" id="mobFlterNav">
    <ul class="navbar-nav">
        <li class="nav-item">
            <a class="collapsed nav-link filter-title" href="#" role="button" data-target="#meanColl" data-toggle="collapse" aria-haspopup="true"
                aria-expanded="false">COLLECTION</a>
            <div class="band-submenu collapse" id='meanColl'>
                <ul class="filter-sub" data-ftitle="collection">
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="casual">Casual</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="couple">Couple</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="lady">Lady</p>
                        </div>
                    </div>
                </ul>
            </div>
        </li>
        <li class="nav-item">
            <a class="collapsed nav-link filter-title" href="#" role="button" data-target="#meanMove" data-toggle="collapse" aria-haspopup="true"
                aria-expanded="false">MOVEMENT</a>
            <div class="band-submenu collapse" id='meanMove'>
                <ul class="filter-sub" data-ftitle="movement">
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="automatic">Automatic</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="quartz">Quartz</p>
                        </div>
                    </div>
                </ul>
            </div>
        </li>
        <li class="nav-item">
            <a class="collapsed nav-link filter-title" href="#" role="button" data-target="#meanGender" data-toggle="collapse" aria-haspopup="true"
                aria-expanded="false">GENDER</a>
            <div class="band-submenu collapse" id='meanGender'>
                <ul class="filter-sub" data-ftitle="gender">
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="M">Male</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="F">Female</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="N">Neutral</p>
                        </div>
                    </div>
                </ul>
            </div>
        </li>
        <li class="nav-item">
            <a class="collapsed nav-link filter-title" href="#" role="button" data-target="#meanBand" data-toggle="collapse" aria-haspopup="true"
                aria-expanded="false">BAND</a>
            <div class="band-submenu collapse" id='meanBand'>
                <ul class="filter-sub" data-ftitle="band">
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="TPU">TPU Strap</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="STEEL">Stainless Steel Bracelet</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="LEATHER">Leather Strap</p>
                        </div>
                    </div>
                </ul>
            </div>
        </li>
        <li class="nav-item">
            <a class="collapsed nav-link filter-title" href="#" role="button" data-target="#meanShape" data-toggle="collapse" aria-haspopup="true"
                aria-expanded="false">CASE SHAPE</a>
            <div class="band-submenu collapse" id="meanShape">
                <ul class="filter-sub" data-ftitle="shape">
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="ROUND">Round</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="SQUARE">Square-arc</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="RECTANGULAR">Rectangular-arc</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="SPECIAL">Special</p>
                        </div>
                    </div>
                </ul>
            </div>
        </li>
        <li class="nav-item">
            <a class="collapsed nav-link filter-title" href="#" role="button" data-target="#meanMaterial" data-toggle="collapse" aria-haspopup="true"
                aria-expanded="false">CASE MATERIAL</a>
            <div class="band-submenu collapse" id="meanMaterial">
                <ul class="filter-sub" data-ftitle="material">
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="KGOLD">Solid Gold</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="STEEL+KGOLD">Stainless Steel + Solid Gold</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="STEEL">Stainless Steel</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="STEEL+PLATING">PDV and Stainless Steel</p>
                        </div>
                    </div>
                </ul>
            </div>
        </li>
        <li class="nav-item">
            <a class="collapsed nav-link filter-title" href="#" role="button" data-target="#meanDial" data-toggle="collapse" aria-haspopup="true"
                aria-expanded="false">DIAL DECO</a>
            <div class="band-submenu collapse" id="meanDial">
                <ul class="filter-sub" data-ftitle="dial">
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="DIAMOND">Diamond</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="SCALE">Scale</p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <p data-filter="SHELL">Shell</p>
                        </div>
                        <div class="col-6">
                            <p data-filter="HYDRAULIC">Hydraulic</p>
                        </div>
                    </div>
                </ul>
            </div>

        </li>
    </ul>
    <button class="all-btn" id="mFilter-all">SHOW ALL WATCHES</button>
    <br>
    <input type="text" class="mob-search" id="mobiSearchBtn" placeholder="Keyword or Model Number">
    <br>
    <button class="show-btn" id="mobiFilterShow">SHOW <span id="mobiFilterdNum">0</span> WATCHES</button>
</div>

<div class="filter-tags container" id="filterTags">
    <div class="alert alert-filter alert-dismissible fade show mr-2 d-none" role="alert" id="filterTagTemp">
        <strong></strong>
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>
</div>
