using OnlineShop.Domain.Models.Categories;
using OnlineShop.Domain.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Contracts
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryById(int id);
        Task<IEnumerable<Category>> GetAllCategoryForMegaMenu();
        Task<IQueryable<Category>> FilterAsync();

        Task<bool> IsExistSlug(string slug);

        Task CreteCategoryAsync(Category category);
        Task EditCategoryAsync(Category category);
        Task UpdateAsync(Category category);
        Task<List<Category>> GetByParentIdAsync(int? parentId);
        Task SaveChangesAsync();

    }
}
