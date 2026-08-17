using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implementation
{
    public class RoleService(IRoleRepository _roleRepository) : IRoleService
    {
        public async Task CreateRole(AdminCreateRoleViewModel role)
        {
            Role roleAdd = new Role()
            {
                CreateDate = DateTime.Now,
                IsDelete = false,
                RoleName = role.RoleName,
            };
            await _roleRepository.CreateAsync(roleAdd);
            await _roleRepository.SaveChanges();

            if (role.PermissionSelectedIds != null && role.PermissionSelectedIds.Any())
            {
                foreach (var item in role.PermissionSelectedIds)
                {
                    await _roleRepository.AddPermissionToRole(roleAdd.Id, item);
                }
                await _roleRepository.SaveChanges();
            }

        }

        public async Task DeleteRole(int roleId)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId);
            role.IsDelete = true;
            role.DeleteDate = DateTime.Now;
            await _roleRepository.UpdateAsync(role);
            await _roleRepository.SaveChanges();
        }

        public async Task EditRole(AdminEditRoleViewModel role)
        {
            var editRole = await _roleRepository.GetRoleByIdAsync(role.RoleId);
            editRole.UpdateDate = DateTime.Now;
            editRole.RoleName = role.RoleName;
            await _roleRepository.UpdateAsync(editRole);

            await _roleRepository.DeleteAllPermissionInRole(editRole.Id);
            if (role.PermissionSelectedIds.Any())
            {
                foreach (var item in role.PermissionSelectedIds)
                {
                    await _roleRepository.AddPermissionToRole(editRole.Id, item);
                }
            }
            await _roleRepository.SaveChanges();
        }

        public async Task<IEnumerable<Permission>> GetAllPermissions()
        {
            return await _roleRepository.GetAllPermissionsAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRole()
        {
            return await _roleRepository.GetAllRoleAsync();
        }

        public async Task<Role> GetRoleByIdForAdmin(int id)
        {
            return await _roleRepository.GetRoleByIdForAdmin(id);
        }
    }
}
