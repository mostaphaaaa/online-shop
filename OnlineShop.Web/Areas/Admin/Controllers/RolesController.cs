using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Models.Roles;
using OnlineShop.Domain.ViewModels.Roles;
using OnlineShop.Infra.Data.Context;
using OnlineShop.Infra.Data.Statics;
using OnlineShop.Web.Attributes;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : AdminBaseController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        // GET: Admin/Roles
        [PermissionChecker(PermissionName.ManageRoles)]
        public async Task<IActionResult> Index()
        {
            return View(await _roleService.GetAllRole());
        }


        // GET: Admin/Roles/Create
        [PermissionChecker(PermissionName.AddRole)]
        public async Task<IActionResult> Create()
        {
            AdminCreateRoleViewModel adminCreate = new AdminCreateRoleViewModel()
            {
                Permissions = await _roleService.GetAllPermissions(),
            };
            return View(adminCreate);
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.AddRole)]
        public async Task<IActionResult> Create(AdminCreateRoleViewModel createRole)
        {
            if (ModelState.IsValid)
            {
                await _roleService.CreateRole(createRole);
                return RedirectToAction(nameof(Index));
            }
            createRole.Permissions = await _roleService.GetAllPermissions();
            return View(createRole);
        }

        // GET: Admin/Roles/Edit/5
        [PermissionChecker(PermissionName.EditRole)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AdminEditRoleViewModel adminEdit = new AdminEditRoleViewModel();
            adminEdit.Permissions = await _roleService.GetAllPermissions();
            var role = await _roleService.GetRoleByIdForAdmin(id.Value);
            adminEdit.RoleId = role.Id;

            if (role.RolePermissionMappings.Any())
            {
                adminEdit.PermissionSelectedIds = role.RolePermissionMappings.Select(r => r.PermissionId).ToList();
            }
            adminEdit.RoleName = role.RoleName;
            return View(adminEdit);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditRole)]
        public async Task<IActionResult> Edit(int id, AdminEditRoleViewModel role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _roleService.EditRole(role);
                return RedirectToAction(nameof(Index));
            }
            role.Permissions = await _roleService.GetAllPermissions();
            return View(role);
        }

        // GET: Admin/Roles/Delete/5
        [PermissionChecker(PermissionName.DeleteRole)]
        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine($"DELETE ROLE: {id}");
            await _roleService.DeleteRole(id);
            return Ok();
        }

    }
}
