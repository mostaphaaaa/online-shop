using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Extensions
{
    public static class PriceConvertor
    {
        public static string ToTooman(this double price)
        {
            return price.ToString("#,0 تومان");
        }
        public static string ToTooman(this int price)
        {
            return price.ToString("#,0 تومان");
        }
        public static string ToToomanEpmty(this double price)
        {
            return price.ToString("#,0");
        }
    }
}
