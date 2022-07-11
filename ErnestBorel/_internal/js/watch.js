var WatchCommon = {
	_selector : null,

	loadSelector:function(lang){
		var that = this;
		var postData = {};
		postData.lang = lang;

		var postURL = "../api/getSelector.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {
			if(result.status == 1){
				that._selector = result.data;
			}
		});
	},


	getSelectorName:function(type, id){
		var that = this;
		var list = that._selector[type];
		var name = id;

		for(var i = 0; i < list.length; i++){
			if(list[i].id == id){
				name = list[i].name;
			}				
		}
		
		return name;
	},

	generateSelector:function(type, target){
		var that = this;
		var list = that._selector[type];
		for(var i = 0; i < list.length; i++){
			var opt = $("<option/>");
			opt.text(list[i].name).val(list[i].id);
			target.append(opt);
		}
	},

	loadCollection:function(lang, target){
		var that = this;
		var postData = {};
		postData.lang = lang;

		var postURL = "api/CollectionSearch.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {
			console.log(result);
			if(result.status == 1){
				var list = result.data;
				for(var i = 0; i < list.length; i++){
					var opt = $("<option/>");
					opt.text(list[i].col_name).val(list[i].idx_collection);
					target.append(opt);
				}	
			}
			
		});
	}
}


var WatchList = {

	init:function(){
		var that = this;
		
		var id = getHashValue('id');
		$("#id").val(id);

		var lang = getHashValue('lang');
		$("#lang").val(lang);


		var col_ref = getHashValue('col_ref');
		$("#col_ref").val(col_ref);

		var name = getHashValue('name');
		$("#col_name").text(name);

		WatchCommon.loadSelector(lang);
		setTimeout(function(){
			that.loadList();
		}, 800);

		var addLink = $(".btnAdd").attr("href");
		addLink = addLink + "#id="+id+"&lang="+lang+"&name="+name;
		$(".btnAdd").attr("href", addLink);

	},

	loadList:function(){
		var id = $("#id").val();
		var lang = $("#lang").val();

		var postData = {};
		postData.lang = lang;
		postData.id = id;

		var postURL = "api/WatchList.ashx";
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
				WatchList.list(result.data);
			}
		});
	},

	list:function(data){
		$("#list").html("");
		var lang = $("#lang").val();
		var name = getHashValue('name');

		for(var i=0; i< data.length; i++){
			var cell = $("#template").clone(true, true);
			cell.attr("id", "w"+i);
			cell.find(".id").text(data[i].idx_watch);
			cell.find(".gender").text( WatchCommon.getSelectorName("gender", data[i].watch_gender));
			cell.find(".match").text(data[i].watch_matching);
			cell.find(".bracelet").text(WatchCommon.getSelectorName("bracelet", data[i].watch_bracelet));
			cell.find(".case").text(WatchCommon.getSelectorName("case", data[i].watch_case));
			cell.find(".shape").text(WatchCommon.getSelectorName("shape", data[i].watch_shape));
			var surface = WatchCommon.getSelectorName("surface", data[i].watch_surface1);
			surface += ", " + WatchCommon.getSelectorName("surface", data[i].watch_surface2);
			surface += ", " + WatchCommon.getSelectorName("surface", data[i].watch_surface3);
			cell.find(".surface").text(surface);
			var lastupdate = (data[i].watch_lastupdate).replace("T", " ");
			cell.find(".date").text(lastupdate);
			cell.find(".sort").text(data[i].sort);
			cell.find(".edit").attr("href","watch_edit.html#id="+data[i].idx_watch + "&lang="+lang+"&name="+name);
			cell.find(".delete").attr("href","watch_delete.html#id="+data[i].idx_watch + "&lang="+lang+"&name="+name);
			cell.find(".detail").attr("href","watch_detail.html#id="+data[i].idx_watch + "&lang="+lang+"&name="+name);
			$("#list").append(cell);
		}
	}
}


var WatchAdd = {

	init:function(){
		var that = this;
		var id = getHashValue('id');
		var lang = getHashValue('lang');
		var name = getHashValue('name');
		

		WatchCommon.loadCollection(lang, $("#idx_collection"));
		WatchCommon.loadSelector(lang);
		setTimeout(function(){
			//Selector
			WatchCommon.generateSelector("gender", $("#gender"));
			WatchCommon.generateSelector("bracelet", $("#bracelet"));
			WatchCommon.generateSelector("shape", $("#shape"));
			WatchCommon.generateSelector("case", $("#case"));
			WatchCommon.generateSelector("surface", $("#surface1"));
			WatchCommon.generateSelector("surface", $("#surface2"));
			WatchCommon.generateSelector("surface", $("#surface3"));

			$("#idx_collection").val(id).prop("selected", true);

		},800);

		$("#form1").submit(that.submitForm);
	},

	submitForm:function(evt){
		evt.preventDefault();
		var id = getHashValue('id');
		var lang = getHashValue('lang');

		var r = confirm("Are you sure?");
		if(!r)
			return;

		var postData = checkInput();
		if(!postData){
			return false;
		}

		var postURL = "api/WatchAdd.ashx";

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
				window.location.href = "watch_detail.html#id="+id+"&lang="+lang;
		  	}else{
		  		alert(result.message);
		  	}
		});
	}
}


var WatchEdit = {
	init:function(){
		var that = this;
		var lang = getHashValue('lang');

		var id = getHashValue('id');
		$("#idx").val(id);
		WatchCommon.loadCollection(lang, $("#idx_collection"));
		WatchCommon.loadSelector(lang);
		setTimeout(function(){
			that.loadWatch();	
		}, 800);
		
		
		$("#form1").submit(that.submitForm);
	},


	loadWatch:function(){
		
		var id = $("#idx").val();
		var lang = getHashValue('lang');

		if(id != undefined && id != ""){
			var postData = {};
			postData.id = id;
			var postURL = "api/WatchDetail.ashx";
			$.ajax({
				type: "POST",
				dataType :"json",
				url: postURL,
				data: postData
			})
			.success(function( result ) {
			  	//select location
			  	var obj = result.data;
			  	$("#idx_collection").val(obj.idx_collection).prop("selected", true);
				$("#sort").val(obj.sort); 
				$("#spec_en").val(obj.spec_en);
				$("#spec_tc").val(obj.spec_tc);
				$("#spec_sc").val(obj.spec_sc);
				$("#spec_fr").val(obj.spec_fr);
				$("#spec_jp").val(obj.spec_jp);
				$("#matching").val(obj.matching);

				//Selector
				WatchCommon.generateSelector("gender", $("#gender"));
				WatchCommon.generateSelector("bracelet", $("#bracelet"));
				WatchCommon.generateSelector("shape", $("#shape"));
				WatchCommon.generateSelector("case", $("#case"));
				WatchCommon.generateSelector("surface", $("#surface1"));
				WatchCommon.generateSelector("surface", $("#surface2"));
				WatchCommon.generateSelector("surface", $("#surface3"));

				$("#gender").val(obj.gender).prop("selected", true);
				$("#bracelet").val(obj.bracelet).prop("selected", true);
				$("#shape").val(obj.shape).prop("selected", true);
				$("#case").val(obj._case).prop("selected", true);
				$("#surface1").val(obj.surface1).prop("selected", true);
				$("#surface2").val(obj.surface2).prop("selected", true);
				$("#surface3").val(obj.surface3).prop("selected", true);

				var bc_link = $(".bc_watch").attr("href");
				bc_link = bc_link.replace("{id}", obj.idx_collection);
				bc_link = bc_link.replace("{name}", obj.collection);
				bc_link = bc_link.replace("{lang}", lang);
				$(".bc_watch").attr("href", bc_link);

			});	

		}//end if
	},


	submitForm:function(evt){
		evt.preventDefault();
		var lang = getHashValue('lang');

		var r = confirm("Are you sure?");
		if(!r)
			return;

		var postData = checkInput();
		if(!postData){
			return false;
		}
		postData.id = $("#idx").val();
		
		//Load submit
		var postURL = "api/WatchEdit.ashx";
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
				window.location.href = "watch_detail.html#id="+id+"&lang="+lang;
			}else{
				alert(result.message);
			}
		});
	}
}


var WatchDetail = {

	init:function(){
		var that = this;

		var lang = getHashValue('lang');
		WatchCommon.loadSelector(lang);
		setTimeout(function(){
			that.loadWatch();	
		}, 800);
		
	},

	loadWatch:function(){
		var id = getHashValue('id');
		$("#idx").text(id);

		var lang = getHashValue('lang');

		if(id != undefined && id != ""){
			var postData = {};
			postData.id = id;
			var postURL = "api/WatchDetail.ashx";
			$.ajax({
				type: "POST",
				dataType :"json",
				url: postURL,
				data: postData
			})
			.success(function( result ) {
			  	//select location
			  	var obj = result.data;
			  	$("#idx_collection").text(obj.collection); 
				$("#sort").html(obj.sort); 
				obj.spec_en = obj.spec_en.replace(/\r\n/g, "<br>");
				obj.spec_en = obj.spec_en.replace(/\n/g, "<br>");
				$("#spec_en").html(obj.spec_en);
				obj.spec_tc = obj.spec_tc.replace(/\r\n/g, "<br>");
				obj.spec_tc = obj.spec_tc.replace(/\n/g, "<br>");
				$("#spec_tc").html(obj.spec_tc);
				obj.spec_sc = obj.spec_sc.replace(/\r\n/g, "<br>");
				obj.spec_sc = obj.spec_sc.replace(/\n/g, "<br>");
				$("#spec_sc").html(obj.spec_sc);
				obj.spec_fr = obj.spec_fr.replace(/\r\n/g, "<br>");
				obj.spec_fr = obj.spec_fr.replace(/\n/g, "<br>");
				$("#spec_fr").html(obj.spec_fr);
				obj.spec_jp = obj.spec_jp.replace(/\r\n/g, "<br>");
				obj.spec_jp = obj.spec_jp.replace(/\n/g, "<br>");
				$("#spec_jp").html(obj.spec_jp);
				var lastupdate = (obj.lastupdate).replace("T", " ");
				$("#lastupdate").text(lastupdate);

				var matching = (obj.matching == "")?"---":obj.matching;
				$("#matching").text(matching);
				$("#gender").text(WatchCommon.getSelectorName("gender", obj.gender));
				$("#bracelet").text(WatchCommon.getSelectorName("bracelet", obj.bracelet));
				$("#shape").text(WatchCommon.getSelectorName("shape", obj.shape));
				$("#case").text(WatchCommon.getSelectorName("case", obj._case));
				$("#surface1").text(WatchCommon.getSelectorName("surface", obj.surface1));
				$("#surface2").text(WatchCommon.getSelectorName("surface", obj.surface2));
				$("#surface3").text(WatchCommon.getSelectorName("surface", obj.surface3));


				var bc_link = $(".bc_watch").attr("href");
				bc_link = bc_link.replace("{id}", obj.idx_collection);
				bc_link = bc_link.replace("{name}", obj.collection);
				bc_link = bc_link.replace("{lang}", lang);
				$(".bc_watch").attr("href", bc_link);
			});

			$("#edit").attr("href" , "watch_edit.html#id="+id+"&lang="+lang);
			$("#delete").attr("href" , "watch_delete.html#id="+id+"&lang="+lang);

		}//end if
	}
}


var WatchDelete = {
	id:0,
	idx_collection:0,

	init:function(){
		var that = this;

		//Load store information
		that.loadWatch();
	},

	loadWatch:function(){
		var that = this;
		var id = getHashValue('id');
		var name = getHashValue('name');
		var lang = getHashValue('lang');

		that.id = id;
		$("#idx").text(id);

		if(id != undefined && id != ""){
			var postData = {};
			postData.id = id;
			var postURL = "api/WatchDetail.ashx";
			$.ajax({
				type: "POST",
				dataType :"json",
				url: postURL,
				data: postData
			})
			.success(function( result ) {
			  	//select location
			  	var obj = result.data;
			  	
			  	$("#idx_collection").text(name); 
				$("#sort").text(obj.sort); 
				$("#spec_en").text(obj.spec_en);
				$("#spec_tc").text(obj.spec_tc);
				$("#spec_sc").text(obj.spec_sc);
				$("#spec_fr").text(obj.spec_fr);
				$("#spec_jp").text(obj.name_jp);
				
				that.idx_collection = obj.idx_collection;

				var bc_link = $(".bc_watch").attr("href");
				bc_link = bc_link.replace("{id}", obj.idx_collection);
				bc_link = bc_link.replace("{name}", name);
				bc_link = bc_link.replace("{lang}", lang);
				$(".bc_watch").attr("href", bc_link);
			});	

			$("#btndelete").click(that.delete);

		}//end if
	},

	delete:function(evt){
		evt.preventDefault();
		var that = WatchDelete;

		var id = getHashValue('id');
		var lang = getHashValue('lang');
		var name = getHashValue('name');

		var r = confirm("Are you sure?");
		if(!r)
			return;

		var postData = {};
		postData.id = id;
		var postURL = "api/WatchDelete.ashx";

		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {

		  	if(result.status == 1){
				alert("Delete done!");
				window.location.href = "watch_list.html#id="+that.idx_collection+"&lang="+lang+"&name="+name;
			}else{
				alert(result.message);
			}
		  	
		});	
	}
}


function checkInput(){
	
	var idx = $("#idx").val();
	var idx_collection = $("#idx_collection").val();
	var spec_en = $("#spec_en").val();
	var spec_sc = $("#spec_sc").val();
	var spec_tc = $("#spec_tc").val();
	var spec_fr = $("#spec_fr").val();
	var spec_jp = $("#spec_jp").val();
	var sort = $("#sort").val();
	var gender = $("#gender").val();
	var matching = $("#matching").val();
	var bracelet = $("#bracelet").val();
	var _case = $("#case").val();
	var shape = $("#shape").val();
	var surface1 = $("#surface1").val();
	var surface2 = $("#surface2").val();
	var surface3 = $("#surface3").val();

	console.log(idx_collection);
	console.log(spec_en);

	var msg = "";
	var isError = false;

	//check form

	if($.trim(idx) == ""){
		msg += "please select Model Number \n";
		isError = true;
	}

	if(idx_collection == ""){
		msg += "please select Collection \n";
		isError = true;
	}
	if($.trim(spec_en) == ""){
		msg += "please input English - Spec\n";
		isError = true;
	}
	if($.trim(spec_sc) == ""){
		msg += "please input 简体中文 - Spec\n";
		isError = true;
	}
	if($.trim(spec_tc) == ""){
		msg += "please input 繁體中文 - Spec\n";
		isError = true;
	}
	if($.trim(spec_jp) == ""){
		msg += "please input 日本語 - Spec\n";  
		isError = true;
	}
	if($.trim(spec_fr) == ""){
		msg += "please input FRANÇAIS - Spec\n";
		isError = true;
	}

	if($.trim(sort) == ""){
		msg += "please input sort	\n";
		isError = true;
	}

	//alert if any error
	if(isError){
		alert(msg);
		return false;
	}


	//no error, can send information to backend
	var postData = {};
	postData.idx = idx;
	postData.idx_collection = idx_collection;
	postData.spec_en = spec_en;
	postData.spec_sc = spec_sc;
	postData.spec_tc = spec_tc;
	postData.spec_fr = spec_fr;
	postData.spec_jp = spec_jp;
	postData.sort = sort;
	postData.gender = gender;
	postData.matching = matching;
	postData.bracelet = bracelet;
	postData._case = _case;
	postData.shape = shape;
	postData.surface1 = surface1;
	postData.surface2 = surface2;
	postData.surface3 = surface3;
	return postData;
}
