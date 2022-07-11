var CollectionList = {

	init:function(){
		var that = this;

		$("#lang").select2();
		$("#type").select2();
		$("#formSearch").submit(that.search);
	},

	search:function(evt){
		evt.preventDefault();

		//check variable
		var that = this;
		var lang = $("#lang").val();
		var type = $("#type").val();
		var name = $("#name").val();
		var col_ref = $("#col_ref").val();
		
		var postData = {};
		postData.lang = lang;
		postData.name = name;
		postData.type = type;
		postData.col_ref = col_ref;

		var postURL = "api/CollectionSearch.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {
			if(result.status == 0){
				alert(result.message);
			}else{
				if(result.data.length == 0){
					alert("No result found!");
				}else{
					CollectionList.list(result.data);
				}
			}
		});

	},

	list:function(data){
		
		$("#list").html("");
		var lang = $("#lang").val();

		for(var i=0; i< data.length; i++){
			var cell = $("#template").clone(true, true);
			cell.attr("id", "c"+i);
			cell.find(".id").text(data[i].idx_collection);
			cell.find(".name").text(data[i].col_name);
			cell.find(".type").text(data[i].col_movement);
			cell.find(".col_ref").text(data[i].col_ref);
			var lastupdate = (data[i].col_lastupdate).replace("T", " ");
			cell.find(".date").text(lastupdate);
			cell.find(".edit").attr("href","collection_edit.html#id="+data[i].idx_collection);
			cell.find(".delete").attr("href","collection_delete.html#id="+data[i].idx_collection);
			cell.find(".detail").attr("href","collection_detail.html#id="+data[i].idx_collection);
			cell.find(".watches").attr("href","watch_list.html#id="+data[i].idx_collection+"&lang="+lang+"&name="+data[i].col_name+"&col_ref="+data[i].col_ref);

			$("#list").append(cell);
		}
	}
}



var CollectionAdd = {

	init:function(){
		var that = this;
		$("#type").select2();
		$("#form1").submit(that.submitForm);
	},

	submitForm:function(evt){
		evt.preventDefault();
		var r = confirm("Are you sure?");
		if(!r)
			return;

		var postData = checkInput();
		if(!postData){
			return false;
		}

		console.log(postData);
		var postURL = "api/CollectionAdd.ashx";

		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		  .success(function( result ) {
		  	if(result.status == 1){
		  		alert("done!");
		  		var id = $("#idx").val();
				window.location.href = "collection_detail.html#id="+id+"&lang="+lang;
		  	}else{
		  		alert(result.message);
		  	}
		  	console.log(result);
		});
	}

}


var CollectionEdit = {
	init:function(){
		var that = this;
		$("#type").select2();
		that.loadCollection();

		$("#form1").submit(that.submitForm);
	},

	loadCollection:function(){
		var id = getHashValue('id');
		$("#idx").val(id);

		if(id != undefined && id != ""){
			var postData = {};
			postData.id = id;
			var postURL = "api/CollectionDetail.ashx";
			$.ajax({
				type: "POST",
				dataType :"json",
				url: postURL,
				data: postData
			})
			.success(function( result ) {
			  	//select location
			  	var obj = result.data;
			  	$("#type").select2("val", obj.type); 
				$("#col_ref").val(obj.col_ref); 
				$("#name_en").val(obj.name_en);
				$("#name_tc").val(obj.name_tc);
				$("#name_sc").val(obj.name_sc);
				$("#name_fr").val(obj.name_fr);
				$("#name_jp").val(obj.name_jp);
				$("#desc_en").html(obj.desc_en);
				$("#desc_tc").html(obj.desc_tc);
				$("#desc_sc").html(obj.desc_sc);
				$("#desc_fr").html(obj.desc_fr);
				$("#desc_jp").html(obj.desc_jp);
			});	

		}//end if
	},

	submitForm:function(evt){
		evt.preventDefault();
		

		var r = confirm("Are you sure?");
		if(!r)
			return;

		var postData = checkInput();
		if(!postData){
			return false;
		}
		postData.id = $("#idx").val();
		
		//Load submit
		var postURL = "api/CollectionEdit.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {
			if(result.status == 1){
				alert("Edit done!");
				var id = $("#idx").val();
				window.location.href = "collection_detail.html#id="+id;
			}else{
				alert(result.message);
			}
		});
	}
}



var CollectionDetail = {

	init:function(){
		var that = this;
		that.loadCollection();
	},

	loadCollection:function(){
		var id = getHashValue('id');
		$("#idx").text(id);

		if(id != undefined && id != ""){
			var postData = {};
			postData.id = id;
			var postURL = "api/CollectionDetail.ashx";
			$.ajax({
				type: "POST",
				dataType :"json",
				url: postURL,
				data: postData
			})
			.success(function( result ) {
			  	//select location
			  	var obj = result.data;
			  	$("#type").text(obj.type); 
				$("#col_ref").html(obj.col_ref); 
				$("#name_en").html(obj.name_en);
				$("#name_tc").html(obj.name_tc);
				$("#name_sc").html(obj.name_sc);
				$("#name_fr").html(obj.name_fr);
				$("#name_jp").html(obj.name_jp);
				$("#desc_en").html(obj.desc_en);
				$("#desc_tc").html(obj.desc_tc);
				$("#desc_sc").html(obj.desc_sc);
				$("#desc_fr").html(obj.desc_fr);
				$("#desc_jp").html(obj.desc_jp);
				var lastupdate = (obj.lastupdate).replace("T", " ");
				$("#lastupdate").text(lastupdate);
			});	

			$("#edit").attr("href" , "collection_edit.html#id="+id);
			$("#delete").attr("href" , "collection_delete.html#id="+id);

		}//end if
	}

}


var CollectionDelete = {
	id:0,

	init:function(){
		var that = this;
		
		//Load store information
		that.loadStore();
	},

	loadStore:function(){
		var that = this;
		var id = getHashValue('id');
		that.id = id;
		$("#idx").text(id);

		if(id != undefined && id != ""){
			var postData = {};
			postData.id = id;
			var postURL = "api/CollectionDetail.ashx";
			$.ajax({
				type: "POST",
				dataType :"json",
				url: postURL,
				data: postData
			})
			.success(function( result ) {
			  	//select location
			  	var obj = result.data;
			  	
			  	$("#type").text(obj.type); 
				$("#col_ref").text(obj.col_ref); 
				$("#name_en").text(obj.name_en);
				$("#name_tc").text(obj.name_tc);
				$("#name_sc").text(obj.name_sc);
				$("#name_fr").text(obj.name_fr);
				$("#name_jp").text(obj.name_jp);
				
			});	

			$("#btndelete").click(that.delete);

		}//end if
	},

	delete:function(evt){
		evt.preventDefault();
		var r = confirm("Are you sure?");
		if(!r)
			return;

		var that = CollectionDelete;
		var postData = {};
		postData.id = that.id;
		var postURL = "api/CollectionDelete.ashx";

		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {

		  	if(result.status == 1){
				alert("Delete done!");
				window.location.href = "collection_list.html";
			}else{
				alert(result.message);
			}
		  	
		});	
	}
}


function checkInput(){
	
	var type = $("#type").val();
	var col_ref = $("#col_ref").val();
	var name_en = $("#name_en").val();
	var name_sc = $("#name_sc").val();
	var name_tc = $("#name_tc").val();
	var name_fr = $("#name_fr").val();
	var name_jp = $("#name_jp").val();
	var desc_en = $("#desc_en").val();
	var desc_sc = $("#desc_sc").val();
	var desc_tc = $("#desc_tc").val();
	var desc_fr = $("#desc_fr").val();
	var desc_jp = $("#desc_jp").val();

	var msg = "";
	var isError = false;

	//check form
	if(col_ref == ""){
		msg += "please input Collection SEO name\n";
		isError = true;
	}
	if(name_en == ""){
		msg += "please input English - Name\n";
		isError = true;
	}
	if(name_sc == ""){
		msg += "please input 简体中文 - Name\n";
		isError = true;
	}
	if(name_tc == ""){
		msg += "please input 繁體中文 - Name\n";
		isError = true;
	}
	if(name_jp == ""){
		msg += "please input 日本語 - Name\n";
		isError = true;
	}
	if(name_fr == ""){
		msg += "please input FRANÇAIS - Name\n";
		isError = true;
	}

	//alert if any error
	if(isError){
		alert(msg);
		return false;
	}


	//no error, can send information to backend
	var postData = {};
	postData.col_ref = col_ref;
	postData.type = type;
	postData.name_en = name_en;
	postData.name_sc = name_sc;
	postData.name_tc = name_tc;
	postData.name_fr = name_fr;
	postData.name_jp = name_jp;
	postData.desc_en = desc_en;
	postData.desc_sc = desc_sc;
	postData.desc_tc = desc_tc;
	postData.desc_fr = desc_fr;
	postData.desc_jp = desc_jp;

	return postData;
}
