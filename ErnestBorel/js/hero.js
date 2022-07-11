var imgTotal 	= 0;
var videoTVC 	= "";
var video3D 	= "";
var jw_tvc;
var jw_3d;


$(document).ready(function(){

	$(".colorbox").colorbox();

	if(imgTotal > 1){
		var cycleObj = new CycleProgress({container:".imgWrapper"});
		$(".imgWrapper").css("border-bottom","none");
	}

	$('.video.tvc').magnificPopup({
		type:'inline',
		callbacks:{
			open:function(){
				jw_tvc.play();
			},
			close:function () {
				// body...
				jw_tvc.stop();
			}
		}
	});

	$('.video.watch').magnificPopup({
		type:'inline',
		callbacks:{
			open:function(){
				jw_3d.play();
			},
			close:function () {
				// body...
				jw_3d.stop();
			}
		}
	});
	
	jw_tvc = jwplayer("video-tvc").setup({
			    'flashplayer': 'player.swf',
			    'id': 's1-video child',
			    'width': 720,
			    'height': 360,
			    'file': videoTVC,
			    'controlbar.idlehide': true,
			     modes: [
					{ type: "html5" },
					{ type: "flash", src: "player.swf" }
				]
			});

	jw_3d = jwplayer("video-watch").setup({
			    'flashplayer': 'player.swf',
			    'id': 's2-video child',
			    'width': 720,
			    'height': 360,
			    'file': video3D,
			    'controlbar.idlehide': true,
			     modes: [
					{ type: "html5" },
					{ type: "flash", src: "player.swf" }
				]
			});
});