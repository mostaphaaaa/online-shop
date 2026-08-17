using ShopStock.Domain.Enums.Common;
using ShopStock.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.User
{
    public class AdminFilterUserViewModel:BasePaging<UserViewModel>
    {
        [DisplayName("نام")]
        public string? FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string? LastName { get; set; }

        [DisplayName("نام کاربری*")]
        public string? UserName { get; set; }

        [DisplayName("ایمیل*")]
        public string? Email { get; set; }

        [DisplayName("موبایل")]
        public string? Mobile { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        [DisplayName("وضعیت حذف")]
        public FilterDeleteStatus DeleteStatus { get; set; }
    }
}
