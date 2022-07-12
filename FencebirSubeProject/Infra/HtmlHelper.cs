using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FencebirSubeProject.Infra
{
    public static class HtmlHelper
    {
        public static string HtmlEncode(this string value)
        {
            value = WebUtility.HtmlEncode(value);
            return value;
        }

        public static string HtmlDecode(this string value)
        {
            value = WebUtility.HtmlDecode(value);
            return value;
        }
    }
}
