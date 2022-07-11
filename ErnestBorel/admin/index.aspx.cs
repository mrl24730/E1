using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel.admin
{
    public partial class index : System.Web.UI.Page
    {
        public string errmsg;
        public string qstring;

        protected void Page_Load(object sender, EventArgs e)
        {
            string qsUsername = "", qsPassword = "", qsRefIR= "IR_pressList.aspx", qsRefDis = "orderList.aspx";

            string irAdminPW = "b0rel#1856";
            string disAdminPW = "bore1@8102";

            if (!String.IsNullOrEmpty(Request["redirect"]))
            {
                qsRefIR = Server.UrlDecode(Request["redirect"]);
            }

            qstring = Request.QueryString.ToString();

            try
            {
                if (Session["logined"] != null && (string)Session["admin"] == "irAdmin")
                {
                    Response.Redirect(qsRefIR);
                    Response.End();
                }

                if (Session["logined"] != null && (string)Session["admin"] == "disAdmin")
                {
                    Response.Redirect(qsRefDis);
                    Response.End();
                }
            }
            catch{ }

            if (Request["frmUsername"] != null || (string)Request["frmPassword"] != null)
            {

                if ((string)Request["frmUsername"] == "ernest" && (string)Request["frmPassword"] == irAdminPW)
                {
                    Session["logined"] = DateTime.Now;
                    Session["admin"] = "irAdmin";
                    Response.Redirect(qsRefIR);
                }
                if ((string)Request["frmUsername"] == "ernest" && (string)Request["frmPassword"] == disAdminPW)
                {
                    Session["logined"] = DateTime.Now;
                    Session["admin"] = "disAdmin";
                    Response.Redirect(qsRefDis);
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
