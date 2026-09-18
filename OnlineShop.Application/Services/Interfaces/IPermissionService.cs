using OnlineShop.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task<bool> CheckUserPermissionAsync(int userId,string permissionName);
        Task<bool> CheckUserPermissionAsync(int userId,IEnumerable<string> permissionNames);
    }
}
