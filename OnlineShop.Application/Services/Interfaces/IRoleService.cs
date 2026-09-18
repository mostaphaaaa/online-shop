using OnlineShop.Domain.Models.Permission;
using OnlineShop.Domain.Models.Roles;
using OnlineShop.Domain.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRole();
        Task<IEnumerable<Permission>> GetAllPermissions();
        Task<Role> GetRoleByIdForAdmin(int id);
        Task CreateRole(AdminCreateRoleViewModel role);
        Task EditRole(AdminEditRoleViewModel role);
        Task DeleteRole(int roleId);
    }
}
