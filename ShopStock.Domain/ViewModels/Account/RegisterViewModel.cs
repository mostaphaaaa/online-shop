using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; }
        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string Email { get; set; }
        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage ="کلمه عبور امن نیست")]
        public string Password { get; set; }
        [DisplayName("تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }

        public string? ImageData { get; set; }

        [DisplayName("عبارت امنیتی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CaptchaAnswer { get; set; }

    }
}
