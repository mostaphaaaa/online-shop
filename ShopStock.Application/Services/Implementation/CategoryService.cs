using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ShopStock.Application.Generator;
using ShopStock.Application.Mapper;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Application.Utilities;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Enums.Common;
using ShopStock.Domain.Models.Categories;
using ShopStock.Domain.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implementation
{
    public class CategoryService(ICategoryRepository _categoryRepository, IMemoryCache _cache) : ICategoryService
    {
        public async Task CreteCategoryAsync(AdminCreateCategoryViewModel categoryViewModel)
        {
            Category category = new Category()
            {
                CreateDate = DateTime.Now,
                IsDelete = false,
                ParentId = categoryViewModel.ParentId,
                Title = categoryViewModel.Title.Trim(),
                Slug = categoryViewModel.Slug.Trim(),
                ImageName = await SaveImageFileAsync(categoryViewModel.Image),
            };
            await _categoryRepository.CreteCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
            _cache.Remove(CacheKeyNames.GetAllCategoryForMegaMenu);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            category.IsDelete = true;
            category.DeleteDate = DateTime.Now;
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task EditCategoryAsync(AdminEditCategoryViewModel categoryViewModel)
        {
            Category category = new Category()
            {
                Id = categoryViewModel.CategoryId,
                ParentId = categoryViewModel.ParentId,
                Title = categoryViewModel.Title.Trim(),
                Slug = categoryViewModel.Slug.Trim(),
                ImageName = categoryViewModel.ImageName,
                UpdateDate = DateTime.Now,
                CreateDate = categoryViewModel.CreateDate,
            };
            if (categoryViewModel.Image != null)
            {
                if (categoryViewModel.ImageName != "NoPhoto.jpg")
                {
                    //Delete Old Image
                    string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryImages", categoryViewModel.ImageName);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                category.ImageName = await SaveImageFileAsync(categoryViewModel.Image);
            }
            await _categoryRepository.EditCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<AdminFilterCategoryViewModel> FilterAsync(AdminFilterCategoryViewModel filter)
        {
            var query = await _categoryRepository.FilterAsync();
            if (filter.ParentId.HasValue)
            {
                query = query.Where(c => c.ParentId == filter.ParentId);
            }
            else
            {
                query = query.Where(c => c.ParentId == null);
            }
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(c => EF.Functions.Like(c.Title, $"%{filter.Title}%"));
            }
            switch (filter.DeleteStatus)
            {
                case FilterDeleteStatus.All:
                    break;
                case FilterDeleteStatus.Deleted:
                    query = query.Where(c => c.IsDelete);
                    break;
                case FilterDeleteStatus.NotDeleted:
                    query = query.Where(c => !c.IsDelete);
                    break;
            }

            #region Sort
            query.OrderByDescending(c => c.CreateDate);
            #endregion

            #region Category Paging
            await filter.Paging(CategoryMapper.MapToCategory(query));
            #endregion

            return filter;
        }

        public async Task<IEnumerable<Category>> GetAllCategoryForMegaMenu()
        {
            string cacheKey = CacheKeyNames.GetAllCategoryForMegaMenu;

            if (_cache.TryGetValue(cacheKey, out IEnumerable<Category> category))
            {
                return category;
            }
            else
            {
                category = await _categoryRepository.GetAllCategoryForMegaMenu();
                _cache.Set(cacheKey, category, TimeSpan.FromHours(1));
                return category;
            }
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync(int? parentId)
        {
            var categories = await _categoryRepository.GetByParentIdAsync(parentId);
            return CategoryMapper.MapToListCategoryViewModel(categories);
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<bool> IsExistSlug(string slug)
        {
            return await _categoryRepository.IsExistSlug(slug);
        }
        private async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {

            if (imageFile == null) return "NoPhoto.jpg";

            var imageName = NameGenerator.GenerateUnicName() +
                Path.GetExtension(imageFile.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CategoryImages", imageName);
            using (var stream = System.IO.File.Create(savePath))
            {
                await imageFile.CopyToAsync(stream);
            }

            return imageName;

        }
    }
}
