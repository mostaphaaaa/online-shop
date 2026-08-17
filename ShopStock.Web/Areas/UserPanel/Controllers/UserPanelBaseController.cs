using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopStock.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class UserPanelBaseController : Controller
    {

    }
}
