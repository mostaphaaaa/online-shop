using OnlineShop.Domain.Models.Common;
using OnlineShop.Domain.Models.Permission;
using OnlineShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Models.Roles
{
    public class Role:BaseEntity
    {
        public string RoleName { get; set; }


        #region Relations
        public ICollection<UserInRoles>? UserInRoles { get; set; }
        public ICollection<RolePermissionMapping>? RolePermissionMappings { get; set; }
        #endregion
    }
}
