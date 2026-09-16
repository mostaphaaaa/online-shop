using Microsoft.AspNetCore.Mvc;

namespace ShopStock.Web.Controllers
{
    public class ProductController : Controller
    {
        public PartialViewResult ShortDescProduct(int id)
        {
            return PartialView();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
