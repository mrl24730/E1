using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel.admin_warranty
{
    public partial class warranty_search : System.Web.UI.Page
    {
        public bool isAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logined_warranty_admin"] == null && Session["logined_warranty_checker"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                isAdmin = Session["logined_warranty_admin"] != null;
            }


        }
    }
}