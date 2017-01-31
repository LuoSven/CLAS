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

        /// <summary>
        /// ApiUrl
        /// </summary>
       private static string ApiSiteUrl
        {
            get
            {
                if (IsDebug)
                {
                    return "http://www.CLAS.com/api";
                }
                return "http://139.196.52.240:8333/api";

            }
        }
 

        public static string GetApiUrl(string url)
        {
            if (url[0]!= '/')
            {
                url = "/" + url;
            }
            return ApiSiteUrl + url;
        }
    }
}
