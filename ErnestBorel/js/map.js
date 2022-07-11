// JavaScript Document

var continents_json = {
							"asia"			: 0, 
							"euro" 			: 1,
							"middle_east" 	: 2,
							"north_america" : 3
					  };


$(document).ready(function(){
	//When Change Select ReGen MapLink
	$("select").click(function(){
		setTimeout(function(){composeMapLink();},500);
	});
	
	//MouseOver on Map
	$("#map_panel area").hover(
		function(){
			$("img.continents."+$(this).attr("data-pos")).addClass("active");
		},
		function(){
			$("img.continents.active."+$(this).attr("data-pos")).removeClass("active");	
		}
	);

	//Map onClick
	$("#map_panel area").click(function(){
		onContinent($(this).attr("data-pos"));
		var selectIndex = continents_json[$(this).attr("data-pos")];
		$("#region").val('opt-r'+selectIndex);
		$("#region").change();
		setTimeout(function(){$("#country").change();},100);
	});

	function composeMapLink(){
		//Gen Google Map link
		$(".addBox").each(function(){

			if($(this).find(".map_link").length >= 1)
				return;

		 	var mapLink = $("<a />");

		 	mapLink.addClass("map_link");
		 	mapLink.attr("href","../map.php");
		 	
		 	var displayText = "Map";
		 	switch(display_lang)
		 	{
		 		case "en":
		 			displayText = "Map";
		 			break;
		 		case "fr":
		 			displayText = "Carte";
		 			break;
		 		case "jp":
		 			displayText = "マップ";
		 			break;
		 		case "sc":
		 			displayText = "地图";
		 			break;
		 		case "tc":
		 			displayText = "地圖";
		 			break;
		 	}
		 	mapLink.html(displayText);

			$(this).append(mapLink);

		 });
		
		//fancyBox
	 	$(".map_link").fancybox({
			'autoScale'	:   false,
			'background':  	'transprent',
			'height'	:	400,
			'type' 		:   'iframe',
			'width'		:	914,
			'scrolling' : 	'no', 
			'padding'	: 	0,
			'margin'	:	0
			
		});

		$(".map_link").hover(function(event){
			$("#showMap").attr("id","");
			$(this).parents(".shop").attr("id","showMap");
		});

	}
	//First Time Gen MapLink
	setTimeout(function(){
		composeMapLink();
		initMap();
		$("#region").change(function(){
			var selectedContinent = getJsonElmByIndex(continents_json, $(this).find(":selected").val().replace("opt-r",""));
			onContinent(selectedContinent);
			$("#country").val($("#country option")[1].value);
			
			setTimeout(function(){
				$("#country").change();
				setTimeout(function(){
					var citySelected = $("#city option[value="+$("#country option")[1].value+"-cc0]");
					if(citySelected.length < 1)
					{
						return;
					}
					$("#city").val($("#country option")[1].value+"-cc0");
					$("#city").change();
					composeMapLink();
				},100);
			},100);

		});

	},100);

});

function getMapInfo()
{
	return $("#showMap");
}

function initMap()
{
	var index = $("#region").find(":selected").val().replace("opt-r","");
	onContinent(getJsonElmByIndex(continents_json,index));
}

function onContinent(selector)
{
	$("img.continents.selected").removeClass("selected");
	$("img.continents."+selector).addClass("selected");
}

function getJsonElmByIndex(obj, idx){
	for ( var key in obj) {
	  if (obj.hasOwnProperty(key) && obj[key] == idx) {
	      return key;
	  }
	}
}