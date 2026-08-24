using ShopStock.Domain.Models.Products;
using ShopStock.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
{
    public interface IProductFeatureService
    {
        Task CreateProductFeature(ProductFeatureViewModel model);

        Task EditProductFeature(ProductFeatureViewModel model);

        Task DeleteProductFeature(int featureId);
        Task<IEnumerable<ProductFeature>> GetAllProductFeatures(int productId);
        
        Task<ProductFeature?> GetProductFeatureById(int featureId);
    }
}
