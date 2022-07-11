using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

using Kitchen;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ErnestBorel.admin;

namespace ErnestBorel
{
	public static partial class DBHelper
	{
        /*
        public static string environment = ConfigurationManager.AppSettings["Environment"];
        public static string constr = ConfigurationManager.ConnectionStrings["db" + environment].ConnectionString;
        public static string defaultSKey = "e8d92sK1";
        public static string fixedIV = "#m0DATwelve***";
        */
        
        /// <summary>
        /// Populate the list of tuple with the IR records by status
        /// </summary>
        /// <param name="_ir_summary"></param>
        public static void get_IRsummary(ref List<Tuple<int,int>> _ir_summary )
        {

            const string sql_IRcount = "SELECT COUNT (rec_idx) AS ttlRec, ir_status FROM tbl_ir_release GROUP BY ir_status";

            string sql_IR_UpdateStatus = "UPDATE tbl_ir_release SET ir_status = " + (int)IR_status.published + " WHERE ir_status != " + (int)IR_status.withheld + " AND ir_releaseDate < '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"'";

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //update ir record status
                    cmd.CommandText = sql_IR_UpdateStatus;
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();


                    cmd.CommandText = sql_IRcount;
                    
                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _ir_summary.Add(Tuple.Create<int,int>( Convert.ToInt16 ( reader["ir_status"] ), (int)reader["ttlRec"]));
                        }

                    }//end reader next

                } //auto dispose cmd

            } //auto close conn
        }

        public static void get_IRList(ref List<IR_masterRecord> _ir_List)
        {

            /*
            const string sql_IRList = @"
            SELECT * FROM
            (SELECT ROW_NUMBER() OVER (ORDER BY ir_releaseDate desc, rec_idx desc) AS rowNum, rec_idx, 
            ir_releaseDate, ir_lastUpdated, ir_status, ir_langFlag, (SELECT TOP 1 ir_title FROM tbl_ir_release_lang WHERE master_idx = rec_idx ORDER BY ir_lang) AS ir_title
            FROM tbl_ir_release) AS ir_orderList
            WHERE rowNum >= @minRow AND rowNum <= @maxRow
            ";
             */

            const string sql_IRList = @"SELECT rec_idx, 
            ir_releaseDate, ir_lastUpdated, ir_status, ir_langFlag, (SELECT TOP 1 ir_title FROM tbl_ir_release_lang WHERE master_idx = rec_idx ORDER BY ir_lang) AS ir_title
            FROM tbl_ir_release ORDER BY ir_releaseDate DESC, rec_idx DESC";


            //int minRow = (_pg-1) * _rpp + 1;
            //int maxRow = _pg * _rpp;
            int i = 1;

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = sql_IRList;
                    cmd.Connection = conn;
                    //cmd.Parameters.AddWithValue("@minRow", minRow);
                    //cmd.Parameters.AddWithValue("@maxRow", maxRow);

                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            IR_masterRecord rec = new IR_masterRecord();
                            //rec.rowNum = Convert.ToInt32(reader["rowNum"]);
                            rec.rowNum = i;
                            rec.rec_idx = Convert.ToInt32(reader["rec_idx"]);
                            rec.ir_releaseDate = (DateTime) reader["ir_releaseDate"];
                            rec.ir_title = (string)reader["ir_title"];
                            rec.ir_lastUpdated = (DateTime) reader["ir_lastUpdated"];
                            rec.ir_langFlag = Convert.ToInt16(reader["ir_langFlag"]);
                            rec.ir_status = Convert.ToInt32(reader["ir_status"]);
                            _ir_List.Add(rec);
                            i++;
                        }

                    }//end reader next

                } //auto dispose cmd

            } //auto close conn
        }

        public static void get_IRRecords(ref IR_masterRecord _ir_RecMaster,  int _master_idx)
        {

            const string sql_IRRecord_master = @"SELECT * from tbl_ir_release where rec_idx=@master_idx";

            const string sql_IRRecord = @"SELECT * from tbl_ir_release_lang where master_idx=@master_idx order by ir_lang";


            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = sql_IRRecord;
                    cmd.Connection = conn;
                    cmd.Parameters.Add("@master_idx", SqlDbType.BigInt);
                    cmd.Parameters["@master_idx"].Value = _master_idx; 
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IR_detailRecord rec = new IR_detailRecord();
                            rec.rec_idx = Convert.ToInt32(reader["rec_lang_idx"]);
                            rec.master_idx = _master_idx;
                            rec.title = (string)reader["ir_title"];
                            rec.desc = (string)reader["ir_desc"];
                            rec.file = (string)reader["ir_file"];
                            rec.lang = Convert.ToInt16(reader["ir_lang"]);
                            rec.filesize = Convert.ToInt32(reader["ir_filesize"]);
                            rec.filesizeStr = (rec.filesize/1024 > 1000) ? (rec.filesize/1024 / 1000).ToString("#.##") + "MB" : (rec.filesize/1024).ToString("#.##") + "KB";
                            rec.langStr = ((IR_lang)rec.lang).ToString();
                            _ir_RecMaster.list.Add(rec);
                            //_ir_detailList.Add(rec);
                        }

                    }//end reader next


                    cmd.CommandText = sql_IRRecord_master;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            _ir_RecMaster.rec_idx = Convert.ToInt32(reader["rec_idx"]);
                            _ir_RecMaster.ir_releaseDate = (DateTime)reader["ir_releaseDate"];
                            _ir_RecMaster.ir_lastUpdated = (DateTime)reader["ir_lastUpdated"];
                            _ir_RecMaster.ir_langFlag = Convert.ToInt16(reader["ir_langFlag"]);
                            _ir_RecMaster.ir_status = Convert.ToInt32(reader["ir_status"]);

                        }

                    }//end reader next

                } //auto dispose cmd

            } //auto close conn
        }

        public static void save_IRRecords(ref IR_masterRecord _ir_RecMaster)
        {
            //AdminHelper.SaveIRrecords(HttpContext.Current, ref list_irDetails, ref ir_Rec, ref rec_idx);

            const string sql_IRRecord_masterNew = @"
            insert into tbl_ir_release (ir_langFlag, ir_status, ir_lastUpdated, ir_releaseDate)
            values (@ir_langFlag, @ir_status, getDate(), @ir_releaseDate); SELECT SCOPE_IDENTITY();
            ";
            //SELECT SCOPE_IDENTITY()"

            const string sql_IRRecord_masterUpdate = @"
            update tbl_ir_release set ir_langFlag = @ir_langFlag, ir_status = @ir_status, ir_lastUpdated = getDate(), ir_releaseDate = @ir_releaseDate 
            where rec_idx=@rec_idx;
            ";

            const string sql_IRRecordDetailsNew = @"
            insert into tbl_ir_release_lang (master_idx,ir_title, ir_desc, ir_file, ir_filesize ,ir_lang)
            values (@master_idx, @ir_title, @ir_desc, @ir_file, @ir_filesize , @ir_lang)
            ";

            const string sql_IRRecordDetailsUpdate = @"
            update tbl_ir_release_lang set ir_title=@ir_title, 
            ir_desc=@ir_desc, 
            ir_file=@ir_file, 
            ir_filesize=@ir_filesize
            where rec_lang_idx=@rec_idx;
            ";


            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //update master records
                    if (_ir_RecMaster.rec_idx == 0) {
                        cmd.CommandText = sql_IRRecord_masterNew;
                    } else {
                        cmd.CommandText = sql_IRRecord_masterUpdate;
                        cmd.Parameters.Add("@rec_idx", SqlDbType.BigInt);
                        cmd.Parameters["@rec_idx"].Value = _ir_RecMaster.rec_idx;
                    }

                    cmd.Parameters.Add("@ir_langFlag", SqlDbType.Int);
                    cmd.Parameters["@ir_langFlag"].Value = _ir_RecMaster.ir_langFlag;

                    cmd.Parameters.Add("@ir_status", SqlDbType.TinyInt);
                    cmd.Parameters["@ir_status"].Value = _ir_RecMaster.ir_status;

                    cmd.Parameters.Add("@ir_releaseDate", SqlDbType.DateTime);
                    cmd.Parameters["@ir_releaseDate"].Value = _ir_RecMaster.ir_releaseDate;

                    cmd.Connection = conn;
                    conn.Open();

                    try
                    {
                        if (_ir_RecMaster.rec_idx == 0)
                        {
                            _ir_RecMaster.rec_idx = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        else
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e)
                    {
                        //cannot insert or update master record somehow!?
                        _ir_RecMaster.rec_idx = 0;
                    }


                    if (_ir_RecMaster.rec_idx != 0)
                    {

                        foreach (IR_detailRecord ir_rec in _ir_RecMaster.list)
                        {

                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("@master_idx", SqlDbType.BigInt);
                            cmd.Parameters.Add("@ir_title", SqlDbType.NVarChar);
                            cmd.Parameters.Add("@ir_desc", SqlDbType.NVarChar);
                            cmd.Parameters.Add("@ir_file", SqlDbType.NVarChar);
                            cmd.Parameters.Add("@ir_filesize", SqlDbType.Int);
                            cmd.Parameters.Add("@ir_lang", SqlDbType.TinyInt);


                            //cmd.Parameters["@master_idx"].Value = ir_rec.master_idx;
                            cmd.Parameters["@master_idx"].Value = _ir_RecMaster.rec_idx;

                            cmd.Parameters["@ir_title"].Value = ir_rec.title;
                            cmd.Parameters["@ir_desc"].Value = ir_rec.desc;
                            cmd.Parameters["@ir_filesize"].Value = ir_rec.filesize;
                            cmd.Parameters["@ir_lang"].Value = ir_rec.lang;

                            if (String.IsNullOrEmpty (ir_rec.file))
                            {
                                cmd.Parameters["@ir_file"].Value = "";
                                cmd.Parameters["@ir_filesize"].Value = 0;
                            }
                            else
                            {
                                if (ir_rec.rec_idx == 0)
                                {
                                    cmd.Parameters["@ir_file"].Value = "Investor_" + _ir_RecMaster.rec_idx + "_" + ((IR_lang) (ir_rec.lang) ).ToString() + ".pdf";
                                }
                                else
                                {
                                    cmd.Parameters["@ir_file"].Value = ir_rec.file;
                                }
                            }
                            

                            //insert - if new
                            if (ir_rec.rec_idx == 0 )
                            {

                                
                                cmd.CommandText = sql_IRRecordDetailsNew;
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                //existing - update 
                                cmd.Parameters.Add("@rec_idx", SqlDbType.BigInt);
                                cmd.Parameters["@rec_idx"].Value = ir_rec.rec_idx;

                                cmd.CommandText = sql_IRRecordDetailsUpdate;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    conn.Close();

                } //auto dispose cmd

            } //auto close conn
        }

        public static void withdraw_IRRecords(int _master_idx, bool reactivate = false )
        {
            //AdminHelper.SaveIRrecords(HttpContext.Current, ref list_irDetails, ref ir_Rec, ref rec_idx);

            const string sql_IRRecord_masterWithdraw = @"
            update tbl_ir_release set ir_status = @ir_status where rec_idx=@rec_idx;
            ";


            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql_IRRecord_masterWithdraw;
                    
                    cmd.Parameters.Add("@rec_idx", SqlDbType.BigInt);
                    cmd.Parameters["@rec_idx"].Value = _master_idx;

                    cmd.Parameters.Add("@ir_status", SqlDbType.TinyInt);
                    

                    if (reactivate)
                    {
                        cmd.Parameters["@ir_status"].Value = (int)IR_status.published;
                        
                    }
                    else
                    {
                        cmd.Parameters["@ir_status"].Value = (int)IR_status.withheld;

                    }
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                } //auto dispose cmd

            } //auto close conn
        }

		public static int getTotalOrder()
		{
			int ttlOrder = 0;
			using (SqlConnection conn = new SqlConnection(constr))
			{
				conn.Open();


				// Create the command object and set its properties
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = conn;
					cmd.Parameters.Clear();


					cmd.CommandText = @"SELECT count(idx_order) as ttl from tbl_order;";
					ttlOrder = (int) cmd.ExecuteScalar();
					

				} //auto dispose cmd

			}

			return ttlOrder;

		}

        public static List<Customer> getCustomerList()
        {
            List<Customer> list = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                string sql = "SELECT idx_customer, company_name, customer_name, email, mobile, created_at from tbl_customer ORDER BY created_at desc";
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Customer cust = new Customer();
                                //cust.company_name = CryptoHelper.decryptAES((string)reader["company_name"], defaultSKey);
                                cust.customer_name = (string)reader["customer_name"];
                                cust.email = (string)reader["email"];
                                cust.mobile = (string)reader["mobile"];
                                cust.created_at = (DateTime)reader["created_at"];

                                cust.company_name = (string)reader["company_name"];
                                cust.email = CryptoHelper.decryptAES(cust.email, defaultSKey);
                                cust.mobile = CryptoHelper.decryptAES(cust.mobile, defaultSKey);

                                list.Add(cust);
                            }
                        }
                    }//end reader
                }//end cmd
            }//end conn
            return list;
        }

        public static DataTable getCustomerDatatable()
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Clear();


                    string sql = @"SELECT idx_customer, company_name, customer_name, email, mobile, created_at from tbl_customer ORDER BY created_at desc";
                    cmd.CommandText = sql;
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow r in dt.Rows)
                        {
                            //r["company_name"] = CryptoHelper.decryptAES((string)r["company_name"], defaultSKey);
                            r["company_name"] = (string)r["company_name"];
                            r["mobile"] = CryptoHelper.decryptAES((string)r["mobile"], defaultSKey);
                            r["email"] = CryptoHelper.decryptAES((string)r["email"], defaultSKey);
                        }


                        return dt;
                    }

                } //auto dispose cmd

            }
            
        }

        public static void getOrderList(ref List<OrderA> orderList, DateTime targetDtFrom, DateTime targetDtTo, string email_aes = "", string name = "")
		{
			using (SqlConnection conn = new SqlConnection(constr))
			{
				conn.Open();
				
				// Create the command object and set its properties
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = conn;
					cmd.Parameters.Clear();
					
                    /*
					cmd.CommandText = @"WITH CTEResults AS
									(SELECT idx_order, order_date, discount, price, price_hkd, price_chf, customer_name, company_name, email, mobile, 
									ROW_NUMBER() OVER(ORDER BY order_date) AS RowNum FROM vw_order)
									SELECT * FROM CTEResults WHERE RowNum BETWEEN @skipRows AND @rowMax;";

					
					cmd.CommandText = @"SELECT idx_order, order_date, discount, price,  price_hkd, price_chf, 
										customer_name, company_name, email, mobile FROM vw_order ORDER BY [order_date] DESC OFFSET @skipRows ROWS FETCH NEXT @pgSize ROWS ONLY;";
					*/


                    //Order number, customer email, total qty, order total (3 price), discount percentage

                    /*
                    string sql = @"WITH CTEResults AS (SELECT o.idx_order, max(o.order_date) order_date, max(o.created_at), max(o.discount) , sum(o.qty) ,
                                            max(o.price), max(o.d_price), max(o.price_hkd), max(o.d_price_hkd), max(o.price_chf), max(o.d_price_chf), 
                                            max(o.company_name), max(o.customer_name), max(o.email), max(o.mobile), ROW_NUMBER() OVER (ORDER BY order_date desc) AS RowNum 
                                            FROM vw_order o 
                                            WHERE o.order_date >= @startDt AND o.order_date < @endDt
                                            GROUP BY o.idx_order ";
                                            */

                    string sql = @"SELECT o.idx_order , max(o.price) price, max(o.discount) discount, max(o.d_price) d_price, sum(qty) qty,
max(o.email) email, max(company_name) company_name, max(order_date) order_date, max(customer_name) customer_name
FROM tbl_order o left join tbl_orderitem oi on o.idx_order = oi.idx_order 
WHERE o.order_date >= @startDt AND o.order_date < @endDt ";



                    if (!string.IsNullOrEmpty(email_aes))
                    {
                        sql += "AND o.email = @email_aes ";
                        cmd.Parameters.Add("@email_aes", SqlDbType.VarChar, 2048).Value = email_aes;
                    }

                    if (!string.IsNullOrEmpty(name))
                    {
                        sql += "AND o.company_name LIKE @name ";
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 2048).Value = "%" + name + "%";
                    }

                    sql += " GROUP BY o.idx_order ORDER BY o.idx_order DESC";

                    cmd.CommandText = sql;
                    //cmd.Parameters.Add("@pgSize", SqlDbType.Int).Value = pgSize;
                    //cmd.Parameters.Add("@skipRows", SqlDbType.Int).Value = skipRows;
                    //cmd.Parameters.Add("@rowMax", SqlDbType.Int).Value = rowMax;

                    cmd.Parameters.Add("@startDt", SqlDbType.DateTime).Value = targetDtFrom;
                    cmd.Parameters.Add("@endDt", SqlDbType.DateTime).Value = targetDtTo;

                    using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								OrderA order = new OrderA();
                                //order.company_name = CryptoHelper.decryptAES((string)reader["company_name"], DBHelper.defaultSKey);
                                //order.customer_mobile = CryptoHelper.decryptAES((string)reader["mobile"], DBHelper.defaultSKey);
                                order.customer_email = CryptoHelper.decryptAES((string)reader["email"], DBHelper.defaultSKey);
                                //order.company_name = CryptoHelper.decryptAES((string)reader["company_name"], DBHelper.defaultSKey);
                                order.company_name = (string)reader["company_name"];
                                order.customer_name = (string)reader["customer_name"];
                                order.idx_order = (int)reader["idx_order"];
								order.order_date = (DateTime)reader["order_date"];
								order.discount = (int)reader["discount"];
                                order.qty = (int)reader["qty"];

                                order.order_number = order.order_date.ToString("yyyyMMdd") + order.idx_order;
								if (reader["price"] != DBNull.Value) order.price = (decimal)reader["price"];
								//if (reader["price_hkd"] != DBNull.Value)  order.price_hkd = (decimal)reader["price_hkd"];
								//if (reader["price_chf"] != DBNull.Value)  order.price_chf = (decimal)reader["price_chf"];

                                if (reader["d_price"] != DBNull.Value) order.d_price = (decimal)reader["d_price"];
                                //if (reader["d_price_hkd"] != DBNull.Value) order.d_price_hkd = (decimal)reader["d_price_hkd"];
                                //if (reader["d_price_chf"] != DBNull.Value) order.d_price_chf = (decimal)reader["d_price_chf"];
                                orderList.Add(order);
							}
						}

					}

				} //auto dispose cmd

			}

		}

        public static DataTable getOrderDatatable(DateTime targetDtFrom, DateTime targetDtTo, string email_aes = "", string name = "")
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();

                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Clear();

                   
                    string sql = @"select o.idx_order,
		                                    o.created_at,
                                            o.company_name,
		                                    oi.idx_watch,
                                            oi.qty,
		                                    oi.price,
		                                    oi.price_hkd,
		                                    oi.price_chf,
                                            o.discount,
                                            o.d_price,
                                            orderItemTotal = (SELECT SUM(so.qty) FROM tbl_orderitem so where so.idx_order = oi.idx_order),
		                                    oi.price*oi.qty us1, 
		                                    oi.price_hkd*oi.qty us2, 
		                                    oi.price_chf*oi.qty us3,
		                                    (oi.price - (oi.price*o.discount/100)) * oi.qty dus1, 
		                                    (oi.price_hkd - (oi.price_hkd*o.discount/100)) * oi.qty dus2, 
		                                    (oi.price_chf - (oi.price_chf*o.discount/100)) * oi.qty dus3, 
		                                    o.customer_name,
                                            o.email,
                                            o.mobile		
                                            from tbl_orderitem oi left join tbl_order o on oi.idx_order = o.idx_order
                                            WHERE o.order_date >= @startDt AND o.order_date < @endDt ";
                    

                    cmd.CommandText = sql;
                    
                    cmd.Parameters.Add("@startDt", SqlDbType.DateTime).Value = targetDtFrom;
                    cmd.Parameters.Add("@endDt", SqlDbType.DateTime).Value = targetDtTo;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow r in dt.Rows)
                        {
                            //r["company_name"] = CryptoHelper.decryptAES((string)r["company_name"], defaultSKey);
                            r["company_name"] = (string)r["company_name"];
                            r["mobile"] = CryptoHelper.decryptAES((string)r["mobile"], defaultSKey);
                            r["email"] = CryptoHelper.decryptAES((string)r["email"], defaultSKey);
                        }

                        
                        return dt;
                    }

                } //auto dispose cmd

            }

        }

        public static void getOrderDetail(ref OrderADetail orderDetail)
		{
			using (SqlConnection conn = new SqlConnection(constr))
			{
				conn.Open();
				// Create the command object and set its properties
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = conn;
					cmd.Parameters.Clear();

					cmd.Parameters.Add("@idx_order", SqlDbType.Int).Value = orderDetail.idx_order;

					cmd.CommandText = @"SELECT o.order_date, o.discount, o.price, o.d_price, o.price_hkd, o.d_price_hkd, o.price_chf, o.d_price_chf, c.idx_customer, c.company_name, c.customer_name, c.email, c.mobile FROM [tbl_order] o inner join tbl_customer c on o.[idx_customer] = c.idx_customer where idx_order = @idx_order";


					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.HasRows)
						{
							reader.Read();

							orderDetail.idx_customer = (int) reader["idx_customer"];
                            orderDetail.company_name = CryptoHelper.decryptAES((string)reader["company_name"], DBHelper.defaultSKey);
							orderDetail.customer_email = CryptoHelper.decryptAES((string)reader["email"], DBHelper.defaultSKey);
							orderDetail.customer_mobile = CryptoHelper.decryptAES((string)reader["mobile"], DBHelper.defaultSKey);
							orderDetail.customer_name = (string)reader["customer_name"];

							orderDetail.order_date =  (DateTime)reader["order_date"];
							orderDetail.discount = (int)reader["discount"];
                            orderDetail.order_number = orderDetail.order_date.ToString("yyyyMMdd") + orderDetail.idx_order;


                            if (reader["price"] != DBNull.Value) orderDetail.price = (decimal)reader["price"];
							if (reader["price_hkd"] != DBNull.Value) orderDetail.price_hkd = (decimal)reader["price_hkd"];
							if (reader["price_chf"] != DBNull.Value) orderDetail.price_chf = (decimal)reader["price_chf"];

							if (reader["d_price"] != DBNull.Value) orderDetail.d_price = (decimal)reader["d_price"];
							if (reader["d_price_hkd"] != DBNull.Value) orderDetail.d_price_hkd = (decimal)reader["d_price_hkd"];
							if (reader["d_price_chf"] != DBNull.Value) orderDetail.d_price_chf = (decimal)reader["d_price_chf"];
						}

					}

					if (orderDetail.idx_customer != 0)
					{
						cmd.CommandText = @"SELECT [idx_watch], [qty], [price], [price_hkd], [price_chf] FROM[tbl_orderitem] where idx_order = @idx_order";

						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								
								OrderItem orderItem = new OrderItem();

								orderItem.idx_watch = (string) reader["idx_watch"];
								orderItem.qty = (int) reader["qty"];
								if (reader["price"] != DBNull.Value)  orderItem.price = (decimal) reader["price"];
								if (reader["price_hkd"] != DBNull.Value)  orderItem.price_hk = (decimal)reader["price_hkd"];
								if (reader["price_chf"] != DBNull.Value)  orderItem.price_chf = (decimal)reader["price_chf"];

								orderDetail.orderItems.Add(orderItem);

							}
						}

					}

				} //auto dispose cmd

			}


		}

        public static int DecryptCompanyName()
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Clear();

                    
                    cmd.CommandText = @"SELECT company_name, idx_customer FROM tbl_customer";

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        string companyName = (string) row["company_name"];
                        int customerId = (int) row["idx_customer"];
                        //customerId = 83;

                        cmd.Parameters.Clear();

                        cmd.CommandText = @"UPDATE tbl_customer SET company_name = @company where idx_customer = @id";
                        cmd.Parameters.Add("@company", SqlDbType.NVarChar, 2048).Value = CryptoHelper.decryptAES(companyName, defaultSKey);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = customerId;

                        cmd.ExecuteNonQuery();
                    }

                } //auto dispose cmd

            }

            return 1;
        }

        public static int DecryptOrderCompanyName()
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Parameters.Clear();


                    cmd.CommandText = @"SELECT company_name, idx_order FROM tbl_order";

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        string companyName = (string)row["company_name"];
                        int customerId = (int)row["idx_order"];
                        //customerId = 83;

                        cmd.Parameters.Clear();

                        cmd.CommandText = @"UPDATE tbl_order SET company_name = @company where idx_order = @id";
                        cmd.Parameters.Add("@company", SqlDbType.NVarChar, 2048).Value = CryptoHelper.decryptAES(companyName, defaultSKey);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = customerId;

                        cmd.ExecuteNonQuery();
                    }

                } //auto dispose cmd

            }

            return 1;
        }


    } // end partial class
} //end namesapce
