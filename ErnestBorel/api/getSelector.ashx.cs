using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getSelector
    /// </summary>
    public class getSelector : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();

            string Lang = (String.IsNullOrEmpty(context.Request["lang"]))? "en": (string)context.Request["lang"];
            DataTable _table = DBHelper.getWatchSelector(Lang);

            Dictionary<string, List<selectorObj>> model = new Dictionary<string, List<selectorObj>>();
            string currentType = "";
            List<selectorObj> list = new List<selectorObj>();

            foreach (DataRow r in _table.Rows)
            {
                string type = (string)r["idx_type"];
                if(currentType != type)
                {
                    if (list.Count > 0) { 
                        model.Add(currentType, list);
                        list = new List<selectorObj>();
                    }
                    currentType = type;
                }
                list.Add(new selectorObj() { id = (string)r["idx_selector"], name = (string)r["selector_name"] });
            }

            model.Add(currentType, list);

            output.data = model;
            output.status = (int)StatusType.success;
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