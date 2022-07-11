var argPagerWidth = 1;
var mySelfObj = null;
function CycleProgress(arg){ 
	this.para = arg;
	this.init(arg);
} 
CycleProgress.prototype ={
	timer 			: null, 
	numOfElm		: 0,
	container		: null,
	status 			: "play",
	cycleTotalElm	: 0,
	cycle_timeout 	: 4000,
	cycle_currentSlide: 0,
	init : function(args)
	{
		mySelfObj = this;
		
		//setting
		this.container = args.container;
		this.numOfElm = $(this.container).find("a").length;
		
		//init cycle
		this.cycleSelf = $(this.container)
		.before('<a id="cycle-next">')
		.before('<a id="cycle-prev">')
		.after('<div id="mynav">')
		.after('<div id="bar">')
		.cycle({
           fx: 'fade',
           speed:  800, 
           timeout: this.cycle_timeout,
           next: '#cycle-next',
           prev: '#cycle-prev',
           pager:  '#mynav',
           before: this.onBefore,
           after: this.onAfter,
           onPrevNextEvent : this.onSlidePrevNext,
           onPagerEvent: this.onPageEvent,
           pagerAnchorBuilder: function(idx, slide) { 
                return '<div class="pauseBtn" id="pauseBtn_' + idx + '">' + '<a href="#" ></a>' + '</div>';
        	}
        });

		argPagerWidth = $(this.container).outerWidth() / this.numOfElm;


		var d = $("<div id='progressBar'>");
		d.appendTo("#bar");


		$("#mynav a").click(function(){
			if($("#mynav").hasClass("pause")){
				$(mySelfObj.container).cycle("resume");	
				mySelfObj.status = "play";
				$("#mynav").removeClass("pause");
				mySelfObj.goTo(mySelfObj.cycle_currentSlide,mySelfObj.cycleTotalElm,"direct");
			}
			else{
				$(mySelfObj.container).cycle("pause");
				mySelfObj.status = "pause";
				$("#mynav").addClass("pause");
			}

		});
		$("#mynav div").css('width',argPagerWidth );
		//console.log($(this.container).outerWidth() - (argPagerWidth* (this.numOfElm -1)));
/*
		var that = this;
		setTimeout( function(){
			console.log($("#mynav a"));
			$("#mynav a").css("width",argPagerWidth+"px");
			var opts = '{"currSlide":0, "slideCount": 99}';
			
		},2500);

*/
/*

		$(".pauseBtn").click(functiuon(){
			if(mySelfObj.status == "play"){
				$(mySelfObj.container).cycle("pause");
				mySelfObj.status = "pause";
			}
			else{
				$(mySelfObj.container).cycle("resume");	
				mySelfObj.status = "play"
			}
		});
*/
	},
	
	onSlidePrevNext : function(){
	},

	onPageEvent : function(zeroBasedSlideIndex, slideElement){
		mySelfObj.goTo(mySelfObj.cycle_currentSlide,mySelfObj.cycleTotalElm,"direct");
		clearTimeout(mySelfObj.timer);
		var isCurrentActiveSlide = $("#pauseBtn_"+ zeroBasedSlideIndex).hasClass("activeSlide");
		
		/*
		if(isCurrentActiveSlide && mySelfObj.status == "play"){
			$(mySelfObj.container).cycle("pause");
			mySelfObj.status = "pause";
			console.log("pause");
		}
		else{
			//mySelfObj.goTo(zeroBasedSlideIndex,mySelfObj.cycleTotalElm,"direct");
			$(mySelfObj.container).cycle("resume");	
			mySelfObj.status = "play";
			console.log("resume");
		}
		*/
	},
	onBefore : function(curr, next, opts){
		$('#bar #progressBar').stop().css("width",(argPagerWidth * (opts.currSlide+1)));
	},
	onAfter : function(curr, next, opts) {
		$("#imgCaption").cycle(opts.currSlide);
		clearTimeout(mySelfObj.timer);
		mySelfObj.timer = setTimeout(function(){mySelfObj.goTo(opts.currSlide,opts.slideCount,"after");},10);
		mySelfObj.cycle_currentSlide = opts.currSlide;

	},
	goTo : function(target, totalElm, from){
		mySelfObj.cycleTotalElm = totalElm;

		//set width before start this progress, e.g. from a jump to c , before start c, a and b need to be finished first
		$('#bar #progressBar').css("width",(argPagerWidth * target));
		
		//start animate the progress bar
		$('#bar #progressBar').stop().animate({ 'width': (argPagerWidth * (target + 1))},
        	3500,
        	function(){
        	 	//if this is the last slide, then reset the  progress bar 
        	 	if(target+1 == totalElm ){
        	 		
        	 		//if the cycle has been paused, no need to back to start point
        	 		if(mySelfObj.status == "pause"){
        	 			return ;	
        	 		}
        	 		$('#bar #progressBar').animate({'width': 1}, 500);
        	 	}

       	});

	}

	
};

