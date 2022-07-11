var l = 0;
var isNext = false;
var isPrev = false;
var scrollInterval;

var current = 1856;
var pos = 0;
var yearArr = [1856, 1859, 1860, 1861, 1866, 1875, 1876, 1878, 1894, 1898, 1903, 1927, 1936, 1937, 1945, 1946, 1947, 1948, 1958, 1980, 1997, 2005, 2009, 2010, 2014, 2015];
var ttl = yearArr.length - 1;



$(document).ready(function(){
	//for video 
	jw_tvc = jwplayer("video-cnt").setup({
	    'flashplayer': '/player.swf',
	    'id': 's1-video child',
	    'width': 700,
	    'height': 400,
	    'file': "/video/Brand_2018.mp4",
	    'controlbar.idlehide': true
	});
	jw_tvc.play();

	setTimeout(function() {
		$("#video-cnt_wrapper").css("margin","0 auto");
	}, 500);

	var tl = new TimelineLite();
        tl.fromTo( $("#y1856 .txt_year"), .5, {y:-50, opacity:1},  {y:0, opacity:1}, "-=0.4");
        tl.fromTo( $("#y1856 .pic"), .5, {y:50, opacity:0},  {y:0, opacity:1},"-=0.2");
        tl.fromTo( $("#y1856 p"), .5, {x:50, opacity:0},  {x:0, opacity:1},"-=0.4");
        tl.play();

	$(".btnNext").mousedown(function(){
		isNext = true;
		scrollInterval = setInterval(moveTimeline, 30);
	});

	$(".btnPrev").mousedown(function(){
		isPrev = true;
		scrollInterval =setInterval(moveTimeline, 30);
	});

	$(".yearNext").click(function(){
		if(pos+1 <= ttl){
			pos = pos +1;
			console.log(yearArr[pos]);

			if(yearArr[pos] == 0){
				pos = pos +1;
			}

			$(".timeline li a").removeClass("selected");
			$(".timeline .y"+pos + " a").addClass("selected");
			
			initYear(yearArr[pos]);	
						
			var tl =  $(".ty.y"+pos).css("left");
			tl = parseInt(tl.replace("px", ""));
			if(tl > 700){
				l = (tl - 400)*-1;
				//$(".timeline").css("left", l+"px");	
				TweenLite.to($(".timeline"), .5, {left:l});
			}
		}
		
	});

	$(".yearPrev").click(function(){
		if(pos-1 >= 0){
			pos = pos - 1;
			if(yearArr[pos] == 0){
				pos = pos - 1;
			}

			$(".timeline li a").removeClass("selected");
			$(".timeline .y"+pos + " a").addClass("selected");
			
			initYear(yearArr[pos]);

			//l = 0
			var tl =  $(".ty.y"+pos).css("left");
			tl = parseInt(tl.replace("px", ""));
			
			if(tl > 700){
				l = (tl - 400)*-1;
			}else{
				l = 0;
			}
			//$(".timeline").css("left", l+"px");
			TweenLite.to($(".timeline"), .5, {left:l});
		}
	});

	$(document).mouseup(function(){
		isNext = false;
		isPrev = false;
		clearInterval(scrollInterval);
	});

	
	$(".timeline a").click(function(){
		var target = $(this).attr("rel");
		$(".ty a").removeClass("selected");
		$(this).addClass("selected");

		target = parseInt(target);
		initYear(target);

	});
	
});


function initYear( target ){

	if(target == 1898) {
		$(".content").css("min-height","1300px");
	}else{
		$(".content").css("min-height","1000px");
	}

	var tl = new TimelineLite();
    tl.to($("#y"+current), .4, {opacity:0});
    tl.set($("#y"+target), { opacity:1});
    tl.fromTo( $("#y"+target+" .txt_year"), .5, {y:-50, opacity:1},  {y:0, opacity:1}, "-=0.4");
    tl.fromTo( $("#y"+target+" .pic"), .5, {y:50, opacity:0},  {y:0, opacity:1},"-=0.2");
    tl.fromTo( $("#y"+target+" p"), .5, {x:50, opacity:0},  {x:0, opacity:1},"-=0.4");
    tl.play();

	current = target;
	pos = yearArr.indexOf(current);
}

function moveTimeline(){
	var step = 25;
	if(isNext){
		//l = l-step;
		l=(l-step < -4035)?-4035:l-step;
		$(".timeline").css("left", l+"px");
	}else if(isPrev){
		l=(l+step>0)?0:l+step;
		$(".timeline").css("left", l+"px");
	}
}