// JavaScript Document
var nid = -1;
var ua = navigator.userAgent;
var checker = {
  ios: ua.match(/(iPhone|iPod|iPad)/),
  blackberry: ua.match(/BlackBerry/),
  android: ua.match(/Android/)
};

var isMobile = checker.ios || checker.blackberry || checker.android;
var inCN = (window.location.host=="www.ernestborel.cn" || window.location.host=="ernestborel.cn");

//*** baidu tracking ***
if(inCN)
{
	var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");

	document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F36a12c1ce17d99f834a8c7a27a9f2b7a' type='text/javascript'%3E%3C/script%3E"));
}

// GA tracking code - replace token / id as needed
var _gaq = _gaq || [];
if(inCN)
{	
	_gaq.push(['_setAccount', 'UA-29270575-1']);
	_gaq.push(['_setDomainName', 'ernestborel.cn']);
}
else
{
	_gaq.push(['_setAccount', 'UA-29270575-2']);
	_gaq.push(['_setDomainName', 'ernestborel.ch']);
}
_gaq.push(['_setAllowLinker', true]);
_gaq.push(['_trackPageview']);
(function() {
	var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
	ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
	var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();

function GAEventTracker(fr_category,fr_action,fr_opt_label,fr_opt_value)
{
	_gaq.push(['_trackEvent', fr_category,fr_action,fr_opt_label,fr_opt_value]);
}


//Disable Right Click
//document.onmousedown=disableclick;
function disableclick(event)
{
  if(event.button==2)
  	return false;
}

$(document).ready(function(){
	$("body").attr("oncontextmenu", "return false");
	$('img').on('dragstart', function(event) { event.preventDefault(); });
})


//*********************************************** NavBar Start *********************************************** 
	var NavBar = {
		activeIdx:0,
		activeSub:0,

		init:function(){
			var that = this;

			try{
				if(nid != undefined){
					NavBar.activeIdx = nid;
					NavBar.showSub(nid);
					
					if(subId != undefined){
						NavBar.activeSub = subId;
						NavBar.showSubSub(nid, subId);
					}

				}
			}catch(e){

			}

			//$("#nav .navGrad").animate({"top":$("#nav ul").height()}).fadeIn(2000);
			
			$("#nav li.nav").mouseenter(
				function(e){
					e.preventDefault();
					var idx = $("#nav li.nav").index(this);
					that.showSub(idx);
				}
			);

			$("#nav .sub-navCtnr>li").mouseenter(
				function(e){
					e.preventDefault();
					var pidx = $(this).attr("data-p");
					pidx = pidx.replace("n", "");
					var idx = $(".nav.n"+pidx+" .sub-navCtnr>li").index(this) + 1;
					that.showSubSub(pidx, idx);
				}
			);

			$("#nav").mouseleave(that.hideSub);
		},

		showSub:function(idx){
			var main = $(".nav.n"+idx);
			if(main.hasClass("hover")) return;

			$(".nav").removeClass("hover");
			main.addClass("hover");
			if(!main.hasClass("haveSub")) return;

			
			$(".nav .sub-navCtnr").hide();
			
			var main = $(".nav.n"+idx);	
			var target = main.find(".sub-navCtnr");
			target.css({"display":"block"});
			TweenMax.set(target, {opacity:0});
			TweenMax.to(main, 0.5, {height:"63px"});
			TweenMax.to(target, 0.5, {opacity:1});	
		},

		showSubSub:function(pidx, idx){
			
			var _this = $(".nav.n"+pidx+" .sub-navCtnr>li:nth-child("+idx+")");
			var pp = "n"+pidx;
			var $pp = $(".n"+pidx);
			
			$(".sub-navCtnr li").removeClass("hover");
			_this.addClass("hover");

			if(_this.hasClass("haveSub2")){
				var target = _this.attr("data-s");
				var $target = $("."+pp + "-" +target);
				$target.css({"display":"block"});
				TweenMax.set($target, {opacity:0});
				TweenMax.to($pp, 0.5, {height:"94px"});
				TweenMax.to($target, 0.5, {opacity:1});	
			}else{
				TweenMax.to($pp, 0.5, {height:"63px"});
			}
		},

		hideSub:function(){
			var that = this;

			$(".nav").removeClass("hover");
			$(".nav .sub-navCtnr").hide();

			var main = $(".nav.n"+NavBar.activeIdx);
			main.addClass("hover");

			if(main.hasClass("haveSub")){

				
				var target = main.find(".sub-navCtnr");
				target.css({"display":"block"});
				TweenMax.set(target, {opacity:0});
				TweenMax.to($(".nav"), 0.5, {height:"63px"});
				TweenMax.to(target, 0.5, {opacity:1});
				

				if(NavBar.activeSub != null){
					var sub = main.find(".sub-navCtnr>li:nth-child("+NavBar.activeSub+")");
					$(".sub-navCtnr li").removeClass("hover");
					sub.addClass("hover");
					if(sub.hasClass("haveSub2")){
						NavBar.showSubSub(NavBar.activeIdx, NavBar.activeSub);
					}
				}
			}else{
				TweenMax.to($(".nav"), 0.5, {height:"33px"});
			}


			
		}
	}

//*********************************************** NavBar End *********************************************** 	

$(document).ready(function(){
	
	hoverForIE6();
	
	window.onorientationchange = function() {
		ipadChgOrientation();
	};
	ipadChgOrientation();
	
	
	initPanel();

	NavBar.init();
	
	$("#header #icon-wechat").hover(function(){
		$("#wechat-qrcode").stop().fadeIn();
	},
	function(){
		$("#wechat-qrcode").stop().fadeOut();
	});

	/*history to top float btn*/
	if ($(".floatbox").length !=0)	$('.floatbox').scrollFollow({speed: 1100,offset:500});
	//$(".floatbox2").scrollFollow({container:'main-content',relativeTo:'bottom', offset:500});	


	$(".totop").click(function(){
		if(navigator.platform != "iPad"){
			//console.log($("html"));
			$("html,body").animate({scrollTop: 0}, 400);
		}else{
			self.scroll(0,0);
		}

	});

	if ($(".totop").length !=0){
		toTopShowhide();
		$(window).scroll(function(){toTopShowhide();});
	}


	/*content animate */
	$('.left_block .history_cont').each(function (i){
		$(this).delay(1000*i).appear(function() {
			$(this).animate({marginRight:"0px",opacity: 1,easing: 'easeOutExpo' } ,700,function(){$(this).find(".history_text").animate({opacity:1}, 4000);});	
		});
	});

	$('.right_block .history_cont').delay(500).each(function (i){
		$(this).delay(1000*i).appear(function() {
			$(this).animate({marginLeft:"0px",opacity: 1, easing: 'easeOutExpo'} ,700, function(){$(this).find(".history_text").animate({opacity:1}, 4000);});
		});
	});


	//Customized Select Box
	if($('.my-ctm-select').length){
		$('.my-ctm-select').each(
			  function(i) {
				selectContainer = $(this);
				// Remove the class for non JS browsers
				selectContainer.removeClass('my-ctm-select');
				// Add the class for JS Browers
				selectContainer.addClass('ctm-select');
				// Find the select box
				selectContainer.children().before('<div class="select-text">a</div>').each(
				  function() {
					$(this).prev().text(this.options[0].innerHTML)
				  }
				);
				// Store the parent object
				var parentTextObj = selectContainer.children().prev();
			  }
		 )
	}

	//attach language switch when ready
	$("#slidePan").delegate("a", "click", function(event){
	
		event.preventDefault();
		
		var lang = $(this).attr("href");
		changeLang(lang);
    });
	
	//attach the baidu static statistics tool
	/*var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
	var fileref=document.createElement('script');
	fileref.setAttribute("type","text/javascript");
	fileref.setAttribute("src", _bdhmProtocol + "hm.baidu.com/h.js%3F046d35adc97849b2a49c46576f7f4fb4" );
	*/
	


	
}); // end of document ready

//footer expand btn
function footerExpandBtnClick(){
	$("#foot-row").toggleClass("collapsed")
	$(".btnShowHide").toggleClass("hideBtn");
	var currentTop = $(document).scrollTop();

	$('html,body').animate({
        scrollTop: currentTop+400
    }, 600);
}

/****************************************************** Generic Function ******************************************************/
/*to top button show hide*/
function toTopShowhide(){
	var scrolltop = $(window).scrollTop();
	
	scrolltop>160?$(".totop").fadeIn():$(".totop").fadeOut();
}

// hover for IE
function hoverForIE6(_list, _class) {
	var _hoverClass = 'hover';
	if(_class) _hoverClass = _class;
	
	var ieversion = isIE();

	if (ieversion != false && ieversion < 7) {
		$(_list).hover(function() {
			document.title = _hoverClass;
			$(this).addClass(_hoverClass);
		}, function() {
			$(this).removeClass(_hoverClass);
		});
	}
	
}

function isIE () {
  var myNav = navigator.userAgent.toLowerCase();
  return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
}



//change language
function changeLang(lang)
{

	var strVal = document.location.pathname;
	
	var slashAry = strVal.split("/",3);
	
	if(slashAry[1]=="en" || slashAry[1]=="fr" || slashAry[1]=="jp" || slashAry[1]=="tc")
		{//if(strVal.indexOf("/"+slashAry[1]+"/") >=0)
			strVal = strVal.replace("/"+slashAry[1]+"/","") + window.location.hash;
			
		}
	else
		strVal = strVal.substring(1)+ window.location.hash;
	//console.log(lang + strVal);
	window.location = lang + strVal;
	
}

// orientation change
function ipadChgOrientation(po, pht) {
	var o = po || window.orientation;
	var winHt = pht || $(window).height();
	o = parseInt(o);
	switch(o) {  
		case 90:
		case -90:
			$("#doc").addClass("landscape");
		break;
		default:
			$("#doc").removeClass("landscape");
		break;
	}
}


function initPanel(){
	
	/*
	$(".btnSearch").click(function(){
		var type = $(this).attr("rel");
		$("#formSearch").attr("action","search.aspx?type="+type);
		$("#formSearch").submit();
	});
	*/

	//panel
	$("#panelBar a.icon").click(function(){
		var panel = $(this).attr("rel");
		if($(this).hasClass("selected")){
			$(this).removeClass("selected");
			showhidePanel("hide", panel);
		}else{
			$("#panelBar a.icon").removeClass("selected");
			$(this).addClass("selected");
			showhidePanel("show", panel);
		}
	});
}

function showhidePanel(action, panel){
	var hh = (panel == "search")? 150:50;
	if(action == "show"){
		$(".panel").css("height", 0);
		$(".panel."+panel).css("height", 0);
		$(".panel."+panel).show();
		$(".panel."+panel+ " .inner-panel").hide();
		$(".panel."+panel).animate({
		    height: hh
		}, 400, function() {
		    // Animation complete.
		    $(".panel."+panel+ " .inner-panel").fadeIn(200);
		    $(".panel."+panel).css("height", "auto");
		});
	}else{
		$(".panel."+panel+ " .inner-panel").fadeOut(200);
		$(".panel."+panel).animate({
		    height: "0"
		}, 400, function() {
		    // Animation complete.
		    $(".panel."+panel).css("height", "auto");
		});
	}
}

function disableAutoCloseSubMenu(){
	window.stopAutoCloseSubMenu = true; 
    $(document).ready(function () {
        $("nav li.active").trigger('mouseenter');
    });
}


/**
* hoverIntent r6 // 2011.02.26 // jQuery 1.5.1+
* <http://cherne.net/brian/resources/jquery.hoverIntent.html>
* 
* @param  f  onMouseOver function || An object with configuration options
* @param  g  onMouseOut function  || Nothing (use configuration options object)
* @author    Brian Cherne brian(at)cherne(dot)net
*/
(function($){$.fn.hoverIntent=function(f,g){var cfg={sensitivity:7,interval:100,timeout:0};cfg=$.extend(cfg,g?{over:f,out:g}:f);var cX,cY,pX,pY;var track=function(ev){cX=ev.pageX;cY=ev.pageY};var compare=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);if((Math.abs(pX-cX)+Math.abs(pY-cY))<cfg.sensitivity){$(ob).unbind("mousemove",track);ob.hoverIntent_s=1;return cfg.over.apply(ob,[ev])}else{pX=cX;pY=cY;ob.hoverIntent_t=setTimeout(function(){compare(ev,ob)},cfg.interval)}};var delay=function(ev,ob){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t);ob.hoverIntent_s=0;return cfg.out.apply(ob,[ev])};var handleHover=function(e){var ev=jQuery.extend({},e);var ob=this;if(ob.hoverIntent_t){ob.hoverIntent_t=clearTimeout(ob.hoverIntent_t)}if(e.type=="mouseenter"){pX=ev.pageX;pY=ev.pageY;$(ob).bind("mousemove",track);if(ob.hoverIntent_s!=1){ob.hoverIntent_t=setTimeout(function(){compare(ev,ob)},cfg.interval)}}else{$(ob).unbind("mousemove",track);if(ob.hoverIntent_s==1){ob.hoverIntent_t=setTimeout(function(){delay(ev,ob)},cfg.timeout)}}};return this.bind('mouseenter',handleHover).bind('mouseleave',handleHover)}})(jQuery);


//Custom functions
function mycarousel_initCallback(carousel)
{

    // Pause autoscrolling if the user moves with the cursor over the clip.
    carousel.clip.hover(function() {
        carousel.stopAuto();
    }, function() {
        carousel.startAuto();
    });
};

//SCROLL TO sub-region
function subregion_scroll(idx){
	var div = $("."+idx).not("#"+idx);
	$('html,body').animate({ scrollTop: div.offset().top}, 'fast');
}



/****************************************************** CS Function ******************************************************/
//Component for CS Pages
(function($){
	$.fn.genLocationBox = function(options){
		var defaults = {
			shadowCont : null,
			selBoxes: null,
			lang: null,
			addressTxt : "Address",
			contactTxt : "Contact"
		};
		var options = $.extend(defaults, options);
		if(options.lang != null)
		{	
			switch(options.lang)
			{
				case "tc":
					options.addressTxt = "地址";
					options.contactTxt = "聯絡方法";
					break;
				case "sc":
					options.addressTxt = "地址";
					options.contactTxt = "联系方式";
				 	break;
				case "fr":
					options.addressTxt = "Adresse";
					options.contactTxt = "Contact";
				  	break;
				case "jp":
					options.addressTxt = "Address";
					options.contactTxt = "Contact";
					break; 
			}
		}
		return this.each(function() {
			//Reindex the id, ignore 
			reindex($(defaults.shadowCont));

			//Set Region Box
			setSelBox(0,$(defaults.shadowCont).find(">div"),showBox,true);
			
			showDefaultContent();
		});

		function reindex(cont){
			var idx_region = 0;			
			cont.find(".region").each(function(){
				var idx_country = 0;
				$(this).find(".country").each(function(){
					var idx_city = 0;
					$(this).find(".city").each(function(){
						var idx_shop = 0;
						$(this).find(".shop").each(function(){
							$(this).attr("id","r" + idx_region + "-c" + idx_country + "-cc" + idx_city + "-s" + idx_shop);
							idx_shop++;
						});
						$(this).attr("id","r" + idx_region + "-c" + idx_country + "-cc" + idx_city);
						idx_city++;
					});
					$(this).attr("id","r" + idx_region + "-c" + idx_country);
					idx_country++;
				});
				$(this).attr("id","r" + idx_region);
				idx_region++;
			});
		}
		
		function setSelBox(lvl,elms,evt,display){
			var sbox = $($(defaults.selBoxes).find(".step")[lvl]).find("select");
			sbox.empty();
			
			var opt = $("<option/>");
			opt.attr('value','').html("-----");
			sbox.append(opt);
			
			$(elms).each(function(){
				var id = $(this).attr("id");
				var title = $(this).attr("title");
				var opt = $("<option/>");
				opt.attr('value','opt-' + id).html(title);
				sbox.append(opt);
			});
			sbox.unbind("change");
			sbox.change(evt);
			
			if(display) sbox.parents(".step").show();
			sbox.prev().text(sbox.find(":selected").text());	//Show Selected Text on front
		}
		
		function showBox(e){
			$(".cityBox").empty();
			$(e.currentTarget).prev().text($(e.currentTarget).find(":selected").text());	//Show Selected Text on front
			var val = e.currentTarget.value;
			var stype = $(e.currentTarget).attr("id");
			if(val != ""){
				var strip_id = val.replace("opt-","");
				var evt = showBox;
				var chk = chkSameCity($("#" + strip_id));
				if(stype == "country"){
					if(chk){
						showCont(e,$("#" + strip_id + "-cc0"));
					}else{
						setSelBox(2,$("#" + strip_id).find(">div"),showCont,true);		
					}
				}else{
					setSelBox(1,$("#" + strip_id).find(">div"),showBox,true);	
				}				
				if(stype == "region" || chk) $($(defaults.selBoxes).find(".step")[2]).hide();

				
			}	
		}
		
		function showCont(e,elm){
			if(e){
				$(e.currentTarget).prev().text($(e.currentTarget).find(":selected").text());	//Show Selected Text on front
				var strip_id =  e.currentTarget.value.replace("opt-","");
			}
			var el = elm || $("#" +strip_id);
			var newElm = $(el).clone();
			newElm.find(".shop").each(function(){
				var addressBox = $("<div/>");
				var contactBox = $("<div/>");
				addressBox.attr("class","addBox");
				contactBox.attr("class","conBox");
				var ah5 = $("<h5/>");
				var ch5 = $("<h5/>");
				ah5.text(defaults.addressTxt);
				ch5.text(defaults.contactTxt);
				ah5.appendTo(addressBox);
				ch5.appendTo(contactBox);
				$(this).find(".address").appendTo(addressBox);
				$(this).find(".contact").appendTo(contactBox);
				addressBox.appendTo($(this));
				contactBox.appendTo($(this));
			});
			newElm.removeAttr("id");
			newElm.find("*[id]").removeAttr("id");
			
			$(".cityBox").empty();
			newElm.appendTo($(".cityBox"));
		}
		
		function chkSameCity(id){
			var isSame = false;
			var p_title = id.attr("title");
			var child = id.find(">div");
			if(child.length == 1){
				if(p_title == child.attr("title")) isSame = true;
			}
			return isSame;
		}
		
		function showDefaultContent(){
			var defDisplay = $(defaults.shadowCont).find(".default");
			var city = $(defDisplay[0]);
			var country = city.parent(".country");
			var region = country.parent(".region");
			
			var rSel = $($(defaults.selBoxes).find(".step")[0]).find("select");
			rSel.val('opt-' + region.attr('id'));
			rSel.trigger("change");
			var cSel = $($(defaults.selBoxes).find(".step")[1]).find("select");
			cSel.val('opt-' + country.attr('id'));
			cSel.trigger("change");
			
			if(city.attr("title") != country.attr("title")){
				var ccSel = $($(defaults.selBoxes).find(".step")[2]).find("select");
				ccSel.val('opt-' + city.attr('id'));
				ccSel.trigger("change");
			}
		}
	}
})(jQuery);


//Snowfall for xmas
/*
(function(b){b.snowfall=function(a,d){function s(c,f,g,h,j){this.id=j;this.x=c;this.y=f;this.size=g;this.speed=h;this.step=0;this.stepSize=e(1,10)/100;if(d.collection)this.target=m[e(0,m.length-1)];c=b(document.createElement("div")).attr({"class":"snowfall-flakes",id:"flake-"+this.id}).css({width:this.size,height:this.size,background:d.flakeColor,position:"absolute",top:this.y,left:this.x,fontSize:0,zIndex:d.flakeIndex});b(a).get(0).tagName===b(document).get(0).tagName?(b("body").append(c),a=b("body")):
b(a).append(c);this.element=document.getElementById("flake-"+this.id);this.update=function(){this.y+=this.speed;this.y>n-(this.size+6)&&this.reset();this.element.style.top=this.y+"px";this.element.style.left=this.x+"px";this.step+=this.stepSize;this.x+=Math.cos(this.step);if(d.collection&&this.x>this.target.x&&this.x<this.target.width+this.target.x&&this.y>this.target.y&&this.y<this.target.height+this.target.y){var b=this.target.element.getContext("2d"),c=this.x-this.target.x,a=this.y-this.target.y,
e=this.target.colData;if(e[parseInt(c)][parseInt(a+this.speed+this.size)]!==void 0||a+this.speed+this.size>this.target.height)if(a+this.speed+this.size>this.target.height){for(;a+this.speed+this.size>this.target.height&&this.speed>0;)this.speed*=0.5;b.fillStyle="#fff";e[parseInt(c)][parseInt(a+this.speed+this.size)]==void 0?(e[parseInt(c)][parseInt(a+this.speed+this.size)]=1,b.fillRect(c,a+this.speed+this.size,this.size,this.size)):(e[parseInt(c)][parseInt(a+this.speed)]=1,b.fillRect(c,a+this.speed,
this.size,this.size));this.reset()}else this.speed=1,this.stepSize=0,parseInt(c)+1<this.target.width&&e[parseInt(c)+1][parseInt(a)+1]==void 0?this.x++:parseInt(c)-1>0&&e[parseInt(c)-1][parseInt(a)+1]==void 0?this.x--:(b.fillStyle="#fff",b.fillRect(c,a,this.size,this.size),e[parseInt(c)][parseInt(a)]=1,this.reset())}(this.x>l-i||this.x<i)&&this.reset()};this.reset=function(){this.y=0;this.x=e(i,l-i);this.stepSize=e(1,10)/100;this.size=e(d.minSize*100,d.maxSize*100)/100;this.speed=e(d.minSpeed,d.maxSpeed)}}
function p(){for(c=0;c<j.length;c+=1)j[c].update();q=setTimeout(function(){p()},30)}var d=b.extend({flakeCount:35,flakeColor:"#ffffff",flakeIndex:999999,minSize:1,maxSize:2,minSpeed:1,maxSpeed:5,round:false,shadow:false,collection:false,collectionHeight:40},d),e=function(b,a){return Math.round(b+Math.random()*(a-b))};b(a).data("snowfall",this);var j=[],f=0,c=0,n=b(a).height(),l=b(a).width(),i=0,q=0;if(d.collection!==false)if(f=document.createElement("canvas"),f.getContext&&f.getContext("2d"))for(var m=
[],f=b(d.collection),k=d.collectionHeight,c=0;c<f.length;c++){var g=f[c].getBoundingClientRect(),h=document.createElement("canvas"),r=[];if(g.top-k>0){document.body.appendChild(h);h.style.position="absolute";h.height=k;h.width=g.width;h.style.left=g.left;h.style.top=g.top-k;for(var o=0;o<g.width;o++)r[o]=[];m.push({element:h,x:g.left,y:g.top-k,width:g.width,height:k,colData:r})}}else d.collection=false;b(a).get(0).tagName===b(document).get(0).tagName&&(i=25);b(window).bind("resize",function(){n=b(a).height();
l=b(a).width()});for(c=0;c<d.flakeCount;c+=1)f=j.length,j.push(new s(e(i,l-i),e(0,n),e(d.minSize*100,d.maxSize*100)/100,e(d.minSpeed,d.maxSpeed),f));d.round&&b(".snowfall-flakes").css({"-moz-border-radius":d.maxSize,"-webkit-border-radius":d.maxSize,"border-radius":d.maxSize});d.shadow&&b(".snowfall-flakes").css({"-moz-box-shadow":"1px 1px 1px #555","-webkit-box-shadow":"1px 1px 1px #555","box-shadow":"1px 1px 1px #555"});p();this.clear=function(){b(a).children(".snowfall-flakes").remove();j=[];clearTimeout(q)}};
b.fn.snowfall=function(a){if(typeof a=="object"||a==void 0)return this.each(function(){new b.snowfall(this,a)});else if(typeof a=="string")return this.each(function(){var a=b(this).data("snowfall");a&&a.clear()})}})(jQuery);


$(document).ready(function(){
	
	//Snowfall for xmas
	
	var options = {
		flakeCount : 500,        // number
		flakeColor : '#ffffff', // string
		flakeIndex: 99,     // number
		minSize : 1,            // number
		maxSize : 4,            // number
		minSpeed : 1,           // number
		maxSpeed : 1,           // number
		round : true,          // bool
		shadow : false
	};
	
	$("#doc").snowfall(options);
	
});
*/