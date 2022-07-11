using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel._internal
{
    public partial class RestartApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.HttpRuntime.UnloadAppDomain();
        }
    }
}