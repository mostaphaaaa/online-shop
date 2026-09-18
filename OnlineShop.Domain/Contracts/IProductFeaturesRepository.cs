using OnlineShop.Domain.Models.Categories;
using OnlineShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Contracts
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
