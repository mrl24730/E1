var isCalling = false;

window.ModelNum = "";

var errorMsgs = {
	ModelNum:{
		en:{m1:"Please enter Model No.",m2:"Please enter a valid Model No."},
		tc:{m1:"請輸入型號",m2:"請輸入有效的型號"},
		sc:{m1:"请输入型号",m2:"请输入有效的型号"},
		fr:{m1:"S'il vous plaît entrée Modèle No.",m2:"Veuillez saisir un modèle valable No."}
	},
	CaseNum:{
		en:{m2:"Please enter a valid Case No."},
		tc:{m2:"請輸入有效的表殼編號"},
		sc:{m2:"请输入有效的表壳编号"},
		fr:{m2:"veuillez saisir une montre nombre de cas valide"}
	},
	WarrantyNum:{
		en:{m1:"please enter warranty number",m2:"please enter a valid warranty number"},
		tc:{m1:"請輸入保修號碼",m2:"請輸入有效的保修號碼"},
		sc:{m1:"请输入保修号码",m2:"请输入一个有效的保修号码"},
		fr:{m1:"s'il vous plaît le numéro de garantie d'entrée",m2:"s'il vous plaît entrer un numéro de garantie valide"}
	},

	Name:{
		en:{m1:"please enter name"},
		tc:{m1:"請輸入姓名"},
		sc:{m1:"请输入姓名"},
		fr:{m1:"s'il vous plaît nom de l'entrée"}
	},

	Title:{
		en:{m1:"please select title"},
		tc:{m1:"請選擇尊稱"},
		sc:{m1:"请选择尊称"},
		fr:{m1:"s'il vous plaît sélectionner le titre"}
	},

	Email:{
		en:{m2:"please enter a valid email address"},
		tc:{m2:"請輸入有效的電郵地址"},
		sc:{m2:"请输入有效的电邮地址"},
		fr:{m2:"s'il vous plaît entrée Une adresse email valide"}
	},

	Phone:{
		en:{m1:"please enter phone number",m2:"please enter a valid phone number"},
		tc:{m1:"請輸入電話號碼",m2:"請輸入一個有效的電話號碼"},
		sc:{m1:"请输入电话号码",m2:"请输入一个有效的电话号码"},
		fr:{m1:"s'il vous plaît le numéro de téléphone d'entrée",m2:"s'il vous plaît entrer un numéro de téléphone valide"}
	},

	Dop:{
		en:{m1:"please select date of purchase",m2:"please select a valid date of purchase",m3:"purchase date should be 11 Nov 2018 or later"},
		tc:{m1:"請選擇購買日期",m2:"請選擇有效的購買日期",m3:"購買日期須為2018年11月11日後"},
		sc:{m1:"请选择购买日期",m2:"请选择有效的购买日期",m3:"購買日期須為2018年11月11日後"},
		fr:{m1:"s'il vous plaît choisir la date d'achat",m2:"s'il vous plaît choisir une date d'achat valide",m3:"date d'achat devrait être le 1 janvier 2018 ou plus tard"}
	},

	InvNum:{
		en:{m1:"please enter invoice number"},
		tc:{m1:"請輸入發票號碼"},
		sc:{m1:"请输入发票号码"},
		fr:{m1:"s'il vous plaît entrer le numéro de la facture"}
	},

	Country:{
		en:{m1:"please select country"},
		tc:{m1:"請選擇國家"},
		sc:{m1:"请选择国家"},
		fr:{m1:"s'il vous plaît sélectionner le pays"}
	},

	City:{
		en:{m1:"please select city"},
		tc:{m1:"請選擇城市"},
		sc:{m1:"请选择城市"},
		fr:{m1:"s'il vous plaît sélectionner la ville"}
	},

	Captcha:{
		en:{m1:"please enter captcha",m2:"please enter a valid captcha"},
		tc:{m1:"請輸入驗證碼",m2:"請輸入有效的驗證碼"},
		sc:{m1:"请输入验证码",m2:"请输入有效的验证码"},
		fr:{m1:"s'il vous plaît entrer captcha",m2:"s'il vous plaît entrer un captcha valide"}
	},

	Agree:{
		en:{m1:"please agree to our 'Security and Privacy Terms'. "},
		tc:{m1:"請先同意《安全及私穩》條例"},
		sc:{m1:"请先同意《安全及私稳》条例"},
		fr:{m1:"S'il vous plaît accepter 《la sécurité et la confidentialité ordonnance》"}
	}
};

$(document).ready(function(){
	$(".fr, .jp").hide();
	Warranty.init();
});


var txtModelNum = "";
var txtCaseNum = "";
var txtWarrantyNum = "";
var txtName = "";
var txtTitle = "";
var txtCode = "";
var txtPhone = "";
var txtEmail = "";
var txtDop = "";
var txtInvNum = "";
var txtCountry = "";
var txtCity = "";
var txtCountryLang = "";
var txtCityLang = "";
var txtCaptcha = "";
var txtAgree = "";


var Warranty = {

	init:function(){

		var that = this;

		$("body").addClass("isWarranty");

		//Ajax call ModelNum
		that.ModelNumAjax();

		//Generate Country and City Relationship
		that.initCountryCity();

		//Set Radio Button
		$(".radio-Gp").each(function(){
			var that = this;

			$(this).find("a").click(function(){
				$(that).find("a").removeClass("selected");
				$(this).addClass("selected");

				$("#" + $(that).data("input")).val($(this).data("val"));
			});
		});

		$(".popup").click(function(){
			$(this).fadeOut();
		});

		$(".hints").click(function(){
			var target = $(this).attr("rel");
			$(".popup .card_frame").hide();
			$(".popup ."+target).show();
			$(".popup").fadeIn();
		});

		$("#CaptchaReload").click(function(){
			var r = Math.random();
			$("#CaptchaImg").attr("src", "/api/getCaptchaImg.ashx?v="+r);
		});

		//Set Datepicker
		$( "#Dop" ).datepicker({
			dateFormat:"yy-mm-dd",
			maxDate: new Date(2019, 0, 10),
			minDate: new Date(2018, 10, 11)
		});

		$('#ui-datepicker-div').wrap('<div class="eb-jqui"></div>');

		//Form Submission
		formSubmission();

		//debug
		//$("#btnNext").click();
		//that.ShowThankYou();
	},

	initCountryCity:function(){
		var countryList = {};

		for(var i = 0; i < countryCityList.length; i++){
			var ccList = countryCityList[i];
			if(countryList[ccList.CountryVal] == undefined) countryList[ccList.CountryVal] = {id:i,txt:ccList.Country,city:new Array()};

			countryList[ccList.CountryVal].city.push({val:ccList.CityVal,txt:ccList.City});
		}

		$("select#Country").empty();

		var defaultCountry = "";

		$.each(countryList,function(key,val){

			var option = $("<option/>");
			option.val(key).text(val.txt);
			option.data("city",val.city);
			$("select#Country").append(option);

			if(defaultCountry == "") defaultCountry = key;
		});

		$(".select2").select2({
	    	allowClear: true,
	    	minimumResultsForSearch: -1
		});

		$("select#Country").on("change", function (e) {

			var cityList = $(e.added.element[0]).data("city");

			changeCityList(cityList);
		});

		var defaultCityList = $("select#Country option:selected").data("city");
		changeCityList(defaultCityList);
		checkCityInvoice();

		function changeCityList(list){

			$("select#City").empty();
			$.each(list,function(key,val){
				var option = $("<option/>");
				option.val(val.val).text(val.txt);
				$("select#City").append(option);
			});
			$("select#City").trigger("change");
		}


		$("select#City").on("change", function (e) {
			checkCityInvoice();
		});

		function checkCityInvoice(){
			//check invoice need or not
			var city = $("select#City").val();

			for(var i = 0; i < countryCityList.length; i++){
				if(countryCityList[i].CityVal == city){
					if(countryCityList[i].NeedInvoice){
						//need invoice
						$("#InvoiceRow").show();
					}else{
						//no need invoice
						$("#InvNum").val("");
						$("#InvoiceRow").hide();
					}
					break;
				}
			}
		}
	},

	ModelNumAjax:function(){
		$("#ModelNum").on("blur",function(){

			if($.trim($(this).val()) != ""){

				var data = {};
				window.ModelNum = $(this).val();
				data.ModelNum = window.ModelNum;

				$.ajax({
					  type: "POST",
					  url: "/api/getWarrantyModel.ashx",
					  data: data
					})
					  .success(function( result ) {
					  	var img = $(".model_preview img");
					  	if(result.status === 1){
					  		if(result.data.Src != null){


					  			img.prop("src",result.data.Src);
					  			img.error(function() {
								    img.prop( "src", "/images/watches/noimage_s.png" );
								    img.unbind("error");
								});
								img.load(function(){
									img.unbind("error, load");
								});
					  		}else{
					  			img.prop( "src", "/images/watches/noimage_s.png" );
					  		}
					  		$("#ModelNum").data("valid",true);
					  	}else{
					  		img.prop( "src", "/images/watches/noimage_s.png" );
					  		$("#ModelNum").data("valid",false);
					  	}

					  });
			}

		});
	},

	ShowThankYou:function(){
		$("#warrantyForm-wrapper").hide();
  		$("#step1, #step2").hide();

  		var thx = $("#success-submission");

  		var finalDate = "";
  		if(txtDop != ""){
			//var d = new Date(txtDop);
			//d.setFullYear(d.getFullYear()+3, d.getMonth(), d.getDate() - 1);
			//finalDate = d.getFullYear() + "-" + (d.getMonth()+1) + "-" + d.getDate();
			var dd = moment.utc(txtDop).local().add(-8, 'hours');
			var dd = moment(dd).add(3, 'years');
			var dd = moment(dd).add(-1, 'days').format("YYYY-MM-DD");
			finalDate = dd;
  		}


		//Product Information
		showConfirmInfo($("#success-submission .ModelNum"), txtModelNum, true);
		showConfirmInfo($("#success-submission .CaseNum"), txtCaseNum, true);
		showConfirmInfo($("#success-submission .WarrantyNum"), txtWarrantyNum, true);
		showConfirmInfo($("#success-submission .WarrantyDate"), finalDate, true);

		//Personal Information
		showConfirmInfo($("#success-submission .Name"), txtTitle + " " + txtName, true);
		showConfirmInfo($("#success-submission .Phone"), txtCode + " - " +  txtPhone, true);
		showConfirmInfo($("#success-submission .Email"), txtEmail, true);
		showConfirmInfo($("#success-submission .Dop"), txtDop, true);
		showConfirmInfo($("#success-submission .InvNum"), txtInvNum, true);
		showConfirmInfo($("#success-submission .Country"), txtCountryLang, true);
		showConfirmInfo($("#success-submission .City"), txtCityLang, true);
  		thx.show();

  		$.cookie('txtModelNum', txtModelNum, { expires:1, path:'/' });
  		$.cookie('txtCaseNum', txtCaseNum, { expires:1, path:'/' });
  		$.cookie('txtWarrantyNum', txtWarrantyNum, { expires:1, path:'/' });
  		$.cookie('finalDate', finalDate, { expires:1, path:'/' });
  		$.cookie('txtName', txtTitle + " " + txtName, { expires:1, path:'/' });
  		$.cookie('txtPhone', txtCode + " - " +  txtPhone, { expires:1, path:'/' });
  		$.cookie('txtEmail', txtEmail, { expires:1, path:'/' });
  		$.cookie('txtDop', txtDop, { expires:1, path:'/' });
  		$.cookie('txtInvNum', txtInvNum, { expires:1, path:'/' });
  		$.cookie('txtCountry', txtCountryLang, { expires:1, path:'/' });
  		$.cookie('txtCity', txtCityLang, { expires:1, path:'/' });

  		thx.find("#btnSave").click(function(){
  			var lang = $("html").prop("lang");

  			if(lang == "sc"){
  				lang = "";
  			}else{
  				lang = "/"+ lang;
  			}
  			window.open(lang+"/warranty_print.aspx", "", "width=800, height=560, left=400, top=200");
  		});

	},



}




function formSubmission(){

	var isConfirmMode = false;

	$("#btnNext").click(function(){
		//validate form
		var isValid = true;

		if(formValidate()){
			//Product Information
			showConfirmInfo($("#ModelNum"),$("#ModelNum").val());
			showConfirmInfo($("#CaseNum"),$("#CaseNum").val());
			showConfirmInfo($("#WarrantyNum"),$("#WarrantyNum").val());

			//Personal Information
			showConfirmInfo($("#Name"), $("#Title").val() + " " + $("#Name").val());
			showConfirmInfo($("#Phone"), $("#Ccode").val() + " - " +  $("#Phone").val());
			showConfirmInfo($("#Email"),$("#Email").val());
			showConfirmInfo($("#Dop"),$("#Dop").val());
			showConfirmInfo($("#InvNum"),$("#InvNum").val());
			showConfirmInfo($("#Country"),$("#Country option:selected").text());
			showConfirmInfo($("#City"),$("#City option:selected").text());
			showConfirmInfo($("#Captcha"),$("#Captcha").val());

			$("#warrantyForm-wrapper").addClass("isConfirm");
			$("#step1").hide();
			$("#step2").show();

			isConfirmMode = true;
		}else{

		}
	});

	$("#btnReset").click(function(){
		if(isConfirmMode){
			$("#warrantyForm-wrapper").removeClass("isConfirm");
		}else{
			$("#warrantyForm-wrapper").find("td input").val("");
			$(".radio-Gp a").removeClass("selected");
		}
	});

	$("#btnBack").click(function(){
		if(isConfirmMode){
			$("#warrantyForm-wrapper").removeClass("isConfirm");
			isConfirmMode = false;
		}
	});

	$("#warrantyForm").submit(function(e){
		e.preventDefault();

		var postData = $(this).serializeArray();

		if(!isCalling){
			isCalling = true;

			$.ajax({
			  type: "POST",
			  url: "/api/warrantyRegistration.ashx",
			  data: postData
			})
			  .success(function( response ) {
			  	isCalling = false;

			  	if(response.status == 1){
			  		Warranty.ShowThankYou();
			  	}else{
			  		$("#btnBack").trigger("click");
			  		if(response.message != ""){
			  			alert(response.message);
			  		}else{
			  			var lang = $("html").prop("lang");
			  			lang = (lang == "")?"sc":lang;
			  			$.each(response.data,function(key,val){
			  				showErrorMsg($("#" + key),errorMsgs[key][lang][val])

			  				if(key == "Captcha"){
			  					$("#CaptchaImg").prop("src","/api/getCaptchaImg.ashx?r=" + (Math.random() * 1000000));
			  					$("#Captcha").val("");
			  				}
			  			});
			  		}
			  	}

			  })
			  .error(function(){
			  	isCalling = false;
			  })

			  ;

		}
		//e.unbind();
	})

	function formValidate(){

		$(".errorMsg").text("");

		isValid = true;
		var lang = $("html").prop("lang");
		lang = (lang == "")?"sc":lang;

		txtModelNum = $.trim($("#ModelNum").val());
		txtCaseNum = $.trim($("#CaseNum").val());
		txtWarrantyNum = $.trim($("#WarrantyNum").val());
		txtName = $.trim($("#Name").val());
		txtTitle = $.trim($("#Title").val());
		txtCode = $.trim($("#Ccode").val());
		txtPhone = $.trim($("#Phone").val());
		txtEmail = $.trim($("#Email").val());
		txtDop = $.trim($("#Dop").val());
		txtInvNum = $.trim($("#InvNum").val());
		txtCountry = $.trim($("#Country").val());
		txtCountryLang = $("#Country option:selected").text();
		txtCity = $.trim($("#City").val());
		txtCityLang = $("#City option:selected").text()
		txtCaptcha = $.trim($("#Captcha").val());
		txtAgree = ($('#Agree').prop('checked'))?"1":"0";


		//Personal Information
		if(txtAgree == "0"){
			isValid = showErrorMsg($("#Agree"),errorMsgs["Agree"][lang]["m1"]);
		}

		if(txtCaptcha == ""){
			isValid = showErrorMsg($("#Captcha"),errorMsgs["Captcha"][lang]["m1"]);
		}

		//Check invoice number depends on country and city
		/*
		if($("#InvNum").is(":visible") && txtInvNum == ""){
			isValid = showErrorMsg($("#InvNum"),errorMsgs["InvNum"][lang]["m1"]);
		}
		*/

		if(txtCity == ""){
			isValid = showErrorMsg($("#City"),errorMsgs["City"][lang]["m1"]);
		}

		if(txtCountry == ""){
			isValid = showErrorMsg($("#Country"),errorMsgs["Country"][lang]["m1"]);
		}

		//Purchase Date
		if(txtDop == ""){
			isValid = showErrorMsg($("#Dop"),errorMsgs["Dop"][lang]["m1"]);
		}

		if(txtEmail != ""){
			var re = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
			if(!re.test(txtEmail)){
				isValid = showErrorMsg($("#Email"),errorMsgs["Email"][lang]["m2"]);
			}
		}

		if(txtPhone == ""){
			isValid = showErrorMsg($("#Phone"),errorMsgs["Phone"][lang]["m1"]);
		}else if(!/^(?=.*\d)[\d ]+$/.test(txtPhone)){
			isValid = showErrorMsg($("#Phone"),errorMsgs["Phone"][lang]["m2"]);
		}

		if(txtName == ""){
			isValid = showErrorMsg($("#Name"),errorMsgs["Name"][lang]["m1"]);
		}

		if(txtTitle == ""){
			isValid = showErrorMsg($("#Title"),errorMsgs["Title"][lang]["m1"]);
		}

		//Warranty Num
		if(txtWarrantyNum == ""){
			isValid = showErrorMsg($("#WarrantyNum"),errorMsgs["WarrantyNum"][lang]["m2"]);

		}

		//Model Num
		if(txtModelNum == ""){
			isValid = showErrorMsg($("#ModelNum"),errorMsgs["ModelNum"][lang]["m1"]);

		}else if(!$("#ModelNum").data("valid")){
			isValid = showErrorMsg($("#ModelNum"),errorMsgs["ModelNum"][lang]["m2"]);
		}



		return isValid;
	}

	function showErrorMsg(elm,val){

		var td = elm.parents("td");
		var error = td.find(".errorMsg");
		if(error.length == 0){
			error = $("<span/>");
			error.addClass("errorMsg");

			td.append(error);
		}
		error.text(val);


		elm.focus();
		return false;
	}

}


function showConfirmInfo(elm,val,isSelf){

	isSelf = (isSelf == undefined)?false: isSelf;
	var confirm = elm;
	if(!isSelf){
		var td = elm.parents("td");
		confirm = td.find(".confirmMode");
		if(confirm.length == 0){
			confirm = $("<div/>");
			confirm.addClass("confirmMode");
			td.append(confirm);
		}
	}

	confirm.text(val == "" ? "- - - " : val );

}
