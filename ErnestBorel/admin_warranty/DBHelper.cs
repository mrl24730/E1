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
using ErnestBorel.admin_warranty;

namespace ErnestBorel
{
    public static partial class DBHelper
    {
        public static int add_CountryCity(List<CountryCityXlsRow> list)
        {
            int numofRecordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql_add_CountryCity = @"INSERT INTO tbl_warranty_countryCity (CountryEN,CityEN,CountrySC,CitySC,CountryTC,CityTC,CountryFR,CityFR,CountryJP,CityJP,NeedInvoice,CreateDate)
                                                    VALUES ";

                    cmd.Connection = conn;
                    conn.Open();

                    int total = list.Count();
                    int bulk = 100;

                    for (int i = 0; i < Math.Ceiling((decimal)total / bulk); i++)
                    {
                        cmd.Parameters.Clear();
                        string bulk_sql = sql_add_CountryCity;

                        for (int j = i * bulk; j < (i * bulk) + bulk; j++)
                        {
                            if (j < total)
                            {
                                var item = list[j];
                                bulk_sql += String.Format("(@CountryEN{0},@CityEN{0},@CountrySC{0},@CitySC{0},@CountryTC{0},@CityTC{0},@CountryFR{0},@CityFR{0},@CountryJP{0},@CityJP{0},@NeedInvoice{0},@CreateDate{0}),", j);


                                cmd.Parameters.Add("@CountryEN" + j, SqlDbType.NVarChar, 80).Value = item.CountryEN;
                                cmd.Parameters.Add("@CityEN" + j, SqlDbType.NVarChar, 80).Value = item.CityEN;
                                cmd.Parameters.Add("@CountrySC" + j, SqlDbType.NVarChar, 80).Value = item.CountrySC;
                                cmd.Parameters.Add("@CitySC" + j, SqlDbType.NVarChar, 80).Value = item.CitySC;
                                cmd.Parameters.Add("@CountryTC" + j, SqlDbType.NVarChar, 80).Value = item.CountryTC;
                                cmd.Parameters.Add("@CityTC" + j, SqlDbType.NVarChar, 80).Value = item.CityTC;
                                cmd.Parameters.Add("@CountryFR" + j, SqlDbType.NVarChar, 80).Value = item.CountryFR;
                                cmd.Parameters.Add("@CityFR" + j, SqlDbType.NVarChar, 80).Value = item.CityFR;
                                cmd.Parameters.Add("@CountryJP" + j, SqlDbType.NVarChar, 80).Value = item.CountryJP;
                                cmd.Parameters.Add("@CityJP" + j, SqlDbType.NVarChar, 80).Value = item.CityJP;
                                cmd.Parameters.Add("@NeedInvoice" + j, SqlDbType.Int).Value = item.NeedInvoice;
                                cmd.Parameters.Add("@CreateDate" + j, SqlDbType.DateTime).Value = DateTime.UtcNow;


                            }
                        }
                        //Remove Last comma
                        bulk_sql = bulk_sql.Substring(0, bulk_sql.Length - 1);
                        cmd.CommandText = bulk_sql;
                        UploadWatcher.current = numofRecordsAffected += cmd.ExecuteNonQuery();

                    }
                }

            }

            return numofRecordsAffected;

        }


        public static int add_CaseNum(List<CaseNumXlsRow> list)
        {
            int numofRecordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql_add_CaseNum = @"INSERT INTO tbl_warranty_case (CaseNum,CaseModel,CreateDate)
                                                    VALUES ";

                    cmd.Connection = conn;
                    conn.Open();

                    int total = list.Count();
                    int bulk = 100;
                    DateTime now = DateTime.UtcNow.AddHours(8);

                    for (int i = 0; i < Math.Ceiling((decimal)total / bulk); i++)
                    {
                        cmd.Parameters.Clear();
                        string bulk_sql = sql_add_CaseNum;
                        

                        for (int j = i * bulk; j < (i * bulk) + bulk; j++)
                        {
                            if (j < total)
                            {
                                var item = list[j];
                                now = DateTime.UtcNow.AddHours(8);
                                bulk_sql += String.Format("(@CaseNum{0},@CaseModel{0},@CreateDate{0}),", j);

                                cmd.Parameters.Add("@CaseNum" + j, SqlDbType.VarChar, 80).Value = item.CaseNum;
                                cmd.Parameters.Add("@CaseModel" + j, SqlDbType.VarChar, 80).Value = item.CaseModel;
                                cmd.Parameters.Add("@CreateDate" + j, SqlDbType.DateTime).Value = now;

                            }
                        }
                        //Remove Last comma
                        bulk_sql = bulk_sql.Substring(0, bulk_sql.Length - 1);
                        cmd.CommandText = bulk_sql;
                        UploadWatcher.current = numofRecordsAffected += cmd.ExecuteNonQuery();

                    }

                }

            }

            return numofRecordsAffected;

        }

        public static int add_ModelNum(List<ModelNumXlsRow> list)
        {
            int numofRecordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql_add_ModelNum = @"INSERT INTO tbl_warranty_model (ModelNum,CaseModel)
                                                    VALUES ";


                    cmd.Connection = conn;
                    conn.Open();

                    int total = list.Count();
                    int bulk = 100;

                    for (int i = 0; i < Math.Ceiling((decimal)total / bulk); i++)
                    {
                        cmd.Parameters.Clear();
                        string bulk_sql = sql_add_ModelNum;

                        for (int j = i * bulk; j < (i * bulk) + bulk; j++)
                        {
                            if (j < total)
                            {
                                var item = list[j];
                                bulk_sql += String.Format("(@ModelNum{0},@CaseModel{0}),", j);
                                cmd.Parameters.Add("@ModelNum" + j, SqlDbType.VarChar, 80).Value = item.ModelNum;
                                cmd.Parameters.Add("@CaseModel" + j, SqlDbType.VarChar, 80).Value = (String.IsNullOrEmpty(item.CaseModel)) ? "" : item.CaseModel;
                            }
                        }
                        //Remove Last comma
                        bulk_sql = bulk_sql.Substring(0, bulk_sql.Length - 1);
                        cmd.CommandText = bulk_sql;
                        UploadWatcher.current = numofRecordsAffected += cmd.ExecuteNonQuery();

                    }
                }

            }

            return numofRecordsAffected;

        }

        public static int add_WarrantyNum(List<WarrantyNumXlsRow> list)
        {
            int numofRecordsAffected = 0;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql_add_WarrantyNum = @"INSERT INTO tbl_warranty_card (WarrantyNum,CreateDate)
                                                    VALUES ";
                    cmd.Connection = conn;
                    conn.Open();
                    

                    int total = list.Count();
                    int bulk = 100;

                    for (int i = 0; i < Math.Ceiling((decimal)total / bulk); i++)
                    {
                        cmd.Parameters.Clear();
                        string bulk_sql = sql_add_WarrantyNum;

                        for (int j = i * bulk; j < (i * bulk) + bulk; j++)
                        {
                            if (j < total)
                            {
                                var item = list[j];
                                bulk_sql += String.Format("(@WarrantyNum{0},@CreateDate{0}),", j);
                                cmd.Parameters.Add("@WarrantyNum"+j, SqlDbType.VarChar, 50).Value = item.WarrantyNum;
                                cmd.Parameters.Add("@CreateDate"+j, SqlDbType.DateTime).Value = item.CreateDate;

                            }
                        }
                        //Remove Last comma
                        bulk_sql = bulk_sql.Substring(0, bulk_sql.Length - 1);
                        cmd.CommandText = bulk_sql;
                        UploadWatcher.current = numofRecordsAffected += cmd.ExecuteNonQuery();
                    }

                    /*foreach (var item in list)
                    {
                        
                        cmd.Parameters.Add("@WarrantyNum", SqlDbType.VarChar, 50).Value = item.WarrantyNum;
                        cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = item.CreateDate;

                       
                    }*/

                }

            }

            return numofRecordsAffected;

        }

        public static List<WarrantyRecord> searchWarranty(WarrantyRecord searchWarrantyRecord)
        {
            List<WarrantyRecord> list = new List<WarrantyRecord>();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"SELECT Guid,ModelNum,CaseNum,WarrantyNum,Title,Name,Phone,Email,Dop,InvNum,Country,City,CreateDate FROM tbl_warranty_registration WHERE 1=1 ";

                    if (searchWarrantyRecord.WarrantyNum != null)
                    {
                        sql += "AND WarrantyNum like '%' + @WarrantyNum + '%'";
                        cmd.Parameters.Add("@WarrantyNum", SqlDbType.NVarChar, 50).Value = searchWarrantyRecord.WarrantyNum;
                    }

                    if (searchWarrantyRecord.CaseNum != null)
                    {
                        sql += "AND CaseNum like '%' + @CaseNum + '%'";
                        cmd.Parameters.Add("@CaseNum", SqlDbType.NVarChar, 50).Value = searchWarrantyRecord.CaseNum;
                    }

                    if (searchWarrantyRecord.Phone != null)
                    {
                        sql += "AND Phone like '%' + @Phone + '%'";
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 200).Value = searchWarrantyRecord.Phone;
                    }
                    


                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;

                    

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WarrantyRecord warrantyRecord = new WarrantyRecord();

                            warrantyRecord.Guid = (string)reader["Guid"];
                            warrantyRecord.ModelNum = (string)reader["ModelNum"];
                            warrantyRecord.CaseNum = Convert.IsDBNull(reader["CaseNum"]) ? null : (string)reader["CaseNum"];
                            warrantyRecord.WarrantyNum = (string)reader["WarrantyNum"];
                            warrantyRecord.Title = (string)reader["Title"];
                            warrantyRecord.Name = (string)reader["Name"];
                            warrantyRecord.Phone = (string)reader["Phone"];
                            warrantyRecord.Email = Convert.IsDBNull(reader["Email"]) ? null : (string)reader["Email"];
                            warrantyRecord.Dop = (DateTime)reader["Dop"];
                            warrantyRecord.InvNum = (string)reader["InvNum"];
                            warrantyRecord.Country = (string)reader["Country"];
                            warrantyRecord.City = (string)reader["City"];
                            warrantyRecord.ExtendedDate = warrantyRecord.Dop.AddYears(3).AddDays(-1);
                            warrantyRecord.CreateDate = (DateTime)reader["CreateDate"];

                            list.Add(warrantyRecord);
                            
                        }
                    }


                }

            }

            return list;
        }

        public static DataTable searchWarranty(DateTime From, DateTime To)
        {
            List<WarrantyRecord> list = new List<WarrantyRecord>();
            DataTable dt = new DataTable("SearchResult");
            DataSet ds = new DataSet("Sheet1");

            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"SELECT idx_warranty, Guid, ModelNum, CaseNum, WarrantyNum, Title, Name, Phone, Email, Dop, InvNum, Country, City, IsSubscribed, CreateDate , SmsResult
                                    FROM tbl_warranty_registration WHERE CreateDate >= @From AND CreateDate <= @To";


                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                    cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
                    cmd.Connection = conn;
                    conn.Open();


                    dt.Columns.Add("Idx", typeof(long));
                    dt.Columns.Add("ModelNum",typeof(String));
                    dt.Columns.Add("CaseNum", typeof(String));
                    dt.Columns.Add("WarrantyNum", typeof(String));
                    dt.Columns.Add("Title", typeof(String));
                    dt.Columns.Add("Name", typeof(String));
                    dt.Columns.Add("Phone", typeof(String));
                    dt.Columns.Add("Email", typeof(String));
                    dt.Columns.Add("PurchaseDate", typeof(String));
                    dt.Columns.Add("InvNum", typeof(String));
                    dt.Columns.Add("Country", typeof(String));
                    dt.Columns.Add("City", typeof(String));
                    dt.Columns.Add("ExtendedDate", typeof(String));
                    dt.Columns.Add("Subscribe", typeof(String));
                    dt.Columns.Add("CreateDate", typeof(String));
                    dt.Columns.Add("SmsResult", typeof(String));
                    

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader["Name"];
                            name = CryptoHelper.decryptAES(name, defaultSKey);
                            string email = "";
                            if(reader["Email"] != null && reader["Email"] != DBNull.Value && (string)reader["Email"] != "")
                            {
                                email = (string)reader["Email"];
                                email = CryptoHelper.decryptAES(email, defaultSKey);
                            }
                            DateTime PurchaseDate = (DateTime)reader["Dop"];
                            DateTime ExtendedDate = PurchaseDate.AddYears(3).AddDays(-1);
                            
                            //WarrantyRecord warrantyRecord = new WarrantyRecord();
                            DataRow dr = dt.NewRow();
                            dr[0] = (long)reader["idx_warranty"];
                            dr[1] = (string)reader["ModelNum"];
                            dr[2] = Convert.IsDBNull(reader["CaseNum"]) ? "" : (string)reader["CaseNum"];
                            dr[3] = "'" + (string)reader["WarrantyNum"];
                            dr[4] = (string)reader["Title"];
                            dr[5] = name;
                            dr[6] = (string)reader["Phone"];
                            dr[7] = email;
                            dr[8] = PurchaseDate.ToString("yyyy-MM-dd");
                            dr[9] = (string)reader["InvNum"];
                            dr[10] = (string)reader["Country"];
                            dr[11] = (string)reader["City"];
                            dr[12] = ExtendedDate.ToString("yyyy-MM-dd");
                            dr[13] = ((bool)reader["IsSubscribed"]) ? "Yes" : "No";
                            dr[14] = ((DateTime)reader["CreateDate"]).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
                            dr[15] = (string)reader["SmsResult"];

                            dt.Rows.Add(dr);
                        }
                        ds.Tables.Add(dt);

                    }
                }

            }

            return dt;
        }



        public static int updateWarranty(string sql)
        {
            int rowAffected = 0;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                // Create the command object and set its properties
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    conn.Open();
                    rowAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowAffected;
        }

    } // end partial class

    public class WarrantyRecord : WarrantyRegistration
    {
        public string Guid { get; set; }
        public DateTime CreateDate { get; set; }
    }
} //end namesapce