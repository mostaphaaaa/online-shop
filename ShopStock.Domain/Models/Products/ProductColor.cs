using ShopStock.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Models.Products
{
    public class ProductColor:BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public  double Price { get; set; }
        public bool IsDefault { get; set; }
        public int Quantity { get; set; }

        public Product? Product { get; set; }

    }
}
