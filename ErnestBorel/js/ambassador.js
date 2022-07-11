var imgTotal = 0;
var jw_tvc;
var jw_file = "";

$(document).ready(function(){

	$(".colorbox").colorbox();
	

    /* Gallery Slider */
    var slider1 = new GallerySlider({
        prevNav: $(".gallery .btnPrev"),
        nextNav: $(".gallery .btnNext"),
        container: $(".gallery .slider ul"),
        timeout_sec: 6
    });

    $('.gallery .slider ul').magnificPopup({
		delegate: 'a',
		type: 'image',
		tLoading: 'Loading image #%curr%...',
		mainClass: 'mfp-img-mobile',
		gallery: {
			enabled: true,
			navigateByImgClick: true,
			preload: [0,1] // Will preload 0 - before current, and 1 after the current image
		},
		image: {
			tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',
			titleSrc: function(item) {
				return ""//item.el.attr('title') + '<small>by Marsel Van Oosten</small>';
			}
		}
	});


	if(imgTotal > 1){
		var cycleObj = new CycleProgress({container:".imgWrapper"});
		$(".imgWrapper").css("border-bottom","none");
	}

	//for video 
	if (jw_file != ""){
		jw_tvc = jwplayer("video").setup({
		    'flashplayer': '/player.swf',
		    'id': 's1-video child',
		    'width': 720,
		    'height': 360,
		    'file': jw_file,
		    'controlbar.idlehide': true
		});

		

		$('.video').magnificPopup({
			type:'inline',
			callbacks:{
				open:function(){
					$(".mfp-content").css({"width":"720px", "height":"360px"});
					jw_tvc.play();
				},
				close:function () {
					// body...
					jw_tvc.stop();
				}
			}
		});
	}

});