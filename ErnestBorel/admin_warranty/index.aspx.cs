using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel.admin_warranty
{
    public partial class index : System.Web.UI.Page
    {
        public string errmsg;
        public string qstring;

        protected void Page_Load(object sender, EventArgs e)
        {
            string qsRef = "warranty_search.aspx";

            if (!String.IsNullOrEmpty(Request["redirect"]))
            {
                qsRef = Server.UrlDecode(Request["redirect"]);
            }

            qstring = Request.QueryString.ToString();

            if (Session["logined_warranty_admin"] != null || Session["logined_warranty_checker"] != null)
            {
                Response.Redirect("warranty_search.aspx");
                Response.End();
            }
            else if (Request["frmUsername"] != null || (string)Request["frmPassword"] != null)
            {

                if ((string)Request["frmUsername"] == "ernest" && (string)Request["frmPassword"] == "#1856b0relwarrantY")
                {
                    Session["logined_warranty_admin"] = DateTime.Now;
                    Response.Redirect(qsRef);
                }
                else if ((string)Request["frmUsername"] == "checkwarranty" && (string)Request["frmPassword"] == "#erNestBore1(:")
                {
                    Session["logined_warranty_checker"] = DateTime.Now;
                    Response.Redirect(qsRef);
                }
                else
                {
                    errmsg = "Error: Incorrect username or/and password.";
                    Session.Abandon();
                }
            }
            else
            {
                Session.Abandon();
            }


        }
    }
}