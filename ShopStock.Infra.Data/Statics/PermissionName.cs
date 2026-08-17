using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Statics
{
    public static class PermissionName
    {
        public const string AdminPanel = "AdminPanel";
        #region Users
        public const string ManageUsers = "ManageUsers";
        public const string AddUser = "AddUser";
        public const string EditUser = "EditUser";
        public const string DeleteUser = "DeleteUser";
        #endregion

        #region Roles
        public const string ManageRoles = "ManageRoles";
        public const string AddRole = "AddRole";
        public const string EditRole = "EditRole";
        public const string DeleteRole = "DeleteRole";
        #endregion
    }
}
