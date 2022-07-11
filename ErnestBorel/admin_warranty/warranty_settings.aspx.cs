using LinqToExcel;
using LinqToExcel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel.admin_warranty
{
    public partial class warranty_settings : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logined_warranty_admin"] == null && Session["logined_warranty_checker"] == null)
            {
                Response.Redirect("index.aspx");
            }


        }

    }

    public class CountryCityXlsRow
    {
        public string CountryEN	{get;set;}
        public string CityEN	{get;set;}
        public string CountrySC	{get;set;}
        public string CitySC	{get;set;}
        public string CountryTC	{get;set;}
        public string CityTC	{get;set;}
        public string CountryFR	{get;set;}
        public string CityFR	{get;set;}
        public string CountryJP	{get;set;}
        public string CityJP { get; set; }
        public int NeedInvoice { get; set; }

    }

    public class CaseNumXlsRow
    {
        public string CaseNum { get; set; }
        public string CaseModel { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class ModelNumXlsRow
    {
        public string ModelNum { get; set; }
        public string CaseModel { get; set; }
    }

    public class WarrantyNumXlsRow
    {
        public string WarrantyNum { get; set; }
        public string CreateDate { get; set; }
    }
}