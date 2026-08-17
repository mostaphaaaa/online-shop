using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopStock.Infra.Data.Statics;
using ShopStock.Web.Attributes;

namespace ShopStock.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminBaseController : Controller
    {



    }
}
