using ShopStock.Domain.Models.Categories;
using ShopStock.Domain.ViewModels.Category;
using ShopStock.Domain.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryById(int id);

        Task<bool> IsExistSlug(string slug);
        Task<IEnumerable<Category>> GetAllCategoryForMegaMenu();
        Task<AdminFilterCategoryViewModel> FilterAsync(AdminFilterCategoryViewModel filter);

        Task CreteCategoryAsync(AdminCreateCategoryViewModel categoryViewModel);
        Task EditCategoryAsync(AdminEditCategoryViewModel categoryViewModel);

        Task DeleteCategoryAsync(int id);
        Task<List<CategoryViewModel>> GetCategoriesAsync(int? parentId);
    }
}
