using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EntitiyLayer
{
        public static class UrlHelper
        {
            public static string EncodeUrl(string url)
            {
                return HttpUtility.UrlEncode(url);
            }

            public static string DecodeUrl(string encodedUrl)
            {
                return HttpUtility.UrlDecode(encodedUrl);
            }
        }
 }


