var lang = "en";
var itemPerPage = 6;
var totalResult = 0;
var result;

$(document).ready(function(){

	//init multi-selectable input
	$("#sel-gender").select2({
    	allowClear: true,
	}); 
	$("#sel-bracelet").select2({
    	allowClear: true
	}); 
	$("#sel-shape").select2({
    	allowClear: true
	}); 
	$("#sel-material").select2({
    	allowClear: true
	}); 
	$("#sel-cover").select2({
    	allowClear: true
	}); 

	$("#resultCount").hide();

	$(".btnSearch").click(doSearch);

	if(keyword != ""){
		$("#keyword").val(keyword);
		$(".btnSearch").click();
	}

});

function doSearch(){
	var type = $(this).attr("rel");
	var postData = {};
	postData.lang 		= lang;
	postData.type 		= type;
	postData.gender 	= ($("#sel-gender").val() == null)? null : ($("#sel-gender").val()).toString();
	postData.bracelet 	= ($("#sel-bracelet").val() == null)? null : ($("#sel-bracelet").val()).toString();
	postData.shape 		= ($("#sel-shape").val() == null)? null : ($("#sel-shape").val()).toString();
	postData.material 	= ($("#sel-material").val() == null)? null : ($("#sel-material").val()).toString();
	postData.cover 		= ($("#sel-cover").val() == null)? null : ($("#sel-cover").val()).toString();
	postData.keyword 	= $("#keyword").val();

	$.ajax({
		type: "POST",
		url: "/api/getSearch.ashx",
		data: postData,
		dataType: "json"
	}).done(function(evt) {
		result = evt;
		searchResult();
	});

}

function searchResult(){
	var ttl = result.length;
	$("#resultCount").show();
	$("#resultCount span").text(ttl);

	if(result.length > 0){
		totalResult = result.length;
		
	    $("#pagination").pagination( 
			totalResult , 
			{
				current_page: 0, //	6th page, 12 x 5 items already displayed
				items_per_page: itemPerPage, //number of photos per page
				next_text: "&gt;",
				prev_text: "&lt;",
				num_display_entries: 10,
				num_edge_entries: 0,
				load_first_page:false,
				callback: showPage
			}
		);

		showPage(0);
	}else{
		$("#resultList").html("");
		$("#pagination").html("");
	}
}
function showPage (page){
	
	$("#pagination").find("a").attr("href", "javascript:void(0)");
	$("#resultList").html("");
	$(window).scrollTop(0);

	var start = parseInt(page) * itemPerPage;
	var end = (page + 1) * itemPerPage;
	end = (end > totalResult)? totalResult: end;
	
	for(var i = start ; i < end ; i++){
		var imgPath = (result[i].idx_watch).replace("-","_");
		var url_ref = imgPath;
		imgPath = "/images/watches/"+imgPath+"_t.png";

		var img = $("<img/>");
		img.attr({"src":imgPath,"data-id":i});
		img.load(function(){
			//nothing to do
		}).error(function(){
			imgPath = "/images/watches/noimage_t.png";
			var id = $(this).attr("data-id")
			$("#result"+id).find("img").attr("src", imgPath);
		});

		var cell = $("#resultCell").clone();
		cell.attr("id","result"+i);
		cell.find(".url").attr("href", "wristwatch/"+result[i].col_movement+"/"+result[i].col_ref+"/#"+url_ref);
		cell.find(".url").attr("title", result[i].col_name);
		cell.find("img").attr("src", imgPath);
		cell.find(".col_name").text(result[i].col_name);
		cell.find(".desc").text(result[i].idx_watch);
		$("#resultList").append(cell);
	}
}
