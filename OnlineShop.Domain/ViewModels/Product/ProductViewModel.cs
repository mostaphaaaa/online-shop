using OnlineShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public double? PriceWithDiscount { get; set; }
        public int? DiscountPrecent { get; set; }
        public string? ShortDescription { get; set; }
        public string? Review { get; set; }
        public string? DetailReview { get; set; }
        public string? ImageName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }


        public List<ProductColor>? ProductColors { get; set; }
        public List<ProductGallery>? ProductGalleries { get; set; }
        public List<ProductFeature>? ProductFeatures { get; set; }
    }
}
