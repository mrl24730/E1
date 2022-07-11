using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErnestBorel.api
{
    /// <summary>
    /// Summary description for getHistory
    /// </summary>
    public class getHistory : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            BasicOutput output = new BasicOutput();
            string lang = context.Request["lang"];
            lang = (String.IsNullOrEmpty(lang)) ? "en" : lang;
            switch (lang)
            {
                case "sc":
                    output.data = @"瑞士依波路始创于1856年，迄今已有160年历史。品牌一直以“浪漫时刻”为核心，始终秉承“坚固耐用，精确可靠，佩戴舒适，美观实用”的设计理念，以“精益求精”的探索精神，运用高新技术结合瑞士的传统制表工艺，从而奠定了在钟表领域的重要地位。

经过长足的发展，瑞士依波路表的销售市场在全球快速扩展，从南北美洲到欧亚各国，随后有感于亚洲的发展潜力，于1903年正式进入中国。到目前为止，已在中国各大城市设立超过800个销售点，近年更积极开设旗舰店，大幅度提升品牌形象和品牌价值。";
                    break;

                case "tc":
                    output.data = @"瑞士依波路始創於1856年，迄今已有160年曆史。品牌一直以「浪漫時刻」為核心，始終秉承「堅固耐用，精確可靠，佩戴舒適，美觀實用」的設計理念，以「精益求精」的探索精神，運用高新技術結合瑞士的傳統製表工藝，從而奠定了在鐘表領域的重要地位。

經過長足的發展，瑞士依波路表的銷售市場在全球快速擴展，從南北美洲到歐亞各國，隨後有感於亞洲的發展潛力，於1903年正式進入中國。到目前為止，已在中國各大城市設立超過800個銷售點，近年更積極開設旗艦店，大幅度提升品牌形象和品牌價值。";
                    break;

                case "en":
                default:
                    output.data = @"Ernest Borel was founded and established in 1856 in Neuchâtel by Jules Borel and Paul Courvoisier. The company has a proud history of 160 years and the brand is well known for its quality and know how on the five continents. Ernest Borel consistently held the belief Solid and Durable, Accurate and Secure, Elegant and Practical. Ernest Borel always follows the initial enterprise spirit of creating perfection. Not only is it very meaningful for the clock and watch industry but also provides an unchangeable life style for those pursuing perfection, fashion and romance.

Ernest Borel's obedience to family strategy of foresight and sagacity enabled its product sales channels to spread to continents like America, Europe and Asia, and then the company quickly expanded to countries like Japan, China and India, these regions becoming Ernest Borel main market. Ernest Borel understands the customer's needs to have a watch that offers quality accuracy solidity durability at an affordable price.

Our new Head Office and production facility was inaugurated in 2009 in the Le Noirmont, in the middle of the Franches-Montagnes, in the Swiss Jura region. The combination of this modern production plant, equipped with the latest watch making machines and our dedicated professional, qualified, motivated team ensure efficient production, sales and customer service. Ernest Borel's watchmakers strive ceaselessly to create new collections, blending cutting-edge technology with meticulous concern for detail.

We welcome you all to our Ernest Borel World!!";
                    break;
            }

            output.status = (int)StatusType.success;
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