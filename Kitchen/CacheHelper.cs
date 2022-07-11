using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Kitchen
{
    public static class CacheHelper
    {

        public static int cacheIPCounter (uint ip,  HttpContext _ctx = null, int expiryMin=1) {

            string key = "ip" + ip.ToString();
            return cacheKeyCounter(key,  _ctx , expiryMin);
        }

        public static int cacheIDCounter(int id, HttpContext _ctx = null, int expiryMin = 1)
        {

            string key = "id"+id.ToString();
            return cacheKeyCounter(key, _ctx, expiryMin);
        }


        public static int cacheKeyCounter(string key,  HttpContext _ctx = null, int expiryMin=1)
        {

            HttpContext context = HttpContext.Current;
            int cnt=1;

            Object cacheObj = null;
            if (_ctx != null) //current context is null, call from asyn handler
            {
                cacheObj = _ctx.Cache[key];
                context = _ctx;
            }
            else if (context != null)
            {
                cacheObj = context.Cache[key];
            }

            if (cacheObj != null)
            {
                int.TryParse(cacheObj.ToString(),out cnt);
                cnt++;

            }

           

            DateTime absExpiry = DateTime.Now.AddMinutes(expiryMin); //absolute expiry


            //adds / update cache of the current IP count
            if (context != null && context.Cache != null)
            {

               context.Cache.Insert(key, cnt, null, absExpiry, System.Web.Caching.Cache.NoSlidingExpiration,
                        System.Web.Caching.CacheItemPriority.Normal, null);
            }
            

            System.Diagnostics.Debug.WriteLine("Cache EffectivePrivateBytesLimit: " + context.Cache.EffectivePrivateBytesLimit);
            System.Diagnostics.Debug.WriteLine("Cached Object Count: " + context.Cache.Count);
            
            return cnt;
        }

    }
}
