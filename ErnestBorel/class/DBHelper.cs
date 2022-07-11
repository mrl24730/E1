using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
//using System.Collections.Generic;
using Kitchen;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ErnestBorel
{
	public static partial class DBHelper
	{

        public static string environment = ConfigurationManager.AppSettings["Environment"];
        public static string constr = ConfigurationManager.ConnectionStrings["db" + environment].ConnectionString;
        public static bool isPreview = (ConfigurationManager.AppSettings["Preview"] == "1") ? true : false;
        public static string defaultSKey = "e8d92sK1";
        public static string fixedIV = "#m0DATwelve***";
        public static string imgWatchPath = "/images/watches/";
        public static string imgWatchFolder = HttpContext.Current.Server.MapPath(imgWatchPath);
        public static string noimage_t = imgWatchPath + "noimage_t.png";
        public static string noimage_s = imgWatchPath + "noimage_s.png";
        public static string noimage_l = imgWatchPath + "noimage_l.png";
        


        #region Watch / Collection
        public static Dictionary<int, collectionObj> getAllCollection(string lang)
        {
            Dictionary<int, collectionObj> list = new Dictionary<int, collectionObj>();
            
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    cmd.Connection = conn;
                    conn.Open();
                    
                    string sql = @"SELECT idx_collection, col_name, col_ref , col_movement, col_desc
                                  FROM vw_collection WHERE idx_lang = @lang AND is_deleted = 0 ORDER BY sort asc ";
                                       
                    
                    // Execute the command and save the results in a DataTable
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                collectionObj model = new collectionObj();
                                model.idx_collection = (int)reader["idx_collection"];
                                model.name = (string)reader["col_name"];
                                model.col_ref = (string)reader["col_ref"];
                                model.type_name = (string)reader["col_movement"];
                                list.Add(model.idx_collection , model);
                            }
                        }
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

            return list;
        }

        public static JObject getCollectionList(string collection,bool is_sale = false)
        {
            JObject obj = new JObject();
            JArray collectionList = new JArray();
            obj.Add("collection", collectionList);

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT idx_watch, watch_type FROM tbl_watch WHERE idx_watch IN (SELECT idx_watch FROM tbl_watch WHERE idx_collection = @collection) {0} order by sort;";
                    sql = String.Format(sql, (is_sale) ? "AND is_sale = 1" : "");

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@collection", SqlDbType.VarChar, 20).Value = collection;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                JObject _watch = new JObject();
                                _watch["idx_watch"] = (string)reader["idx_watch"];
                                _watch["watch_type"] = (string)reader["watch_type"];
                                
                                string img_s = ((string)_watch["idx_watch"]).Replace("-", "_") + "_s.png";
                                string img_l = ((string)_watch["idx_watch"]).Replace("-", "_") + "_l.png";
                                string img_t = ((string)_watch["idx_watch"]).Replace("-", "_") + "_t.png";


                                _watch["image_s"] = (File.Exists(imgWatchFolder + img_s)) ? imgWatchPath + img_s : noimage_s;
                                _watch["image_l"] = (File.Exists(imgWatchFolder + img_l)) ? imgWatchPath + img_l : noimage_l;
                                _watch["image_t"] = (File.Exists(imgWatchFolder + img_t)) ? imgWatchPath + img_t : noimage_t;

                                collectionList.Add(_watch);
                            }

                        }
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

            return obj;
        }

        public static void GetCollectionByFamily(string lang, string family, out DataTable _table, bool is_sale = false)
        {
            _table = new DataTable();
            Dictionary<int, int> qtyList = new Dictionary<int, int>();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql_qty = "select idx_collection, count(idx_watch) qty from tbl_watch where is_deleted = 0 AND is_sale = 1 AND {0} group by idx_collection";
                    if (is_sale)
                    {
                        sql_qty = String.Format(sql_qty, "is_sale = 1");
                    }
                    else
                    {
                        sql_qty = String.Format(sql_qty, "");
                    }

                    /*
                    string sql = @";WITH cte AS
                                    (
                                       SELECT idx_watch, watch_type, idx_collection, sort,
                                             ROW_NUMBER() OVER (PARTITION BY idx_collection ORDER BY sort DESC) AS rn
                                       FROM tbl_watch WHERE 1=1 AND {0}
                                    )
                                    SELECT cte.*, cl.col_name, cl.col_desc, cl.col_movement 
                                    FROM cte LEFT JOIN vw_collection cl on cte.idx_collection = cl.idx_collection
                                    WHERE rn = 1 and idx_lang = @lang and col_movement = @family order by cl.sort";
                                    */

                    string sql = "select idx_collection, col_image, col_name, sort from vw_collection WHERE idx_lang = @lang and is_deleted = 0 and col_movement = @family order by sort";
                    /*
                    if (is_sale)
                    {
                        sql = String.Format(sql, "is_sale = 1");
                    }
                    else
                    {
                        sql = String.Format(sql, "");
                    }
                    */

                    cmd.Connection = conn;
                    cmd.CommandText = sql_qty;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                qtyList.Add((int)reader["idx_collection"], (int)reader["qty"]);
                            }

                        }
                    }//end reader

                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@family", SqlDbType.VarChar, 10).Value = family;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            _table.Load(reader);
                            _table.Columns.Add("qty", typeof(int));
                            _table.Columns.Add("image_s", typeof(string));
                            _table.Columns.Add("image_l", typeof(string));
                            _table.Columns.Add("image_t", typeof(string));

                            foreach (DataRow r in _table.Rows)
                            {
                                r["qty"] = qtyList[(int)r["idx_collection"]];
                                string img_s = (string)r["col_image"];
                                string img_l = (string)r["col_image"];
                                string img_t = (string)r["col_image"];
                                
                                r["image_s"] = (File.Exists(imgWatchFolder + img_s)) ? imgWatchPath + img_s : noimage_s;
                                r["image_l"] = (File.Exists(imgWatchFolder + img_l)) ? imgWatchPath + img_l : noimage_l;
                                r["image_t"] = (File.Exists(imgWatchFolder + img_t)) ? imgWatchPath + img_t : noimage_t;

                            }
                        }
                    }//end reader



                } //auto dispose cmd

            } //auto close conn
        }

        public static WatchDetail getWatchDetail(string watch, string lang)
        {
            WatchDetail watchDetail = new WatchDetail();
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
                    string sql = @"SELECT w.idx_watch, watch_oldmodel, watch_type, watch_spec, watch_gender, watch_matching," + price + " as price, w.idx_collection, col_name, (SELECT " + price + " FROM tbl_watch " +
                        "WHERE idx_watch = (SELECT watch_matching FROM tbl_watch WHERE idx_watch = @watch)) AS cupPrice, (SELECT watch_type FROM tbl_watch WHERE idx_watch = (SELECT watch_matching FROM tbl_watch WHERE idx_watch = @watch)) AS cupType, (SELECT col_name FROM tbl_watch, tbl_collection_lang WHERE tbl_watch.idx_collection = tbl_collection_lang.idx_collection AND tbl_collection_lang.idx_lang = @lang AND idx_watch = (SELECT watch_matching FROM tbl_watch WHERE idx_watch = @watch)) As cupCol FROM tbl_watch w, tbl_watch_lang l, tbl_collection_lang col WHERE w.idx_watch = l.idx_watch AND col.idx_collection = w.idx_collection AND l.idx_lang = col.idx_lang AND w.idx_watch = @watch AND l.idx_lang = @lang AND is_deleted = 0";

                    sql += " ORDER BY watch_type";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@watch", SqlDbType.VarChar, 20).Value = watch;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            watchDetail.idx_watch = (string)reader["idx_watch"];
                            watchDetail.watch_oldmodel = (string)reader["watch_oldmodel"];
                            watchDetail.watch_type = (string)reader["watch_type"];
                            watchDetail.watch_spec = (string)reader["watch_spec"];
                            watchDetail.watch_gender = (string)reader["watch_gender"];
                            watchDetail.price = (reader["price"] == DBNull.Value) ? 0 : (decimal)reader["price"];
                            
                            watchDetail.idx_collection = (int)reader["idx_collection"];
                            watchDetail.col_name = (string)reader["col_name"];
                            watchDetail.watch_type_lang = Properties.Resources.ResourceManager.GetString(String.Format("watchtype_{0}_{1}", watchDetail.watch_type, lang));
                            
                            string img_s = (watchDetail.idx_watch).Replace("-", "_") + "_s.png";
                            string img_l = (watchDetail.idx_watch).Replace("-", "_") + "_l.png";
                            string img_t = (watchDetail.idx_watch).Replace("-", "_") + "_t.png";

                            watchDetail.image_s = (File.Exists(imgWatchFolder + img_s)) ? imgWatchPath + img_s : noimage_s;
                            watchDetail.image_l = (File.Exists(imgWatchFolder + img_l)) ? imgWatchPath + img_l : noimage_l;
                            watchDetail.image_t = (File.Exists(imgWatchFolder + img_t)) ? imgWatchPath + img_t : noimage_t;


                            JObject _cuple = new JObject();
                            _cuple["cuple"] = (reader["watch_matching"] == DBNull.Value) ? "" : (string)reader["watch_matching"];
                            if (!String.IsNullOrEmpty((string)_cuple["cuple"]))
                            {
                                _cuple["price"] = (reader["cupPrice"] == DBNull.Value) ? 0 : (decimal)reader["cupPrice"];
                                _cuple["col"] = (reader["cupCol"] == DBNull.Value) ? null : (string)reader["cupCol"];
                                _cuple["type"] = (reader["cupType"] == DBNull.Value) ? null : (string)reader["cupType"];
                                _cuple["type_lang"] = Properties.Resources.ResourceManager.GetString(String.Format("watchtype_{0}_{1}", _cuple["type"], lang));
                                img_s = ((string)_cuple["cuple"]).Replace("-", "_") + "_s.png";
                                _cuple["image_s"] = (File.Exists(imgWatchFolder + img_s)) ? imgWatchPath + img_s : noimage_s;
                            }
                            else
                            {
                                _cuple["type_lang"] = "";
                            }
                            watchDetail.watch_matching = _cuple;
                        }
                        else
                        {
                            throw new Distributor.WatchDetailNotFound();
                        }
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

            return watchDetail;
        }

        public static DataTable getFamilyLang(string lang)
        {
            DataTable _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
              // Create the command object and set its properties
              using (SqlCommand cmd = new SqlCommand())
              {
                // Open the connection
                string sql = @"SELECT fl.idx_family, fl.family_desc, f.family_img, fl.family_name
                              FROM tbl_family f INNER JOIN tbl_family_lang fl ON f.idx_family = fl.idx_family
                              WHERE f.is_deleted = 0 AND idx_lang = @lang
                              ORDER BY f.sort ASC";

                //if (!isPreview)
                //{
                //  sql += " AND is_published = 1 ";
                //}

                //sql += " ORDER BY idx_collection, idx_watch";

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                conn.Open();

                // Execute the command and save the results in a DataTable

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                  _table.Load(reader);
                  return _table;
                }//end reader


              } //auto dispose cmd

            } //auto close conn
    }

        public static void getLatestCollection(int qty, string lang, string type, out DataTable _table, bool isDetail = false)
        {

            _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    cmd.Connection = conn;
                    conn.Open();
                    
                    string detailQuery = "";
                    string qtyQuery = "";
                    string sql = @"SELECT {0} c.idx_collection, c.col_name, c.col_ref, c.idx_lang {1}
                                  FROM vw_collection c WHERE is_deleted = 0 ";

                    if (lang != "all")
                    {
                        sql = sql + " AND idx_lang = @lang ";
                        cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    }
                    
                    if (qty > 0)
                    {
                        qtyQuery = "TOP " + qty;
                    }

                    if (!String.IsNullOrEmpty(type))
                    {
                        sql += " AND col_movement = @type ";
                        cmd.Parameters.Add("@type", SqlDbType.VarChar, 10).Value = type;
                    }

                    if (isDetail)
                    {
                        detailQuery = ", c.col_image, (select count(idx_watch) FROM tbl_watch WHERE idx_collection = c.idx_collection) as col_ttl ";
                    }

                    sql = String.Format(sql, qtyQuery, detailQuery);
                    sql = sql + " ORDER BY c.sort asc";
                    

                    // Execute the command and save the results in a DataTable
                    cmd.CommandText = sql;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

        }

        public static collectionObj getCollectionDetail(string lang, string col_ref)
        {
            collectionObj obj = new collectionObj();
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    cmd.Connection = conn;
                    cmd.Parameters.Clear();


                    string detailQuery = "";
                    string qtyQuery = "";
                    string sql = @"SELECT c.col_ref, l.col_name, l.col_desc
                                    FROM tbl_collection_lang l LEFT JOIN tbl_collection c On l.idx_collection = c.idx_collection 
                                    WHERE c.col_ref = @col_ref AND idx_lang = @lang ";
                    

                    sql = String.Format(sql, qtyQuery, detailQuery);

                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@col_ref", SqlDbType.VarChar, 50).Value = col_ref;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            obj.name = (string)reader["col_name"];
                            obj.desc = (reader["col_desc"] == DBNull.Value) ? "" : (string)reader["col_desc"];
                            obj.col_ref = (string)reader["col_ref"];
                        }
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

            return obj;
        }

        public static void getWatchByCollection(string lang, string col_ref,out DataTable _table)
        {

            _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string detailQuery = "";
                    string qtyQuery = "";
                    string sql = @"SELECT idx_watch, watch_oldmodel, idx_collection, watch_type, watch_matching, watch_spec, watch_have3D
                                  FROM vw_watch WHERE is_deleted = 0 AND idx_collection = (SELECT idx_collection FROM tbl_collection WHERE col_ref = @col_ref) AND idx_lang = @lang
                                  Order By sort DESC";

                    if (!isPreview)
                    {
                        sql += " AND is_published = 1 ";
                    }

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@col_ref", SqlDbType.VarChar, 50).Value = col_ref;
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

        public static void getWatchBySearch(string lang, searchObj search, out DataTable _table)
        {

            _table = new DataTable();
            string detailQuery = "";

            if (search.type == "keyword")
            {
                detailQuery = "AND (w.idx_watch like @keyword OR c.col_name like @keyword2 OR w.watch_oldmodel like @keyword) ";
            }
            else
            {
                detailQuery = (String.IsNullOrEmpty(search.gender)) ? detailQuery : detailQuery + " AND w.watch_gender in (" + search.gender + ")";
                detailQuery = (String.IsNullOrEmpty(search.bracelet)) ? detailQuery : detailQuery + " AND w.watch_bracelet in (" + search.bracelet + ")";
                detailQuery = (String.IsNullOrEmpty(search.shape)) ? detailQuery : detailQuery + " AND w.watch_shape in (" + search.shape + ")";
                detailQuery = (String.IsNullOrEmpty(search.material)) ? detailQuery : detailQuery + " AND w.watch_case in (" + search.material + ")";
                detailQuery = (String.IsNullOrEmpty(search.cover)) ? detailQuery : detailQuery + " AND ( w.watch_surface1 in (" + search.cover + ") OR w.watch_surface2 in (" + search.cover + ") OR w.watch_surface3 in (" + search.cover + ") )";
            }

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT c.col_movement, c.col_ref, c.col_name, w.idx_watch 
                                    FROM vw_watch w LEFT JOIN vw_collection c ON w.idx_collection  = c.idx_collection 
                                    WHERE w.is_deleted = 0 AND c.idx_lang = @lang AND w.idx_lang = @lang {0} ";

                    if (!isPreview)
                    {
                        sql += " AND w.is_published = 1";
                    }

                    sql = string.Format(sql, detailQuery);

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    if (search.type == "keyword")
                    {
                        cmd.Parameters.Add("@keyword", SqlDbType.VarChar, 20).Value = "%" + search.keyword + "%";
                        cmd.Parameters.Add("@keyword2", SqlDbType.NVarChar, 100).Value = "%" + search.keyword + "%";
                    }
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

        }

        public static void getWatchByLang(string lang, out DataTable _table)
        {

            _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT idx_collection, idx_watch, watch_spec FROM vw_watch WHERE idx_lang = @lang AND is_deleted = 0 ";

                    if (!isPreview)
                    {
                        sql += " AND is_published = 1 ";
                    }

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

        public static DataTable getWatchSelector(string lang)
        {
            DataTable _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT idx_type, idx_selector, selector_name FROM tbl_watch_selector WHERE idx_lang = @lang AND is_published = 1 ORDER BY idx_type, sort";
                    
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

            return _table;
        }

        public static void getWatchByKeyword(string lang, string keyword, out DataTable _table, bool is_sale = false)
        {
            _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT w.watch_type, cl.col_name, w.idx_watch, w.price
                                    FROM tbl_watch w
                                    INNER JOIN tbl_watch_lang wl ON w.idx_watch = wl.idx_watch
                                    INNER JOIN tbl_collection_lang cl ON w.idx_collection = cl.idx_collection
                                    where wl.idx_lang = @lang AND cl.idx_lang = @lang AND (wl.watch_spec LIKE @keyword OR cl.col_name LIKE @keyword OR w.idx_watch LIKE @keyword OR w.watch_oldmodel LIKE @keyword) {0}";

                    if (is_sale)
                    {
                        sql = String.Format(sql, "AND is_sale = 1");
                    }
                    else
                    {
                        sql = String.Format(sql, "");
                    }
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    cmd.Parameters.Add("@keyword", SqlDbType.NVarChar, 50).Value = String.Format("%{0}%", keyword);
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                        _table.Columns.Add("watch_type_lang", typeof(string));
                        _table.Columns.Add("qty", typeof(int));
                        _table.Columns.Add("image_s", typeof(string));
                        _table.Columns.Add("image_l", typeof(string));
                        _table.Columns.Add("image_t", typeof(string));

                        foreach (DataRow r in _table.Rows)
                        {
                            string img_s = ((string)r["idx_watch"]).Replace("-", "_") + "_s.png";
                            string img_l = ((string)r["idx_watch"]).Replace("-", "_") + "_l.png";
                            string img_t = ((string)r["idx_watch"]).Replace("-", "_") + "_t.png";

                            r["image_s"] = (File.Exists(imgWatchFolder + img_s)) ? imgWatchPath + img_s : noimage_s;
                            r["image_l"] = (File.Exists(imgWatchFolder + img_l)) ? imgWatchPath + img_l : noimage_l;
                            r["image_t"] = (File.Exists(imgWatchFolder + img_t)) ? imgWatchPath + img_t : noimage_t;

                            r["watch_type_lang"] = Properties.Resources.ResourceManager.GetString(String.Format("watchtype_{0}_{1}", r["watch_type"], lang));
                        }
                    }//end reader
                }
            }
        }

#endregion

        #region News
                /// <summary>
                /// News
                /// </summary>
                /// <param name="qty"></param>
                /// <param name="page"></param>
                /// <param name="lang"></param>
                /// <param name="_table"></param>
                /// <returns></returns>
                public static int getNewsListing(int qty, int page, string lang, out DataTable _table)
        {

            _table = new DataTable();
            int start = ((page-1)*qty)+1;
            int end = (page*qty);
            int ttl = 0;

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"WITH OrderedOrders AS
                                    (
                                        SELECT idx_news, news_title, news_desc, news_image1, news_ref, news_date,
                                        ROW_NUMBER() OVER (ORDER BY news_date DESC) AS RowNumber
                                        FROM vw_news WHERE idx_lang = @lang AND is_deleted = 0 {0}
                                    ) 
                                    SELECT idx_news, news_title, news_desc, news_image1, news_ref, news_date, RowNumber 
                                    FROM OrderedOrders 
                                    WHERE RowNumber BETWEEN {1} AND {2}";

                    string published = "";
                    if (!isPreview)
                    {
                        published = "AND is_published = 1 ";
                    }
                    sql = String.Format(sql, published, start, end);
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }//end reader


                    sql = "SELECT COUNT(idx_news) as ttl FROM vw_news WHERE idx_lang = @lang AND is_deleted = 0 " + published;
                    cmd.CommandText = sql;
                    Object obj = cmd.ExecuteScalar();
                    if (obj != null && obj != DBNull.Value)
                    {
                        ttl = Convert.ToInt32(obj);
                    }

                } //auto dispose cmd

            } //auto close conn

            return ttl;
        }

        public static void getNewsDetail(string lang, ref articleObj article)
        {

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT idx_news, news_date, news_ref, news_title, news_desc, tag, news_video, 
                                    news_image1, news_image2, news_image3, news_image4, news_image5, news_image6, 
                                    news_caption1, news_caption2, news_caption3, news_caption4, news_caption5, news_caption6
                                    FROM vw_news WHERE news_ref = @ref AND idx_lang = @lang";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    cmd.Parameters.Add("@ref", SqlDbType.VarChar, 20).Value = article.news_ref;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            article.news_id = (int)reader["idx_news"];
                            article.title = (string)reader["news_title"];
                            article.desc = (string)reader["news_desc"];
                            article.date = (DateTime)reader["news_date"];
                            article.videoURL = (reader["news_video"] == DBNull.Value)? "":(string)reader["news_video"];
                            article.imageURL = new List<string>();
                            article.imageCaption = new List<string>();
                            article.tag = (string)reader["tag"];

                            if (reader["news_image1"] != DBNull.Value && reader["news_image1"] != "")
                            {
                                article.imageURL.Add((string)reader["news_image1"]);
                                article.imageCaption.Add((reader["news_caption1"] == DBNull.Value) ? "" : (string)reader["news_caption1"]);
                            }
                            if (reader["news_image2"] != DBNull.Value && reader["news_image2"] != "")
                            {
                                article.imageURL.Add((string)reader["news_image2"]);
                                article.imageCaption.Add((reader["news_caption2"] == DBNull.Value) ? "" : (string)reader["news_caption2"]);
                            }
                            if (reader["news_image3"] != DBNull.Value && reader["news_image3"] != "")
                            {
                                article.imageURL.Add((string)reader["news_image3"]);
                                article.imageCaption.Add((reader["news_caption3"] == DBNull.Value) ? "" : (string)reader["news_caption3"]);
                            }
                            if (reader["news_image4"] != DBNull.Value && reader["news_image4"] != "")
                            {
                                article.imageURL.Add((string)reader["news_image4"]);
                                article.imageCaption.Add((reader["news_caption4"] == DBNull.Value) ? "" : (string)reader["news_caption4"]);
                            }
                            if (reader["news_image5"] != DBNull.Value && reader["news_image5"] != "")
                            {
                                article.imageURL.Add((string)reader["news_image5"]);
                                article.imageCaption.Add((reader["news_caption5"] == DBNull.Value) ? "" : (string)reader["news_caption5"]);
                            }
                            if (reader["news_image6"] != DBNull.Value && reader["news_image6"] != "")
                            {
                                article.imageURL.Add((string)reader["news_image6"]);
                                article.imageCaption.Add((reader["news_caption6"] == DBNull.Value) ? "" : (string)reader["news_caption6"]);
                            }

                        }
                    }//end reader

                } //auto dispose cmd

            } //auto close conn

        }

        public static void getRelatedNews(string lang, ref articleObj article, bool isAmbassador)
        {
            string tagCondition = "";

            if (isAmbassador)
            {
                string ambassador = (string)article.ambassador;
                tagCondition = "AND ambassador like '%" + ambassador + "%' ";
            }
            else
            {
                string[] tags = ((string)article.tag).Split('|');
                
                foreach (string t in tags)
                {
                    tagCondition = "AND tag like '%" + t + "%' ";
                }
            }

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT TOP 3 news_ref, news_title, news_date FROM vw_news WHERE idx_lang = @lang AND is_deleted = 0 AND is_published = 1 AND news_ref != @ref " + tagCondition + " ORDER BY news_date DESC";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    cmd.Parameters.Add("@ref", SqlDbType.VarChar, 20).Value = article.news_ref;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                relateNewsObj rn = new relateNewsObj();
                                rn.Url = (string)reader["news_ref"];
                                rn.Title = (string)reader["news_title"];
                                rn.Date = ((DateTime)reader["news_date"]).ToString("yyyy-MM-dd");
                                article.relatedNews.Add(rn);
                            }
                        }
                    }//end reader

                } //auto dispose cmd

            } //auto close conn
        }
        
        public static void getNextNews(string lang, ref articleObj article)
        {

            string published = "";
            if (!isPreview)
            {
                published = "AND is_published = 1 ";
            }

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sqlPrev = @"SELECT TOP 1 news_ref, news_title FROM vw_news WHERE idx_lang = @lang AND is_deleted = 0 " + published + " AND news_date > @date ORDER BY news_date";
                    string sqlNext = @"SELECT TOP 1 news_ref, news_title FROM vw_news WHERE idx_lang = @lang AND is_deleted = 0 " + published + " AND news_date < @date ORDER BY news_date DESC";

                    cmd.Connection = conn;
                    cmd.CommandText = sqlNext;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = article.date;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            article.next.news_ref = (string)reader["news_ref"];
                            article.next.news_title = (string)reader["news_title"];
                        }
                    }//end reader next


                    cmd.CommandText = sqlPrev;
                    
                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            article.prev.news_ref = (string)reader["news_ref"];
                            article.prev.news_title = (string)reader["news_title"];
                        }
                    }//end reader prev


                } //auto dispose cmd

            } //auto close conn
        }

        public static void getNewsById(string lang, List<int> idList, ref articleObj article)
        {

            string ids = String.Join(",", idList);

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT TOP 3 news_ref, news_title, news_date FROM vw_news WHERE idx_lang = @lang AND is_deleted = 0 AND idx_news in (" + ids + ") ORDER BY news_date DESC";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                relateNewsObj rn = new relateNewsObj();
                                rn.Url = (string)reader["news_ref"];
                                rn.Title = (string)reader["news_title"];
                                rn.Date = ((DateTime)reader["news_date"]).ToString("yyyy-MM-dd");
                                article.relatedNews.Add(rn);
                            }
                        }
                    }//end reader

                } //auto dispose cmd

            } //auto close conn
        }

        #endregion

        #region Store
        /// <summary>
        /// Global Start Function
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="lang"></param>
        /// <param name="type"></param>
        /// <param name="_table"></param>

        public static Dictionary<string, dynamic> getRegion()
        {

            Dictionary<string, dynamic> region = new Dictionary<string, dynamic>();
            List<locationObj> content_en = new List<locationObj>();
            List<locationObj> content_tc = new List<locationObj>();
            List<locationObj> content_sc = new List<locationObj>();
            List<locationObj> content_fr = new List<locationObj>();
            List<locationObj> content_jp = new List<locationObj>();
            locationObj obj = new locationObj();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT idx_region, idx_lang, region_name FROM tbl_region";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string lang = (string)reader["idx_lang"];
                                obj = new locationObj();
                                obj.idx = (string)reader["idx_region"];
                                obj.name = (string)reader["region_name"];
                                switch (lang)
                                {
                                    case "en":
                                        content_en.Add(obj);
                                        break;
                                    case "tc":
                                        content_tc.Add(obj);
                                        break;
                                    case "sc":
                                        content_sc.Add(obj);
                                        break;
                                    case "fr":
                                        content_fr.Add(obj);
                                        break;
                                    case "jp":
                                        content_jp.Add(obj);
                                        break;
                                }
                            }
                        }
                    }//end reader

                } //auto dispose cmd

            } //auto close conn

            region.Add("en", content_en);
            region.Add("tc", content_tc);
            region.Add("sc", content_sc);
            region.Add("fr", content_fr);
            region.Add("jp", content_jp);

            content_en = null;
            content_tc = null;
            content_sc = null;
            content_fr = null;
            content_jp = null;

            return region;
        }

        public static Dictionary<string, dynamic> getCountry()
        {

            Dictionary<string, dynamic> list = new Dictionary<string, dynamic>();
            List<locationObj> content_en = new List<locationObj>();
            List<locationObj> content_tc = new List<locationObj>();
            List<locationObj> content_sc = new List<locationObj>();
            List<locationObj> content_fr = new List<locationObj>();
            List<locationObj> content_jp = new List<locationObj>();
            locationObj obj = new locationObj();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT DISTINCT(s.idx_country), s.idx_region, c.country_name, c.idx_lang, c.country_default
                                    FROM tbl_store s LEFT JOIN tbl_country c ON s.idx_country = c.idx_country;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    string lang = (string)reader["idx_lang"];
                                    obj = new locationObj();
                                    obj.idx = (string)reader["idx_country"];
                                    obj.idx_parent = (string)reader["idx_region"];
                                    obj.name = (string)reader["country_name"];
                                    obj.default_cs = (int)reader["country_default"];

                                    switch (lang)
                                    {
                                        case "en":
                                            content_en.Add(obj);
                                            break;
                                        case "tc":
                                            content_tc.Add(obj);
                                            break;
                                        case "sc":
                                            content_sc.Add(obj);
                                            break;
                                        case "fr":
                                            content_fr.Add(obj);
                                            break;
                                        case "jp":
                                            content_jp.Add(obj);
                                            break;
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                    }//end reader

                } //auto dispose cmd

            } //auto close conn

            list.Add("en", content_en);
            list.Add("tc", content_tc);
            list.Add("sc", content_sc);
            list.Add("fr", content_fr);
            list.Add("jp", content_jp);

            content_en = null;
            content_tc = null;
            content_sc = null;
            content_fr = null;
            content_jp = null;

            return list;
        }

        public static Dictionary<string, dynamic> getCity()
        {

            Dictionary<string, dynamic> list = new Dictionary<string, dynamic>();
            List<locationObj> content_en = new List<locationObj>();
            List<locationObj> content_tc = new List<locationObj>();
            List<locationObj> content_sc = new List<locationObj>();
            List<locationObj> content_fr = new List<locationObj>();
            List<locationObj> content_jp = new List<locationObj>();
            locationObj obj = new locationObj();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT DISTINCT(s.idx_city), s.idx_country, c.city_name, c.idx_lang, c.city_lng, c.city_lat, c.city_zoom, c.city_zoom_baidu
                                    FROM tbl_store s LEFT JOIN tbl_city c ON s.idx_city = c.idx_city WHERE s.is_deleted = 0;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    string lang = (string)reader["idx_lang"];
                                    obj = new locationObj();
                                    obj.idx = (string)reader["idx_city"];
                                    obj.idx_parent = (string)reader["idx_country"];
                                    obj.name = (string)reader["city_name"];
                                    obj.lng = (decimal)reader["city_lng"];
                                    obj.lat = (decimal)reader["city_lat"];
                                    obj.zoom = (int)reader["city_zoom"];
                                    obj.zoom_baidu = (int)reader["city_zoom_baidu"];

                                    switch (lang)
                                    {
                                        case "en":
                                            content_en.Add(obj);
                                            break;
                                        case "tc":
                                            content_tc.Add(obj);
                                            break;
                                        case "sc":
                                            content_sc.Add(obj);
                                            break;
                                        case "fr":
                                            content_fr.Add(obj);
                                            break;
                                        case "jp":
                                            content_jp.Add(obj);
                                            break;
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                    }//end reader

                } //auto dispose cmd

            } //auto close conn

            list.Add("en", content_en);
            list.Add("tc", content_tc);
            list.Add("sc", content_sc);
            list.Add("fr", content_fr);
            list.Add("jp", content_jp);

            content_en = null;
            content_tc = null;
            content_sc = null;
            content_fr = null;
            content_jp = null;

            return list;
        }

        /// <summary>
        /// Location
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="page"></param>
        /// <param name="lang"></param>
        /// <param name="_table"></param>
        /// <returns></returns>
        /// 
        public static void getLocation(string lang, string idx_city, ref DataTable _table, bool isPOS, bool isAfterSales)
        {
            string type = "";
            if (isPOS)
            {
                type += " AND is_pos = 1 ";
            }
            if (isAfterSales)
            {
                type += " AND is_aftersales = 1 ";
            }
            
            string city = "";
            if(idx_city != "all")
            {
                city = "AND idx_city = @idx_city";
            }

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    

                    //string target = (locationType == (int)LocationType.network) ? "vw_network" : "vw_distributor";
                    string sql = @"SELECT idx_region, idx_country, idx_city, idx_shop, is_pos, is_aftersales, shop_address, shop_name, shop_tel, shop_fax, shop_web, shop_email, shop_lng, shop_lat
                                    FROM vw_store WHERE is_deleted = 0 " + city + type + " AND idx_lang = @lang ORDER BY idx_region, idx_country, idx_city";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    if (idx_city != "all")
                        cmd.Parameters.Add("@idx_city", SqlDbType.VarChar, 30).Value = idx_city;
                    conn.Open();

                    // Execute the command and save the results in a DataTable

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }//end reader


                } //auto dispose cmd

            } //auto close conn

        }

#endregion

        #region Investor

        /*
        public static DataSet getInvestorAnnounce(int lang = 0, int page = 1, int itemPerPage = 10)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    int stNum = (page - 1) * itemPerPage + 1; // 0 base
                    int edNum = page * itemPerPage;

                    string sql = String.Format(@"select ttl,convert(varchar, ir_releaseDate, 23) as ir_releaseDate, ir_title,ir_desc,ir_file, floor(ir_filesize / 1024) as ir_filesize from 
                        (select ROW_NUMBER() OVER (ORDER BY ir_releaseDate DESC, rec_idx DESC) AS idx, ttl = Count(rec_idx) OVER(),ir_releaseDate, ir_title,ir_desc,ir_file, ir_filesize from vw_ir_release WHERE ir_lang = {0} AND ir_releaseDate <= GETDATE())  As NewTable
                           WHERE idx >= {1} AND idx <= {2}", lang, stNum, edNum);

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;




                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }

            return ds;

        }
        */

        public static DataTable getInvestorAnnounce(int lang = 0, int page = 1, int itemPerPage = 10)
        {
            DataTable _table = new DataTable();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    int stNum = (page - 1) * itemPerPage + 1; // 0 base
                    int edNum = page * itemPerPage;

                    string sql = String.Format(@"select ttl,convert(varchar, ir_releaseDate, 23) as ir_releaseDate, ir_title,ir_desc,ir_file, floor(ir_filesize / 1024) as ir_filesize from 
                        (select ROW_NUMBER() OVER (ORDER BY ir_releaseDate DESC, rec_idx DESC) AS idx, ttl = Count(rec_idx) OVER(),ir_releaseDate, ir_title,ir_desc,ir_file, ir_filesize from vw_ir_release WHERE ir_lang = {0} AND ir_releaseDate <= GETDATE() AND ir_status != 2 )  As NewTable
                           WHERE idx >= {1} AND idx <= {2}", lang, stNum, edNum);

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;




                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }
                }
            }

            return _table;
        }

        #endregion

        #region Extra
        
        /// <summary>
        /// For OLD Mobile App, 3 level only [2017-03-23 before]
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static appLocation getLocation(string lang, string type)
        {
            DataTable region = new DataTable();
            DataTable country = new DataTable();
            DataTable province = new DataTable();
            DataTable city = new DataTable();

            appLocation location = new appLocation();
            location.region = new List<appRegion>();

            #region SQL load db info
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection

                    string target = (type == "network") ? "AND is_aftersales_active = 1" : "AND is_pos_active = 1";

                    string sql_region = @" SELECT idx_region, region_name FROM tbl_region WHERE idx_lang = @lang ";

                    string sql_country = @" SELECT idx_region, idx_country, country_name FROM tbl_country WHERE idx_lang = @lang " + target + " ";

                    string sql_province = @" SELECT idx_province, province_name FROM tbl_province WHERE idx_lang = @lang " + target + " ";

                    string sql_city = @" SELECT * FROM tbl_city WHERE idx_lang = @lang " + target + " ";

                    cmd.Connection = conn;
                    cmd.CommandText = sql_region;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        region.Load(reader);
                    }

                    cmd.CommandText = sql_country;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        country.Load(reader);
                    }

                    cmd.CommandText = sql_province;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        province.Load(reader);
                    }

                    cmd.CommandText = sql_city;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        city.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn
            #endregion


            List<appCity> list_city = new List<appCity>();

            foreach (DataRow rr in region.Rows)
            {
                appRegion _region = new appRegion();
                _region.name = (string)rr["region_name"];
                _region.country = new List<appCountry>();
                string idx_region = (string)rr["idx_region"];

                foreach (DataRow rc in country.Rows)
                {
                    if (idx_region == (string)rc["idx_region"])
                    {
                        string idx_country = (string)rc["idx_country"];
                        appCountry _country = new appCountry();
                        _country.id = idx_country;
                        _country.name = (string)rc["country_name"];
                        _country.city = new List<appCity>();
                        //3 level
                        foreach (DataRow rcc in city.Rows)
                        {
                            if (idx_country == (string)rcc["idx_country"])
                            {
                                appCity _city = new appCity();
                                _city.id = (string)rcc["idx_city"];
                                _city.name = (string)rcc["city_name"];
                                _city.lng = (decimal)rcc["city_lng"];
                                _city.lat = (decimal)rcc["city_lat"];
                                _city.zoom = (int)rcc["city_zoom"];
                                _city.zoom_baidu = (int)rcc["city_zoom_baidu"];
                                _country.city.Add(_city);
                            }
                        }//end for city

                        _region.country.Add(_country);
                    }
                }//end for country
                location.region.Add(_region);
            }//end for region

            return location;

        }


        /// <summary>
        /// For new Mobile App, unlimited level [2017-03-23 later]
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static LocationList getCityList(string lang, string type)
        {
            DataTable region = new DataTable();
            DataTable country = new DataTable();
            DataTable province = new DataTable();
            DataTable city = new DataTable();

            LocationList location = new LocationList();
            List<LocationItem> region_list = new List<LocationItem>();
            List<LocationItem> country_list = new List<LocationItem>();
            List<LocationItem> province_list = new List<LocationItem>();
            List<CityItem> city_list = new List<CityItem>();

            #region SQL grab info from DB
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection

                    string target = "";
                    if (type == "all")
                    {
                        target = "";
                    }
                    else if (type == "network")
                    {
                        target = "AND is_aftersales_active = 1";
                    }
                    else
                    {
                        target = "AND is_pos_active = 1";
                    }

                    string sql_region = @" SELECT idx_region, region_name FROM tbl_region WHERE idx_lang = @lang ";

                    string sql_country = @" SELECT idx_region, idx_country, country_name FROM tbl_country WHERE idx_lang = @lang " + target + " ";

                    string sql_province = @" SELECT idx_province, province_name FROM tbl_province WHERE idx_lang = @lang " + target + " ";

                    string sql_city = @" SELECT * FROM tbl_city WHERE idx_lang = @lang " + target + " AND idx_province != '' ";

                    cmd.Connection = conn;
                    cmd.CommandText = sql_region;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        region.Load(reader);
                    }
                    
                    cmd.CommandText = sql_country;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        country.Load(reader);
                    }

                    cmd.CommandText = sql_province;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        province.Load(reader);
                    }

                    cmd.CommandText = sql_city;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        city.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn
            #endregion


            region_list = new List<LocationItem>();
            foreach (DataRow rr in region.Rows)
            {
                string idx_region = (string)rr["idx_region"];
                LocationItem _region = new LocationItem();
                _region.id = idx_region;
                _region.name = (string)rr["region_name"];

                country_list = new List<LocationItem>();
                foreach (DataRow rc in country.Rows)
                {
                    if (idx_region == (string)rc["idx_region"])
                    {
                        string idx_country = (string)rc["idx_country"];
                        LocationItem _country = new LocationItem();
                        _country.id = idx_country;
                        _country.name = (string)rc["country_name"];

                        if (idx_country != "CN")
                        {
                            //3 level
                            city_list = new List<CityItem>();
                            foreach (DataRow rcc in city.Rows)
                            {
                                if (idx_country == (string)rcc["idx_country"])
                                {
                                    CityItem _city = new CityItem();
                                    _city.id = (string)rcc["idx_city"];
                                    _city.name = (string)rcc["city_name"];
                                    _city.lng = (decimal)rcc["city_lng"];
                                    _city.lat = (decimal)rcc["city_lat"];
                                    _city.zoom = (int)rcc["city_zoom"];
                                    _city.zoom_baidu = (int)rcc["city_zoom_baidu"];
                                    city_list.Add(_city);
                                }
                            }//end for city
                            _country.list = city_list;
                            country_list.Add(_country);
                        }
                        else
                        {
                            //4 level
                            province_list = new List<LocationItem>();
                            foreach (DataRow rp in province.Rows)
                            {
                                string idx_province = (string)rp["idx_province"];
                                LocationItem _province = new LocationItem();
                                _province.id = idx_province;
                                _province.name = (string)rp["province_name"];

                                city_list = new List<CityItem>();
                                foreach (DataRow rcc in city.Rows)
                                {
                                    if (idx_province == (string)rcc["idx_province"])
                                    {
                                        CityItem _city = new CityItem();
                                        _city.id = (string)rcc["idx_city"];
                                        _city.name = (string)rcc["city_name"];
                                        _city.lng = (decimal)rcc["city_lng"];
                                        _city.lat = (decimal)rcc["city_lat"];
                                        _city.zoom = (int)rcc["city_zoom"];
                                        _city.zoom_baidu= (int)rcc["city_zoom_baidu"];
                                        city_list.Add(_city);
                                    }
                                }//end for city
                                _province.list = city_list;
                                province_list.Add(_province);

                            }//end for province
                            _country.list = province_list;
                            country_list.Add(_country);
                        }//end of "CN"
                    }
                }//end for country
                _region.list = country_list;
                region_list.Add(_region);
            }//end for region

            location.list = region_list;

            return location;

        }

        public static JObject getStore(string lang, string city_idx, bool addSplit = false, string shop_type = "")
        {
            JObject obj = new JObject();
            JArray jarray_shop = new JArray();
            obj.Add("shop", jarray_shop);

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection

                    //string target = (locationType == (int)LocationType.network) ? "vw_network" : "vw_distributor";
                    string sql = @"SELECT 
                        ts.idx_shop as idx_shop,
                        tsl.shop_name as name,
                        tsl.shop_address as address,
                        ts.shop_lng as lng,
                        ts.shop_lat as lat,
                        ts.shop_tel as tel,
                        ts.shop_fax as fax,
                        ts.shop_email as email,
                        ts.shop_web as web,
                        ts.shop_sort as sort
                    FROM tbl_store ts RIGHT JOIN tbl_store_lang tsl ON ts.idx_shop = tsl.idx_shop 
                    WHERE tsl.idx_lang=@lang AND ts.idx_city=@idx_city {0} AND ts.is_deleted = 0 ORDER BY tsl.shop_address ASC";

                    
                    if (shop_type == "network")
                    {
                        sql = String.Format(sql, "AND ts.is_aftersales = 1");
                    }
                    else
                    {
                        sql = String.Format(sql, "AND ts.is_pos= 1");
                    }
                    

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    cmd.Parameters.Add("@idx_city", SqlDbType.VarChar, 30).Value = city_idx;
                    
                    conn.Open();


                    // Execute the command and save the results in a DataTable
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] shopInfo = new string[7];
                            shopInfo[0] = (string) reader["name"];
                            shopInfo[1] = (string)reader["address"];
                            shopInfo[2] = "";

                            if ((string)reader["tel"] != "")
                            {
                                shopInfo[2] += "Tel: " + (string)reader["tel"];

                                if (addSplit) shopInfo[2] += "//";
                            }
                            

                            if ((string)reader["fax"] != "")
                            {
                                shopInfo[2] += "Fax: " + (string)reader["fax"];

                                if (addSplit) shopInfo[2] += "//";
                            }

                            if ((string)reader["email"] != "")
                            {
                                shopInfo[2] += "Email: " + (string)reader["email"];

                                if (addSplit) shopInfo[2] += "//";
                            }

                            if ((string)reader["web"] != "")
                            {
                                shopInfo[2] += "Web: " + (string)reader["web"];
                            }

                            shopInfo[3] = reader["lat"].ToString();
                            shopInfo[4] = reader["lng"].ToString();
                            shopInfo[5] = String.Format("{0}-s{1}", city_idx, reader["idx_shop"].ToString());
                            shopInfo[6] = reader["sort"].ToString();
                            
                            jarray_shop.Add(new JArray(shopInfo));
                        }
                    }
                }

            }

            return obj;

        }

        public static void getStoreDetail(string lang, string idx_city, int idx_shop, ref decimal lat, ref decimal lng, ref string shopName, ref string shopAddress, ref string shopContact)
        {
            string published = "";
            if (!isPreview)
            {
                published = "AND ts.is_published = 1 ";
            }

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection

                    //string target = (locationType == (int)LocationType.network) ? "vw_network" : "vw_distributor";
                    string sql = @"SELECT 
                        ts.idx_shop as idx_shop,
                        tsl.shop_name as name,
                        tsl.shop_address as address,
                        ts.shop_lng as lng,
                        ts.shop_lat as lat,
                        ts.shop_tel as tel,
                        ts.shop_fax as fax,
                        ts.shop_email as email,
                        ts.shop_web as web 
                    FROM tbl_store ts RIGHT JOIN tbl_store_lang tsl ON ts.idx_shop = tsl.idx_shop 
                    WHERE tsl.idx_lang=@lang AND ts.idx_city=@idx_city AND ts.idx_city = @idx_city AND ts.idx_shop = @idx_shop AND ts.is_deleted = 0" + published;

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@lang", SqlDbType.Char, 2).Value = lang;
                    cmd.Parameters.Add("@idx_city", SqlDbType.VarChar, 30).Value = idx_city;
                    cmd.Parameters.Add("@idx_shop", SqlDbType.Int).Value = idx_shop;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lat = (decimal)reader["lat"];
                            lng = (decimal)reader["lng"];

                            shopName = (string) reader["name"];
                            shopAddress = (string)reader["address"];


                            shopContact = (string)reader["tel"];

                            if ((string)reader["fax"] != "")
                            {
                                shopContact += " " + (string)reader["fax"];
                            }

                            if ((string)reader["email"] != "")
                            {
                                shopContact += " " + (string)reader["email"];
                            }

                            if ((string)reader["web"] != "")
                            {
                                shopContact += " " + (string)reader["web"];
                            }



                        }

                    }
                }

            }
        }
        #endregion

    }
}
