using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Interfaces;

namespace OnlineShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductService _productService;

        public CategoryController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("/Category/{slug}")]
        public async Task<IActionResult> Index(string slug)
        {
            var products =await _productService.GetProductsByCategorySlug(slug);
            return View(products);
        }
    }
}
