using ASPSnippets.Core.Captcha;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.Win32;
using Newtonsoft.Json;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Enums;
using ShopStock.Domain.ViewModels.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopStock.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        #region Create Captcha
        public Captcha Captcha
        {
            get
            {
                return JsonConvert.DeserializeObject<Captcha>(TempData["Captcha"].ToString());
            }
            set
            {
                TempData["Captcha"] = JsonConvert.SerializeObject(value);
            }
        }
        #endregion

        #region Constructor
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }
        #endregion

        #region Register
        [Route("Register")]
        public IActionResult Register(string ReturnUrl = "/")
        {
            this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D",
                Mode.Numeric);
            ViewBag.ReturnUrl = ReturnUrl;
            return View(new RegisterViewModel()
            {
                ImageData = this.Captcha.ImageData
            });
        }


        [HttpPost("Register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (!Captcha.IsValid(register.CaptchaAnswer))
            {
                this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D",
                Mode.AlphaNumeric);
                ModelState.AddModelError("CaptchaAnswer", "عبارت امنیتی صحیح نیست");
                register.ImageData = Captcha.ImageData;
                return View(register);
            }

            var result = await _accountService.RegisterAsync(register);
            switch (result)
            {
                case RegisterUserResult.Success:
                    {
                        return View("SuccessRegister", register);
                    }
                case RegisterUserResult.UserNameDuplicated:
                    {
                        ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری است");
                        this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D", Mode.Numeric);
                        register.ImageData = Captcha.ImageData;
                        break;
                    }
                case RegisterUserResult.EmailDuplicated:
                    {
                        ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است");
                        this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D", Mode.Numeric);
                        register.ImageData = Captcha.ImageData;
                        break;
                    }
                case RegisterUserResult.SendActivationEmail:
                    {
                        ModelState.AddModelError("Email", "ارسال ایمیل فعال سازی با مشکل مواجه شد . لطفا دقایقی دیگر تلاش کنید");
                        this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D", Mode.Numeric);
                        register.ImageData = Captcha.ImageData;
                        break;
                    }
                case RegisterUserResult.InvalidInputs:
                    {
                        ModelState.AddModelError("", "لطفا فیلد های روی فرم را پر کنید");
                        this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D", Mode.Numeric);
                        register.ImageData = Captcha.ImageData;
                        break;
                    }
                case RegisterUserResult.Failed:
                    {
                        ModelState.AddModelError("", "خطای ناشناخته. لطفا به پشتیبانی اطلاع دهید !");
                        this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D", Mode.Numeric);
                        register.ImageData = Captcha.ImageData;
                        break;
                    }
            }
            return View(register);
        }
        #endregion

        #region Activation Email
        [Route("VerifyEmail/{activeCode}")]
        public async Task<IActionResult> ActiveAccount(string activeCode)
        {
            ViewBag.ActiveAccount = await _accountService.ActiveAccountAsync(activeCode);
            return View();
        }
        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login(string ReturnUrl = "/")
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return Redirect("/");
            //}
            this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D",
                Mode.Numeric);
            ViewBag.ReturnUrl = ReturnUrl;
            return View(new LoginViewModel()
            {
                ImageData = this.Captcha.ImageData
            });
        }

        //mostapha
        //123
        [HttpPost("Login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            if (!ModelState.IsValid)
            {
                return View(login);
            }
            if (!Captcha.IsValid(login.CaptchaAnswer))
            {
                this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D",
                Mode.AlphaNumeric);
                ModelState.AddModelError("CaptchaAnswer", "عبارت امنیتی صحیح نیست");
                login.ImageData = Captcha.ImageData;
                return View(login);
            }

            var result = await _accountService.LoginUserAsync(login);


            if (result == LoginUserResult.NotFound)
            {
                ModelState.AddModelError("", "اطلاعات وارد شده صحیح نمی باشد");
                this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D",
                Mode.AlphaNumeric);
                login.ImageData = Captcha.ImageData;
                return View(login);
            }
            if (result == LoginUserResult.NotActive)
            {
                ViewBag.UserNotActive = true;
                this.Captcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D", Mode.Numeric);
                login.ImageData = Captcha.ImageData;
                return View(login);
            }
            var user = await _accountService.GetUserByEmailOrUserName(login.UserNameOrEmail);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("FullName",$"{user.FirstName} {user.LastName}"),
                new Claim("Mobile",user.Mobile??""),
                new Claim("Avatar",user.Avatar)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = login.RememberMe
            };
            await HttpContext.SignInAsync(principal, properties);
            if (Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return View("/");
        }
        #endregion

        #region SignOut
        [Route("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion

        #region Refresh Captcha
        public IActionResult RefreshCaptcha()
        {
            var newCaptcha = new Captcha(140, 40, 20f, "#FFFFFF", "#61028D", Mode.Numeric);
            this.Captcha = newCaptcha;
            return Json(new { ImageData = newCaptcha.ImageData });
        }
        #endregion
    }
}
