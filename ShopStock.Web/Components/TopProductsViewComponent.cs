using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Services.Interfaces;

namespace ShopStock.Web.Components
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