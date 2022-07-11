using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getCollection
    /// </summary>
    public class getCollection : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string lang = context.Request["lang"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;
            string id = context.Request["id"];
            id = (String.IsNullOrEmpty(id)) ? "" : id;
            string category = context.Request["category"];
            category = (String.IsNullOrEmpty(category)) ? "automatic" : category;
            int idx_collection = 0;
            Int32.TryParse(id, out idx_collection);

            string domain_cn = Global.domain_cn;
            string imgPath_s = domain_cn + "/api/image.ashx?w=700&h=700&file=watches/{0}_l.png&name={0}_s3.png";
            string imgPath_l = domain_cn + "/images/watches/{0}_l.png?name={0}_l3.png";
            
            appWatchItem w;
            appWatchOutput output = new appWatchOutput();
            output.watch = new List<appWatchItem>();

            DataTable _table = DBHelper.getAppCollection(lang, idx_collection);
            
            foreach (DataRow r in _table.Rows)
            {
                string oldmodel = "";

                if(r["watch_oldmodel"] != null && !String.IsNullOrEmpty((string)r["watch_oldmodel"]))
                {
                    oldmodel = " (" + (string)r["watch_oldmodel"] + ")";
                }
                w = new appWatchItem();
                w.model = (string)r["idx_watch"];
                string filename = (w.model).Replace("-", "_");
                w.img = String.Format(imgPath_s, filename);
                w.large = String.Format(imgPath_l, filename);
                w.spec = ((string)r["watch_spec"]).Split('\n').ToList();
                w.model = (string)r["idx_watch"] + oldmodel;
                output.watch.Add(w);
            }

            output.total = _table.Rows.Count;
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
