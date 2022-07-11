using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel
{
    public static partial class DBHelper
	{
        public static string warrantySecret = "eBWarranTy;20l5";

        public static bool GetWarrantyModel(string ModelNum)
        {
            bool isValid = false;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"SELECT ModelNum FROM tbl_warranty_model WHERE ModelNum=@ModelNum";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.Add("@ModelNum", SqlDbType.VarChar, 80).Value = ModelNum;

                    isValid = cmd.ExecuteScalar() != null;
                }

            }

            return isValid;
        }

        public static bool GetWarrantyCase(string CaseNum)
        {
            bool isValid = false;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"SELECT CaseNum FROM tbl_warranty_case WHERE CaseNum=@CaseNum";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.Add("@CaseNum", SqlDbType.VarChar, 80).Value = CaseNum;

                    isValid = cmd.ExecuteScalar() != null;
                }

            }

            return isValid;
        }

        public static bool GetWarrantyCard(string WarrantyNum)
        {
            bool isValid = false;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"SELECT WarrantyNum FROM tbl_warranty_card WHERE WarrantyNum=@WarrantyNum";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.Add("@WarrantyNum", SqlDbType.VarChar, 80).Value = WarrantyNum;

                    isValid = cmd.ExecuteScalar() != null;
                }

            }

            return isValid;
        }

        public static string GetMovementByModel(string ModelNum)
        {
            string movement = null;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"SELECT coll.col_movement
                        FROM tbl_watch watch LEFT JOIN tbl_collection coll ON (watch.idx_collection = coll.idx_collection) WHERE watch.idx_watch = @ModelNum";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;

                    cmd.Parameters.Add("@ModelNum", SqlDbType.VarChar, 20).Value = ModelNum;

                    object obj = cmd.ExecuteScalar();
                    movement = (obj == null) ? null : (string)obj;                    
                }

            }

            return movement;

        }

        public static bool IsWatchExist(string ModelNum)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
              using (SqlCommand cmd = new SqlCommand())
              {
                string sql = @"SELECT idx_watch
                                    FROM tbl_watch WHERE idx_watch = @ModelNum";

                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = sql;

                cmd.Parameters.Add("@ModelNum", SqlDbType.VarChar, 20).Value = ModelNum;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                  return reader.HasRows;
                }

              }

              return false;
            }
        }

        public static List<string> CheckRegistration(WarrantyRegistration warrantyReg)
        {
            List<string> Name = new List<string>();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    string sql = @"SELECT Name FROM tbl_warranty_registration WHERE 
                                    ModelNum = @ModelNum and WarrantyNum = @WarrantyNum and InvNum = @InvNum and 
                                    Phone = @Phone and Dop = @Dop and Country = @Country and City = @City";

                    if(String.IsNullOrEmpty(warrantyReg.CaseNum))
                    {
                        sql += " and CaseNum is null ";
                    }
                    else
                    {
                        sql += " and CaseNum = @CaseNum ";
                    }


                    if (String.IsNullOrEmpty(warrantyReg.Email))
                    {
                        sql += " and Email is null ";
                    }
                    else
                    {
                        sql += " and Email = @Email  ";
                    }

                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@ModelNum", SqlDbType.NVarChar, 50).Value = warrantyReg.ModelNum;
                    cmd.Parameters.Add("@CaseNum", SqlDbType.NVarChar, 50).Value = (object)warrantyReg.CaseNum ?? DBNull.Value;
                    cmd.Parameters.Add("@WarrantyNum", SqlDbType.NVarChar, 50).Value = warrantyReg.WarrantyNum;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 500).Value = warrantyReg.Name;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 200).Value = warrantyReg.Phone;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 500).Value = (object)warrantyReg.Email ?? DBNull.Value;
                    cmd.Parameters.Add("@Dop", SqlDbType.DateTime).Value = warrantyReg.Dop;
                    cmd.Parameters.Add("@InvNum", SqlDbType.VarChar, 50).Value = (object)warrantyReg.InvNum ?? "";
                    cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 80).Value = warrantyReg.Country;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar, 80).Value = warrantyReg.City;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Name.Add((string)reader["Name"]);
                            }
                        }
                    }
                    
                }
            }

            return Name;
        }


        public static void AddRegistration(WarrantyRegistration warrantyReg)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = @"INSERT INTO tbl_warranty_registration
                                    (Guid, ModelNum, CaseNum, WarrantyNum, Name, Title, Phone, Email, Dop, InvNum, Country, City, IsSubscribed, CreateDate, SmsResult )
                                        VALUES
                                    (@Guid, @ModelNum, @CaseNum, @WarrantyNum, @Name, @Title, @Phone, @Email, @Dop, @InvNum, @Country, @City, @IsSubscribed, @CreateDate, @SmsResult )";
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@Guid", SqlDbType.Char, 36).Value = warrantyReg.Guid;
                    cmd.Parameters.Add("@ModelNum", SqlDbType.NVarChar, 50).Value = warrantyReg.ModelNum;
                    cmd.Parameters.Add("@CaseNum", SqlDbType.NVarChar, 50).Value = (object) warrantyReg.CaseNum ?? DBNull.Value;
                    cmd.Parameters.Add("@WarrantyNum", SqlDbType.NVarChar, 50).Value = warrantyReg.WarrantyNum;

                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 500).Value = warrantyReg.Name;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = warrantyReg.Title;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 200).Value = warrantyReg.Phone;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 500).Value = (object) warrantyReg.Email ?? DBNull.Value;
                    cmd.Parameters.Add("@Dop", SqlDbType.DateTime).Value = warrantyReg.Dop;
                    cmd.Parameters.Add("@InvNum", SqlDbType.VarChar, 50).Value = (object)warrantyReg.InvNum ?? "";
                    cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 80).Value = warrantyReg.Country;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar, 80).Value = warrantyReg.City;
                    cmd.Parameters.Add("@IsSubscribed", SqlDbType.Bit).Value = warrantyReg.IsSubscribed;
                    cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = DateTime.UtcNow;
                    cmd.Parameters.Add("@SmsResult", SqlDbType.NVarChar, 500).Value = warrantyReg.SmsResult;
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public static void GetCountryCity(string lang, ref List<object> list)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = String.Format(@"SELECT Country{0} as Country,City{0} as City, CountryEN as CountryVal, CityEN as CityVal, NeedInvoice FROM tbl_warranty_countryCity",lang.ToUpper());
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = sql;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new
                            {
                                City = (string)reader["City"],
                                Country = (string) reader["Country"],
                                CityVal = (string)reader["CityVal"],
                                CountryVal = (string)reader["CountryVal"],
                                NeedInvoice = (bool)reader["NeedInvoice"]
                            });
                            
                        }

                    }


                }

            }

        }
	}
    
    public class WarrantyRegistration
    {
        public string Guid { get; set; }

        public string ModelNum { get; set; }
        public string CaseNum { get; set; }
        public string WarrantyNum { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Dop { get; set; }
        public string InvNum { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTime ExtendedDate { get; set; }
        public string SmsResult { get; set; }

    }
}
