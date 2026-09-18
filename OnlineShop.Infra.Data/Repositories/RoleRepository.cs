using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Permission;
using OnlineShop.Domain.Models.Roles;
using OnlineShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Repositories
{
    public class RoleRepository(EshopDbContext _context) : IRoleRepository
    {
        public async Task AddPermissionToRole(int roleId, int permissionId)
        {
            await _context.RolePermissionMappings.AddAsync(new RolePermissionMapping
            {
                RoleId = roleId,
                PermissionId = permissionId
            });
        }

        public async Task CreateAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task DeleteAllPermissionInRole(int roleId)
        {
            var permissions= await GetAllPermissionInRole(roleId);
            _context.RolePermissionMappings.RemoveRange(permissions);
        }

        public async Task DeleteAsync(Role role)
        {
            role.IsDelete = true;
            role.DeleteDate = DateTime.Now;
            _context.Roles.Update(role);
        }

        public async Task DeleteAsync(int roleId)
        {
            var role = await GetRoleByIdAsync(roleId);
            await DeleteAsync(role);
        }

        public async Task<IEnumerable<RolePermissionMapping>> GetAllPermissionInRole(int roleId)
        {
            return await _context.RolePermissionMappings.Where(p => p.RoleId == roleId).ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<Role> GetRoleByIdForAdmin(int id)
        {
            return await _context.Roles.Include(r => r.RolePermissionMappings)
                 .Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
        }

        public async Task UpdateUserInRole(int userId, List<int> selectedRoles)
        {
            var rolesUser = _context.UserInRoles.Where(r => r.UserId == userId).ToList();

            foreach (var role in rolesUser)
            {
                _context.Remove(role);
            }

            if (selectedRoles != null && selectedRoles.Count > 0)
            {
                foreach (var role in selectedRoles)
                {
                    _context.UserInRoles.Add(new UserInRoles
                    {
                        RoleId = role,
                        UserId = userId,
                    });
                }
            }
            _context.SaveChanges();
        }
    }
}
