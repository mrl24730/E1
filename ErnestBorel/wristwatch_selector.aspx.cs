using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel
{
    public partial class wristwatch_selector : WebBasePage
    {

        public string keyword = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                keyword = Request.Form["keyword"];
            }
            catch { }
        }
    }
}