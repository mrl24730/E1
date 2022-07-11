using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ErnestBorel
{
	public static partial class DBHelper
    {
        #region Generic
        public static DataTable ExecuteReader2DataTable(string sql)
        {
            DataTable _table = new DataTable();
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn

            return _table;
        }

        public static int ExecuteUpdate(string sql)
        {
            int rowAffected = 0;
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();
                    rowAffected = cmd.ExecuteNonQuery();

                } //auto dispose cmd

            } //auto close conn

            return rowAffected;
        }

        #endregion

        #region City 
        public static DataTable searchCity(string sql)
        {
            DataTable _table = new DataTable();
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn

            return _table;
        }

        public static CityModel CityDetail(string sql)
        {
            CityModel model = new CityModel();
            DataTable _table = new DataTable();
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn

            //Convert result to model
            if (_table.Rows.Count > 0)
            {

                model.idx_city = (string)_table.Rows[0]["idx_city"];
                model.province = (string)_table.Rows[0]["idx_province"];
                model.country = (string)_table.Rows[0]["idx_country"];
                model.lng = (Decimal)_table.Rows[0]["city_lng"];
                model.lat = (Decimal)_table.Rows[0]["city_lat"];
                model.zoom = (int)_table.Rows[0]["city_zoom"];
                model.zoom_baidu = (int)_table.Rows[0]["city_zoom_baidu"];
                foreach (DataRow r in _table.Rows)
                {
                    string lang = (string)r["idx_lang"];
                    switch (lang)
                    {
                        case "en":
                            model.eName = (string)r["city_name"];
                            break;

                        case "tc":
                            model.tName = (string)r["city_name"];
                            break;

                        case "sc":
                            model.sName = (string)r["city_name"];
                            break;

                        case "fr":
                            model.fName = (string)r["city_name"];
                            break;

                        case "jp":
                            model.jName = (string)r["city_name"];
                            break;
                    }
                }
            }
            return model;
        }

        public static bool editCity(CityModel model)
        {
            int rowAffected = 0;
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //UPDATE Basic information
                    string sql = @"UPDATE tbl_city SET city_lng = @lng , city_lat = @lat, city_zoom = @zoom, city_zoom_baidu = @zoom_baidu ,idx_province = @idx_province WHERE idx_city = @idx_city;
                                   UPDATE tbl_city SET city_name = @eName WHERE idx_lang = 'en' AND idx_city = @idx_city;
                                   UPDATE tbl_city SET city_name = @sName WHERE idx_lang = 'sc' AND idx_city = @idx_city;
                                   UPDATE tbl_city SET city_name = @tName WHERE idx_lang = 'tc' AND idx_city = @idx_city;
                                   UPDATE tbl_city SET city_name = @jName WHERE idx_lang = 'jp' AND idx_city = @idx_city;
                                   UPDATE tbl_city SET city_name = @fName WHERE idx_lang = 'fr' AND idx_city = @idx_city;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@eName", SqlDbType.NText, 100).Value = model.eName;
                    cmd.Parameters.Add("@sName", SqlDbType.NText, 100).Value = model.sName;
                    cmd.Parameters.Add("@tName", SqlDbType.NText, 100).Value = model.tName;
                    cmd.Parameters.Add("@jName", SqlDbType.NText, 100).Value = model.jName;
                    cmd.Parameters.Add("@fName", SqlDbType.NText, 100).Value = model.fName;
                    cmd.Parameters.Add("@idx_province", SqlDbType.VarChar, 100).Value = model.province;
                    cmd.Parameters.Add("@idx_city", SqlDbType.VarChar, 100).Value = model.idx_city;
                    cmd.Parameters.Add("@lng", SqlDbType.Decimal).Value = model.lng;
                    cmd.Parameters.Add("@lat", SqlDbType.Decimal).Value = model.lat;
                    cmd.Parameters.Add("@zoom", SqlDbType.Int).Value = model.zoom;
                    cmd.Parameters.Add("@zoom_baidu", SqlDbType.Int).Value = model.zoom_baidu;
                    conn.Open();

                    rowAffected = cmd.ExecuteNonQuery();
                    
                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }

        public static bool insertCity(CityModel city)
        {
            bool isSuccess = false;

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    //INSERT Basic information
                    string sql = @"INSERT INTO tbl_city VALUES 
                                (@idx_city,'en',@idx_province,@idx_country,@city_ename,@city_lng,@city_lat,@city_zoom,0,1,0,0),
                                (@idx_city,'sc',@idx_province,@idx_country,@city_sname,@city_lng,@city_lat,@city_zoom,0,1,0,0),
                                (@idx_city,'tc',@idx_province,@idx_country,@city_tname,@city_lng,@city_lat,@city_zoom,0,1,0,0),
                                (@idx_city,'jp',@idx_province,@idx_country,@city_jname,@city_lng,@city_lat,@city_zoom,0,1,0,0),
                                (@idx_city,'fr',@idx_province,@idx_country,@city_fname,@city_lng,@city_lat,@city_zoom,0,1,0,0)";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@idx_city", SqlDbType.NVarChar, 20).Value = city.idx_city;
                    cmd.Parameters.Add("@idx_province", SqlDbType.NVarChar, 50).Value = city.province;
                    cmd.Parameters.Add("@idx_country", SqlDbType.Char, 2).Value = city.country;
                    cmd.Parameters.Add("@city_ename", SqlDbType.NVarChar, 50).Value = city.eName;
                    cmd.Parameters.Add("@city_sname", SqlDbType.NText, 50).Value = city.sName;
                    cmd.Parameters.Add("@city_tname", SqlDbType.NText , 50).Value = city.tName;
                    cmd.Parameters.Add("@city_jname", SqlDbType.NText, 50).Value = city.jName;
                    cmd.Parameters.Add("@city_fname", SqlDbType.NText, 50).Value = city.fName;
                    cmd.Parameters.Add("@city_lng", SqlDbType.Decimal).Value = city.lng;
                    cmd.Parameters.Add("@city_lat", SqlDbType.Decimal).Value = city.lat;
                    cmd.Parameters.Add("@city_zoom", SqlDbType.Int).Value = city.zoom;
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    isSuccess = true;

                } //auto dispose cmd

            } //auto close conn


            return isSuccess;
        }

        #endregion

        #region Store
        public static DataTable searchStore(string sql)
        {
            DataTable _table = new DataTable();
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn

            return _table;
        }


        public static StoreModel getStore(int id)
        {
            StoreModel model = new StoreModel(); ;
            DataTable _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //INSERT Basic information
                    string sql = @"Select * from vw_store where idx_shop = @id";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }
                    
                } //auto dispose cmd

            } //auto close conn


            //Convert result to model
            if(_table.Rows.Count > 0){

                model.id = (int)_table.Rows[0]["idx_shop"];
                model.regionId= (string)_table.Rows[0]["idx_region"];
                model.countryId = (string)_table.Rows[0]["idx_country"];
                model.provinceId = (string)_table.Rows[0]["idx_province"];
                model.cityId= (string)_table.Rows[0]["idx_city"];
                model.tel = ((string)_table.Rows[0]["shop_tel"])??"";
                model.fax = ((string)_table.Rows[0]["shop_fax"]) ?? "";
                model.email = ((string)_table.Rows[0]["shop_email"]) ?? "";
                model.web = ((string)_table.Rows[0]["shop_web"]) ?? "";
                model.isAftersales = (bool)_table.Rows[0]["is_aftersales"];
                model.isPos = (bool)_table.Rows[0]["is_pos"];
                model.lng = (decimal)_table.Rows[0]["shop_lng"];
                model.lat = (decimal)_table.Rows[0]["shop_lat"];

                foreach (DataRow r in _table.Rows)
                {
                    string lang = (string)r["idx_lang"];
                    switch (lang )
                    {
                        case "en":
                        model.name_en = (string)r["shop_name"];
                        model.address_en= (string)r["shop_address"];
                        break;

                        case "tc":
                        model.name_tc = (string)r["shop_name"];
                        model.address_tc = (string)r["shop_address"];
                        break;

                        case "sc":
                        model.name_sc = (string)r["shop_name"];
                        model.address_sc = (string)r["shop_address"];
                        break;

                        case "fr":
                        model.name_fr = (string)r["shop_name"];
                        model.address_fr = (string)r["shop_address"];
                        break;

                        case "jp":
                        model.name_jp = (string)r["shop_name"];
                        model.address_jp = (string)r["shop_address"];
                        break;
                    }
                }
            }

            return model;
        }


        public static bool insertStore(StoreModel model)
        {
            bool isSuccess = false;

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    
                    //INSERT Basic information
                    string sql = @"INSERT INTO tbl_store VALUES (@idx_region, @idx_country, @idx_province, @idx_city, @is_pos, @is_aftersales, 0, 
                                    @shop_tel, @shop_fax, @shop_email, @shop_web, @shop_lng, @shop_lat, 1, 0 ) SELECT SCOPE_IDENTITY()";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@idx_region", SqlDbType.Char, 2).Value = model.regionId;
                    cmd.Parameters.Add("@idx_country", SqlDbType.Char, 2).Value = model.countryId;
                    cmd.Parameters.Add("@idx_province", SqlDbType.VarChar, 50).Value = model.provinceId;
                    cmd.Parameters.Add("@idx_city", SqlDbType.VarChar, 30).Value = model.cityId;
                    cmd.Parameters.Add("@is_pos", SqlDbType.Bit, 30).Value = model.isPos;
                    cmd.Parameters.Add("@is_aftersales", SqlDbType.Bit, 30).Value = model.isAftersales;
                    cmd.Parameters.Add("@shop_tel", SqlDbType.VarChar, 50).Value = model.tel;
                    cmd.Parameters.Add("@shop_fax", SqlDbType.VarChar, 50).Value = model.fax;
                    cmd.Parameters.Add("@shop_email", SqlDbType.VarChar, 100).Value = model.email;
                    cmd.Parameters.Add("@shop_web", SqlDbType.VarChar, 100).Value = model.web;
                    cmd.Parameters.Add("@shop_lng", SqlDbType.Decimal).Value = model.lng;
                    cmd.Parameters.Add("@shop_lat", SqlDbType.Decimal).Value = model.lat;
                    conn.Open();

                    //INSERT differernt langauge information
                    model.id = Convert.ToInt32(cmd.ExecuteScalar());

                    if (model.id > 0)
                    {
                        string sql_lang = @"INSERT INTO tbl_store_lang VALUES
                                        (@idx_shop, 'en', @name_en, @address_en),
                                        (@idx_shop, 'tc', @name_tc, @address_tc),
                                        (@idx_shop, 'sc', @name_sc, @address_sc),
                                        (@idx_shop, 'fr', @name_fr, @address_fr),
                                        (@idx_shop, 'jp', @name_jp, @address_jp)";

                        cmd.CommandText = sql_lang;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@idx_shop", SqlDbType.Int).Value = model.id;
                        cmd.Parameters.Add("@name_en", SqlDbType.NVarChar, 100).Value = model.name_en;
                        cmd.Parameters.Add("@name_tc", SqlDbType.NVarChar, 100).Value = model.name_tc;
                        cmd.Parameters.Add("@name_sc", SqlDbType.NVarChar, 100).Value = model.name_sc;
                        cmd.Parameters.Add("@name_fr", SqlDbType.NVarChar, 100).Value = model.name_fr;
                        cmd.Parameters.Add("@name_jp", SqlDbType.NVarChar, 100).Value = model.name_jp;
                        cmd.Parameters.Add("@address_en", SqlDbType.NVarChar, 300).Value = model.address_en;
                        cmd.Parameters.Add("@address_tc", SqlDbType.NVarChar, 300).Value = model.address_tc;
                        cmd.Parameters.Add("@address_sc", SqlDbType.NVarChar, 300).Value = model.address_sc;
                        cmd.Parameters.Add("@address_fr", SqlDbType.NVarChar, 300).Value = model.address_fr;
                        cmd.Parameters.Add("@address_jp", SqlDbType.NVarChar, 300).Value = model.address_jp;
                        cmd.ExecuteNonQuery();

                        isSuccess = true;
                    }
                    
                } //auto dispose cmd

            } //auto close conn


            return isSuccess;
        }


        public static bool editStore(StoreModel model)
        {
            int rowAffected = 0;
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //UPDATE Basic information
                    string sql = @"UPDATE tbl_store SET idx_region = @idx_region, idx_country = @idx_country, idx_city = @idx_city, is_pos = @is_pos, is_aftersales = @is_aftersales, 
                                    shop_tel = @shop_tel, shop_fax = @shop_fax, shop_email = @shop_email, shop_web = @shop_web, shop_lng = @shop_lng, shop_lat = @shop_lat WHERE idx_shop = @id;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                    cmd.Parameters.Add("@idx_region", SqlDbType.Char, 2).Value = model.regionId;
                    cmd.Parameters.Add("@idx_country", SqlDbType.Char, 2).Value = model.countryId;
                    cmd.Parameters.Add("@idx_city", SqlDbType.VarChar, 30).Value = model.cityId;
                    cmd.Parameters.Add("@is_pos", SqlDbType.Bit, 30).Value = model.isPos;
                    cmd.Parameters.Add("@is_aftersales", SqlDbType.Bit, 30).Value = model.isAftersales;
                    cmd.Parameters.Add("@shop_tel", SqlDbType.VarChar, 50).Value = model.tel;
                    cmd.Parameters.Add("@shop_fax", SqlDbType.VarChar, 50).Value = model.fax;
                    cmd.Parameters.Add("@shop_email", SqlDbType.VarChar, 100).Value = model.email;
                    cmd.Parameters.Add("@shop_web", SqlDbType.VarChar, 100).Value = model.web;
                    cmd.Parameters.Add("@shop_lng", SqlDbType.Decimal).Value = model.lng;
                    cmd.Parameters.Add("@shop_lat", SqlDbType.Decimal).Value = model.lat;
                    conn.Open();

                    rowAffected = cmd.ExecuteNonQuery();
                    
                    //UPDATE differernt langauge information
                    string sql_base = "UPDATE tbl_store_lang SET shop_name = N'{0}', shop_address = N'{1}' WHERE idx_shop = {2} AND idx_lang = '{3}'; ";
                    string sql_lang = "";
                    sql_lang += String.Format(sql_base, model.name_en, model.address_en, model.id, "en");
                    sql_lang += String.Format(sql_base, model.name_tc, model.address_tc, model.id, "tc");
                    sql_lang += String.Format(sql_base, model.name_sc, model.address_sc, model.id, "sc");
                    sql_lang += String.Format(sql_base, model.name_fr, model.address_fr, model.id, "fr");
                    sql_lang += String.Format(sql_base, model.name_jp, model.address_jp, model.id, "jp");

                    cmd.CommandText = sql_lang;
                    cmd.Parameters.Clear();
                    rowAffected = cmd.ExecuteNonQuery();

                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }


        public static bool deleteStore(int id)
        {
            int rowAffected = 0;
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //UPDATE Basic information
                    string sql = @"UPDATE tbl_store SET is_deleted = 1 WHERE idx_shop = @id;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conn.Open();

                    rowAffected = cmd.ExecuteNonQuery();

                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }


        public static bool restoreStore(int id)
        {
            int rowAffected = 0;
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //UPDATE Basic information
                    string sql = @"UPDATE tbl_store SET is_deleted = 0 WHERE idx_shop = @id;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conn.Open();

                    rowAffected = cmd.ExecuteNonQuery();

                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }
        #endregion

        #region Collection
        public static DataTable searchCollection(string sql)
        {
            DataTable _table = new DataTable();
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn

            return _table;
        }
        

        public static CollectionModel getCollection(int id)
        {
            CollectionModel model = new CollectionModel();
            DataTable _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //INSERT Basic information
                    string sql = @"Select * from vw_collection where idx_collection = @id";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn


            //Convert result to model
            if (_table.Rows.Count > 0)
            {

                model.id = (int)_table.Rows[0]["idx_collection"];
                model.type = (string)_table.Rows[0]["col_movement"];
                model.col_ref = (string)_table.Rows[0]["col_ref"];
                model.lastupdate = (DateTime)_table.Rows[0]["col_lastupdate"];
                
                foreach (DataRow r in _table.Rows)
                {
                    string lang = (string)r["idx_lang"];
                    switch (lang)
                    {
                        case "en":
                            model.name_en = (string)r["col_name"];
                            model.desc_en = (r["col_desc"] != DBNull.Value) ? (string)r["col_desc"] : "";
                            break;

                        case "tc":
                            model.name_tc = (string)r["col_name"];
                            model.desc_tc = (r["col_desc"] != DBNull.Value) ? (string)r["col_desc"] : "";
                            break;

                        case "sc":
                            model.name_sc = (string)r["col_name"];
                            model.desc_sc = (r["col_desc"] != DBNull.Value) ? (string)r["col_desc"] : "";
                            break;

                        case "fr":
                            model.name_fr = (string)r["col_name"];
                            model.desc_fr = (r["col_desc"] != DBNull.Value) ? (string)r["col_desc"] : "";
                            break;

                        case "jp":
                            model.name_jp = (string)r["col_name"];
                            model.desc_jp = (r["col_desc"] != DBNull.Value) ? (string)r["col_desc"] : "";
                            break;
                    }
                }
            }

            return model;
        }
        

        public static bool checkColRefExist(string col_ref, int id=0)
        {

            bool isExist = false;

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //UPDATE Basic information
                    string sql = @"SELECT idx_collection FROM tbl_collection where col_ref = @col_ref;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@col_ref", SqlDbType.VarChar, 50).Value = col_ref;
                    conn.Open();

                    Object obj = cmd.ExecuteScalar();

                    if (obj != null && obj != DBNull.Value)
                    {
                        int idxExist = Convert.ToInt32(obj);

                        if (id == 0)
                        {
                            //new collection
                            isExist = true;
                        }
                        else if (id != idxExist)
                        {
                            //edit current collection but col_ref collision
                            isExist = true;
                        }
                    }

                } //auto dispose cmd

            } //auto close conn


            return isExist;
        }


        public static bool addCollection(CollectionModel model)
        {
            int rowAffected = 0;
            DateTime now = DateTime.UtcNow;

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //INSERT Basic information
                    string sql = @"INSERT INTO tbl_collection (col_movement, col_ref, col_lastupdate, is_published, is_deleted) VALUES (@type, @col_ref, @now, 1, 0) SELECT SCOPE_IDENTITY()";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = model.type;
                    cmd.Parameters.Add("@col_ref", SqlDbType.VarChar, 50).Value = model.col_ref;
                    cmd.Parameters.Add("@now", SqlDbType.DateTime).Value = now;
                    conn.Open();

                    //Object obj = cmd.ExecuteScalar();
                    model.id = Convert.ToInt32(cmd.ExecuteScalar());
                    
                    try
                    {   
                        //INSERT differernt langauge information
                        string sql_base = "INSERT INTO tbl_collection_lang (idx_collection, idx_lang, col_name, col_desc) VALUES ({0}, N'{1}', N'{2}', N'{3}'); ";
                        string sql_lang = "";
                        sql_lang += String.Format(sql_base, model.id, "en", model.name_en, model.desc_en);
                        sql_lang += String.Format(sql_base, model.id, "tc", model.name_tc, model.desc_tc);
                        sql_lang += String.Format(sql_base, model.id, "sc", model.name_sc, model.desc_sc);
                        sql_lang += String.Format(sql_base, model.id, "fr", model.name_fr, model.desc_fr);
                        sql_lang += String.Format(sql_base, model.id, "jp", model.name_jp, model.desc_jp);
                        cmd.CommandText = sql_lang;
                        cmd.Parameters.Clear();
                        rowAffected = cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        rowAffected = 0;
                    }
                    
                    
                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }


        public static bool editCollection(CollectionModel model)
        {
            int rowAffected = 0;
            DateTime now = DateTime.UtcNow;

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //UPDATE Basic information
                    string sql = @"UPDATE tbl_collection SET col_movement = @type, col_ref = @col_ref, col_lastupdate = @now WHERE idx_collection = @id;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = model.id;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = model.type;
                    cmd.Parameters.Add("@col_ref", SqlDbType.VarChar, 50).Value = model.col_ref;
                    cmd.Parameters.Add("@now", SqlDbType.DateTime).Value = now;
                    conn.Open();

                    rowAffected = cmd.ExecuteNonQuery();

                    //UPDATE differernt langauge information
                    string sql_base = "UPDATE tbl_collection_lang SET col_name = N'{2}', col_desc = N'{3}' WHERE idx_collection = {0} AND idx_lang = '{1}'; ";
                    string sql_lang = "";
                    sql_lang += String.Format(sql_base, model.id, "en", model.name_en, model.desc_en);
                    sql_lang += String.Format(sql_base, model.id, "tc", model.name_tc, model.desc_tc);
                    sql_lang += String.Format(sql_base, model.id, "sc", model.name_sc, model.desc_sc);
                    sql_lang += String.Format(sql_base, model.id, "fr", model.name_fr, model.desc_fr);
                    sql_lang += String.Format(sql_base, model.id, "jp", model.name_jp, model.desc_jp);

                    cmd.CommandText = sql_lang;
                    cmd.Parameters.Clear();
                    rowAffected = cmd.ExecuteNonQuery();

                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }


        public static bool deleteCollection(int id)
        {
            int rowAffected = 0;
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //UPDATE Basic information
                    string sql = @"UPDATE tbl_collection SET is_deleted = 1 WHERE idx_collection = @id;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    conn.Open();

                    rowAffected = cmd.ExecuteNonQuery();

                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }

        #endregion

        #region Watch
        public static DataTable searchWatch(string Lang, string Id)
        {
            DataTable _table = new DataTable();
            // Create the connection object

            string sql = @"SELECT * From vw_watch WHERE is_deleted = 0 AND idx_lang = '{0}' and idx_collection = {1} Order By sort DESC";
            sql = String.Format(sql, Lang, Id);

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn

            return _table;
        }

        public static WatchModel getWatch(string id)
        {
            WatchModel model = null;
            DataTable _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //INSERT Basic information
                    string sql = @"Select * from vw_watch where idx_watch = @id ";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 20).Value = id;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        _table.Load(reader);
                    }

                } //auto dispose cmd

            } //auto close conn


            //Convert result to model
            if (_table.Rows.Count > 0)
            {
                model = new WatchModel();
                model.id = (string)_table.Rows[0]["idx_watch"];
                model.idx_collection = (int)_table.Rows[0]["idx_collection"];
                model.sort = (int)_table.Rows[0]["sort"];
                model.gender = (string)_table.Rows[0]["watch_gender"];
                model.matching = (string)_table.Rows[0]["watch_matching"];
                model.bracelet = (string)_table.Rows[0]["watch_bracelet"];
                model._case = (string)_table.Rows[0]["watch_case"];
                model.shape = (string)_table.Rows[0]["watch_shape"];
                model.surface1 = (string)_table.Rows[0]["watch_surface1"];
                model.surface2 = (string)_table.Rows[0]["watch_surface2"];
                model.surface3 = (string)_table.Rows[0]["watch_surface3"];
                model.lastupdate = (DateTime)_table.Rows[0]["watch_lastupdate"];

                foreach (DataRow r in _table.Rows)
                {
                    string lang = (string)r["idx_lang"];
                    switch (lang)
                    {
                        case "en":
                            model.spec_en= (string)r["watch_spec"];
                            break;

                        case "tc":
                            model.spec_tc = (string)r["watch_spec"];
                            break;

                        case "sc":
                            model.spec_sc = (string)r["watch_spec"];
                            break;

                        case "fr":
                            model.spec_fr = (string)r["watch_spec"];
                            break;

                        case "jp":
                            model.spec_jp = (string)r["watch_spec"];
                            break;
                    }
                }
            }

            return model;
        }

        public static bool addWatch(WatchModel model)
        {
            int rowAffected = 0;
            DateTime now = DateTime.UtcNow;

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //INSERT Basic information
                    string sql = @"INSERT INTO tbl_watch VALUES 
                                    (@id, @idx_collection, @sort, @gender, @matching, @bracelet, @case, @shape, @surface1, @surface2, @surface3, 0, @now, 1, 0)";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 20).Value = model.id;
                    cmd.Parameters.Add("@idx_collection", SqlDbType.Int).Value = model.idx_collection;
                    cmd.Parameters.Add("@sort", SqlDbType.Int).Value = model.sort;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar, 20).Value = model.gender;
                    cmd.Parameters.Add("@matching", SqlDbType.VarChar, 20).Value = model.matching;
                    cmd.Parameters.Add("@bracelet", SqlDbType.VarChar, 50).Value = model.bracelet;
                    cmd.Parameters.Add("@case", SqlDbType.VarChar, 50).Value = model._case;
                    cmd.Parameters.Add("@shape", SqlDbType.VarChar, 50).Value = model.shape;
                    cmd.Parameters.Add("@surface1", SqlDbType.VarChar, 50).Value = model.surface1;
                    cmd.Parameters.Add("@surface2", SqlDbType.VarChar, 50).Value = model.surface2;
                    cmd.Parameters.Add("@surface3", SqlDbType.VarChar, 50).Value = model.surface3;
                    cmd.Parameters.Add("@now", SqlDbType.DateTime).Value = now;
                    conn.Open();

                    //Object obj = cmd.ExecuteScalar();
                    rowAffected = cmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        try
                        {
                            //INSERT differernt langauge information
                            string sql_base = "INSERT INTO tbl_watch_lang VALUES ('{0}', N'{1}', N'{2}'); ";
                            string sql_lang = "";
                            sql_lang += String.Format(sql_base, model.id, "en", model.spec_en);
                            sql_lang += String.Format(sql_base, model.id, "tc", model.spec_tc);
                            sql_lang += String.Format(sql_base, model.id, "sc", model.spec_sc);
                            sql_lang += String.Format(sql_base, model.id, "fr", model.spec_fr);
                            sql_lang += String.Format(sql_base, model.id, "jp", model.spec_jp);
                            cmd.CommandText = sql_lang;
                            cmd.Parameters.Clear();
                            rowAffected = cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            rowAffected = 0;
                        }
                    }


                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }

        public static bool editWatch(WatchModel model)
        {
            int rowAffected = 0;
            DateTime now = DateTime.UtcNow;

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //UPDATE Basic information
                    string sql = @"UPDATE tbl_watch SET idx_collection = @idx_collection, sort = @sort, watch_gender = @gender, watch_matching = @matching, 
                                    watch_bracelet = @bracelet, watch_case = @case, watch_shape = @shape, watch_surface1 = @surface1, 
                                    watch_surface2 = @surface2, watch_surface3 = @surface3, watch_lastupdate = @now
                                    WHERE idx_watch = @id;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 20).Value = model.id;
                    cmd.Parameters.Add("@idx_collection", SqlDbType.Int).Value = model.idx_collection;
                    cmd.Parameters.Add("@sort", SqlDbType.Int).Value = model.sort;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar, 20).Value = model.gender;
                    cmd.Parameters.Add("@matching", SqlDbType.VarChar, 20).Value = model.matching;
                    cmd.Parameters.Add("@bracelet", SqlDbType.VarChar, 50).Value = model.bracelet;
                    cmd.Parameters.Add("@case", SqlDbType.VarChar, 50).Value = model._case;
                    cmd.Parameters.Add("@shape", SqlDbType.VarChar, 50).Value = model.shape;
                    cmd.Parameters.Add("@surface1", SqlDbType.VarChar, 50).Value = model.surface1;
                    cmd.Parameters.Add("@surface2", SqlDbType.VarChar, 50).Value = model.surface2;
                    cmd.Parameters.Add("@surface3", SqlDbType.VarChar, 50).Value = model.surface3;
                    cmd.Parameters.Add("@now", SqlDbType.DateTime).Value = now;
                    conn.Open();

                    rowAffected = cmd.ExecuteNonQuery();

                    //UPDATE differernt langauge information
                    string sql_base = "UPDATE tbl_watch_lang SET watch_spec = N'{2}' WHERE idx_watch= '{0}' AND idx_lang = '{1}'; ";
                    string sql_lang = "";
                    sql_lang += String.Format(sql_base, model.id, "en", model.spec_en);
                    sql_lang += String.Format(sql_base, model.id, "tc", model.spec_tc);
                    sql_lang += String.Format(sql_base, model.id, "sc", model.spec_sc);
                    sql_lang += String.Format(sql_base, model.id, "fr", model.spec_fr);
                    sql_lang += String.Format(sql_base, model.id, "jp", model.spec_jp);
                    cmd.CommandText = sql_lang;
                    cmd.Parameters.Clear();
                    rowAffected = cmd.ExecuteNonQuery();

                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }

        public static bool deleteWatch(string id)
        {
            int rowAffected = 0;
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {

                    //UPDATE Basic information
                    string sql = @"UPDATE tbl_watch SET is_deleted = 1 WHERE idx_watch = @id;";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar, 20).Value = id;
                    conn.Open();

                    rowAffected = cmd.ExecuteNonQuery();

                } //auto dispose cmd

            } //auto close conn


            return (rowAffected > 0);
        }
        #endregion

    }
}