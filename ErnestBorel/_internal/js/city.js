var cur_lang = 'tc';
var cur_region = 0;
var cur_country = 1;
var cur_province = 0;
var cur_city = 0;
var tempCityList = [];

var CityList = {

	init:function(){
	    var that = this;
	    $("#formSearch").submit(that.search);
	    if (getHashValue('search') != null) {
	        $("#name").val(getHashValue('search'));
	        $("#btnSearch").trigger("click");
	    };
	},

	search:function(evt){
		evt.preventDefault();

		var that = CityList;
		//check variable
		
		var name = $("#name").val();

		if(name == ""){
			alert("Please input name");
			return false;
		}

		var postData = {};
		postData.name = name;

		var postURL = "api/CitySearch.ashx";
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
			CityList.listing(result.data);
		});

	},

	listing:function(data){
		
		$("#list").html("");

		if(data.length == 0){
			alert("no search result.");
			return;
		}
		
		for(var i=0; i< data.length; i++){
			var cell = $("#template").clone(true, true);
			cell.attr("id", "s"+i);
			cell.find(".id").text(data[i].idx_city);
			cell.find(".lang").text(data[i].idx_lang);
			cell.find(".name").text(data[i].city_name);
			cell.find(".country").text(data[i].idx_country);
			cell.find(".province").text(data[i].idx_province);
			cell.find(".edit").attr("href", "city_edit.html#id=" + data[i].idx_city + "&lang=" + data[i].idx_lang + "&search=" + $("#name").val());
			cell.find(".detail").attr("href","store_detail.html#id="+data[i].idx_city);
			$("#list").append(cell);
		}
	}
}

/*
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
*/

var CityAdd = {

    locationData: null,

    init: function () {
        var that = this;
        var postData = {};
        postData.lang = cur_lang;
        postData.type = "all";

        var postURL = "../api/getCityList.ashx";
        $.ajax({
            type: "POST",
            dataType: "json",
            url: postURL,
            data: postData
        })
		.success(function (result) {
		    console.log(result.list);
		    that.getAllCityID(result);
		    that.locationData = result;
		    LocationObj.loadData(result);
		    LocationObj.initialize();
		});
        $("#form1").submit(CityAdd.submitForm);
    },
    getAllCityID: function(result){
        if (result.list) {
            CityAdd.lookForList(result.list);
        } else {
            console.log("answer", result.id.toLowerCase());
            tempCityList.push(result.id.toLowerCase());
        }
    },
    lookForList: function (result) {
        for (var j = 0; j < result.length; j++) {
            CityAdd.getAllCityID(result[j]);
        }
    },
    submitForm: function (evt) {
        evt.preventDefault();
        var isError = false;
        var errorMsg = "";
        var postData = {};
        if (!postData) {
            return false;
        }
        var idx_city = $('#id').val();
        var region = $("#region option:selected").data("id");
        var country = $("#country option:selected").data("id");
        var eName = $('#eName').val();
        var sName = $('#sName').val();
        var tName = $('#tName').val();
        var jName = $('#jName').val();
        var fName = $('#fName').val();
        var lng = $('#lng').val();
        var lat = $('#lat').val();
        var zoom = $('#zoom').val();

        if (tempCityList.includes(idx_city.toLowerCase())) {
            isError = true;
            errorMsg += $('#label_id').text() + " is duplicated \n";
        }
        if (idx_city == "") {
            isError = true;
            errorMsg += $('#label_id').text() + "\n";
        }
        if (eName == "") {
            isError = true;
            errorMsg += $('#label_eName').text() + "\n";
        }
        if (sName == "") {
            isError = true;
            errorMsg += $('#label_sName').text() + "\n";
        }
        if (tName == "") {
            isError = true;
            errorMsg += $('#label_tName').text() + "\n";
        }
        if (jName == "") {
            isError = true;
            errorMsg += $('#label_jName').text() + "\n";
        }
        if (fName == "") {
            isError = true;
            errorMsg += $('#label_fName').text() + "\n";
        }
        if (lng == 0) {
            isError = true;
            errorMsg += $('#label_lng').text() + "\n";
        }
        if (lat == 0) {
            isError = true;
            errorMsg += $('#label_lat').text() + "\n";
        }
        if (zoom < 3 || zoom > 19) {
            isError = true;
            errorMsg += $('#label_zoom').text();
        }
        if (isError) {
            alert("The following field is error : \n" + errorMsg);
            return false;
        }
        postData.idx_city = idx_city;
        postData.region = region;
        postData.country = country;
        postData.eName = eName;
        postData.sName = sName;
        postData.tName = tName;
        postData.jName = jName;
        postData.fName = fName;
        postData.lng = lng;
        postData.lat = lat;
        postData.zoom = zoom;

        if (postData.country == 'CN') {
            postData.province = $("#province option:selected").data("id");
        } else {
            postData.province = postData.idx_city;
        }

        var postURL = "api/CityAdd.ashx";
        $.ajax({
            type: "POST",
            dataType: "json",
            url: postURL,
            data: postData
        })
		  .success(function (result) {
		      if (result.status == 1) {
		          alert("done!");
		          window.location.href = "city_list.html";
		      } else {
		          alert(result.message);
		      }
		  });
    }
}

var LocationObj = {

    // ebMapClass : null,
    locationData: null,

    initialize: function () {

        var that = this;

        //generate region / country / city selection
        this.loadSelection("region", $("#region"));
        this.loadSelection("country", $("#country"));
        this.loadSelection("city", $("#city"));
        //$("#province").hide();

        $(".province_wrapper").hide();

        $("#region").select2("val", cur_region);
        $("#country").select2("val", cur_country);
        $("#city").select2("val", "all");


        $("#region").change(function () {
            var p = $(this).val();
            cur_region = p;
            that.loadSelection("country", $("#country"));
            $("#country").change();
        });

        $("#country").change(function () {
            console.log("changed?");
            var p = $(this).val();
            cur_country = p;
            if (that.locationData.list[cur_region].list[cur_country].id == "CN") {
                //$("#province").show();
                $(".province_wrapper").show();
                that.loadSelection("province", $("#province"));
                $("#province").change();
            } else {
                //$("#province").hide();
                $(".province_wrapper").hide();
                that.loadSelection("city", $("#city"));
                $("#city").select2("val", "all");
            }
        });

        $("#province").change(function () {
            var p = $(this).val();
            cur_province = p;
            that.loadSelection("city", $("#city"));
            $("#city").change();
        });

        /*
		$("#city").change(function(){
			var p = $(this).val();
			cur_city = p;
		});
		*/
    },

    loadData: function (_data) {
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
    }
}


var CityEdit = {
	locationData: null,

	init:function(){
		var that = this;
		var id = getHashValue('id');
		var cur_lang = getHashValue('lang');
		$("#idx_city").val(id);

		//Load all location 
		var postData = {};
		postData.id = id;

		var postURL = "api/CityDetail.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function (result) {
		    console.log(result);
		    console.log("result.length : " + result.length);
		    console.log("result.data.length : " + result.data.length);
		    console.log("Object.keys(result.data).length : " + Object.keys(result.data).length);
		    if (result.status == 1 /*&& Object.keys(result.data).length == 13*/) {
				that.loadCity(result);
			}else{
				alert("error..");
			}
		});
		$("#form1").submit(that.submitForm);
		$("#back").attr("onClick", "window.location='city_list.html#search=" + getHashValue('search') + "'");
	},

	loadCity:function(result){
		var that = this;
		var obj = result.data;
		cur_province = obj.province;
		$("#id").text(obj.idx_city);
		$("#country").text(obj.country);
		if (obj.country == "CN") {
		    that.loadAllCNCity();
		} else {
		    var opt = $("<option/>");
		    opt.val(obj.idx_city).text(obj.idx_city);
		    $('#province').append(opt);
		}
		$("#eName").val(obj.eName);
		$("#tName").val(obj.tName);
		$("#sName").val(obj.sName);
		$("#jName").val(obj.jName);
		$("#fName").val(obj.fName);
		$("#lng").val(obj.lng);
		$("#lat").val(obj.lat);
		$("#zoom").val(obj.zoom);
	},
	loadAllCNCity: function () {
	    var that = this;
	    var postData = {};
	    postData.lang = cur_lang;
	    postData.type = "all";

	    var postURL = "../api/getCityList.ashx";
	    $.ajax({
	        type: "POST",
	        dataType: "json",
	        url: postURL,
	        data: postData
	    })
		.success(function (result) {
		    that.getAllCNCityID(result.list);
		});
	},
	getAllCNCityID: function (result) {
	    var that = this;
	    if (result.list && result.id && tempCityList.length==0) {
	        if (result.id == "CN") {
	            //console.log("is CN!");
	            for (var i = 0; i < result.list.length; i++) {
	                tempCityList.push(result.list[i].id);
	                //console.log("Add " + result.list[i].id)
	                var opt = $("<option/>");
	                if (cur_province == result.list[i].id) {
	                    opt.val(result.list[i].id).text(result.list[i].name).attr('selected',true);
	                }
	                else {
	                    opt.val(result.list[i].id).text(result.list[i].name);
	                }
	                $('#province').append(opt);
	            }
	            return;
	        }
	        else {
	            //console.log("non CN! search continuesly");
	            CityEdit.lookForList(result.list);
	        }
	    }
	    else {
	        //console.log("no list or id , search!");
	        CityEdit.lookForList(result);
            //console.log(result)
	    }
	},
	lookForList: function (result) {
	    for (var j = 0; j < result.length; j++) {
	        CityEdit.getAllCNCityID(result[j]);
	    }
	},

	submitForm:function(evt){
		evt.preventDefault();
		
		var postData = checkCityInput();
		if(!postData){
			return false;
		}
		
		//Load submit
		var postURL = "api/CityEdit.ashx";
		$.ajax({
			type: "POST",
			dataType :"json",
			url: postURL,
			data: postData
		})
		.success(function( result ) {
			if(result.status == 1){
				alert("Edit done!");
				var id = $("#idx_city").val();
				window.location.href = "city_list.html" + "#search=" + getHashValue('search');
			}else{
				alert(result.message);
			}
		});
	}
}




function checkCityInput(){

	var idx_city = $("#idx_city").val();
	var lng = parseFloat($("#lng").val());
	var lat = parseFloat($("#lat").val());
	var zoom = parseInt($("#zoom").val());
	var eName = $("#eName").val();
	var tName = $("#tName").val();
	var sName = $("#sName").val();
	var fName = $("#fName").val();
	var jName = $("#jName").val();
	var idx_province = $("#province").val();

	var msg = "";
	var isError = false;

	if (eName == "") {
	    msg += "please input City English Name\n";
	    isError = true;
	}
	if (tName == "") {
	    msg += "please input City Traditional Chinese Name\n";
	    isError = true;
	}
	if (sName == "") {
	    msg += "please input City Simplified Chinese Name\n";
	    isError = true;
	}
	if (jName == "") {
	    msg += "please input City Japanese Name\n";
	    isError = true;
	}
	if (fName == "") {
	    msg += "please input City French Name\n";
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

	if(zoom < 3 || zoom > 19){
		msg += "please input a valid zoom value\n";
		isError = true;
	}

	

	//alert if any error
	if(isError){
		alert(msg);
		return false;
	}


	//no error, can send information to backend
	var postData = {};
	postData.id = idx_city;
	postData.lng = lng;
	postData.lat = lat;
	postData.zoom = zoom;
	postData.eName = eName;
	postData.sName = sName;
	postData.tName = tName;
	postData.fName = fName;
	postData.jName = jName;
	postData.idx_province = idx_province;
	return postData;
}