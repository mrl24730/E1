var mainCtr;


$(document).ready(function () {
	mainCtr = new MainController();
});

var MainController = (function () {

	var cls = function () {

		var that = this;
		that.pg = 0;
		that.rpp = 100;
		that.callingAPI = false;

		function init() {
			$('#target-date-fr, #target-date-to').mobiscroll().date({
				theme: 'ios',
				display: 'bottom',
				dateFormat: 'yyyy-mm-dd',
				// min:minDate,
				max: new Date(),
				onInit: function (event, inst) {
					inst.setVal(new Date(), true);
        		},
                onSet: function (event, inst) {
                    updateDownloadLink();
                }
			});

			$("#sorting-table").tablesorter();

			that.getOrderList();
            updateDownloadLink();

			$(".btn-detail").click(loadDetail);
			$(".btn-back").click(function(evt){
				$(".step").removeClass("active");
				$(".list-wrapper").addClass("active");
			});

			$("#btn-search").on("click", function(){
				that.getOrderList();
			});

		};
		//end -- init --

		that.getOrderList = function () {

			if (that.callingAPI) return;

			that.callingAPI = true;
			var targetDtF = $("#target-date-fr").val();
            var targetDtT = $("#target-date-to").val();
			var targetEmail = $("#target-email").val();
			var targetName = encodeURIComponent($("#target-name").val().trim());
				$.ajax({
					type: "GET",
					url: 'api/getOrderList.ashx?df='+targetDtF+'&dt='+targetDtT+'&email='+targetEmail+'&name='+targetName+
					'&pg='+that.pg+'&rpp='+that.rpp+'&v=' + Math.random(),
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
			if(data.orderList == null)
				return;

			for(var i=0; i<data.orderList.length; i++){
				var order = data.orderList[i];
				var cell = $("#template").clone(true, true);
				cell.attr("id", "o"+order.idx_order);
				cell.find(".idx_order").text(order.order_number);
				cell.find(".email").text(order.customer_email);
                cell.find(".company_name").text(order.company_name);
                cell.find(".customer_name").text(order.customer_name);
				cell.find(".order_date").text(order.order_date.replace("T", " "));
				cell.find(".qty").text(order.qty);
				// var price = "全單總值: ¥"+order.price+"<br> (全單減免 "+order.discount+ "%)<br>折後總值: ¥"+order.d_price;
				cell.find(".price").html(order.price);
				cell.find(".d-price").html(order.discount);
				cell.find(".f-price").html(order.d_price);

				cell.find(".btn-detail").data("id", order.idx_order);

				$(".list").append(cell);
			}

		}

		function loadDetail(evt){
			evt.preventDefault();
			var idx_order = $(this).data("id");

			$.ajax({
				type: "GET",
				url: 'api/getOrderDetail.ashx?idx_order='+idx_order+'&v=' + Math.random(),
				processData: false,
				contentType: false,

				success: function (result) {
					showDetail(result.data);
				},

				complete: function (xhr, status) {
				}

			});
		}

		function showDetail(data){
			$(".step").removeClass("active");
			$(".detail-wrapper").addClass("active");
			$(document).scrollTop(0);

			var detailPnl = $(".detail-wrapper");
			detailPnl.find(".idx_order").html(data.order_number);
			detailPnl.find(".order_data").html(data.order_date.replace("T", " "));
			detailPnl.find(".price").html(data.price);
			detailPnl.find(".d_price").html(data.d_price);
			detailPnl.find(".discount").html(data.discount);
			detailPnl.find(".diff_price").html(data.price - data.d_price);

			detailPnl.find(".company_name").html(data.company_name);
			detailPnl.find(".customer_name").html(data.customer_name);
			detailPnl.find(".email").html(data.customer_email);
			detailPnl.find(".mobile").html(data.customer_mobile);



			var total_qty = 0;
			detailPnl.find(".itemList").html("");
			for(var i=0;i<data.orderItems.length;i++){
				var item = data.orderItems[i];
				var cell = $("#temp_item").clone();
				cell.removeAttr("id");
				cell.find(".idx_watch").html(item.idx_watch);
				cell.find(".u_price").html(item.price);
				cell.find(".qty").html(item.qty);
				cell.find(".subtotal").html(item.price * item.qty);
				cell.find(".img_path").attr("src","/images/watches/"+(item.idx_watch.replace("-","_"))+"_s.png");
				total_qty += item.qty;

				var spacer = $("#temp_div").clone(true, true);
				spacer.removeAttr("id");
				detailPnl.find(".itemList").append(spacer);
				detailPnl.find(".itemList").append(cell);
			}
			detailPnl.find(".total_qty").text(total_qty);
		}

        function updateDownloadLink(){
            var targetDtF = $("#target-date-fr").val();
            var targetDtT = $("#target-date-to").val();
            $("#btn-download").attr("href", "/admin/api/downloadorderlist.ashx?df=" + targetDtF + "&dt=" + targetDtT);
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
