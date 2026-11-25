using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Encoders;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Models.Products;
using OnlineShop.Domain.ViewModels.Product;
using System.Security.Claims;

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

        [Route("Product/{slug}")]
        public async Task<IActionResult> ShowProduct(string slug)
        {
            var product= await _productService.GetProductBySlug(slug);
            return View(product);
        }

        [Route("p/{id}")]
        public async Task<IActionResult> ShortLinkRedirect(int id)
        {
            var slug = await _productService.GetSlugProduct(id);
            var encodeSlug = UrlEncoder.Encode(slug);
            return RedirectPermanent($"/Product/{encodeSlug}");
        }
        public IActionResult Index()
        {
            return View();
        }


        [Route("AddComment/{id}")]
        public async Task<IActionResult> AddComment(int id)
        {
            var product =await _productService.GetProductForProductPage(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Product = product;

            var model = new CreateCommentViewModel()
            {
                ProductId = product.Id
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AddComment/{id}")]
        public async Task<IActionResult> AddComment(CreateCommentViewModel comment)
        {
            if(!ModelState.IsValid)
            {
                var product = await _productService.GetProductForProductPage(comment.ProductId);
                ViewBag.Product = product;
                return View(comment);
            }

            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());

            await _productService.CreateProductCommentAsync(comment, currentUserId);

            return RedirectToAction(nameof(ShortLinkRedirect),new {id=comment.ProductId});
        }
    }
}
