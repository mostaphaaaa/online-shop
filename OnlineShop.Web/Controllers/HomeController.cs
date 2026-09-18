using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Security;
using OnlineShop.Domain.Models.Users;
using OnlineShop.Infra.Data.Context;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly EshopDbContext context;

        public HomeController(EshopDbContext context)
        {
            this.context = context;
            //var user = context.Users.SingleOrDefault(u => u.Id == 16);
            //user.Password = PasswordHelper.EncodePasswordMd5("1234");
            //context.SaveChanges();
        }
        public IActionResult Index()
        {
            return View();
        }


        [Route("About-Us")]
        public IActionResult AboutUs()
        {
            return View();
        }

        [Route("Contact_Us")]
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }
}
