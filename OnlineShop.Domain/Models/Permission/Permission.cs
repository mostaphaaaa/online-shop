using OnlineShop.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Models.Permission
{
    public class Permission
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string UniqName { get; set; }
        public string DisplayName { get; set; }



        public Permission? Parent { get; set; }
        public ICollection<RolePermissionMapping>? RolePermissionMappings { get; set; }
    }
}
