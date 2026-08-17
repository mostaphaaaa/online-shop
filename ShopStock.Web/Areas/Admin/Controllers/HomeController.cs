using Microsoft.AspNetCore.Mvc;
using ShopStock.Infra.Data.Statics;
using ShopStock.Web.Attributes;

namespace ShopStock.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        [PermissionChecker(PermissionName.AdminPanel)]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
