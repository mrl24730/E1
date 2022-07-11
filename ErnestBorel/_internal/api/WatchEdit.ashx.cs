using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for WatchEdit
    /// </summary>
    public class WatchEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.error;
            output.message = "";

            WatchModel input = new WatchModel();

            #region get POST variable
            try
            {
                input.id = request["idx"];
                input.idx_collection= Convert.ToInt32(request["idx_collection"]);
                input.sort = Convert.ToInt32(request["sort"]);
                input.spec_en = request["spec_en"];
                input.spec_tc = request["spec_tc"];
                input.spec_sc = request["spec_sc"];
                input.spec_fr = request["spec_fr"];
                input.spec_jp = request["spec_jp"];
                input.gender = request["gender"];
                input.matching = request["matching"];
                input.bracelet = request["bracelet"];
                input._case = request["_case"];
                input.shape = request["shape"];
                input.surface1 = request["surface1"];
                input.surface2 = request["surface2"];
                input.surface3 = request["surface3"];
            }
            catch (Exception e)
            {
                output.message = "Input error: " + e.Message;
                Helper.writeOutput(output);
                response.End();
            }
            #endregion
            

            bool isSuccess = DBHelper.editWatch(input);
            if (isSuccess)
            {
                output.status = (int)StatusType.success;
            }
            else
            {
                output.message = "Error during edit";
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