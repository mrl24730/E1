using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for LocationActivate
    /// </summary>
    public class LocationActivate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();
            string updateCountry = "";
            string updateProvince = "";
            string updateCity = "";

            #region Country
            updateCountry = "update [tbl_country] set is_pos_active = 0, is_aftersales_active = 0;";
            DBHelper.ExecuteUpdate(updateCountry);
            updateCountry = "update [tbl_country] set is_pos_active = 1 where idx_country in (select idx_country from tbl_store where is_pos = 1 and is_deleted = 0 group by idx_country);";
            DBHelper.ExecuteUpdate(updateCountry);
            updateCountry = "update [tbl_country] set is_aftersales_active = 1 where idx_country in (select idx_country from tbl_store where is_aftersales = 1 and is_deleted = 0 group by idx_country);";
            DBHelper.ExecuteUpdate(updateCountry);
            #endregion


            #region Province 
            updateProvince = "update [tbl_province] set is_pos_active = 0, is_aftersales_active = 0;";
            DBHelper.ExecuteUpdate(updateProvince);
            updateProvince = "update [tbl_province] set is_pos_active = 1 where idx_province in (select idx_province from tbl_store where is_pos = 1 and is_deleted = 0 group by idx_province);";
            DBHelper.ExecuteUpdate(updateProvince);
            updateProvince = "update [tbl_province] set is_aftersales_active = 1 where idx_province in (select idx_province from tbl_store where is_aftersales = 1 and is_deleted = 0 group by idx_province);";
            DBHelper.ExecuteUpdate(updateProvince);
            #endregion


            #region City 
            updateCity = "update [tbl_city] set is_pos_active = 0, is_aftersales_active = 0;";
            DBHelper.ExecuteUpdate(updateCity);
            updateCity = "update [tbl_city] set is_pos_active = 1 where idx_city in (select idx_city from tbl_store where is_pos = 1 and is_deleted = 0 group by idx_city);";
            DBHelper.ExecuteUpdate(updateCity);
            updateCity = "update [tbl_city] set is_aftersales_active = 1 where idx_city in (select idx_city from tbl_store where is_aftersales = 1 and is_deleted = 0 group by idx_city);";
            DBHelper.ExecuteUpdate(updateCity);
            #endregion

            output.status = 0;
            output.message = "done";
            Helper.writeOutput(output);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}