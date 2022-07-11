using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for CollectionAdd
    /// </summary>
    public class CollectionAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.error;
            output.message = "";

            CollectionModel input = new CollectionModel();

            #region get POST variable
            try
            {
                input.col_ref = request["col_ref"];
                input.type = request["type"];
                input.name_en = request["name_en"];
                input.name_tc = request["name_tc"];
                input.name_sc = request["name_sc"];
                input.name_fr = request["name_fr"];
                input.name_jp = request["name_jp"];
                input.desc_en = request["desc_en"];
                input.desc_tc = request["desc_tc"];
                input.desc_sc = request["desc_sc"];
                input.desc_fr = request["desc_fr"];
                input.desc_jp = request["desc_jp"];

            }
            catch (Exception e)
            {
                output.message = "Input error: " + e.Message;
                Helper.writeOutput(output);
                response.End();
            }
            #endregion


            #region check exist col_ref
            bool isExist = DBHelper.checkColRefExist(input.col_ref);
            if (isExist)
            {
                output.message = "SEO name already used, please give another name.";
                Helper.writeOutput(output);
                response.End();
            }
            #endregion

            bool isSuccess = DBHelper.addCollection(input);
            if (isSuccess)
            {
                output.status = (int)StatusType.success;
            }
            else
            {
                output.message = "Error during insert";
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