using Microsoft.AspNetCore.Mvc;

namespace ShopStock.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleError(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("NotFound");
                case 403:
                    return View("Forbidden");
                case 500:
                    return View("ServerError");
                case 401:
                    return View("Unauthorized");
                case 400:
                    return View("BadRequest");
                default:
                    return View("Error");
            }
        }
    }
}
