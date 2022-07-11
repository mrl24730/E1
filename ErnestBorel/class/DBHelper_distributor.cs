using Kitchen;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace ErnestBorel
{

    public static partial class DBHelper
    {
        public static bool getLoginSession()
        {
            bool isLogin = false;
            if(HttpContext.Current.Session["distributor_loggedin"] != null)
            {
                isLogin = (HttpContext.Current.Session["distributor_loggedin"] == "1");
            }
            return isLogin;
        }

        public static Dictionary<int, OrderOutput> getOrder(string email, string idcard, string lang)
        {
            DataTable items = new DataTable();
            Dictionary<int, OrderOutput> orders = new Dictionary<int, OrderOutput>();
            
            string iPrice= "price";
            string uPrice= "u_price";
            string dPrice= "d_price";

            if (lang == "tc")
            {
                iPrice = "price_hkd";
                uPrice = "u_price_hkd";
                dPrice = "d_price_hkd";
            }
            else if (lang == "en")
            {
                iPrice = "price_chf";
                uPrice = "u_price_chf";
                dPrice = "d_price_chf";
            }

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"select idx_order, order_date, {0} as price, discount, {1} as d_price, idx_watch, qty, {2} as u_price, 
                                        idx_collection, watch_type, email, customer_name, company_name, mobile, idcard, watch_gender
                                        FROM vw_order where email = @email AND idcard = @idcard ORDER BY idx_order desc";

                    sql = String.Format(sql, iPrice, dPrice, uPrice);
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 2048).Value = email;
                    cmd.Parameters.Add("@idcard", SqlDbType.VarChar, 10).Value = idcard;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        items.Load(reader);
                    } //end reader
                } //auto dispose cmd
            }

            
            foreach (DataRow dr in items.Rows)
            {
                
                int idx_order = (int)dr["idx_order"];
                
                if (!orders.ContainsKey(idx_order))
                {
                    OrderOutput _order = new OrderOutput();
                    _order.idx_order = (int)dr["idx_order"];
                    _order.order_date = DateTime.Parse(dr["order_date"].ToString());
                    _order.order_number = _order.order_date.ToString("yyyyMMdd")+_order.idx_order;
                    _order.order_date_display = _order.order_date.ToString("yyyy-MM-dd HH:mm:ss");
                    _order.price = (decimal)dr["price"];
                    _order.discount = (int)dr["discount"];
                    _order.d_price = (decimal)dr["d_price"];
                    _order.email = (string)dr["email"];
                    _order.customer_name = (string)dr["customer_name"];
                    _order.company_name = (string)dr["company_name"];
                    _order.mobile = (string)dr["mobile"];
                    _order.idcard = (string)dr["idcard"];

                    _order.email = CryptoHelper.decryptAES(_order.email, DBHelper.defaultSKey);
                    _order.mobile = CryptoHelper.decryptAES(_order.mobile, DBHelper.defaultSKey);

                    _order.total_qty = 0;
                    orders.Add(idx_order, _order);
                }

                OrderItemOutput _item = new OrderItemOutput();
                _item.idx_watch = (string)dr["idx_watch"];
                _item.qty = (int)dr["qty"];
                _item.u_price = (decimal)dr["u_price"];
                _item.idx_collection = (int)dr["idx_collection"];
                _item.collection = Global.collections[lang][_item.idx_collection].name;
                _item.watch_type = (string)dr["watch_type"];
                _item.watch_gender = (string)dr["watch_gender"];
                orders[idx_order].list.Add(_item);
                orders[idx_order].total_qty += _item.qty;
            }

            return orders;
        }

        public static int insertCustomer(Customer customer)
        {
            int customerId = 0;
            string e_company_name = customer.company_name;
            string e_email = CryptoHelper.encryptAES(customer.email.ToLower(), defaultSKey, fixedIV);
            string e_mobile = CryptoHelper.encryptAES(customer.mobile, defaultSKey, fixedIV);


            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {

                // Create the command object and set its properties
                using (SqlCommand cmd1 = new SqlCommand())
                {
                    //INSERT Basic information
                    string sql = @"INSERT INTO tbl_customer(company_name, customer_name, email, mobile, idcard) VALUES (@company_name, @customer_name, @email, @mobile, @idcard) SELECT SCOPE_IDENTITY()";
                    cmd1.Connection = conn;
                    cmd1.CommandText = sql;
                    cmd1.Parameters.Add("@company_name", SqlDbType.NVarChar, 2048).Value = e_company_name;
                    cmd1.Parameters.Add("@customer_name", SqlDbType.NVarChar, 2048).Value = customer.customer_name;
                    cmd1.Parameters.Add("@email", SqlDbType.VarChar, 2048).Value = e_email;
                    cmd1.Parameters.Add("@mobile", SqlDbType.VarChar, 2048).Value = e_mobile;
                    cmd1.Parameters.Add("@idcard", SqlDbType.VarChar, 10).Value = customer.idcard;

                    conn.Open();

                    customerId = Convert.ToInt32(cmd1.ExecuteScalar());
                   
                } //auto dispose cmd
                

            } //auto close conn

            return customerId;
        }

        public static Customer getCustomerById(int idx_customer)
        {
            Customer cust = new Customer();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    //INSERT Basic information

                    string sql = @"SELECT company_name, email, mobile, customer_name, idcard FROM tbl_customer WHERE idx_customer = @idx_customer";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@idx_customer", SqlDbType.Int).Value = idx_customer;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            cust.idx_customer = idx_customer;
                            cust.customer_name = (string)reader["customer_name"];
                            cust.company_name = (string)reader["company_name"];
                            cust.email = (string)reader["email"];
                            cust.mobile = (string)reader["mobile"];
                            cust.idcard = (string)reader["idcard"];

                            cust.company_name = cust.company_name;
                            cust.email = CryptoHelper.decryptAES(cust.email, defaultSKey);
                            cust.mobile = CryptoHelper.decryptAES(cust.mobile, defaultSKey);
                        }
                    }

                } //auto dispose cmd

            } //auto close conn

            return cust;
        }

        public static int checkCustomer(Customer customer)
        {
            int idx_customer = 0;

            customer.company_name = customer.company_name;
            customer.email = CryptoHelper.encryptAES(customer.email.ToLower(), defaultSKey, fixedIV);
            customer.mobile = CryptoHelper.encryptAES(customer.mobile, defaultSKey, fixedIV);

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    //INSERT Basic information

                    string sql = @"SELECT idx_customer FROM tbl_customer WHERE company_name = @company_name AND email = @email AND mobile = @mobile AND customer_name = @customer_name and idcard = @idcard ";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 2048).Value = customer.company_name;
                    cmd.Parameters.Add("@customer_name", SqlDbType.NVarChar, 2048).Value = customer.customer_name;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 2048).Value = customer.email;
                    cmd.Parameters.Add("@mobile", SqlDbType.VarChar, 2048).Value = customer.mobile;
                    cmd.Parameters.Add("@idcard", SqlDbType.VarChar, 10).Value = customer.idcard;
                    conn.Open();

                    Object obj = cmd.ExecuteScalar();
                    if(obj != null && obj != DBNull.Value)
                    {
                        idx_customer = Convert.ToInt32(obj);
                    }

                } //auto dispose cmd

            } //auto close conn
            return idx_customer;
        }

        public static void rollbackOrder(int? idx_order)
        {
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd1 = new SqlCommand())
                {
                    //INSERT Basic information
                    string sql = @"DELETE FROM tbl_orderitem WHERE idx_order = @idx_order";
                    cmd1.Connection = conn;
                    cmd1.CommandText = sql;
                    cmd1.Parameters.Add("@idx_order", SqlDbType.Int).Value = idx_order;

                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                } //auto dispose cmd

                // Create the command object and set its properties
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    //INSERT Basic information
                    string sql = @"DELETE FROM tbl_order WHERE idx_order = @idx_order";
                    cmd2.Connection = conn;
                    cmd2.CommandText = sql;
                    cmd2.Parameters.Add("@idx_order", SqlDbType.Int).Value = idx_order;

                    conn.Open();
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                } //auto dispose cmd

            } //auto close conn

        }

        public static bool insertOrderItem(int? idx_order, decimal price, decimal price_hkd, decimal price_chf, OrderItem orderItem)
        {
            bool isSuccess = false;
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    //INSERT Basic information
                    string sql = @"INSERT INTO tbl_orderitem(idx_order, idx_watch, qty, price, price_hkd, price_chf) VALUES (@idx_order, @idx_watch, @qty, @price, @price_hkd, @price_chf)";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@idx_order", SqlDbType.Int).Value = idx_order;
                    cmd.Parameters.Add("@idx_watch", SqlDbType.VarChar, 20).Value = orderItem.idx_watch;
                    cmd.Parameters.Add("@qty", SqlDbType.Int).Value = orderItem.qty;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = price;
                    cmd.Parameters.Add("@price_hkd", SqlDbType.Decimal).Value = price_hkd;
                    cmd.Parameters.Add("@price_chf", SqlDbType.Decimal).Value = price_chf;
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    isSuccess = true;
                } //auto dispose cmd

            } //auto close conn

            return isSuccess;
        }

        public static int? insertOrder(Order order)
        {
            int? insertedOrderID;

            order.email = CryptoHelper.encryptAES(order.email.ToLower(), defaultSKey, fixedIV);
            order.mobile = CryptoHelper.encryptAES(order.mobile, defaultSKey, fixedIV);

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    //INSERT Basic information
                    string sql = @"INSERT INTO tbl_order(order_date, idx_customer, price, price_hkd, price_chf, ip_address, discount, d_price, d_price_hkd, d_price_chf, company_name, customer_name, email, mobile, idcard) OUTPUT Inserted.idx_order 
                        VALUES (@order_date, @idx_customer, @price, @price_hkd, @price_chf, @ip_address, @discount, @d_price, @d_price_hkd, @d_price_chf, @company_name, @customer_name, @email, @mobile, @idcard)";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@order_date", SqlDbType.DateTime, 20).Value = order.order_date;
                    cmd.Parameters.Add("@idx_customer", SqlDbType.Int).Value = order.idx_customer;
                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = order.price;
                    cmd.Parameters.Add("@price_hkd", SqlDbType.Decimal).Value = order.price_hkd;
                    cmd.Parameters.Add("@price_chf", SqlDbType.Decimal).Value = order.price_chf;
                    cmd.Parameters.Add("@ip_address", SqlDbType.VarChar, 128).Value = order.ip_address;
                    cmd.Parameters.Add("@discount", SqlDbType.Int).Value = order.discount;
                    cmd.Parameters.Add("@d_price", SqlDbType.Decimal).Value = order.d_price;
                    cmd.Parameters.Add("@d_price_hkd", SqlDbType.Decimal).Value = order.d_price_hkd;
                    cmd.Parameters.Add("@d_price_chf", SqlDbType.Decimal).Value = order.d_price_chf;

                    cmd.Parameters.Add("@company_name", SqlDbType.NVarChar, 2048).Value = order.company_name;
                    cmd.Parameters.Add("@customer_name", SqlDbType.NVarChar, 2048).Value = order.customer_name;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 2048).Value = order.email;
                    cmd.Parameters.Add("@mobile", SqlDbType.VarChar, 2048).Value = order.mobile;
                    cmd.Parameters.Add("@idcard", SqlDbType.VarChar, 10).Value = order.idcard;
                    
                    conn.Open();
                    insertedOrderID = (int)cmd.ExecuteScalar();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    return insertedOrderID;

                } //auto dispose cmd

            } //auto close conn

        }

        public static JObject getItemSpec(string watch)
        {
            // decimal price = 0;
            JObject price = new JObject();
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    //INSERT Basic information

                    string sql = @"SELECT idx_collection, watch_gender, watch_type, price, price_hkd, price_chf FROM tbl_watch WHERE idx_watch = @idx_watch AND is_deleted = 0;";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@idx_watch", SqlDbType.VarChar, 20).Value = watch;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            price["price"] = (decimal)reader["price"];
                            price["price_hkd"] = (decimal)reader["price_hkd"];
                            price["price_chf"] = (decimal)reader["price_chf"];
                            price["idx_collection"] = (int)reader["idx_collection"];
                            price["watch_gender"] = (string)reader["watch_gender"];
                            price["watch_type"] = (string)reader["watch_type"];
                        }
                    }

                } //auto dispose cmd

            } //auto close conn

            return price;
        }



        public static List<WatchList> getWatchPrice(string lang)
        {
            List<WatchList> result = new List<WatchList>();
            DataTable items = new DataTable();

            string price;
            if (lang == "tc")
            {
                price = "price_hkd";
            }
            else if (lang == "sc")
            {
                price = "price";
            }
            else
            {
                price = "price_chf";
            }

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT w.idx_watch, w.watch_oldmodel, w.idx_collection, watch_matching, watch_gender, watch_bracelet, watch_case, watch_shape, watch_surface1, watch_surface2, watch_surface3, 
                                    {0} as price, l.idx_lang, watch_spec, watch_type, col_name, col_desc, col_movement FROM tbl_watch w, tbl_watch_lang l, tbl_collection_lang col_l, tbl_collection col 
                                    WHERE w.idx_watch = l.idx_watch AND w.is_sale = 1 AND w.is_deleted = 0 AND w.idx_collection = col_l.idx_collection AND w.idx_collection = col.idx_collection AND
                                    l.idx_lang = col_l.idx_lang AND l.idx_lang = @lang ORDER BY w.sort_distributor, watch_type";

                    sql = String.Format(sql, price);
                    
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        items.Load(reader);                        
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

            foreach (DataRow dr in items.Rows)
            {
                WatchList _watchList = new WatchList();
                _watchList.idx_watch = (string)dr["idx_watch"];
                _watchList.idx_collection = (int)dr["idx_collection"];
                _watchList.watch_matching = (string)dr["watch_matching"];
                _watchList.watch_gender = (string)dr["watch_gender"];
                _watchList.watch_bracelet = (string)dr["watch_bracelet"];
                _watchList.watch_case = (dr["watch_case"] == DBNull.Value) ? "" : (string)dr["watch_case"];
                _watchList.watch_shape = (string)dr["watch_shape"];

                _watchList.watch_surface1 = (dr["watch_surface1"] == DBNull.Value) ? "" : (string)dr["watch_surface1"];
                _watchList.watch_surface2 = (dr["watch_surface2"] == DBNull.Value) ? "" : (string)dr["watch_surface2"];
                _watchList.watch_surface3 = (dr["watch_surface3"] == DBNull.Value) ? "" : (string)dr["watch_surface3"];
                _watchList.price = (dr["price"] == DBNull.Value) ? 0 : (decimal)dr["price"];
                _watchList.idx_lang = (string)dr["idx_lang"];
                _watchList.watch_spec = (string)dr["watch_spec"];
                _watchList.watch_type = (dr["watch_type"] == DBNull.Value) ? "" : (string)dr["watch_type"];
                _watchList.col_movement = (string)dr["col_movement"];
                _watchList.col_name = (string)dr["col_name"];
                _watchList.col_desc = (string)dr["col_desc"];
                
                string img_s = ((string)dr["idx_watch"]).Replace("-", "_") + "_s.png";
                string img_l = ((string)dr["idx_watch"]).Replace("-", "_") + "_l.png";
                string img_t = ((string)dr["idx_watch"]).Replace("-", "_") + "_t.png";

                _watchList.image_s = (File.Exists(imgWatchFolder + img_s)) ? imgWatchPath + img_s : noimage_s;
                _watchList.image_l = (File.Exists(imgWatchFolder + img_l)) ? imgWatchPath + img_l : noimage_l;
                _watchList.image_t = (File.Exists(imgWatchFolder + img_t)) ? imgWatchPath + img_t : noimage_t;

                _watchList.watch_type_lang = Properties.Resources.ResourceManager.GetString(String.Format("watchtype_{0}_{1}", _watchList.watch_type, lang));

                result.Add(_watchList);
            }
            return result;
        }

        public static void getWatchPriceForm(string lang, out DataTable _table)
        {

            _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT idx_collection, idx_watch FROM tbl_watch WHERE is_deleted = 0 ";

                    sql += " ORDER BY idx_collection, idx_watch";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

        }

        public static List<WatchList> getFeaturedWatch(string lang)
        {
            List<WatchList> result = new List<WatchList>();
            DataTable items = new DataTable();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"select idx_watch, watch_matching, w.price, w.idx_lang, watch_spec, watch_type, col_name from vw_watch w left join vw_collection c
                                    on w.idx_collection = c.idx_collection where is_featured = 1 and w.idx_lang = @idx_lang and  c.idx_lang = @idx_lang order by w.sort_feature";

                    //sql += " ORDER BY w.sort_distributor";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@idx_lang", SqlDbType.Char, 2).Value = lang;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        items.Load(reader);
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

            foreach (DataRow dr in items.Rows)
            {
                WatchList _watchList = new WatchList();
                _watchList.idx_watch = (string)dr["idx_watch"];
                _watchList.watch_matching = (string)dr["watch_matching"];
                _watchList.price = (decimal)dr["price"];
                _watchList.idx_lang = (string)dr["idx_lang"];
                _watchList.watch_spec = (string)dr["watch_spec"];
                _watchList.watch_type = (string)dr["watch_type"];
                _watchList.watch_type_lang = Properties.Resources.ResourceManager.GetString(String.Format("watchtype_{0}_{1}", _watchList.watch_type, lang)); ;
                _watchList.col_name = (string)dr["col_name"];

                string img_s = (_watchList.idx_watch).Replace("-", "_") + "_s.png";
                string img_l = (_watchList.idx_watch).Replace("-", "_") + "_l.png";
                string img_t = (_watchList.idx_watch).Replace("-", "_") + "_t.png";

                _watchList.image_s = (File.Exists(imgWatchFolder + img_s)) ? imgWatchPath + img_s : noimage_s;
                _watchList.image_l = (File.Exists(imgWatchFolder + img_l)) ? imgWatchPath + img_l : noimage_l;
                _watchList.image_t = (File.Exists(imgWatchFolder + img_t)) ? imgWatchPath + img_t : noimage_t;


                result.Add(_watchList);
            }
            return result;
        }




    }
}
