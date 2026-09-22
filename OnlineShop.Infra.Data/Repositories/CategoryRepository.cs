using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Categories;
using OnlineShop.Domain.Models.Products;
using OnlineShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Repositories
{
    public class CategoryRepository(EshopDbContext _context) : ICategoryRepository
    {
        public async Task CreteCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task EditCategoryAsync(Category category)
        {
            await Task.Run(() => _context.Categories.Update(category));
        }

        public async Task<IQueryable<Category>> FilterAsync()
        {
            return _context.Categories.AsQueryable();
        }

        public async Task<IEnumerable<Category>> GetAllCategoryForMegaMenu()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetByParentIdAsync(int? parentId)
        {
            return await _context.Categories
                .Where(c => c.ParentId == parentId && !c.IsDelete)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsBySlug(string slug)
        {
            return await  _context.Products
                .Where(p=>p.Category.Slug == slug 
                || p.Category.Parent.Slug == slug 
                || p.Category.Parent.Parent.Slug == slug)        
                .ToListAsync();
        }

        public Task<bool> IsExistSlug(string slug)
        {
            return _context.Categories.AnyAsync(c => c.Slug == slug.Trim());
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            await Task.Run(() => _context.Categories.Update(category));
        }
    }
}
