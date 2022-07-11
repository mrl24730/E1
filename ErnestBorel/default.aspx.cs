using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using Newtonsoft.Json;

namespace ErnestBorel
{
    public partial class _default : WebBasePage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            DataTable _table = new DataTable();
            DBHelper.getNewsListing(3, 1, lang, out _table);
            string subfix = "_tl.jpg";
            _table.Columns.Add("img", typeof(string));
            
            foreach (DataRow r in _table.Rows)
            {
                r["img"] = ((string)r["news_ref"]) + subfix;
            }

            newsList.DataSource = _table;
            newsList.DataBind();

            string host = Request.Url.Host.ToLower();
            
            if (domain_cn.Contains(host) && lang != "sc")
            {
                Response.Redirect("http://" + host);
            }
            else if (domain_ch.Contains(host) && lang == "sc")
            {
                Response.Redirect("http://" + host + "/en");
            }
        }
    }
}