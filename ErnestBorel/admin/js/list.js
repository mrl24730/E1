var fullList;
var obj;
var ttl = 0;
var rpp = 10;

var status_all = 0;
var status_published = 0;
var status_pending = 0;
var status_withheld = 0;

$(document).ready(function(){
    //init JQuery UI elements
    $("#selectmenu").selectmenu({change:filterList});
    $("#selectmenu #all").text("All ("+status_all+")");
    $("#selectmenu #published").text("Published ("+status_published+"/"+ status_all +")");
    $("#selectmenu #pending").text("Pending ("+status_pending+"/"+ status_all +")");
    $("#selectmenu #withheld").text("Withheld ("+status_withheld+"/"+ status_all +")");


    $("#btnCreate").button();
    $("#btnCreate").click(function () {
        window.location = "IR_pressDetail.aspx?act=create";
    });

    $(".irRecord").click(function(){
        var id = $(this).attr("data-id");
        window.location = "ir_pressDetail.aspx?rec_idx=" + id;
    });

    $("#pagination").pagination( 
        ttl , 
        {
            current_page: 0, // 6th page, 12 x 5 items already displayed
            items_per_page: rpp, //number of photos per page
            next_text: "&gt;",
            prev_text: "&lt;",
            num_display_entries: 10,
            num_edge_entries: 0,
            load_first_page:false,
            callback: showPage
        }
    );

    showPage(0);

});

function showPage(page){
    var start = page * rpp;
    var end = (page+1)*rpp;
    end = (end > ttl)? ttl: end;

    $("#content").html("");
    for(var i = start; i< end; i++){
        var cell = $("#template").clone(true, true);
        cell.attr({"id":"r"+ obj[i].rec_idx, "data-id": obj[i].rec_idx});
        cell.find(".no").text((i+1));
        cell.find(".release").text(obj[i].ir_releaseDate.replace("T"," "));
        cell.find(".status").text(obj[i].ir_statusDisplay).addClass(obj[i].ir_statusDisplay);
        cell.find(".title").text(obj[i].ir_title);
        cell.find(".lang").text(obj[i].ir_langStr);
        cell.find(".update").text(obj[i].ir_lastUpdated.replace("T"," "));
        $("#content").append(cell);
    }
}

function filterList(){
    
    var status = $("#selectmenu").val();
    if(status == "all") {
        obj = fullList;
    }else{
        obj = new Array();
        for(var i = 0; i < fullList.length; i++){
            if(status == fullList[i].ir_statusDisplay){
                obj.push(fullList[i]);
            }
        }
    }
    ttl = obj.length;

    showPage(0);
}
