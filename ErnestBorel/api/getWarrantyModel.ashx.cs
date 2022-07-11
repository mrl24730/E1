using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getWarrantyModel
    /// </summary>
    public class getWarrantyModel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            BasicOutput output = new BasicOutput();

            if (!String.IsNullOrEmpty(context.Request["ModelNum"]))
            {
                string ModelNum = context.Request["ModelNum"];

                if (DBHelper.GetWarrantyModel(ModelNum))
                {
                    Dictionary<string,string> kv = new Dictionary<string,string>(){
                        {"Src",null}
                    };
                    if(DBHelper.IsWatchExist(ModelNum))
                    {
                        kv["Src"] = String.Format("../images/watches/{0}_s.png", ModelNum.Replace("-", "_"));
                    }

                    output.data = kv;
                    output.status = 1;

                }
                else
                {
                    //Incorrect Model Num
                    output.message = "NORECORD";

                }

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
