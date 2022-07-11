using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErnestBorel
{
	public static partial class DBHelper
	{
        //private static readonly string constr = (string)ConfigurationManager.ConnectionStrings["dBProduction"].ConnectionString;
        private static readonly int recPerPage = Int32.Parse(ConfigurationManager.AppSettings["recPerPage"]);

        public static void getIGNonExist(ref List<string> list)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    
                    string idx_photos = String.Join(", ", list.Select(x => "'" + x + "'"));

                    string sql = String.Format(@"SELECT idx_photo FROM tbl_instagram WHERE idx_photo in ({0})",idx_photos);

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@idx_photos", SqlDbType.VarChar, 250).Value = idx_photos;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //remove already existed
                            if (list.Contains((string)reader["idx_photo"])) list.Remove((string)reader["idx_photo"]);
                        }
                    }

                }
            }
        }

        public static void setIGMedia(List<InstagramMediaObj> list)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"INSERT INTO tbl_instagram (idx_photo,idx_user,username,photo_low,photo_std,photo_thumb,photo_create_date,create_date,is_display) VALUES
                        (@idx_photo,@idx_user,@username,@photo_low,@photo_std,@photo_thumb,@photo_create_date,@create_date,@is_display)";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;

                    foreach (InstagramMediaObj obj in list)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@idx_photo", SqlDbType.VarChar, 40).Value = obj.idx_photo;
                        cmd.Parameters.Add("@idx_user", SqlDbType.BigInt).Value = obj.idx_user;
                        cmd.Parameters.Add("@username", SqlDbType.NVarChar, 40).Value  = obj.username;
                        cmd.Parameters.Add("@photo_low", SqlDbType.VarChar, 200).Value = obj.photo_low;
                        cmd.Parameters.Add("@photo_std", SqlDbType.VarChar, 200).Value = obj.photo_std;
                        cmd.Parameters.Add("@photo_thumb", SqlDbType.VarChar, 200).Value = obj.photo_thumb;
                        cmd.Parameters.Add("@photo_create_date", SqlDbType.VarChar,20).Value = obj.photo_create_date;
                        cmd.Parameters.Add("@create_date", SqlDbType.DateTime).Value = DateTime.UtcNow;
                        cmd.Parameters.Add("@is_display", SqlDbType.Bit).Value = 1;

                        cmd.ExecuteNonQuery();

                    }
                }
            }
        }


        public static string getIGMinTagId()
        {
            string min_tag_id = "";
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"SELECT option_val FROM tbl_instagram_options WHERE option_var=@option_var";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@option_var", SqlDbType.VarChar, 50).Value = "min_tag_id";
                    Object obj = cmd.ExecuteScalar();

                    if (obj != null && obj != DBNull.Value)
                    {
                        min_tag_id = (string) obj;
                    }
                }
            }

            return min_tag_id;
        }

        public static void setIGMinTagId(string _min_tag_id)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"IF EXISTS (SELECT option_val FROM tbl_instagram_options WHERE option_var = @option_var)
                                        UPDATE tbl_instagram_options SET option_val = @option_val WHERE option_var = @option_var
                                        ELSE
                                        INSERT INTO tbl_instagram_options (option_var, option_val) VALUES (@option_var, @option_val)"; 

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@option_var", SqlDbType.VarChar, 50).Value = "min_tag_id";
                    cmd.Parameters.Add("@option_val", SqlDbType.VarChar, 50).Value = _min_tag_id;

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public static void setIGDisplay(string idx_photo,bool display = false)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"UPDATE tbl_instagram SET is_display = @is_display WHERE idx_photo = @idx_photo";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@is_display", SqlDbType.Bit).Value = display?1:0;
                    cmd.Parameters.Add("@idx_photo", SqlDbType.VarChar, 40).Value = idx_photo;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable getIGList(bool isAdmin = false, int paging = 0, string idx_photo = "")
        {
            /*
             * SELECT  *
FROM (SELECT  ROW_NUMBER() OVER (ORDER BY create_date DESC)
AS RowNum, CustomerID, CompanyName, ContactName, Country FROM Customers)
AS NewTable
WHERE RowNum >= 11 AND RowNum <= 20
             * */
            string normalField = "";
            string normalCondition = "WHERE is_display = 1 ";

            int stNum = paging * recPerPage + 1;
            int edNum = (paging + 1) * recPerPage;

            if (isAdmin)
            {
                normalField = ",is_display ";
                normalCondition = "";

                if (idx_photo != "")
                {
                    normalCondition = "WHERE idx_photo=@idx_photo ";
                    stNum = 0;
                }
            }

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = String.Format(@"SELECT  * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY photo_create_date DESC) AS idx, idx_photo, photo_low, photo_create_date{0} FROM tbl_instagram {1} )
                                AS NewTable
                                WHERE idx >= {2} AND idx <= {3}", normalField, normalCondition, stNum, edNum);

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;

                    if (idx_photo != "") cmd.Parameters.Add("@idx_photo", SqlDbType.VarChar, 40).Value = idx_photo;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }

            return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }


	}
}