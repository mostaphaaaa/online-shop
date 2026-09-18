using OnlineShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Models.Roles
{
    public class UserInRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }



        #region Relations
        public User User { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
