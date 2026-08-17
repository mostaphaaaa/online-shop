using ShopStock.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Models.Products
{
    public class ProductFeature:BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }


        public Product? Product { get; set; }
    }
}
