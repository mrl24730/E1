using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel._internal.api
{
    /// <summary>
    /// Summary description for StoreEdit
    /// </summary>
    public class StoreEdit : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            BasicOutput output = new BasicOutput();
            output.status = (int)StatusType.error;
            output.message = "";

            StoreModel input = new StoreModel();

            #region get POST variable
            try
            {
                input.id= Convert.ToInt32(request["id"]);
                input.isPos = Convert.ToBoolean(request["pos"]);
                input.isAftersales = Convert.ToBoolean(request["aftersales"]);
                input.regionId = request["region"];
                input.countryId = request["country"];
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
            catch (Exception e)
            {
                output.message = "Input error: " + e.Message;
                Helper.writeOutput(output);
                response.End();
            }
            #endregion

            bool isSuccess = DBHelper.editStore(input);
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