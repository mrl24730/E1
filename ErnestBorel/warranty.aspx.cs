using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ErnestBorel
{
    public partial class warranty : WebBasePage
    {

        public string warranty_country_list = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            warranty_country_list = WarrantyHelper.getCountryCityByLang(lang);


            
            
        }
    }
}