using ShopStock.Domain.Models.Products;
using ShopStock.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Mapper
{
    public static class ProductMapper
    {
        public static IQueryable<ProductViewModel> MapToProductViewModel(IQueryable<Product> query)
        {
            return query.Select(p => new ProductViewModel
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Title,
                Title = p.Title,
                Price = p.Price,
                ShortDescription = p.ShortDescription,
                Review = p.Review,
                DetailReview = p.DetailReview,
                ImageName = p.ImageName,
                IsActive = p.IsActive,
                IsDelete = p.IsDelete,
                CreateDate = p.CreateDate
            });
        }
        public static Product MapToProduct(AdminCreateProductViewModel model, string imageName)
        {
            return new Product
            {
                CategoryId = model.CategoryId,
                Count = model.Count,
                CreateDate = DateTime.Now,
                DetailReview = model.DetailReview,
                ImageName = imageName,
                IsActive = model.IsActive,
                IsDelete = false,
                Review = model.Review,
                Title = model.Title,
                ShortDescription = model.ShortDescription,
                Price = model.Price
            };
        }
    }
}
