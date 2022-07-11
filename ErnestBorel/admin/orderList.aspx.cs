using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ErnestBorel.admin
{
    public partial class orderList : System.Web.UI.Page
    {
        public string adminMode = ConfigurationManager.AppSettings["APIadminKey"];
        public string SystemTime = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logined"] == null || Session["admin"] == null || (string)Session["admin"] != "disAdmin")
            {
                string link = "orderList.aspx";

                Response.Redirect("index.aspx?redirect=" + Server.UrlEncode(link));
            }

            SystemTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");


        }
    }
}