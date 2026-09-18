using OnlineShop.Domain.Enums;
using OnlineShop.Domain.Models.Users;
using OnlineShop.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterUserResult> RegisterAsync(RegisterViewModel model);
        Task<LoginUserResult>LoginUserAsync(LoginViewModel model);
        Task<bool> ActiveAccountAsync(string activeCode);
        Task<User?>GetUserByEmailOrUserName(string emailOrUserName);
        Task<bool> ChangePassword(int userId,ChangePasswordViewModel changePassword);
        Task<bool>IsCompleteProfile(int userId);
        Task<bool> EditProfile(int userId,ProfileViewModel profile);
        Task<ProfileViewModel>GetUserProfile(int userId);
    }
}
