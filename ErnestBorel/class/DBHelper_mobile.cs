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

namespace ErnestBorel
{
    public static partial class DBHelper
	{


        public static DataTable getInitWatch(string lang)
        {
            DataTable _table = new DataTable();
            
            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    
                    string sql = @"SELECT idx_collection, col_image, col_name, col_movement, sort
                                    FROM vw_collection WHERE idx_lang = @lang AND is_deleted = 0 ORDER BY col_movement, sort ASC";
                    
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



        public static DataTable getInitNews(string lang)
        {
            DataTable _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT TOP 100 idx_news, news_title, news_date, news_image1
                                  FROM vw_news WHERE idx_lang = @lang Order By news_date Desc ";

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




        public static DataTable getAppCollection(string lang, int idx_collection)
        {
            DataTable _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT idx_watch, watch_spec, watch_oldmodel
                                  FROM vw_watch WHERE idx_lang = @lang AND idx_collection = @idx_collection AND is_deleted = 0 ORDER BY sort DESC";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@idx_collection", SqlDbType.Int).Value = idx_collection;
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




        public static DataTable getAppNews(string lang, int idx_news)
        {
            DataTable _table = new DataTable();

            // Create the connection object
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    // Open the connection
                    string sql = @"SELECT news_date, news_image1, news_title, news_desc
                                  FROM vw_news WHERE idx_lang = @lang AND idx_news = @idx_news";

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@idx_news", SqlDbType.Int).Value = idx_news;
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
	}
}
