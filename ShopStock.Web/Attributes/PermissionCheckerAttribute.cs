using Microsoft.AspNetCore.Mvc.Filters;
using ShopStock.Application.Extensions;
using ShopStock.Application.Services.Interfaces;

namespace ShopStock.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class PermissionCheckerAttribute(string permissionName)
        : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId=context.HttpContext.User.GetUserId();
                bool userHasPermission = await permissionService.CheckUserPermissionAsync(userId, permissionName);

                if (!userHasPermission)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.HttpContext.Response.Redirect("/Admin/AccessDenied");
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.HttpContext.Response.Redirect("/Admin/AccessDenied");
            }
        }
    }
}
