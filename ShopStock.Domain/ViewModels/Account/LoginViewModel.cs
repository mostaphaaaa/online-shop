using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Account
{
    public class LoginViewModel
    {

        [DisplayName("نام کاربری یا ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserNameOrEmail { get; set; }



        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [DisplayName("مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

        public string? ImageData { get; set; }

        [DisplayName("عبارت امنیتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CaptchaAnswer { get; set; }
    }
}
