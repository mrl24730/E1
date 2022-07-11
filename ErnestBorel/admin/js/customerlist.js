var mainCtr;


$(document).ready(function () {
	mainCtr = new MainController();
});

var MainController = (function () {

	var cls = function () {

		var that = this;
		that.callingAPI = false;

		function init() {
			
			/*$('#target-date').mobiscroll().date({
				theme: 'ios',
				display: 'bottom',
				dateFormat: 'yyyy-mm-dd',
				// min:minDate,
				max: new Date(),
				onInit: function (event, inst) {
					inst.setVal(new Date(), true);
        		}
			});
			*/

			$("#sorting-table").tablesorter();

			that.getCustList();
			
		};
		//end -- init --

		that.getCustList = function () {

			if (that.callingAPI) return;

			that.callingAPI = true;
		
			$.ajax({
				type: "GET",
				url: 'api/getCustomerList.ashx?v=' + Math.random(),
				processData: false,
				contentType: false,

				success: function (result) {
					genList(result.data);
					$("#sorting-table").trigger("update");
					//onMobiDatePickSet({}, $('#target-date').mobiscroll('getInst'));
				},

				complete: function (xhr, status) {
					that.callingAPI = false;
				}

			});

			return false;
		}

		function genList(data){
			$(".list").html("");
			
			for(var i=0; i<data.length; i++){
				var cust = data[i];
				var cell = $("#template").clone(true, true);
				cell.attr("id", "c"+cust.idx_cust);
				cell.find(".company_name").text(cust.company_name);
				cell.find(".email").text(cust.email);
				cell.find(".customer_name").text(cust.customer_name);
				cell.find(".mobile").text(cust.mobile);
				cell.find(".created_at").text(cust.created_at.replace("T", " "));

				$(".list").append(cell);
			}

		}

		init();
	};

	return cls;

})();


var KitUIHelper = (function () {

	var cls = function () {

		var that = this;

		that.scrollToElm = function (elm, time, offset) {
			var x = $(elm).offset().top + $('body').scrollTop();

			if (time == undefined) {
				time = 500;
			}

			if (offset != undefined) {
				x += offset;
			}


			$('html,body').animate({ scrollTop: x }, time);
		};

		that.scrollToX = function (x, time) {
			if (time == undefined)
				time = 500;
			$('html,body').animate({ scrollTop: x }, time);
		};


		that.getQueryString = function () {
			var vars = {}, hash;
			var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
			for (var i = 0; i < hashes.length; i++) {
				hash = hashes[i].split('=');
				// vars.push(hash[0]);
				vars[hash[0]] = hash[1];
			}
			return vars;
		};

		that.getHashByKey = function () {
			var vars = [], hash;
			var hashes = window.location.hash.slice(window.location.hash.indexOf('#') + 1).split('&');
			for (var i = 0; i < hashes.length; i++) {
				hash = hashes[i].split('=');
				vars.push(hash[0]);
				vars[hash[0]] = hash[1];
			}
			return vars;
		};
	};

	return cls;

})();


$.fn.serializeObject = function () {
	var o = {};
	var a = this.serializeArray();
	$.each(a, function () {
		if (o[this.name]) {
			if (!o[this.name].push) {
				o[this.name] = [o[this.name]];
			}
			o[this.name].push(this.value || '');
		} else {
			o[this.name] = this.value || '';
		}
	});
	return o;
};

Date.prototype.isSameDateAs = function(pDate) {
  return (
    this.getFullYear() === pDate.getFullYear() &&
    this.getMonth() === pDate.getMonth() &&
    this.getDate() === pDate.getDate()
  );
}
