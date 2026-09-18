using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Permission;
using OnlineShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Repositories
{
    public class PermissionRepository(EshopDbContext context) : IPermissionRepository
    {
        public async Task CreateAsync(Permission permission)
        {
            await context.Permissions.AddAsync(permission);
        }

        public async Task DeleteAsync(Permission permission)
        {
            context.Remove(permission);
        }

        public async Task DeleteAsync(int permissionId)
        {
            var permission = await GetPermissionByIdAsync(permissionId);
            await DeleteAsync(permission);
        }

        public async Task<Permission?> GetPermissionByIdAsync(int permissionId)
        {
            return await context.Permissions.SingleOrDefaultAsync(p => p.Id == permissionId);
        }

        public async Task<Permission?> GetPermissionByNameAsync(string PermissionName)
        {
            return await context.Permissions.Include(p=>p.RolePermissionMappings).SingleOrDefaultAsync(p => p.UniqName == PermissionName);
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await context.Permissions.Include(p=>p.Parent).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permission permission)
        {
            context.Permissions.Update(permission);
        }
    }
}
