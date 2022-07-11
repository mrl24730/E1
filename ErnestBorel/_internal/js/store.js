var cur_region = 0;
var cur_country = 1;
var cur_province = 0;
var cur_city = 0;
var cur_lang = 'sc';

var LocationObj = {

	// ebMapClass : null,
	locationData:null,

	initialize:function(){

		var that = this;

		//generate region / country / city selection
		this.loadSelection("region",$("#region"));
		this.loadSelection("country",$("#country"));
		this.loadSelection("city",$("#city"));
		//$("#province").hide();

		$(".province_wrapper").hide();

		$("#region").select2("val", cur_region); 
		$("#country").select2("val", cur_country); 
		$("#city").select2("val", "all");
        
		$("#lang").change(function () {
		    cur_lang = $('#lang').val();
		    StoreList.reloadCityList();
		});
        
		$("#region").change(function(){
			var p = $(this).val();
			cur_region = p;
			that.loadSelection("country",$("#country"));
			$("#country").change();
		});

		$("#country").change(function(){
			console.log("changed?");
			var p = $(this).val();
			cur_country = p;
			if(that.locationData.list[cur_region].list[cur_country].id == "CN"){
				//$("#province").show();
				$(".province_wrapper").show();
				that.loadSelection("province",$("#province"));
				$("#province").change();
			}else{
				//$("#province").hide();
				$(".province_wrapper").hide();
				that.loadSelection("city",$("#city"));
				$("#city").select2("val", "all");
			}
		});

		$("#province").change(function(){
			var p = $(this).val();
			cur_province = p;
			that.loadSelection("city",$("#city"));
			$("#city").change();
		});
		
		/*
		$("#city").change(function(){
			var p = $(this).val();
			cur_city = p;
		});
		*/
	},

	loadData: function(_data){
		this.locationData = _data;
	},

	loadSelection: function (type, target) {
	    var data;
	    target.html("");

	    if (type == "region") {
	        data = this.locationData.list;
	    } else if (type == "country") {
	        data = this.locationData.list[cur_region].list;
	    } else if (type == "province") {
	        data = this.locationData.list[cur_region].list[cur_country].list;
	    } else if (type == "city" && this.locationData.list[cur_region].list[cur_country].id == "CN") {
	        data = this.locationData.list[cur_region].list[cur_country].list[cur_province].list;
	    } else {
	        data = this.locationData.list[cur_region].list[cur_country].list;
	    }

	    if (type == "city") {
	        var opt = $("<option/>");
	        opt.val("all").text("All");
	        target.append(opt);
	    }


	    for (var i = 0; i < data.length; i++) {

	        //selected = (selected == "")? data[i].id : selected;
	        var opt = $("<option/>");
	        opt.val(i).text(data[i].name).data("id", data[i].id);
	        target.append(opt);
	    }

	    $(target).select2();
	},

	reloadSelection: function () {
	    this.loadSelection("region", $("#region"));
	    this.loadSelection("country", $("#country"));
	    this.loadSelection("city", $("#city"));
	}
}


var StoreList = {

	locationData: null,

	init:function(){
		var that = this;
		var postData = {};
		postData.lang = cur_lang;
		var postURL = "../api/getCityList.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
			.success(function( result ) {
			  	//console.log(result);
				//region = result.region;
				//country = result.country;
				//city = result.city;
				that.locationData = result;
				LocationObj.loadData(result);
				LocationObj.initialize();
				
		});
		$("#lang").select2();
		$("#formSearch").submit(that.search);
	},
	reloadCityList:function(){
	    var that = this;
	    var postData = {};
	    postData.lang = cur_lang;
	    var postURL = "../api/getCityList.ashx";
	    $.ajax({
	        type: "POST",
	        dataType: "json",
	        url: postURL,
	        data: postData
	    })
			.success(function (result) {
			    that.locationData = result;
			    LocationObj.loadData(result);
			    LocationObj.reloadSelection();

			});
	},
	search:function(evt){
		evt.preventDefault();

		var that = StoreList;
		//check variable
		var lang = $("#lang").val();
		var region = $("#region").val();
		var province = $("#province").val();
		var country = $("#country").val();
		var city = $("#city").val();
		var isPos = ($('#pos').prop('checked'))?"1":"0";
		var isAftersales = ($('#aftersales').prop('checked'))?"1":"0";
		var name = $("#name").val();
		var address = $("#address").val();
		var tel = $("#tel").val();
		var email = $("#email").val();
		var include_deleted = $("#include_deleted").val();

		if(isPos== "0" && isAftersales == "0"){
			alert("Please select type, POS / Aftersales");
			return false;
		}

		var postData = {};
		postData.lang = lang;
		postData.region = $("#region option:selected").data("id");
		postData.country = $("#country option:selected").data("id");
		if(postData.country == 'CN')
			postData.province = $("#province option:selected").data("id");

		if(city == "all"){
			postData.city = city;
		}else{
			postData.city = $("#city option:selected").data("id");
		}

		postData.pos = isPos;
		postData.aftersales = isAftersales;
		postData.name = name;
		postData.address = address;
		postData.tel = tel;
		postData.email = email;
		postData.include_deleted = include_deleted;


		var postURL = "api/StoreSearch.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {
			if(result.status == 0){
				alert(result.Message);
				return;
			}
			StoreList.listStore(result.data);
		});

	},

	listStore:function(data){
		
		$("#list").html("");

		if(data.length == 0){
			alert("no search result.");
			return;
		}
		
		for(var i=0; i< data.length; i++){
			var cell = $("#template").clone(true, true);
			cell.attr("id", "s"+i);
			cell.find(".pos").text(data[i].is_pos);
			cell.find(".aftersales").text(data[i].is_aftersales);
			cell.find(".city").text(data[i].idx_city);
			cell.find(".name").text(data[i].shop_name);
			cell.find(".address").text(data[i].shop_address);
			cell.find(".tel").text(data[i].shop_tel);
			cell.find(".email").text(data[i].shop_email);
			cell.find(".is_deleted").text(data[i].is_deleted);
			cell.find(".edit").attr("href","store_edit.html#id="+data[i].idx_shop);
			if(data[i].is_deleted){
				cell.find(".delete").attr("href","store_restore.html#id="+data[i].idx_shop).text("Restore");
			}else{
				cell.find(".delete").attr("href","store_delete.html#id="+data[i].idx_shop);
			}
			cell.find(".detail").attr("href","store_detail.html#id="+data[i].idx_shop);

			$("#list").append(cell);
		}
	}
}


var StoreAdd = {
	
	locationData: null,

	init:function(){
		var that = this;
		var postData = {};
		postData.lang = cur_lang;
		var postURL = "../api/getCityList.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {
		  	//console.log(result.region);
			that.locationData = result;
			LocationObj.loadData(result);
			LocationObj.initialize();
		});

		$("#form1").submit(StoreAdd.submitForm);
	},

	submitForm:function(evt){
		evt.preventDefault();
		var postData = checkStoreInput();
		if(!postData){
			return false;
		}

		postData.region = $("#region option:selected").data("id");
		postData.country = $("#country option:selected").data("id");

		if(city == "all"){
			alert("please select a city");
		}else{
			postData.city = $("#city option:selected").data("id");
		}

		if(postData.country == 'CN'){
			postData.province = $("#province option:selected").data("id");
		}else{
			postData.province = postData.city;
		}

		var postURL = "api/StoreAdd.ashx";

		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		  .success(function( result ) {
		  	if(result.status == 1){
		  		alert("done!");
		  		window.location.href = "store_detail.html#id="+result.data;
		  	}else{
		  		alert(result.message);
		  	}
		});
	}
}


var StoreEdit = {
	
	locationData: null,

	init:function(){
		var that = this;
		
		//Load all location 
		var postData = {};
		postData.lang = cur_lang;
		postData.type = "all";
		var postURL = "../api/getCityList.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {
		  	//console.log(result.region);
			that.locationData = result;
			LocationObj.loadData(result);
			LocationObj.initialize();
			that.loadStore();
			
		});

		$("#form1").submit(that.submitForm);
	},

	loadStore:function(){

		var that = this;
		var id = getHashValue('id');
		$("#idx_shop").val(id);

		if(id != undefined && id != ""){
			var postData = {};
			postData.id = id;
			var postURL = "api/StoreDetail.ashx";
			$.ajax({
				type: "POST",
				dataType :"json",
				url: postURL,
				data: postData
			})
			.success(function( result ) {
			  	var obj = result.data;
			  	
			  	//select location
			  	var rid = 0, cid = 0, pid = 0, ccid = 0;
			  	var myList = that.locationData.list;
			  	for(var i = 0; i < myList.length; i++){
			  		if(myList[i].id == obj.regionId){
			  			rid = i;
			  			cur_region = rid;
			  			break;
			  		}
			  	}

			  	myList = that.locationData.list[rid].list;
			  	for(var i = 0; i < myList.length; i++){
			  		if(myList[i].id == obj.countryId){
			  			cid = i;
			  			cur_country = cid;
			  			break;
			  		}
			  	}


			  	if(obj.countryId  != 'CN'){
			  		//3 Level
			  		myList = that.locationData.list[rid].list[cid].list;
					for(var i = 0; i < myList.length; i++){
						if(myList[i].id == obj.cityId){
							ccid = i;
							cur_city = ccid;
							break;
						}
					}
				}else{

					//4 Level
					myList = that.locationData.list[rid].list[cid].list;
					for(var i = 0; i < myList.length; i++){
						if(myList[i].id == obj.provinceId){
							pid = i;
							cur_province = pid;
							break;
						}
					}

					myList = that.locationData.list[rid].list[cid].list[pid].list;
					for(var i = 0; i < myList.length; i++){
						if(myList[i].id == obj.cityId){
							ccid = i;
							cur_city = ccid;
							break;
						}
					}
				}

				LocationObj.loadSelection("region",$("#region"));
			  	$("#region").select2("val", rid); 
			  	
			  	LocationObj.loadSelection("country",$("#country"));
				$("#country").select2("val", cid);
				
				if(obj.countryId == 'CN'){
					$(".province_wrapper").show();
					LocationObj.loadSelection("province",$("#province"));
					$("#province").select2("val", pid);
				}

				LocationObj.loadSelection("city",$("#city"));
				$("#city").select2("val", ccid); 
				
				if(obj.isPos) $("#pos").attr("checked", "checked");
				if(obj.isAftersales) $("#aftersales").attr("checked", "checked");

				$("#eName").val(obj.name_en);
				$("#tName").val(obj.name_tc);
				$("#sName").val(obj.name_sc);
				$("#fName").val(obj.name_fr);
				$("#jName").val(obj.name_jp);
				$("#eAddress").val(obj.address_en);
				$("#tAddress").val(obj.address_tc);
				$("#sAddress").val(obj.address_sc);
				$("#fAddress").val(obj.address_fr);
				$("#jAddress").val(obj.address_jp);
				$("#telephone").val(obj.tel);
				$("#email").val(obj.email);
				$("#fax").val(obj.fax);
				$("#web").val(obj.web);
				$("#lng").val(obj.lng);
				$("#lat").val(obj.lat);
			});	

		}//end if
	},

	submitForm:function(evt){
		evt.preventDefault();
		
		var postData = checkStoreInput();
		if(!postData){
			return false;
		}
		postData.id = $("#idx_shop").val();
		postData.region = $("#region option:selected").data("id");
		postData.country = $("#country option:selected").data("id");
		if(postData.country == 'CN')
			postData.province = $("#province option:selected").data("id");

		if(city == "all"){
			alert("please select a city");
		}else{
			postData.city = $("#city option:selected").data("id");
		}

		if(postData.country == 'CN'){
			postData.province = $("#province option:selected").data("id");
		}else{
			postData.province = postData.city;
		}

		//Load submit
		var postURL = "api/StoreEdit.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {
			if(result.status == 1){
				alert("Edit done!");
				var id = $("#idx_shop").val();
				window.location.href = "store_detail.html#id="+id;
			}else{
				alert(result.message);
			}
		});
	}
}


var StoreDetail = {

	init:function(){
		var that = this;
		
		//Load store information
		that.loadStore();
	},

	loadStore:function(){
		var id = getHashValue('id');
		$("#idx_shop").text(id);

		if(id != undefined && id != ""){
			var postData = {};
			postData.id = id;
			var postURL = "api/StoreDetail.ashx";
			$.ajax({
				type: "POST",
				dataType :"json",
				url: postURL,
				data: postData
			})
			.success(function( result ) {
			  	//select location
			  	var obj = result.data;
			  	
			  	$("#region").text(obj.regionId); 
				$("#country").text(obj.countryId); 
				$("#city").text(obj.cityId); 

				if(obj.isPos) $("#pos").attr("checked", "checked");
				if(obj.isAftersales) $("#aftersales").attr("checked", "checked");

				$("#eName").text(obj.name_en);
				$("#tName").text(obj.name_tc);
				$("#sName").text(obj.name_sc);
				$("#fName").text(obj.name_fr);
				$("#jName").text(obj.name_jp);
				$("#eAddress").text(obj.address_en);
				$("#tAddress").text(obj.address_tc);
				$("#sAddress").text(obj.address_sc);
				$("#fAddress").text(obj.address_fr);
				$("#jAddress").text(obj.address_jp);
				$("#telephone").text(obj.tel);
				$("#email").text(obj.email);
				$("#fax").text(obj.fax);
				$("#web").text(obj.web);
				$("#lng").text(obj.lng);
				$("#lat").text(obj.lat);
			});	


			$("#edit").attr("href" , "store_edit.html#id="+id);
			$("#delete").attr("href" , "store_delete.html#id="+id);
		}//end if
	},
}


var StoreDelete = {
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
		$("#idx_shop").text(id);

		if(id != undefined && id != ""){
			var postData = {};
			postData.id = id;
			var postURL = "api/StoreDetail.ashx";
			$.ajax({
				type: "POST",
				dataType :"json",
				url: postURL,
				data: postData
			})
			.success(function( result ) {
			  	//select location
			  	var obj = result.data;
			  	
			  	$("#region").text(obj.regionId); 
				$("#country").text(obj.countryId); 
				$("#city").text(obj.cityId); 

				if(obj.isPos) $("#pos").attr("checked", "checked");
				if(obj.isAftersales) $("#aftersales").attr("checked", "checked");

				$("#eName").text(obj.name_en);
				$("#tName").text(obj.name_tc);
				$("#sName").text(obj.name_sc);
				$("#fName").text(obj.name_fr);
				$("#jName").text(obj.name_jp);
				$("#eAddress").text(obj.address_en);
				$("#tAddress").text(obj.address_tc);
				$("#sAddress").text(obj.address_sc);
				$("#fAddress").text(obj.address_fr);
				$("#jAddress").text(obj.address_jp);
			});	

			$("#btndelete").click(that.delete);
			$("#btnrestore").click(that.restore);
			

		}//end if
	},

	delete:function(){
		var that = StoreDelete;
		var postData = {};
		postData.id = that.id;
		var postURL = "api/StoreDelete.ashx";

		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {

		  	if(result.status == 1){
				alert("Delete done!");
				window.location.href = "store_list.html";
			}else{
				alert(result.message);
			}
		  	
		});	
	},

	restore:function(){
		var that = StoreDelete;
		var postData = {};
		postData.id = that.id;
		var postURL = "api/StoreRestore.ashx";

		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {

		  	if(result.status == 1){
				alert("Restore done!");
				window.location.href = "store_list.html";
			}else{
				alert(result.message);
			}
		  	
		});	
	}
}


function checkStoreInput(){

	var isPos = $('#pos').prop('checked');
	var isAftersales = $('#aftersales').prop('checked');
	var region = $("#region").val();
	var country = $("#country").val();
	var city = $("#city").val();
	var telephone = $("#telephone").val();
	var fax = $("#fax").val();
	var email = $("#email").val();
	var web = $("#web").val();
	var eName = $("#eName").val();
	var sName = $("#sName").val();
	var tName = $("#tName").val();
	var jName = $("#jName").val();
	var fName = $("#fName").val();
	var eAddress = $("#eAddress").val();
	var sAddress = $("#sAddress").val();
	var tAddress = $("#tAddress").val();
	var jAddress = $("#jAddress").val();
	var fAddress = $("#fAddress").val();
	var lng = parseFloat($("#lng").val());
	var lat = parseFloat($("#lat").val());

	var msg = "";
	var isError = false;

	//check form
	if(!isPos && !isAftersales){
		msg += "please select type is POS / Aftersale\n";
		isError = true;
	}

	if(region == ""){
		msg += "please input Region\n";
		isError = true;
	}
	if(country == ""){
		msg += "please input Country\n";
		isError = true;
	}

	if(city == "" || city == "all"){
		msg += "please input City\n";
		isError = true;
	}

	if(eName == ""){
		msg += "please input English - Name\n";
		isError = true;
	}
	if(sName == ""){
		msg += "please input 简体中文 - Name\n";
		isError = true;
	}
	if(tName == ""){
		msg += "please input 繁體中文 - Name\n";
		isError = true;
	}
	if(jName == ""){
		msg += "please input 日本語 - Name\n";
		isError = true;
	}
	if(fName == ""){
		msg += "please input FRANÇAIS - Name\n";
		isError = true;
	}
	if(eAddress == ""){
		msg += "please input English - Address\n";
		isError = true;
	}
	if(sAddress == ""){
		msg += "please input 简体中文 - Address\n";
		isError = true;
	}
	if(tAddress == ""){
		msg += "please input 繁體中文 - Address\n";
		isError = true;
	}
	if(jAddress == ""){
		msg += "please input 日本語 - Address\n";
		isError = true;
	}
	if(fAddress == ""){
		msg += "please input FRANÇAIS - Address\n";
		isError = true;
	}
	if(lng == 0){
		msg += "please input lng\n";
		isError = true;
	}
	if(lat == 0){
		msg += "please input lat\n";
		isError = true;
	}

	//alert if any error
	if(isError){
		alert(msg);
		return false;
	}


	//no error, can send information to backend
	var postData = {};
	postData.pos = isPos;
	postData.aftersales = isAftersales;
	postData.region = region;
	postData.country = country;
	postData.city = city;
	postData.tel = telephone;
	postData.fax = fax;
	postData.email = email;
	postData.web = web;
	postData.eName = eName;
	postData.sName = sName;
	postData.tName = tName;
	postData.jName = jName;
	postData.fName = fName;
	postData.eAddress = eAddress;
	postData.sAddress = sAddress;
	postData.tAddress = tAddress;
	postData.jAddress = jAddress;
	postData.fAddress = fAddress;
	postData.lng = lng;
	postData.lat = lat;

	return postData;
}
