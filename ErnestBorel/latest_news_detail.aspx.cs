using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;

namespace ErnestBorel
{
    public partial class latest_news_detail : WebBasePage
    {

        public articleObj obj = new articleObj();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(Request["article"]))
            {
                obj.news_ref = Request["article"];
                DBHelper.getNewsDetail(lang, ref obj);
                obj.displayDate = obj.date.ToString("yyyy-MM-dd");
                obj.captionString = JsonConvert.SerializeObject(obj.imageCaption);
                
                DBHelper.getRelatedNews(lang, ref obj, false);
                RelatedNewList.DataSource = obj.relatedNews;
                RelatedNewList.DataBind();

                imageList.DataSource = obj.imageURL;
                imageList.DataBind();

                DBHelper.getNextNews(lang, ref obj);

            }
            else
            {
                Response.Redirect("404.html");
            }
        }
    }
}