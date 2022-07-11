using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ErnestBorel.admin;

namespace ErnestBorel
{
    public partial class investor : WebBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = DBHelper.getInvestorAnnounce((int)Enum.Parse(typeof(IR_lang), lang), 1, 3);

            announceList.DataSource = dt;
            announceList.DataBind();
        }
    }
}