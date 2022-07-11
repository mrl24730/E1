using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for StoreAdd
    /// </summary>
    public class StoreAdd : IHttpHandler
    {
        public HttpRequest request;
        public HttpResponse response;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            request = context.Request;
            response = context.Response;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.error;
            output.message = "";

            StoreModel input = new StoreModel();
            
            #region get POST variable
            try
            {
                input.isPos = Convert.ToBoolean(request["pos"]);
                input.isAftersales = Convert.ToBoolean(request["aftersales"]);
                input.regionId = request["region"];
                input.countryId = request["country"];
                input.provinceId = request["province"];
                input.cityId = request["city"];
                input.tel = request["tel"];
                input.fax = request["fax"];
                input.email = request["email"];
                input.web = request["web"];
                input.name_en = request["eName"];
                input.name_tc = request["tName"];
                input.name_sc = request["sName"];
                input.name_fr = request["fName"];
                input.name_jp = request["jName"];
                input.address_en = request["eAddress"];
                input.address_tc = request["tAddress"];
                input.address_sc = request["sAddress"];
                input.address_fr = request["fAddress"];
                input.address_jp = request["jAddress"];
                input.lng = Convert.ToDecimal(request["lng"]);
                input.lat = Convert.ToDecimal(request["lat"]);
            }
            catch(Exception e)
            {
                output.message = "Input error: " + e.Message;
                Helper.writeOutput(output);
                response.End();
            }
            #endregion


            bool isSuccess = DBHelper.insertStore(input);
            if (isSuccess)
            {
                output.status = (int)StatusType.success;
                output.data = input.id;
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