using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for CityEdit
    /// </summary>
    public class CityEdit : IHttpHandler
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
                input.idx_city = (string)request["id"];
                input.eName = (string)request["eName"];
                input.tName = (string)request["tName"];
                input.sName = (string)request["sName"];
                input.jName = (string)request["jName"];
                input.fName = (string)request["fName"];
                input.province = (string)request["idx_province"];
                input.lng = Convert.ToDecimal(request["lng"]);
                input.lat = Convert.ToDecimal(request["lat"]);
                input.zoom = Convert.ToInt32(request["zoom"]);
                input.zoom_baidu = Convert.ToInt32(request["zoom_baidu"]);
            }
            catch (Exception e)
            {
                output.message = "Input error: " + e.Message;
                Helper.writeOutput(output);
                response.End();
            }

            if (String.IsNullOrEmpty(input.idx_city))
            {
                output.message = "No city id";
                Helper.writeOutput(output);
                response.End();
            }
            #endregion

            bool isSuccess = DBHelper.editCity(input);
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