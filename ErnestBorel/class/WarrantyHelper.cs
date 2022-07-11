using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel
{
	public class WarrantyHelper
	{
        public static string getCountryCityByLang(string lang)
        {
            string json = "";

            List<object> countryCityList = new List<object>();

            DBHelper.GetCountryCity(lang, ref countryCityList);

            json = JsonConvert.SerializeObject(countryCityList);

            return json;
        }
	}
}