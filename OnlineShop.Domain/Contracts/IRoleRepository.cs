using OnlineShop.Domain.Models.Permission;
using OnlineShop.Domain.Models.Roles;
using OnlineShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Contracts
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<Role?> GetRoleByIdAsync(int roleId);
        Task<Role> GetRoleByIdForAdmin(int id);
        Task<IEnumerable<RolePermissionMapping>> GetAllPermissionInRole(int roleId);
        Task CreateAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(Role role);
        Task DeleteAsync(int roleId);
        Task DeleteAllPermissionInRole(int roleId);
        Task UpdateUserInRole(int userId, List<int> selectedRoles);
        Task AddPermissionToRole(int roleId, int permissionId);
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();

        Task SaveChanges();
    }
}
