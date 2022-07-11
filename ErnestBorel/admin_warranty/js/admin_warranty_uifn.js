var UploadObj = {};
$(document).ready(function(){
	$( "input[type=submit], a.btnDownload, button").button();

	$('input:text').button().addClass('ui-textfield');

	$("#FormSearch").submit(function(e){

		if(	$.trim($("#WarrantyNum").val()) != "" || 
			$.trim($("#CaseNum").val()) != "" || 
			$.trim($("#Phone").val()) != ""){

			var postData = $(this).serializeArray();
		
			$.ajax({
			  type: "POST",
			  url: "api/getSearchWarranty.ashx",
			  data: postData
			})
			  .success(function( response ) {
			  	console.log(response);
			  	if(response.status === 1){

			  		//Reset Result
			  		$(".tbl_searchResult tbody").empty();
			  		var list = response.data;
			  		if(list.length > 0){
			  			for(var i = 0; i < list.length;i++){

			  				var data = list[i];
			  				var tr = $(".template tr").clone();
			  				$.each(data,function(key,val){
					  			tr.find(".Res" + key).text(val)
					  		});

					  		tr.find(".ResName").text(data.Title + " " + data.Name);
					  		tr.find(".ResDop").text(data.Dop.split("T")[0]);
					  		tr.find(".ResCountryCity").html(data.Country + "<br>" + data.City);
					  		tr.find(".ResExtendedDate").text(data.ExtendedDate.split("T")[0]);
					  		tr.find(".ResCreateDate").text(data.CreateDate.replace("T","  "));
					  		if(!data.InvNum) tr.find(".ResInvNum").text("N/A")
					  		if(!data.CaseNum) tr.find(".ResCaseNum").text("N/A");
					  		if(!data.Email) tr.find(".ResEmail").text("N/A");

					  		$(".tbl_searchResult tbody").append(tr);
					  		$("#message").text("");
			  			}
			  		}else{
			  			// No result
			  			$("#message").text("No search result.");
			  		}

			  	}else{
					$(".tbl_searchResult tbody").empty();
					$("#message").text("No search result.");
			  	}
			    
			  });
		}else{
			$("#message").text("Please input search criteria.");
		}

		

		e.preventDefault();
		//e.unbind();
	});


	$("#FormUpload").submit(function(e){

		var type = $('input[name=SettingType]:checked', '#FormUpload').val();
		var file = $("#FileUpload").val();
		if(	type != "" && file != ""){

			var data = new FormData();

			var files = $("#FileUpload").get(0).files;
			data.append("SettingType",type);
			if (files.length > 0) {
	           data.append("FileUpload", files[0]);
	      }
		
			$.ajax({
				type: "POST",
				url: "api/uploadWarrantySetting.ashx",
				contentType: false,
				processData: false,
				data: data
			})
			  .success(function( response ) {
			  	
			  	var txt = "";
			  	var title = "";
			  	var dialogObj = {
			  		resizable: false,
					height:140,
					modal: false
					
			  	};
			  	if(response.status == 1){
			  		$("#consolePanel").empty();
			  		UploadObj.Guid = response.data.Guid;
			  		UploadObj.Type = response.data.Type

			  		
			  		txt = 'These have <b class="number">{num}</b> rows will be insert to Database. Are you sure?';
			  		txt = txt.replace('{num}',response.data.Total);

			  		dialogObj.title = "Adding new " + response.data.Type;
			  		dialogObj.buttons = {
						"Okay": function() {
							processXlsData();
							$(this).dialog( "close" );
						},
						Cancel: function() {
							$(this).dialog( "close" );
						}
					};

			  	}else if(response.status == 2){

			  		dialogObj.title = "Unable to save file";
			  		txt = response.message;
			  		dialogObj.buttons = {
						"Okay": function() {
							$(this).dialog( "close" );
						}
					};

			  	}else if(response.status == 3){
			  		dialogObj.title = "Incorrect file format";
			  		txt = "Please upload a proper xls or xlsx file";
			  		dialogObj.buttons = {
						"Okay": function() {
							$(this).dialog( "close" );
						}
					};
				}else if(response.status == 4){

					dialogObj.title = "Missing file";
			  		dialogObj.buttons = {
						"Okay": function() {
							$(this).dialog( "close" );
						}
					};


			  	}else if(response.status == 5){
			  		//Timeout
			  		dialogObj.title = "Session timeout";
			  		dialogObj.buttons = {
						"Reload": function() {
							location.reload();
							$(this).dialog( "close" );
						}
					};
			  		
			  	}

			  	//$("#dialog-confirm").prop("title",title);
			  	$("#dialog-confirm .txt").html(txt);
			  	$("#dialog-confirm").dialog(dialogObj);
			    
			  });
		}



		


		e.preventDefault();

		
	});
	
	$(".datepicker").datepicker({
		dateFormat:"yy-mm-dd",
		maxDate: 0
	});


	
	$(".btnDownload").click(function(e){
		if($("#txtFrom").val() != "" && $("txtTo").val() != ""){
			var href = "api/getWarrantyRegistration.ashx?From={From}&To={To}";
			href = href.replace("{From}",$("#txtFrom").val());
			href = href.replace("{To}",$("#txtTo").val());
			$(this).attr("href",href);
		}else{
			e.preventDefault();
		}
		
	});

	$("#SettingsGroup").buttonset();
});

function processXlsData(){
	UploadObj.status = "Processing";
	var data = {
		Status : "Start",
		Guid : UploadObj.Guid,
		Type : UploadObj.Type
	}
	$.ajax({
		type: "POST",
		url: "api/processStatus.ashx",
		dataType: 'json',
		data: data
	})
	  .success(function( response ) {
	  	
	  	if(response.status == 1){
	  		
	  		$("#consolePanel").append($("<p/>").text("Starting Process...."));
	  	}else if(response.status == 2){
	  		
	  	}else if(response.status == 3){


	  	}else{

	  	}
	    
	  });

	monitorXlsData();
}
var intervalMonitor;

function monitorXlsData(){
	intervalMonitor = setInterval(intervalMonitorFN,2000);
}

function intervalMonitorFN(){

	if(UploadObj.status == "Processing"){
		var data = {
			Status : "Monitor"
		};

		$.ajax({
			type: "POST",
			url: "api/processStatus.ashx",
			dataType: 'json',
			data: data
		})
		  .success(function( response ) {
		  	
		  	
		  	if(response.status == 1){
		  		var str = "";
		  		switch(response.data.status){
		  			case "Processing":
		  				if(response.data.current == 0 && response.data.ttl == 0){
		  					//Assume Reading
		  					str = "Reading Excel file";
		  				}else{
		  					str = "Processing {current} / {ttl}...";
		  					str = str.replace("{current}",response.data.current);
	  						str = str.replace("{ttl}",response.data.ttl);		  		
		  				}
		  				break;
		  			case "Error":
		  				UploadObj.status = "Error";
	  					str = "Something Error near row {current} - {current2} , Please check the excel file: " + response.data.message;
	  					str = str.replace("{current}",response.data.current);
	  					str = str.replace("{current2}", (response.data.current+100));
	  					str = str.replace("{ttl}",response.data.ttl);
	  					break;
	  				case "Complete":
	  					UploadObj.status = "Complete";
	  					str = "Process Completed";
	  					break;
		  		}

		  		$("#consolePanel").append($("<p/>").text(str));
		  		
		  	}
		    
		});
	}else{
		//Should be completed or error
		clearInterval(intervalMonitor);
	}
	
}