using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Products;
using ShopStock.Domain.ViewModels.Product;
using ShopStock.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopStock.Infra.Data.Repositories
{
    public class ProductRepository(EshopDbContext _context) : IProductRepository, IGenericRepository<Product>
    {
        public void Add(Product sender)
        {
            _context.Products.Add(sender);
        }

        public async Task AddProductGalleryAsync(ProductGallery productGallery)
        {
            await _context.ProductGalleries.AddAsync(productGallery);
        }

        public async Task AddProductTagsAsync(int productId, List<ProductTagViewModel> list)
        {
            foreach (var item in list)
            {
                await _context.ProductTags.AddAsync(new ProductTag
                {
                    ProductId = productId,
                    TagTitle = item.value
                });
            }
        }

        public async Task<IQueryable<Product>> FilterAsync()
        {
            return _context.Products.AsQueryable();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _context.Products.ToListAsync();
        }

        public Product GetById(object id)
        {
            return _context.Products.Find(id);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _context.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public Product GetByIdWithInclude(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductGalleries)
                .FirstOrDefault(p => p.Id == id);
        }

        public Task<Product> GetByIdWithIncludeAsync(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductGalleries)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductTag>> GetAllTagsAsync(string query)
        {
            return await _context.ProductTags.Where(t => EF.Functions.Like(t.TagTitle, $"%{query}%")).ToListAsync();
        }

        public Task<bool> IsExistAsync(int productId)
        {
            return _context.Products.AnyAsync(p => p.Id == productId);
        }

        public bool Remove(int id)
        {
            var product = GetById(id);
            Remove(product);
            return true;
        }

        public bool Remove(Product sender)
        {
            _context.Products.Remove(sender);
            return true;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Product Select(Expression<Func<Product, bool>> where)
        {
            return _context.Products.FirstOrDefault(where);
        }

        public Task<Product> SelectAsync(Expression<Func<Product, bool>> where)
        {
            return _context.Products.FirstOrDefaultAsync(where);
        }

        public void Update(Product sender)
        {
            _context.Products.Update(sender);
        }

        public async Task<Product> GetProductForEditInAdminAsync(int productId)
        {
            return await _context.Products.Where(p => p.Id == productId)
                .Include(p => p.ProductGalleries)
                .Include(p => p.ProductTags)
                .Include(p => p.Category)
                .ThenInclude(p => p.Parent)
                .ThenInclude(p => p.Parent)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductGallery>> GetProductGalleriesAsync(int productId)
        {
            return await _context.ProductGalleries.Where(g => g.ProductId == productId).ToListAsync();
        }

        public async Task DeleteProductGalleryAsync(ProductGallery gallery)
        {
            _context.ProductGalleries.Remove(gallery);
        }

        public async Task AddProductGalleryAsync(int productId, string imageName)
        {
            await _context.ProductGalleries.AddAsync(new ProductGallery
            {
                ProductId = productId,
                ImageName = imageName
            });
        }

        public async Task DeleteAllProductTagsAsunc(int productId)
        {
            var tags= await _context.ProductTags.Where(t => t.ProductId == productId).ToListAsync();
            foreach (var tag in tags)
            {
                _context.ProductTags.Remove(tag);
            }
        }

        public async Task<ProductGallery> GetProductGalleryById(int galleryId)
        {
            return await _context.ProductGalleries.AsNoTracking().SingleOrDefaultAsync(g => g.Id == galleryId);
        }

        public async Task<IQueryable<Product>> GetTopProductsForShowAsync()
        {
            return  _context.Products
                .Include(p=>p.ProductColors)
                .OrderByDescending(p=>p.CreateDate)
                .Take(12)
                .AsQueryable();
        }
    }
}
