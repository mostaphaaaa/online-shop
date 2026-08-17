using Microsoft.AspNetCore.Mvc;
using ShopStock.Application.Generator;
using ShopStock.Application.Security;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.ViewModels.Account;
using System.Security.Claims;

namespace ShopStock.Web.Areas.UserPanel.Controllers
{
    public class ProfileController : UserPanelBaseController
    {
        private readonly IAccountService _accountService;

        public ProfileController(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        #region Edit Profile
        public async Task<IActionResult> Index()
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());
            var profile = await _accountService.GetUserProfile(currentUserId);
            return View(profile);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ProfileViewModel model, IFormFile? imgAvatar)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (imgAvatar != null)
            {
                if (!imgAvatar.ImageValidate())
                {
                    ViewBag.ValidateImage = false;
                    return View(model);
                }

                if (model.Avatar!="NoPhoto.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars", model.Avatar);
                    if (System.IO.File.Exists(deletePath))
                        System.IO.File.Delete(deletePath);
                }

                string avatarName = NameGenerator.GenerateUnicName() + Path.GetExtension(imgAvatar.FileName);

                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars", avatarName);
                using (var stream = System.IO.File.Create(savePath))
                {
                    imgAvatar.CopyTo(stream);
                }
                model.Avatar = avatarName;
            }
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());
            await _accountService.EditProfile(currentUserId, model);

            TempData["SuccessEditProfile"] = "True";
            return Redirect("/UserPanel");
        }
        #endregion
        #region Change Password
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel change)
        {
            if (!ModelState.IsValid)
                return View(change);

            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());

            bool result = await _accountService.ChangePassword(currentUserId, change);

            if (!result)
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمی باشد");
                return View(change);
            }
            return Redirect("/LogOut");
        }
        #endregion
    }
}
