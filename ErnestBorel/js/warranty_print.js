$(document).ready(function(){

	var ModelNum = $.cookie('txtModelNum');
	var CaseNum = $.cookie('txtCaseNum');
	var WarrantyNum = $.cookie('txtWarrantyNum');
	var WarrantyDate = $.cookie('finalDate');
	var Name = $.cookie('txtName');
	var Phone = $.cookie('txtPhone');
	var Email = $.cookie('txtEmail');
	var Dop = $.cookie('txtDop');
	var InvNum = $.cookie('txtInvNum');
	var Country = $.cookie('txtCountry');
	var City = $.cookie('txtCity');

	CaseNum = (CaseNum == "")?"- - -":CaseNum;
	Email = (Email == "")?"- - -":Email;
	InvNum = (InvNum == "")?"- - -":InvNum;

	$(".ModelNum").text(ModelNum);
	$(".CaseNum").text(CaseNum);
	$(".WarrantyNum").text(WarrantyNum);
	$(".WarrantyDate").text(WarrantyDate);
	$(".Name").text(Name);
	$(".Phone").text(Phone);
	$(".Email").text(Email);
	$(".Dop").text(Dop);
	$(".InvNum").text(InvNum);
	$(".Country").text(Country);
	$(".City").text(City);
});