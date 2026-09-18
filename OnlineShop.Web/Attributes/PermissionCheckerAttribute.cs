using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineShop.Application.Extensions;
using OnlineShop.Application.Services.Interfaces;

namespace OnlineShop.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionCheckerAttribute(string permissionName)
        : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            var permissionService = context.HttpContext.RequestServices.GetRequiredService<IPermissionService>();
            var userId = context.HttpContext.User.GetUserId();
            bool userHasPermission = await permissionService.CheckUserPermissionAsync(userId, permissionName);

            if (!userHasPermission)
            {
                context.Result = new RedirectResult("/Admin/AccessDenied");
            }

        }
    }
}
