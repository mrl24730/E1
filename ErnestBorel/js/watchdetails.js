var watchDetailObj = {
	idx : null,
	type : null,
	init:function(){

		var that = this;
		try{
			that.type = type;
		}catch(err){
			that.type = "automatic";
		}

		$("#zoom_photo").elevateZoom({ 
			zoomWindowFadeIn: 500, 
			zoomWindowFadeOut: 500,
			lensFadeIn: 500, 
			lensFadeOut: 500,
			zoomWindowWidth:200,
			zoomWindowHeight:200,
			zoomWindowOffetx:-100,
			zoomWindowOffety:15,
			borderColour : "#880707",
			lensBorder : 1,
			borderSize : 1
		});			

		/* Watches Slider */
		setTimeout(function(){
			var slider1 = new GallerySlider({
		        prevNav: $("#watches .btnPrev"),
		        nextNav: $("#watches .btnNext"),
		        container: $(".slider ul"),
		        hasPrev:true,
						isAutoPlay:false,
					target: $("#thumb_" + window.location.hash.split('#')[1]),
		    });
		}, 500);

		ttl = $("#watches ul li").length;
		if(ttl<=10){
			$(".btnPrev, .btnNext").hide();
		}

		$(".thumbnail a, .viewCouple a").click(that.thumbnailClickEvent);

		//load first element
		var model = window.location.hash;
		if(model != ""){
			model = model.replace("#","");
		}else{
			model = $("#watches ul li:nth-child(1) a").attr("rel");	
		}
	    that.idx = model;
	    setTimeout(function(){
	    	that.showWatch();	
	    },200);
	    

	   	$("#prev").click(function(){
	   		if($(this).hasClass("dim"))
	   			return;
			$("#watches li.selected").prev("li").find("a").click();
		});
			
		$("#next").click(function(){
			if($(this).hasClass("dim"))
	   			return;
			$("#watches li.selected").next("li").find("a").click();
		});


	},

	thumbnailClickEvent:function(evt){

		if(evt != undefined)
    		evt.preventDefault();
    	
    	var idx = $(this).attr("rel");

    	watchDetailObj.idx = idx;
    	watchDetailObj.showWatch();
	},

	showWatch:function(){
		var that = this;
		var idx = that.idx;
		window.location.hash = idx;
		$("#model").text(idx);
		$(".thumbnail").removeClass("selected");
	    $("#thumb_"+idx).addClass("selected");
	    var watch_type = $("#thumb_"+idx).data("type");
	    var info = $("#"+idx).clone(true, true);
	    $("#infoWrapper").html(info);

	    var imagePath = $("#"+idx).find(".image_url").text();
	    var smallImage = "/images/watches/"+imagePath+"_s.png";
	    var largeImage = "/images/watches/"+imagePath+"_l.png";
	    $("#zoom_photo").attr({"src":smallImage,"data-zoom-image":largeImage});

	    var ez = $('#zoom_photo').data('elevateZoom');
	    if(ez != undefined){
	    	ez.swaptheimage(smallImage, largeImage);
	    }

			//Prepare Next / Prev button
	    if($("#watches li.selected").prev("li").length == 0){
	    	$("#prev").addClass("dim");
	    }else{
	    	$("#prev").removeClass("dim");
	    }

	    if($("#watches li.selected").next("li").length == 0){
	    	$("#next").addClass("dim");
	    }else{
	    	$("#next").removeClass("dim");
	    }

	    //Prepare Couple Matching Watch
	    var matching = $("#"+idx).find(".matching").text();
	    if(matching != ""){
	    	$("#infoWrapper .viewCouple").find("a").removeClass("selected");
	    	$("#infoWrapper .viewCouple").find("."+idx).addClass("selected");
			$("#infoWrapper .viewCouple").show();
	    }else{
	    	$("#infoWrapper .viewCouple").hide();
	    }
	    
	}

}
