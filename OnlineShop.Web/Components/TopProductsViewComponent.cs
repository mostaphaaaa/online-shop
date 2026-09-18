using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Interfaces;

namespace OnlineShop.Web.Components
{
    public class TopProductsViewComponent(IProductService productService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await productService.GetTopProductsForShowAsync();
            return View(data);
        }
    }
}