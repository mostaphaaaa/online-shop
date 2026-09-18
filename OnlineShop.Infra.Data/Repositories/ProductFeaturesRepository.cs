using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Products;
using OnlineShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Repositories
{
    public class ProductFeaturesRepository(EshopDbContext _context) : IProductFeaturesRepository
    {
        public async Task CreateProductFeatureAsync(ProductFeature feature)
        {
            await _context.ProductFeatures.AddAsync(feature);
        }

        public async Task DeleteById(int fetureId)
        {
            var feature = await _context.ProductFeatures.FindAsync(fetureId);
            if (feature == null)
                return;
            _context.ProductFeatures.Remove(feature);
        }


        public async Task<IEnumerable<ProductFeature>> GetAllProductFeaturesAsync(int productId)
        {
            return await _context.ProductFeatures.Where(p=>p.ProductId == productId).ToListAsync();
        }

        public async Task<ProductFeature?> GetProductFeatureByIdAsync(int fetureId)
        {
            return await _context.ProductFeatures.FindAsync(fetureId);
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductFeature feature)
        {
           _context.ProductFeatures.Update(feature);
        }
    }
}
