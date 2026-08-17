using Microsoft.AspNetCore.Http;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.User
{
    public class AdminCreateUserViewModel
    {
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
        [RegularExpression("^09[0-9]{9}$", ErrorMessage = "شماره وارد شده صحیح نیست فرمت مثال 09123456789")]
        public string? Mobile { get; set; }

        [DisplayName("کد ملی")]
        public string? NationalCode { get; set; }


        [DisplayName("کلمه عبور*")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [DisplayName("آواتار")]
        public string? Avatar { get; set; }

        
        [DisplayName("انتخاب آواتار")]
        public IFormFile? AvatarFile { get; set; }

        [DisplayName("کاربر فعال هست؟")]
        public bool IsActive { get; set; }

        public IEnumerable<Role>? Roles { get; set; }

        public List<int>? UserSelectedRoles { get; set; }
    }
}
