using Microsoft.AspNetCore.Mvc;
using OnlineShop.Infra.Data.Statics;
using OnlineShop.Web.Attributes;

namespace OnlineShop.Web.Areas.Admin.Controllers
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
