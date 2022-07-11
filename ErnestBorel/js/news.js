
$(document).ready(function(){

	$(".desc").ellipsis();
	
    $("#pagination").pagination( 
		totalResult , 
		{
			current_page : currentPage,
			items_per_page: itemPerPage, //number of photos per page
			next_text: "&gt;",
			prev_text: "&lt;",
			num_display_entries: 10,
			num_edge_entries: 0,
			link_to : "newspage/__id__",
			one_base_display:true,
			stopPropagation:false
		}
	);

	
});