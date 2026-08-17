using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.ViewModels.Product;
using ShopStock.Infra.Data.Context;

namespace ShopStock.Web.Areas.Admin.Controllers
{
    public class ProductsController(IProductService _productService,ICategoryService _categoryService, EshopDbContext context) : AdminBaseController
    {
        public async Task<IActionResult> Index(AdminFilterProductViewModel filter)
        {
            var result = await _productService.FilterProductsAsync(filter);

            return View(result);
        }

        #region Create Product
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateProductViewModel product)
        {
            //[{ "value":"کلمه اول"},{ "value":"کلمه دوم"},{ "value":"کلمه سوم"}]
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _productService.CreateProductAsync(product);
            return RedirectToAction("Index");
        }
        #endregion


        #region Edit Product

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _productService.GetProductForEditAsync(id);


            #region Bind Select List Category
            var categories =await _categoryService.GetAllCategoryForMegaMenu();

            int categoryId=model.CategoryId;
            int sub_category = categories.First(c => c.Id == categoryId).ParentId.Value;
            int main_category=categories.First(c=>c.Id==sub_category).ParentId.Value;
            ViewBag.CategoryId = new SelectList(categories.Where(c=>c.ParentId==sub_category), "Id", "Title", categoryId);  //list , value , display, selected
            ViewBag.sub_category = new SelectList(categories.Where(c => c.ParentId == main_category), "Id", "Title", sub_category);
            ViewBag.main_category=new SelectList(categories.Where(c=>c.ParentId==null), "Id", "Title", main_category);
            #endregion

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminEditProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _productService.EditProductAsync(product);
            return RedirectToAction("Index");
        }

        #endregion

        public async Task<JsonResult> GetTags(string query)
        {
            var tags = await _productService.GetTagsAsync(query);

            return Json(tags.Select(x => new
            {
                id = x.Id,
                value = x.TagTitle
            }));
        }
    }
}
