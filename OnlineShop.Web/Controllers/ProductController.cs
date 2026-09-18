using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Interfaces;

namespace OnlineShop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<PartialViewResult> ShortDescProduct(int id)
        {
            var product=await _productService.GetProductForShortDescById(id);
            return PartialView(product);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
