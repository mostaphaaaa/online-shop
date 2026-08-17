using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("نام کاربری*")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [DisplayName("ایمیل*")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
        public string Email { get; set; }

        [DisplayName("موبایل")]
        public string? Mobile { get; set; }

        [DisplayName("کد ملی")]
        public string? NationalCode { get; set; }

        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [DisplayName("آواتار")]
        public string? Avatar { get; set; }

        [DisplayName("وضعیت")]
        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
