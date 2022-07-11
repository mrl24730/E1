using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel
{


	public class OrderAList
	{
		public int ttlOrder = 0;
		public List<OrderA> orderList = new List<OrderA>();
	}

	public class OrderA
	{
		public int idx_order { get; set; }
        public string order_number { get;set;}
		public DateTime order_date { get; set; }
		public string company_name { get; set; }
		public string customer_name { get; set; }
		public string customer_email { get; set; }
		public string customer_mobile { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
		public decimal price_hkd { get; set; }
		public decimal price_chf { get; set; }
		public int discount { get; set; }
		public decimal d_price { get; set; }
		public decimal d_price_hkd { get; set; }
		public decimal d_price_chf { get; set; }

	}

	public struct Order
	{
		public int idx_order { get; set; }
        public string order_number { get; set; }
		public DateTime order_date { get; set; }
		public decimal price { get; set; }
		public decimal price_hkd { get; set; }
		public decimal price_chf { get; set; }
		public string ip_address { get; set; }
		public int discount { get; set; }
		public decimal d_price { get; set; }
		public decimal d_price_hkd { get; set; }
		public decimal d_price_chf { get; set; }

        public int idx_customer { get; set; }
        public string company_name { get; set; }
        public string customer_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string idcard { get; set; }
    }

	public class OrderADetail 
	{

		public int idx_order { get; set; }
        public string order_number { get; set; }
		public DateTime order_date { get; set; }
		public int idx_customer { get; set; }

		public string ip_address { get; set; }
		public int discount { get; set; }

		public decimal price { get; set; }
		public decimal price_hkd { get; set; }
		public decimal price_chf { get; set; }

		public decimal d_price { get; set; }
		public decimal d_price_hkd { get; set; }
		public decimal d_price_chf { get; set; }

		public string company_name { get; set; }
		public string customer_name { get; set; }
		public string customer_email { get; set; }
		public string customer_mobile { get; set; }
		public List<OrderItem> orderItems = new List<OrderItem>();
	}


    public struct OrderItem
    {
        public string idx_watch { get; set; }
        public int idx_collection { get; set; }
        public string watch_gender { get; set; }
        public string watch_type { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
        public decimal price_hk { get; set; }
        public decimal price_chf { get; set; }
    }


    public class OrderOutput
    {
        public int idx_order { get; set; }
        public DateTime order_date { get; set; }
        public string order_number { get; set; }
        public string order_date_display { get; set; }
        public Decimal price { get; set; }
        public int discount { get; set; }
        public int total_qty { get; set; }
        public decimal d_price { get; set; }
        public string company_name { get; set; }
        public string customer_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string idcard { get; set; }

        public List<OrderItemOutput> list { get; set; }

        public OrderOutput()
        {
            list = new List<OrderItemOutput>();
        }
        
    }

    public class OrderItemOutput
    {
        public string idx_watch { get; set; }
        public int qty { get; set; }
        public decimal u_price { get; set; }
        public int idx_collection { get; set; }
        public string collection { get; set; }
        public string watch_type { get; set; }
        public string watch_gender { get; set; }
    }
}
