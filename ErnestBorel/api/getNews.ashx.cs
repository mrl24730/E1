using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getNews
    /// </summary>
    public class getNews : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;
            string id = context.Request["id"];
            id = (String.IsNullOrEmpty(id)) ? "" : id;
            int idx_news = 0;
            Int32.TryParse(id, out idx_news);

            string domain_cn = Global.domain_cn;
            string imgPath = domain_cn + "/images/latest_news/{0}_h.jpg";


            Dictionary<string, string> source = new Dictionary<string, string>();
            source.Add("en", "Source: Swiss Ernest Borel offical website");
            source.Add("tc", "來源：瑞士依波路官網");
            source.Add("sc", "来源：瑞士依波路官網");

            Dictionary<string, string> author = new Dictionary<string, string>();
            author.Add("en", "Author: Swiss Ernest Borel");
            author.Add("tc", "作者：瑞士依波路");
            author.Add("sc", "作者：瑞士依波路");

            Dictionary<string, string> published = new Dictionary<string, string>();
            published.Add("en", "Article published time:");
            published.Add("tc", "文章發布時間：");
            published.Add("sc", "文章發布时间：");


            appNews output = new appNews();
            output.source = source[lang];
            output.author = author[lang];

            DataTable _table = DBHelper.getAppNews(lang, idx_news);
            foreach (DataRow r in _table.Rows)
            {
                output.date = published[lang] +  ((DateTime)r["news_date"]).ToString("yyyy-MM-dd");
                output.title = ((string)r["news_title"]).Replace("<br>", "\n").Replace("<br/>", "\n");
                output.desc = ((string)r["news_desc"]).Replace("<br>", "\n").Replace("<br/>", "\n");
                output.img = String.Format(imgPath, (string)r["news_image1"]);
            }

            
            Helper.writeOutput(output);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}