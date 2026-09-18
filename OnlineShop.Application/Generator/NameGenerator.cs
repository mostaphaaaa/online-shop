using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Generator
{
    public static class NameGenerator
    {
        public static string GenerateUnicName()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
