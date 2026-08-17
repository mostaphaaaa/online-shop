using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopStock.Domain.Models.Products
{
    public class ProductTag
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string TagTitle { get; set; }


        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

    }
}
