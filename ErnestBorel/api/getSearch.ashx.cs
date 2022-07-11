using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using Newtonsoft.Json;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getSearch
    /// </summary>
    public class getSearch : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            searchObj search = new searchObj();
            DataTable _table = new DataTable();
            string output = "[]";
            bool haveCriteria = false;
            string lang = Request.Form["lang"];
            search.type = Request.Form["type"];
            search.gender = Request.Form["gender"];
            search.bracelet = Request.Form["bracelet"];
            search.shape = Request.Form["shape"];
            search.material = Request.Form["material"];
            search.cover = Request.Form["cover"];
            search.keyword = Request.Form["keyword"];

            if (search.type == "keyword")
            {
                checkEmpty(search.keyword, ref haveCriteria);
            }
            else
            {
                checkEmpty(search.gender, ref haveCriteria);
                checkEmpty(search.bracelet, ref haveCriteria);
                checkEmpty(search.shape, ref haveCriteria);
                checkEmpty(search.material, ref haveCriteria);
                checkEmpty(search.cover, ref haveCriteria);
            }

            if (haveCriteria)
            {
                DBHelper.getWatchBySearch(lang, search, out _table);
                _table.Columns.Add("img", typeof(string));
                output = JsonConvert.SerializeObject(_table);
            }


            Response.Write(output);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void checkEmpty(string selection, ref bool haveCriteria)
        {

            if (!String.IsNullOrEmpty(selection))
            {
                haveCriteria = true;
            }

        }
    }
}