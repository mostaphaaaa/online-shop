using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Services.Interfaces;

namespace ShopStock.Web.Components
{
    public class MegaMenu(ICategoryService _categoryService) : ViewComponent
    {
        public async Task<IViewComponentResult>InvokeAsync()
        {
            var data=await _categoryService.GetAllCategoryForMegaMenu();
            return View(data);
        }
    }
}
