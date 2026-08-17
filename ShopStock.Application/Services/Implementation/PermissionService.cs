using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implementation
{
    public class PermissionService(IUserRepository userRepository,
       IPermissionRepository permissionRepository) : IPermissionService
    {
        public async Task<bool> CheckUserPermissionAsync(int userId, string permissionName)
        {
            var user = await userRepository.GetUserFullDataAsync(userId);
            if (user == null) return false;

            var permission = await permissionRepository.GetPermissionByNameAsync(permissionName);
            if (permission == null) return false;

            return user.UserInRoles.Any(
                s => permission.RolePermissionMappings.Any(p => p.RoleId == s.RoleId)
                );

        }

        public Task<bool> CheckUserPermissionAsync(int userId, IEnumerable<string> permissionNames)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
