using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Account
{
    public class ProfileViewModel
    {
        [DisplayName("نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string LastName { get; set; }

        [DisplayName("موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [RegularExpression("^09[0-9]{9}$", ErrorMessage ="شماره وارد شده صحیح نیست فرمت مثال 09123456789")]
        public string Mobile { get; set; }

        [DisplayName("کد ملی")]
        [MaxLength(15, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string? NationalCode { get; set; }
        [DisplayName("تصویر پروفایل")]
        public string? Avatar { get; set; }
    }
}
