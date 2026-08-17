using ShopStock.Application.TextFixer;
using ShopStock.Application.Generator;
using ShopStock.Application.Mapper;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Enums;
using ShopStock.Domain.Models.Users;
using ShopStock.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;
using ShopStock.Application.Security;

namespace ShopStock.Application.Services.Implementation
{
    public class AccountService(IUserRepository _userRepository) : IAccountService
    {
        public async Task<bool> ActiveAccountAsync(string activeCode)
        {
            var user = await _userRepository.GetUserByActiveCodeAsync(activeCode);

            if (user == null) return false;

            user.IsEmailActive = true;
            user.EmailActiveCode = NameGenerator.GenerateUnicName();

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();

            return true;
        }

        public async Task<bool> ChangePassword(int userId, ChangePasswordViewModel changePassword)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("کاربر یافت نشد");
            if (!PasswordHelper.VerifyPassword(changePassword.OldPassword, user.Password))
                return false;

            user.Password = PasswordHelper.EncodePasswordMd5(changePassword.Password);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();
            return true;

        }

        public async Task<bool> EditProfile(int userId, ProfileViewModel profile)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.Avatar = profile.Avatar;
            user.Mobile = profile.Mobile;
            user.NationalCode = profile.NationalCode;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();
            return true;
        }

        public async Task<User?> GetUserByEmailOrUserName(string emailOrUserName)
        {
            return await _userRepository.GetUserByEmailOrUserName(emailOrUserName);
        }

        public async Task<ProfileViewModel> GetUserProfile(int userId)
        {
            var user=await _userRepository.GetUserByIdAsync(userId);
            return new ProfileViewModel()
            {
                Avatar=user.Avatar,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Mobile=user.Mobile,
                NationalCode=user.NationalCode,
            };
        }

        public async Task<bool> IsCompleteProfile(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user.Mobile != null && user.Mobile != "")
            {
                return true;
            }
            return false;
        }

        public async Task<LoginUserResult> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userRepository.GetUserByEmailOrUserName(model.UserNameOrEmail);
            if (user == null)
                return LoginUserResult.NotFound;

            if (user.IsDelete)
                return LoginUserResult.NotFound;

            if (!PasswordHelper.VerifyPassword(model.Password, user.Password))
                return LoginUserResult.NotFound;

            if (!user.IsEmailActive)
                return LoginUserResult.NotActive;

            return LoginUserResult.Success;

        }

        public async Task<RegisterUserResult> RegisterAsync(RegisterViewModel model)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(model.UserName) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password)
                )
            {
                return RegisterUserResult.InvalidInputs;
            }
            if (await _userRepository.IsExistEmailAsync(model.Email.FixEmail()))
            {
                return RegisterUserResult.EmailDuplicated;
            }

            if (await _userRepository.IsExistUserNameAsync(model.UserName.FixUserName()))
            {
                return RegisterUserResult.UserNameDuplicated;
            }
            #endregion

            var user = UserMapper.MapToUser(model);
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveAsync();


            return RegisterUserResult.Success;

            //ToDo Send Email Activation
            //https://localhost:7198/Account/ActiveAccount/6563a09c9bac41bb82140b418abe299f
        }
    }
}
