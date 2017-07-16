using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyLittleCMS.Core.Utilities
{
    public class WebHelper
    {

        public static string GetIpAddress()
        {
            String ip =
            HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        public static string GetUrlReferrer()
        {
            if (!IsRequestAvailable())
                return string.Empty;
            return HttpContext.Current.Request.Headers["Referer"];
        }
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static bool IsRequestAvailable()
        {
            if (HttpContext.Current == null)
                return false;

            try
            {
                if (HttpContext.Current.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
