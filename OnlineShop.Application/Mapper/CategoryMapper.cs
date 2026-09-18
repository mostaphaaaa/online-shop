using OnlineShop.Domain.Models.Categories;
using OnlineShop.Domain.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Mapper
{
    public static class CategoryMapper
    {
        public static IQueryable<CategoryViewModel> MapToCategory(IQueryable<Category> category)
        {
            return category.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                ParentId = c.ParentId,
                Title = c.Title,
                ImageName = c.ImageName,
                IsDeleted = c.IsDelete
            });
        }
        public static CategoryViewModel MapToCategory(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                ParentId = category.ParentId,
                Title = category.Title,
                ImageName = category.ImageName,
                IsDeleted = category.IsDelete
            };
        }
        public static List<CategoryViewModel> MapToListCategoryViewModel(List<Category> categories)
        {
            return categories.Select(MapToCategory).ToList();

        }
    }
}
