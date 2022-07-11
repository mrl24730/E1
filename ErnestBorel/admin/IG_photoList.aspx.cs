using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ErnestBorel.admin
{
    public partial class IG_photoList : System.Web.UI.Page
    {
        public string adminMode = ConfigurationManager.AppSettings["APIadminKey"];
        public string SystemTime = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logined"] == null)
            {
                string link = "IG_photoList.aspx";
                string idx_photo = String.IsNullOrEmpty(Request["idx_photo"]) ? "" : Request["idx_photo"];
                if (idx_photo != "")
                {
                    link += "?idx_photo=" + idx_photo;
                }

                Response.Redirect("index.aspx?redirect=" + Server.UrlEncode(link));
            }

            SystemTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");


        }
    }
}