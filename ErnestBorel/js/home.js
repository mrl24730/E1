
$(document).ready(function () {

    //2015-10-02: no need caption any more
    $("#caption").hide();

    $('#bannerWrapper').cycle({
        fx: 'scrollHorz',
        speed: 500,
        prev: '.btn-prev',
        next: '.btn-next',
        timeout: 3000,
        manualTrump: false,
        slideResize: 1,
        pause: 1,
        before:function(e){
        	var txt= $(this).attr("data-caption");
        	//showCaption(txt);
        }

    });

    // $.colorbox({href:"images/move.jpg"});
    //$(".btn-pause").click(function () { $('#bannerWrapper').cycle('pause'); });
    //$(".btn-resume").click(function () { $('#bannerWrapper').cycle('resume'); });

    $('#themeWrapper').cycle({
        speed: 1000,
        timeout: 4000,
        manualTrump: false,
        slideResize: 1,
        pause: 1,
        random: 0,
        startingSlide: 0
    });

});

function showCaption(txt){


	$("#caption").fadeOut(400,
		function(){
			$("#caption").html(txt);
			$("#caption").fadeIn();
		}
	);

}
