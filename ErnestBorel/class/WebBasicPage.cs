using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel
{

    public class WebBasePage : System.Web.UI.Page
    {
        public string lady = "";
        public string couple = "";
        public string casual = "";
        public string lang = "";
        public static string domain_ch = Global.domain_ch;
        public static string domain_cn = Global.domain_cn;

        protected override void OnInit(EventArgs e)
        {

            lang = Helper.getLang();
            lady = Global.lady[lang];
            couple = Global.couple[lang];
            casual = Global.casual[lang];

            base.OnInit(e);

        }
    }
}