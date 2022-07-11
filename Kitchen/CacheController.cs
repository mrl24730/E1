using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Kitchen
{
    public static class CacheController
    {

        public static int cacheKVP(object idx, int expiryMin, bool counter = true, object value = null, HttpContext _ctx = null)
        {

            HttpContext context = HttpContext.Current;
            DateTime absExpiry = DateTime.Now.AddMinutes(expiryMin); //absolute expiry
            int cnt = 1;
            string key = idx.ToString();



            Object cacheObj = null;
            if (_ctx != null) //current context is null, call from asyn handler
            {
                cacheObj = _ctx.Cache[key];
                context = _ctx;
            }
            else if (context != null)
            {
                cacheObj = context.Cache[key];

                if (cacheObj != null)
                {
                    try
                    {
                        absExpiry = (DateTime)context.Cache[key + "-absExpiry"];
                    }
                    catch
                    {
                        absExpiry = DateTime.Now.AddMinutes(expiryMin); //absolute expiry
                    }
                }

            }

            if (cacheObj != null && counter)
            {
                int.TryParse(cacheObj.ToString(), out cnt);
                cnt++;
            }





            //adds / update cache of the current IP count
            if (context != null && context.Cache != null)
            {
                if (counter)
                {
                    context.Cache.Insert(key, cnt, null, absExpiry, System.Web.Caching.Cache.NoSlidingExpiration,
                        System.Web.Caching.CacheItemPriority.Normal, null);
                }
                else if (value != null)
                {
                    context.Cache.Insert(key, value, null, absExpiry, System.Web.Caching.Cache.NoSlidingExpiration,
                        System.Web.Caching.CacheItemPriority.Normal, null);
                }

                context.Cache.Insert(key + "-absExpiry", absExpiry, null, absExpiry, System.Web.Caching.Cache.NoSlidingExpiration,
                       System.Web.Caching.CacheItemPriority.Normal, null);


            }


            return cnt;
        }

    }
}
