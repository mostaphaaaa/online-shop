using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Encoders
{
    public static class UrlEncoder
    {
        public static string Encode(string param)
        {
            return Uri.EscapeDataString(param);
        }
    }
}
