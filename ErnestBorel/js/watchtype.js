
$(document).ready(function(){
	$(".watch-menu .w0").addClass("selected");
	

	generateCollection(ladyObj, "lady");

	generateCollection(coupleObj, "couple");

	generateCollection(casualObj, "casual");
	
	/*$('.collection').click(function () {
		var href = $(this).find(".name a").attr("href");
		window.location = href;
	});*/

	var start = Math.floor(Math.random()*8);
	$('.bannerWrapper .lady').cycle({
        speed: 1000,
        timeout: 3300,
        manualTrump: false,
        slideResize: 1,
        pause: 1,
        startingSlide:start,
        before:function(e){
        	var txt= $(this).attr("data-caption");
        	showCaption(txt, "lady");
        }
    });

	start = Math.floor(Math.random()*6);
    $('.bannerWrapper .couple').cycle({
        speed: 1000,
        timeout: 3800,
        manualTrump: false,
        slideResize: 1,
        pause: 1,
        startingSlide:start,
        before:function(e){
        	var txt= $(this).attr("data-caption");
        	showCaption(txt, "couple");
        }
    });

    start = Math.floor(Math.random()*6);
    $('.bannerWrapper .casual').cycle({
        speed: 1000,
        timeout: 3800,
        manualTrump: false,
        slideResize: 1,
        pause: 1,
        startingSlide:start,
        before:function(e){
        	var txt= $(this).attr("data-caption");
        	showCaption(txt, "casual");
        }
    });

});


function generateCollection(obj, type){
	var ttl = obj.length;

	if(ttl == 0){
		$("#"+type+"Repeater").hide();
		return;
	}

	for(var i = 0; i < ttl; i++){
		var cell = $("#template").clone(true, true);
		cell.removeAttr("id");
		cell.find("a.name").attr("href", "wristwatch_collection/"+type+"/"+obj[i].col_ref+"/").text(obj[i].col_name);
		$("#"+type+"List").append(cell);
	}
}


function showCaption(txt, type){

	var target = $("."+type + ".caption");

	
	target.fadeOut(200,
		function(){
			target.html(txt);
			target.fadeIn(500);
		}
	);
	
	/*
	target.animate({
	opacity: 0.0,
	top: "-=20"
	}, 200, function() {
		// Animation complete.
		target.html(txt);
		target.animate({
		    opacity: 1.0,
		    top: "+=20"
		}, 500, function() {
		    // Animation complete.
		});
	});
	*/
}