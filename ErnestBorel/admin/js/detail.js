var isNew = false;
var recordID = "";

$(document).ready(function () {

    initPage();

    //init controls
    $(':button').each(function () {
        $("#" + this.id).button();
    });

    $(':file').each(function () {
        $("#" + this.id).css("margin", "0px").button();
    });


    $("#sel_lang").selectmenu();


    $("#btnWithdraw").click(function () {

        var ans = confirm("Withdraw the current Investor Release?");
        if (ans) {

            $("#input_withdraw").val(recordID);
            window.onbeforeunload = null;
            frm_detail.submit();
        }
    });

    $("#btnReactivate").click(function () {

        var ans = confirm("Reactivate the current Investor Release?");
        if (ans) {

            $("#input_reactivate").val(recordID);
            window.onbeforeunload = null;
            frm_detail.submit();
        }
    });

    $("#btnEdit").click(function () {

        //hide edit btn
        $("#btnEdit").css("display", "none");

        //show save btn
        $("#btnSave").css("display", "inline-block");

        //edit release date
        $("#edit_releaseDate").css("display", "block");
        $("#disp_releaseDate").css("display", "none");

        //show text box;
        $("#submitFrm div").each(function () {

            if (this.id.indexOf("disp_") >= 0) {

                $(this).css("display", "none");

            } else if (this.id.indexOf("edit_") >= 0) {

                $(this).css("display", "block");
                $(this).change(function () { changedNotSave(); });

            }
        });
    }); //end btnEdit;

    $("#btnSave").click(function () {

        window.onbeforeunload = null;
        frm_detail.submit();

    }); //end btnSave;


    if (isNew) {
       $('#btnEdit').click();
       $("#btnWithdraw").css("display","none");
    }


    //hh:mm spinners
    $("#spinnerHH").css("width", "2em").focusout(function () {
        this.value = checkValWrap(this.value, 0, 23);

    }).spinner(
    {
        spin: function (event, ui) {
            var val = ui.value;
            if (val < 10 && val >= 0) val = "0" + val;
            var limit = checkValWrap(val, 0, 23);
            if (limit != val) {
                $(this).spinner("value", limit);
                return false;
            }

        }
    });


    $("#spinnerMM").css("width", "2em").focusout(function () {
        this.value = checkValWrap(this.value, 0, 59);

    }).spinner(
    {
        spin: function (event, ui) {
            var val = ui.value;
            if (val < 10 && val >= 0) val = "0" + val;
            var limit = checkValWrap(val, 0, 59);
            if (limit != val) {
                $(this).spinner("value", limit);
                return false;
            }
        }
    });
    

    //function
    $("#btnAddLang").click(function () {
        var lang = $("#sel_lang").val();

        if (lang == "") {

            alert("Please select language.");

        } else {
            //show save btn
            $("#btnSave").css("display", "inline-block");

            var id = "release_" + lang;

            if ($("#" + id).length > 0) {
                alert("The selected language already exist!");
                return;
            }

            var frm = $("#baseFrm").clone().attr("id", id).css("display", "block").appendTo("#submitFrm");

            //change elements to lang specific ID
            $('#' + id + " *").each(function () {
                if (this.id != "") {
                    $(this).attr("id", this.id + "_" + lang);
                    $(this).attr("name", this.id);
                }
            });

            $("#input_lang_" + lang).val($("#sel_lang")[0].selectedIndex);

            $("#span_lang_" + lang).html(lang);
        }
    })
});//end document ready






/*********************************************** Generic Function ******************************************************/
function changedNotSave() {
    window.onbeforeunload = confirmOnPageExit;
}


//before upload
var confirmOnPageExit = function (e) {
    // If we haven't been passed the event get the window.event
    e = e || window.event;

    var message = 'Record(s) has been changed. Leave page without saving?';

    // For IE6-8 and Firefox prior to version 4
    if (e) {
        e.returnValue = message;
    }

    // For Chrome, Safari, IE8+ and Opera 12+
    return message;
};


function checkValWrap(val, min, max) {
    val = parseInt(val, 10);

    if (!isNaN(val)) {
        if (val > max) {
            val = min
        } else if (val < min) {
            val = max;
        }
    } else {
        val = 0;
    }

    if (val < 10 && val >= 0) {
        val = "0" + val;
    }

    return val;
}

function initPage(){

        if(isWithdrawn){
            $("#btnWithdraw").hide();
        }else{
            $("#btnReactivate").hide();
        }

        if(isNew){
            $("h2").text("Investor Relation - New Announcement");
            $("#ir_lastUpdated").text("---");
        }else{
            $("h2").text("Investor Relation - Announcement ID: " + recordID);
            $("#ir_lastUpdated").text(obj.ir_lastUpdated.replace("T", " "));
            $("#isNewRec").hide();
        }

        $("#input_recIdx").val(recordID);
        $("#disp_releaseDate").text(obj.ir_releaseDate.replace("T", " "));
        $("#ir_status").text(obj.ir_statusDisplay).addClass(obj.ir_statusDisplay);


        for (var i = 0; i < obj.list.length; i++){
            var lang = obj.list[i].langStr;
            var cell = $("#recordTemplate").clone();
            cell.removeAttr("id");
            cell.find("#rec_idx").attr({"id":"rec_idx_"+lang, "name":"rec_idx_"+lang}).val(obj.list[i].rec_idx);
            cell.find("#input_lang").attr({"id":"input_lang_"+lang, "name":"input_lang_"+lang}).val((obj.list[i].lang + 1));
            cell.find("#span_lang").attr("id","span_lang_"+lang).text(lang);
            cell.find("#disp_title").text(obj.list[i].title);
            cell.find("#input_title").attr({"id":"input_title_"+lang, "name":"input_title_"+lang}).val(lang).val(obj.list[i].title);
            cell.find("#disp_desc").text(obj.list[i].desc);
            cell.find("#input_desc").attr({"id":"input_desc_"+lang, "name":"input_desc_"+lang}).val(lang).val(obj.list[i].desc);
            cell.find("#disp_path").attr("href","/pdf/"+obj.list[i].file).text(obj.list[i].file + "(" + obj.list[i].filesizeStr + ")");
            cell.find("#input_file").attr({"id":"input_file_"+lang, "name":"input_file_"+lang});
            cell.find("#org_filename").attr({"id":"org_filename_"+lang, "name":"org_filename_"+lang}).val(obj.list[i].file);
            cell.find("#org_filesize").attr({"id":"org_filesize_"+lang, "name":"org_filesize_"+lang}).val(obj.list[i].filesize);

            $("#submitFrm").append(cell);

        }
    }