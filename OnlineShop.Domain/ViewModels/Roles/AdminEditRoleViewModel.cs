using OnlineShop.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Domain.ViewModels.Roles
{
    public class AdminEditRoleViewModel
    {
        public int RoleId { get; set; }

        [DisplayName("عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RoleName { get; set; }

        public IEnumerable<Permission>? Permissions { get; set; }
        public List<int>? PermissionSelectedIds { get; set; }= new List<int>();
    }
}
