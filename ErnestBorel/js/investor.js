var totalResult = 0;
var itemPerPage = 6;
var currentPage = 0;

$(document).ready(function(){

	$(".fr, .jp").hide();
	
	if($("#pagination").length > 0){
		$("#pagination").pagination( 
			totalResult , 
			{
				current_page : currentPage,
				items_per_page: itemPerPage, //number of photos per page
				next_text: "&gt;",
				prev_text: "&lt;",
				num_display_entries: 10,
				num_edge_entries: 0,
				link_to : "investor_announcement/__id__",
				one_base_display:true,
				stopPropagation:false
			}
		);	
	}
    

	
});
