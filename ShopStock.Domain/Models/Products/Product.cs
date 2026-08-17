using ShopStock.Domain.Models.Categories;
using ShopStock.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopStock.Domain.Models.Products
{
    public class Product : BaseEntity
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string? ShortDescription { get; set; }
        public string? Review { get; set; }
        public string? DetailReview { get; set; }
        public string? ImageName { get; set; }
        public bool IsActive { get; set; }
        public int Count { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public ICollection<ProductColor>? ProductColors { get; set; }
        public ICollection<ProductFeature>? ProductFeatures  { get; set; }
        public ICollection<ProductGallery>? ProductGalleries  { get; set; }
        public ICollection<ProductTag>? ProductTags { get; set; }
    }
}
