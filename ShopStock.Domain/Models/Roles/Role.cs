using ShopStock.Domain.Models.Common;
using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Models.Roles
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
