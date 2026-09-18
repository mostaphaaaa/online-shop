using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Interfaces;

namespace OnlineShop.Web.Components
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
