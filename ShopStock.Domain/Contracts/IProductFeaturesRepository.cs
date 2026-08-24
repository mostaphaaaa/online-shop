using ShopStock.Domain.Models.Categories;
using ShopStock.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Contracts
{
    public interface IProductFeaturesRepository
    {
        Task<IEnumerable<ProductFeature>> GetAllProductFeaturesAsync(int productId);
        Task<ProductFeature?> GetProductFeatureByIdAsync(int featureId);

        Task CreateProductFeatureAsync(ProductFeature feature);
        Task UpdateAsync(ProductFeature feature);
        Task DeleteById(int fetureId);
        Task SaveChangesAsync();

    }
}
