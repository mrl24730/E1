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
    public partial class investor_announcement : WebBasePage
    {


        public int page = 1;
        public int itemPerPage = 10;
        public int totalResult = 0;
        public bool langAllow = true;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!String.IsNullOrEmpty(Request["page"]))
            {
                Int32.TryParse(Request["page"], out page);
            }

            DataTable dt = DBHelper.getInvestorAnnounce((int)Enum.Parse(typeof(IR_lang), lang), page, itemPerPage);

            if (dt.Rows.Count > 0)
            {
                rptInvestorAnnounce.DataSource = dt;
                rptInvestorAnnounce.DataBind();

                totalResult = (int)dt.Rows[0][0];
            }
        }
    }
}