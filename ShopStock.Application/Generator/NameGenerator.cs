using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Generator
{
    public static class NameGenerator
    {
        public static string GenerateUnicName()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
