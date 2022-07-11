using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Newtonsoft.Json;

namespace ErnestBorel
{
    public partial class search : WebBasePage
    {

        public string output = "[]";

        protected void Page_Load(object sender, EventArgs e)
        {

            searchObj search = new searchObj();
            DataTable _table = new DataTable();
            
            bool haveCriteria = false;
            search.type = Request.QueryString["type"];
            search.gender = Request.Form["gender"];
            search.bracelet = Request.Form["bracelet"];
            search.shape = Request.Form["shape"];
            search.material = Request.Form["material"];
            search.cover = Request.Form["cover"];
            search.keyword = Request.Form["keyword"];

            if (search.type == "keyword" )
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
                //_table.Columns.Add("img", typeof(string));
                output = JsonConvert.SerializeObject(_table);
            }

            
            


        }

        private void checkEmpty(string selection, ref bool haveCriteria)
        {

            if(!String.IsNullOrEmpty(selection)){
                haveCriteria = true;
            }

        }
    }
}