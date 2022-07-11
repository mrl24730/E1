using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for StoreSearch
    /// </summary>
    public class StoreSearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            BasicOutput output = new BasicOutput();

            string Lang = context.Request["lang"];
            string Region = context.Request["region"];
            string Country = context.Request["country"];
            string City = context.Request["city"];
            string Name = context.Request["name"];
            string Address = context.Request["address"];
            string Tel = context.Request["tel"];
            string Email = context.Request["email"];
            string POS = context.Request["pos"];
            string AfterSales = context.Request["aftersales"];
            string IncludeDeleted = context.Request["include_deleted"];
            string condition = "";

            #region Check Variable
            if (String.IsNullOrEmpty(Region) || String.IsNullOrEmpty(Country) || String.IsNullOrEmpty(City) || String.IsNullOrEmpty(Lang))
            {
                output.status = (int)StatusType.error;
                output.message = "input variable not enough";
                Helper.writeOutput(output);
                context.Response.End();
            }
            #endregion

            if (City != "all")
            {
                condition += " AND idx_city = '" + City + "'";
            }

            if (!String.IsNullOrEmpty(Name))
            {
                condition += " AND shop_name like N'%" + Name + "%'";
            }

            if (!String.IsNullOrEmpty(Address))
            {
                condition += " AND shop_address like N'%" + Address+ "%'";
            }

            if (!String.IsNullOrEmpty(Tel))
            {
                condition += " AND shop_tel like '%" + Tel + "%'";
            }

            if (!String.IsNullOrEmpty(Email))
            {
                condition += " AND shop_email like '%" + Email+ "%'";
            }

            if (IncludeDeleted == "1")
            {
                condition += " ";
            }
            else
            {
                condition += " AND is_deleted = 0 ";
            }

            

            string sql = @"SELECT * From vw_store WHERE idx_lang = '{0}' AND idx_region = '{1}' AND idx_country = '{2}'  
                            AND (is_pos = {3} OR is_aftersales = {4}) {5}" ;
            sql = String.Format(sql, Lang, Region, Country, POS, AfterSales, condition);

            DataTable _table = DBHelper.searchCollection(sql);
            
            output.data = _table;
            output.message = sql;
            output.status = (int)StatusType.success;
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