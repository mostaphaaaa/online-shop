using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Products;
using ShopStock.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implementation
{
    public class ProductFeatureService(IProductFeaturesRepository _productFeaturesRepository) : IProductFeatureService
    {
        public async Task CreateProductFeature(ProductFeatureViewModel model)
        {
            ProductFeature productFeature = new ProductFeature()
            {
                Name = model.Name,
                Value = model.Value,
                CreateDate = DateTime.Now,
                ProductId = model.ProductId,
                IsDelete = false
            };
            await _productFeaturesRepository.CreateProductFeatureAsync(productFeature);
            await _productFeaturesRepository.SaveChangesAsync();
        }

        public async Task DeleteProductFeature(int featureId)
        {
            await _productFeaturesRepository.DeleteById(featureId);
            await _productFeaturesRepository.SaveChangesAsync();
        }

        public async Task EditProductFeature(ProductFeatureViewModel model)
        {
            var feature = await _productFeaturesRepository.GetProductFeatureByIdAsync(model.Id);
            if (feature == null)
                return;

            feature.Name = model.Name;
            feature.Value = model.Value;
            feature.UpdateDate = DateTime.Now;


            await _productFeaturesRepository.UpdateAsync(feature);
            await _productFeaturesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductFeature>> GetAllProductFeatures(int productId)
        {
            return await _productFeaturesRepository.GetAllProductFeaturesAsync(productId);
        }

        public async Task<ProductFeature?> GetProductFeatureById(int featureId)
        {
            return await _productFeaturesRepository.GetProductFeatureByIdAsync(featureId);
        }
    }
}
