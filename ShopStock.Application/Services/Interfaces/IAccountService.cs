using ShopStock.Domain.Enums;
using ShopStock.Domain.Models.Users;
using ShopStock.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
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
