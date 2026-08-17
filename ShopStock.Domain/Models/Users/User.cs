using ShopStock.Domain.Models.Common;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Models.Users
{
    public class User : BaseEntity
    {
        #region Properties
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? EmailActiveCode { get; set; }
        public bool IsEmailActive { get; set; }
        public string? Mobile { get; set; }
        public int? MobileActiveCode { get; set; }
        public string? NationalCode { get; set; }
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }

        #endregion


        #region Relations
        public ICollection<UserAddress>? Addresses { get; set; }
        public ICollection<UserInRoles>? UserInRoles { get; set; }
        #endregion
    }
}
