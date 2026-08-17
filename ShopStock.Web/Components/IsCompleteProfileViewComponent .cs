using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Services.Interfaces;
using System.Security.Claims;

namespace ShopStock.Web.Components
{
    public class IsCompleteProfileViewComponent : ViewComponent
    {
        private readonly IAccountService _accountService;

        public IsCompleteProfileViewComponent(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            bool model = await _accountService.IsCompleteProfile(userId);
            return View(model);
        }
    }
}
