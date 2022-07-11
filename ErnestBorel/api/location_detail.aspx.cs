using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel.api
{
    public partial class location_detail : System.Web.UI.Page
    {
        public string domain = Global.domain_cn;
        public decimal lat = 0.0m;
        public decimal lng = 0.0m;
        public string shopName = "";
        public string shopAddress = "";
        public string shopContact = "";
        public string title = "Details";

        protected void Page_Load(object sender, EventArgs e)
        {
            string lang = Request["language"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;

            switch (lang)
            {
                case "sc":
                    title = "详情";
                    break;

                case "tc":
                    title = "詳情";
                    break;

                case "en":
                default:
                    title = "詳情";
                    break;
            }
            if(!String.IsNullOrEmpty(Request["id"]))
            {
                string[] ids = Request["id"].Split('-');

                if (ids.Count() > 1)
                {

                    string idx_city = ids[0];

                    int idx_shop = 0;

                    Int32.TryParse(ids[1].Replace("s", ""), out idx_shop);

                    if (idx_city != "" && idx_shop != 0)
                    {
                        DBHelper.getStoreDetail(lang, idx_city, idx_shop, ref lat, ref lng, ref shopName, ref shopAddress, ref shopContact);
                    }
                }
                
                

            }
        }
    }
}