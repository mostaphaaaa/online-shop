using Microsoft.AspNetCore.Mvc;

namespace ShopStock.Web.Components
{
    public class UserPanelViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult>InvokeAsync()
        {
            return View();
        }
    }
}
