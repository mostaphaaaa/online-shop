using OnlineShop.Domain.Enums.User;
using OnlineShop.Domain.Models.Users;
using OnlineShop.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<AdminFilterUserViewModel> AdminFilterAsync(AdminFilterUserViewModel filter);
        Task<IEnumerable<UserViewModel>> ListUserForAdmin();
        Task<AdminCreateUserResult> CreateUserAsync(AdminCreateUserViewModel model);
        Task<AdminEditUserResult> EditUserAsync(AdminEditUserViewModel model);
        Task<User> GetUserForDelete(int userId);
        Task <AdminEditUserViewModel> GetUserForEditAsync(int userId);
        Task DeleteUser(int userId);
    }
}
