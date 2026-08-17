using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.ViewModels.Category;
using ShopStock.Infra.Data.Context;
using ShopStock.Web.Attributes;
using static System.Net.WebRequestMethods;

namespace ShopStock.Web.Areas.Admin.Controllers
{
    public class CategoriesController(EshopDbContext _context, ICategoryService _categoryService) : AdminBaseController
    {
        public async Task<IActionResult> Index(AdminFilterCategoryViewModel filter)
        {
            var result = await _categoryService.FilterAsync(filter);
            return View(result);
        }
        public async Task<IActionResult> SubCategory(int id)
        {
            var filter = new AdminFilterCategoryViewModel()
            {
                ParentId = id,
            };
            var result = await _categoryService.FilterAsync(filter);
            return View("Index", result);
        }


        //Get
        public async Task<IActionResult> Create(int? Id)
        {
            AdminCreateCategoryViewModel categoryViewModel = new AdminCreateCategoryViewModel()
            {
                ParentId = Id,
            };
            if (Id != null)
            {
                var categoryParent = await _categoryService.GetCategoryById(Id.Value);
                categoryViewModel.CategoryParentTitle = categoryParent.Title;
            }
            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateCategoryViewModel createCategory)
        {
            if (ModelState.IsValid)
            {
                if (await _categoryService.IsExistSlug(createCategory.Slug))
                {
                    ModelState.AddModelError("Slug", "چنین اسلاگی ای وجود دارد");
                    return View(createCategory);
                }
                await _categoryService.CreteCategoryAsync(createCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(createCategory);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminEditCategoryViewModel adminEdit = new AdminEditCategoryViewModel();
            var category = await _categoryService.GetCategoryById(id.Value);

            if (category.ParentId != null)
            {
                var categoryParent = await _categoryService.GetCategoryById(category.ParentId.Value);
                adminEdit.CategoryParentTitle = categoryParent.Title;
            }
            adminEdit.Slug = category.Slug;
            adminEdit.Title = category.Title;
            adminEdit.CategoryId = category.Id;
            adminEdit.ParentId = category.ParentId;
            adminEdit.ImageName = category.ImageName;
            adminEdit.CreateDate = category.CreateDate;
            return View(adminEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,AdminEditCategoryViewModel category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _categoryService.EditCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category= await _categoryService.GetCategoryById(id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> GetCategories(int? parentId)
        {
            var categories = await _categoryService.GetCategoriesAsync(parentId);
            return Json(categories);
        }
    }
}