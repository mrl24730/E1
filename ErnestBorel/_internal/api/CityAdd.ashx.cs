using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for CityAdd
    /// </summary>
    public class CityAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.error;
            output.message = "";

            CityModel input = new CityModel();

            #region get POST variable
            try
            {
                input.idx_city = request["idx_city"];
                input.region = request["region"];
                input.province = request["province"];
                input.country = request["country"];
                input.eName = request["eName"];
                input.sName = request["sName"];
                input.tName = request["tName"];
                input.jName = request["jName"];
                input.fName = request["fName"];
                input.lng = Convert.ToDecimal(request["lng"]);
                input.lat = Convert.ToDecimal(request["lat"]);
                input.zoom = Convert.ToInt32(request["zoom"]);
            }
            catch (Exception e)
            {

                output.message = "Input error: " + e.Message;
                Helper.writeOutput(output);
                response.End();
            } 
            #endregion

            /*
            #region check existing model number
            var existWatch = DBHelper.getWatch(input.id);
            if (existWatch != null)
            {
                output.message = "This model number already existing : " + input.id;
                Helper.writeOutput(output);
                response.End();
            }
            #endregion
            */

            bool isSuccess = DBHelper.insertCity(input);
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