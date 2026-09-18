using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Enums.User;
using OnlineShop.Domain.Models.Users;
using OnlineShop.Domain.ViewModels.User;
using OnlineShop.Infra.Data.Context;
using OnlineShop.Infra.Data.Statics;
using OnlineShop.Web.Attributes;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : AdminBaseController
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        #region Index
        // GET: Admin/Users
        [PermissionChecker(PermissionName.ManageUsers)]
        public async Task<IActionResult> Index(AdminFilterUserViewModel filter, string Create = "false")
        {
            var lst = await userService.AdminFilterAsync(filter);
            ViewBag.Create = Create;
            return View(lst);
        }

        #endregion

        #region Create User
        // GET: Admin/Users/Create
        [PermissionChecker(PermissionName.AddUser)]
        public async Task<IActionResult> Create()
        {
            var model = new AdminCreateUserViewModel()
            {
                Roles = await roleService.GetAllRole()
            };
            return View(model);
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.AddUser)]
        public async Task<IActionResult> Create(AdminCreateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.CreateUserAsync(user);
                if (result == Domain.Enums.User.AdminCreateUserResult.Success)
                {
                    return Redirect("/Admin/Users?Create=Success");
                }
                else
                {
                    ViewBag.Error = result;
                }
            }
            user.Roles = await roleService.GetAllRole();
            return View(user);
        }
        #endregion

        #region Edit User
        // GET: Admin/Users/Edit/5
        [PermissionChecker(PermissionName.EditUser)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userService.GetUserForEditAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.EditUser)]
        public async Task<IActionResult> Edit(int id, AdminEditUserViewModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await userService.EditUserAsync(user);
                if (result == AdminEditUserResult.Success)
                    return RedirectToAction(nameof(Index));
                else ViewBag.Error = result;
            }
            user.Roles = await roleService.GetAllRole();
            return View(user);
        }
        #endregion

        //GET: Admin/Users/Delete/5
        [PermissionChecker(PermissionName.DeleteUser)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userService.GetUserForDelete(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            //خط پایین را خودم اضافه کردم (بعدا چک کنم مشکلی نداشته باشه
            //await userService.DeleteUser(id.Value);
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PermissionChecker(PermissionName.DeleteUser)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await userService.DeleteUser(id);
            // اگر درخواست AJAX هست، ریدایرکت نکن
            //if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //return Ok("success");  // ← فقط وضعیت موفقیت برگردون
            return RedirectToAction(nameof(Index));
        }
    }
}
