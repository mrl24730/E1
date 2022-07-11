using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

namespace ErnestBorel.api
{
    public partial class getWatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string lang = Request["lang"];
            lang = String.IsNullOrEmpty(lang) ? "en" : lang;
            DataTable _table = new DataTable();

            DBHelper.getWatchByLang(lang, out _table);
            foreach (DataRow r in _table.Rows)
            {
                r["watch_spec"] = ((string)r["watch_spec"]).Replace("\r\n", "[br]");
            }

            repeatSpec.DataSource = _table;
            repeatSpec.DataBind();
        }
    }
}