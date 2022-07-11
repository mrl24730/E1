// JavaScript Document
var colName = "empty";
function setBtnEable(chkEl, chkClass, setEl){

	$(chkEl+":first").prev(chkClass).length!=0?$(setEl+">.prev").removeClass("dim"):$(setEl+">.prev").addClass("dim");
	$(chkEl+":last").next(chkClass).length!=0?$(setEl+">.next").removeClass("dim"):$(setEl+">.next").addClass("dim");

}



function init_serialBlock(){
	var ttlNo = $("#mycarousel li").length;
	if(ttlNo<=3) $("a.collapse").hide();
	$(".scroll-pane").addClass("act");
	//switch to target watch
	var modelNo = getModelNo();
	
	if(modelNo == "")
	{
		$("#mycarousel li:first").click();
	}
	else
	{
		$("#mycarousel li#"+modelNo).click();
	}
	
}

function watchHover(){
	$("#mycarousel li").hover(
		function(){
			$(this).not(".selected").animate({"padding-top":"0px", "padding-bottom":"10px"},100);
		},
		function(){
			$(this).not(".selected").animate({"padding-top":"10px", "padding-bottom":"0px"},100,function(){$(this).css("padding-top","").css("padding-bottom","");});
		}
		
		);	
}

function watchcarousel_initDone(carousel) {
	
	var showNo =3;
	$("#mycarousel li").bind('click', function(){
		GAEventTracker(watchtype,colName,$(this).attr("id"));
		if(!$(this).hasClass("selected")){
			var thisid = $(this).attr("id");
			var imgPath =$(this).attr("data-img");
			/*
			var altVal = $(this).find("img").attr("alt");

			switch(altVal)
			{
				case "image_not_available":
				imgPath = "noimage_en";
				break;	
				case "未有图片显示":
				imgPath = "noimage_sc";
				break;	
				case "画像が表示されていません":
				imgPath = "noimage_jp";
				break;	
				case "Non photo affichée":
				imgPath = "noimage_fr";
				break;	
				case "未有圖片顯示":
				imgPath = "noimage_tc";
				break;	
			}
			*/
			

			
			var smallImage = "../images/watches/"+watchtype+"/"+imgPath+"_s.png";
			var largeImage = "../images/watches/"+watchtype+"/"+imgPath+"_l.png";
			$("#layer0").attr({"src":"../images/trans.gif","data-zoom-image":largeImage}).css("opacity",0).attr({"src":smallImage,"alt":thisid}).stop().animate({"opacity": 1, "easing":'easeOutExpo'},1000);
			$("#layer1").attr({"src":largeImage,"alt":thisid});
			
			if(!isMobile){
				var ez = $('#layer0').data('elevateZoom');
				if(ez == undefined){
					$("#layer0").elevateZoom({
						zoomType: "inner",
						cursor: "crosshair",
						zoomWindowFadeIn: 500,
						zoomWindowFadeOut: 750
					});
				}else{
					ez.swaptheimage(smallImage, largeImage);
				}
			}

			
			if($(this).attr("data-couple") != ""){
				$("#viewCouple").attr({"class":"","rel":$(this).attr("data-couple")});
			}else{
				$("#viewCouple").attr("class", "dim");
			}

			if($(this).attr("data-video") == "True"){
				$("#view3D").attr({"class":"","rel":$(this).attr("id")});
			}else{
				$("#view3D").attr("class", "dim");
			}

			$(".watchspec.selected").removeClass("selected");
			$("#"+thisid+"_spec").addClass("selected");

			var specPan = $("#"+thisid+"_spec").parents(".scroll-pane");
		
			
			if (specPan.length!=0){
				if(specPan.hasClass("ui-accordion-content-active"))
					
				specPan.jScrollPane().addTouch().removeClass("act");
					
				else
				specPan.addClass("act");
			}
			else 
				$(".ui-accordion-content-active").jScrollPane().addTouch().removeClass("act");
			
			$("#mycarousel li.selected").animate({"padding-top":"10px", "padding-bottom":"0px"},100,function(){$(this).css("padding-top","").css("padding-bottom","").removeClass("selected");});
			$(this).animate({"padding-top":"0px", "padding-bottom":"10px"},100,function(){$(this).addClass("selected");setBtnEable("#mycarousel li.selected","li","#zoomController");});
			
			$("#viewport").mapbox("zoomTo", 0);
			
			$(".zoom").removeClass("dim");
			$("#zoomOut").addClass("dim");
			//append model no. to the end of url
			
			
			
			window.location.hash = $(this).attr("id");
			$(window).scrollTop(0);
			$("#breadcrumbNav #model").html($(this).attr("id"));
		}
	});
	
	watchHover();
	
	$("#viewCouple").click(function(){
		var id = $(this).attr("rel");
		id = id.replace("-","_");
		$("#mycarousel li#"+id).click();
	});

	/*
	$("#view3D").click(function(){
		var id = $(this).attr("rel");
		id = id.replace("-","_");
		alert("show video: "+ id);
	});
	*/


	$("#prev").click(function(){
		//if($(".serial-item.s:first").hasClass("selected"))
		//$(".jcarousel-prev").click();
		$("#mycarousel li.selected").prev("li").click();
		var scrollto = $("#mycarousel li.selected").index()- Math.floor((showNo-1)/2);
		carousel.scroll(scrollto);
	});
		
	$("#next").click(function(){
		//if($(".serial-item.s:last").hasClass("selected"))
		//$(".jcarousel-next").click();
		$("#mycarousel li.selected").next("li").click();
		var scrollto = $("#mycarousel li.selected").index()- Math.floor(showNo/2-2);
		carousel.scroll(scrollto);
	});
	
	
    $('a.collapse').bind('click', function() {

		var towidth;
		var serialLen = $("#mycarousel li").length;
		var selectIndex = $("#mycarousel li.selected").index()+1;
		var scrollto = selectIndex;

		if($("#slider").hasClass("open")){
			showNo = serialLen<3?serialLen:3;
			$("#slider .jcarousel-container ").animate({"padding-right" : "24px","padding-left" : "24px"},100, function(){$("#slider").removeClass("open");});
			towidth = "274px";
			scrollto--;
		}else{
			showNo = serialLen <10?serialLen:10;
			$("#slider .jcarousel-container").animate({"padding-right" : "30px","padding-left" : "30px"},100, function(){$("#slider").addClass("open");});	
			towidth = "812px";
			scrollto = scrollto - Math.floor(showNo/2);
		}
		
		var serialwidth = 75*showNo+"px";
		$("#slider").animate({width: towidth},500);
		$(".jcarousel-skin-tango .jcarousel-container-horizontal").animate({width: serialwidth},500,function(){
        //carousel.scroll(jQuery.jcarousel.intval(jQuery(this).text()));
		
		
		carousel.scroll(scrollto);
		});
		
		
        return false;
    });

}

$(document).ready(function(){


	var modelNo = getModelNo();
	var selectIndex = 0;
	var jw_3d;
	if(modelNo != "" || modelNo.length>1)
	{
		modelNo = modelNo.replace("#", "");
		selectIndex = $("#mycarousel li#"+modelNo).index(); 
	}
	
	$('#view3D').magnificPopup({
		type:'inline',
		callbacks: {
		    beforeOpen: function() {
		      // Will fire when this exact popup is opened
		      // this - is Magnific Popup object
		      var vid = $("#view3D").attr("rel");
		      vid.replace("-","_");
		      jwplayer().load([{file:"/video/3d_"+vid+".mp4"}]);
		    },
		    open: function(){
		    	jw_3d.play();
		    },
			close: function(){
		    	jw_3d.stop();
		    }
		  }
	});

	jw_3d = jwplayer("video-watch").setup({
	    'flashplayer': 'player.swf',
	    'id': 's2-video child',
	    'width': 720,
	    'height': 360,
	    'file': "/video/3d_GBR8180_28551.mp4",
	    'controlbar.idlehide': true,
	     modes: [
			{ type: "html5" },
			{ type: "flash", src: "player.swf" }
		]
	});
	
	$("#mycarousel").jcarousel({
        scroll: 1,
		start: selectIndex,
        initCallback: watchcarousel_initDone
    });


	$(".navContainer").accordion({
			fillSpace: true,
			collapsible: true,
			animated: 'swing',
			accordionchangestart: function(event, ui){
				
					
			},
            change: function(event, ui) {            
                if(ui.newContent.hasClass("act")) ui.newContent.jScrollPane().addTouch().removeClass("act"); 
				if(ui.newHeader.length==0)
					ui.oldHeader.siblings(".ui-accordion-header").click();
            }
        });


	$("#viewport").mapbox().addTouch();

	if(!isMobile){
		$(".zoom").hide();
	}
	$(".zoom").click(function() {//control panel 
		if(!$(this).hasClass("dim")){
			var action = $(this).attr("id");
			
			var toLayer = 0;
			if (action=="zoomOut"){

				toLayer = 0;
			}
			else
			{

				var Ptop = ($("#viewport").height() - $("layer1").height())/2;
				var Pleft = ($("#viewport").width() - $("layer1").width())/2;
				
				//console.log(Ptop,Pleft);
				$("#viewport div").not(".current-map-layer").css("top", -Ptop);
				$("#viewport div").not(".current-map-layer").css("left", -Pleft);
				toLayer = 1; 
			}
			
			$(".zoom").removeClass("dim");
			$(this).addClass("dim");

			$("#viewport .current-map-layer").fadeOut("slow", function(){$("#viewport").mapbox("zoomTo", toLayer); });
			$("#viewport div").not(".current-map-layer").fadeIn("slow");
			
		}
		
		return false; 
	});
	
	
	$("#zoomController a").tipTip({defaultPosition:"left",edgeOffset: 1}).bind("touchend",function(){$(this).click();});
	

	init_serialBlock();
	
});
function getModelNo()
{	
	
	var pathLevel = 0;
	var pathArray = window.location.pathname.split( '/' );
	
	if(inCN)
	{
		pathLevel = 5; 
		colName = pathArray[pathLevel-2] ;
	}
	else
	{
		pathLevel = 6; 
		colName = pathArray[pathLevel-2] ;
	}
	//for DEV
	var str = window.location.href;
	if(str.indexOf("ernestborel.cn/dev/")>0)
	{
		pathLevel = 6; 
		colName = pathArray[pathLevel-2] ;
	}
	
	
	
	if(pathArray[pathLevel-1] && window.location.hash.length<1)
	{
		return pathArray[pathLevel-1].replace(".html","");  
	}
	else
	{
		var hash = window.location.hash;
		return hash.replace("#","");  	
	}
	
}
function QueryString(name) 
{
	var AllVars = window.location.search.substring(1);
	var Vars = AllVars.split("&");
	for (i = 0; i < Vars.length; i++)
	{
		var Var = Vars[i].split("=");
		if (Var[0] == name) return Var[1];
	}
	return "";
}
