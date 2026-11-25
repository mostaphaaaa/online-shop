using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Security;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Models.Slider;
using OnlineShop.Domain.Models.Users;
using OnlineShop.Infra.Data.Context;
using System.Text.Json;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISlideService _slideService;

        public HomeController(ISlideService slideService)
        {
            _slideService = slideService;
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

        //counting number of visits to the slide
        [Route("SlideShow/{id}")]
        public async Task<IActionResult> SlideShow(int id)
        {
            string url = "";

            if (Request.Cookies.TryGetValue("SlideVisit", out string? jsonSlideVisit) && !string.IsNullOrEmpty(jsonSlideVisit))
            {
                var list = JsonSerializer.Deserialize<List<string>>(jsonSlideVisit)?? new List<string>();
                if (!list.Any(s => s == id.ToString()))
                {
                    list.Add(id.ToString());
                    SetCooki(list);
                    url = await _slideService.VisitSlideAsync(id, true);
                }
                else
                {
                    url =await _slideService.VisitSlideAsync(id, false);
                }
            }
            else
            {
                List<String> list = new List<String>();
                list.Add(id.ToString());
                url = await _slideService.VisitSlideAsync(id, true);
                SetCooki(list);
            }

            return Redirect(url);
        }

        private void SetCooki(List<string> list)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            string json = JsonSerializer.Serialize(list);
            Response.Cookies.Append("SlideVisit", json, cookieOptions);
        }
    }
}
