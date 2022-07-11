function GallerySlider(conf) {
    //===================================
    //setting
    var isAutoPlay = !!conf.timeout_sec ? true : false;
    var playTimer = null;
    var totalWidth = getTotalWidth(); //calculate all image width
    var wrapper = conf.container.parent();
    var moveDistance = wrapper.width(); //look for parent width
    var offsetLeft = 0;
    var thumbnails = conf.container;
    var onEdge = false;
    var isAnimating = false;
    var hasNext = true;
    var hasPrev = false;
    var isReverse = false;
    
    var target = conf.target;
    var thumbnail = $(".thumbnail", thumbnails);
    //====================================
   
    //setup
    $(conf.container).width(totalWidth); //set hidden div width to align all image on same line
    //set autoplay if needed.
    if (isAutoPlay) {
        startAutoPlay();

        $(wrapper).mouseover(function() {
            clearInterval(playTimer);
            playTimer = null;
        })
        .mouseout(function() {
            startAutoPlay();
        });
    }

    //--------
    //NAV
    //prev btn click
    $(conf.prevNav).click(function() {
        moveSlider(false);
    });
    //next btn click
    $(conf.nextNav).click(function() {
        moveSlider(true);
    });

    if (target) {
        moveToTarget(thumbnail.index(target), thumbnail.length, true)
    }

    $(window).on('hashchange', function () {
        target = $("#thumb_" + window.location.hash.split('#')[1]);
        moveToTarget(thumbnail.index(target), thumbnail.length)
    });

    checkBtnNav();

    //====================================
    //private function
    function getTotalWidth() {
        var returnWidth = 0;
        var elms = conf.container.children();
        for (var i = 0; i < elms.length; i++) {
            returnWidth += $(elms[i]).outerWidth(true);
        }
        return returnWidth;
    }

    function startAutoPlay(){
        playTimer = setInterval(function() {
            if (!hasNext) {
                isReverse = true;
            }

            if (!hasPrev) {
                isReverse = false;
            }

            moveSlider(!isReverse);
        }, conf.timeout_sec * 1000);
    }

    function checkBtnNav(){
        hasPrev = offsetLeft == 0 ? false : true;
        hasNext = offsetLeft + moveDistance >= totalWidth ? false : true;

        if(!hasPrev){
            $(conf.prevNav).addClass("nomore");
        }else{
            $(conf.prevNav).removeClass("nomore");
        }

        if(!hasNext){
            $(conf.nextNav).addClass("nomore");
        }else{
            $(conf.nextNav).removeClass("nomore");
        }
    }

    function moveSlider(toRight) {
        if (isAnimating) return; // do nothing when doing animation

        if ((hasNext && toRight) || (hasPrev && !toRight)) {
            isAnimating = true;
            var direction = toRight ? 1 : -1;

            if (offsetLeft + (moveDistance * 2 * direction) > totalWidth || (direction == -1 && (offsetLeft - moveDistance) < 0)) {
                offsetLeft += (totalWidth % moveDistance) * direction;
            } else { //normal
                offsetLeft += moveDistance * direction;
            }

            startSlideAnimation(offsetLeft, 1000);
        }
    }

    function moveToTarget(index, length, isFirstView) {
        if (length > 10) {
            var edgeChild = (length - 1 - index) <= ((length - 1) % 10) ? (length % 10) * 75 : 0;
        }

        if (edgeChild) {
            offsetLeft = (moveDistance % totalWidth) * (Math.floor(index / 10)) - moveDistance + edgeChild;
            onEdge = !onEdge;
        } else {
            offsetLeft = (moveDistance % totalWidth) * (Math.floor(index / 10));
        }

        if (isFirstView) {
            startSlideAnimation(offsetLeft, 1000);
        } else {
            startSlideAnimation(offsetLeft, 0);
        }
    }

    function startSlideAnimation(offsetLeft, time) {
        $(thumbnails).animate({
            marginLeft: (offsetLeft * -1) + "px"
        }, time, function () {
            // Animation complete.
            isAnimating = false;
            checkBtnNav();
        });
    }
}
