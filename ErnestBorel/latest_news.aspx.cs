using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using Kitchen;

namespace ErnestBorel
{
    public partial class latest_news : WebBasePage
    {
        
        public string metatitle = "";
        public string metadesc = "";
        public string content = "";
        public int pageNow = 1, newsTotal = 0, pageItem = 6;

        protected void Page_Load(object sender, EventArgs e)
        {

            DataTable _table;

            if (!String.IsNullOrEmpty(Request["page"]))
            {
                string p = Request["page"];
                p = p.Replace("/", "");
                Int32.TryParse(Request["page"], out pageNow);
            }

            newsTotal = DBHelper.getNewsListing(pageItem, pageNow, lang, out _table);

            //pageTotal = (int)Math.Ceiling(newsTotal / pageItem * 1.0);

            /* News highlight */
            //bool isHero = true;
            //string hero = "hero";
            string subfix = "_tl.jpg";

            //_table.Columns.Add("hero", typeof(string));
            _table.Columns.Add("img", typeof(string));
            _table.Columns.Add("date", typeof(string));

            foreach (DataRow r in _table.Rows)
            {
                
                //r["hero"] = hero;
                r["img"] = ((string)r["news_ref"]) + subfix;
                r["date"] = ((DateTime)r["news_date"]).ToString("yyyy-MM-dd");

                //isHero = false;

            }//end foreach

            newsList.DataSource = _table;
            newsList.DataBind();


        }
            
    }
}