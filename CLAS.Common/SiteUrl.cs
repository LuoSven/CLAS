using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Common
{
   public class SiteUrl
    {
       private static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#endif
                return false;
            }
        }

        public static string LoaclApiSiteUrl= "www.CLAS.com";
        public static string ReleaseApiSiteUrl = "139.196.52.240:8333";

        /// <summary>
        /// ApiUrl
        /// </summary>
        private static string ApiSiteUrl
        {
            get
            {
                var host = ReleaseApiSiteUrl;
                if (IsDebug)
                {
                    host = LoaclApiSiteUrl;
                }
                return "http://"+ host + "/api";
            }
        }

        /// <summary>
        /// ApiUrl
        /// </summary>
        private static string StaticUrl
        {
            get
            {
                var host = ReleaseApiSiteUrl;
                if (IsDebug)
                {
                    host = LoaclApiSiteUrl;
                }
                return "http://" + host ;

            }
        }


        public static string GetApiUrl(string url)
        {
            if (url[0] != '/')
            {
                url = "/" + url;
            }
            return ApiSiteUrl + url;
        }
        public static string GetStaticUrl(string url)
        {
            if (url[0] != '/')
            {
                url = "/" + url;
            }
            return StaticUrl + url;
        }
    }
}
