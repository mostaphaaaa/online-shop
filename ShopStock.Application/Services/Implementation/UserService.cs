using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopStock.Application.Generator;
using ShopStock.Application.Mapper;
using ShopStock.Application.Security;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Application.TextFixer;
using ShopStock.Application.Utilities;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Enums.Common;
using ShopStock.Domain.Enums.User;
using ShopStock.Domain.Models.Users;
using ShopStock.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implementation
{
    public class UserService(IUserRepository userRepository, IRoleRepository roleRepository) : IUserService
    {
        public async Task<AdminCreateUserResult> CreateUserAsync(AdminCreateUserViewModel model)
        {
            #region Validations
            if (string.IsNullOrEmpty(model.UserName) ||
                string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.Password))
            {
                return AdminCreateUserResult.Error;
            }
            if (await userRepository.IsExistEmailAsync(model.Email))
            {
                return AdminCreateUserResult.EmailDuplicated;
            }
            if (await userRepository.IsExistUserNameAsync(model.UserName))
            {
                return AdminCreateUserResult.UserNameDuplicated;
            }
            if (!string.IsNullOrEmpty(model.Mobile))
            {
                if (await userRepository.IsExistMobileAsync(model.Mobile.Trim()))
                {
                    return AdminCreateUserResult.MobileDuplicated;
                }
            }
            if (model.AvatarFile?.ImageValidate() == false)
            {
                return AdminCreateUserResult.InvalidImage;
            }
            #endregion

            #region Save Avatar
            string avatarName = await SaveImageFileAsync(model.AvatarFile);
            model.Avatar = avatarName;
            #endregion

            User user = UserMapper.MapToUser(model);

            await userRepository.CreateAsync(user);
            await userRepository.SaveAsync();


            if (model.UserSelectedRoles != null && model.UserSelectedRoles.Any())
            {
                await userRepository.AddUserToRoles(user.Id, model.UserSelectedRoles);
                await userRepository.SaveAsync();
            }

            return AdminCreateUserResult.Success;
        }
        public async Task<AdminEditUserResult> EditUserAsync(AdminEditUserViewModel model)
        {
            #region Validations
            if (string.IsNullOrEmpty(model.UserName) ||
                string.IsNullOrEmpty(model.Email))
            {
                return AdminEditUserResult.Error;
            }
            if (await userRepository.IsExistEmailAsync(model.Email, model.Id))
            {
                return AdminEditUserResult.EmailDuplicated;
            }
            if (await userRepository.IsExistUserNameAsync(model.UserName, model.Id))
            {
                return AdminEditUserResult.UserNameDuplicated;
            }
            if (!string.IsNullOrEmpty(model.Mobile))
            {
                if (await userRepository.IsExistMobileAsync(model.Mobile.Trim(), model.Id))
                {
                    return AdminEditUserResult.MobileDuplicated;
                }
            }
            if (model.AvatarFile?.ImageValidate() == false)
            {
                return AdminEditUserResult.InvalidImage;
            }
            #endregion

            #region Save Avatar
            if (model.AvatarFile != null)
            {
                if (model.Avatar != "NoPhoto.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars", model.Avatar);
                    FileHelper.DeleteFile(deletePath);
                }

                string avatarName = await SaveImageFileAsync(model.AvatarFile);
                model.Avatar = avatarName;
            }
            #endregion

            #region Map To User & Edit User
            var user = await userRepository.GetUserFullDataAsync(model.Id);
            user.Avatar = model.Avatar;
            user.Email = model.Email.FixEmail();
            user.FirstName = model.FirstName?.Trim();
            user.UserName = model.UserName.FixUserName();
            user.LastName = model.LastName?.Trim();
            user.UpdateDate = DateTime.Now;
            user.IsActive = model.IsActive;
            user.IsDelete = model.IsDelete;
            user.Mobile = model.Mobile;
            user.NationalCode = model.NationalCode;
            await userRepository.UpdateAsync(user);
            await userRepository.SaveAsync();
            #endregion

            #region Edit Role
            await roleRepository.UpdateUserInRole(user.Id, model.UserSelectedRoles);
            #endregion

            return AdminEditUserResult.Success;
        }


        public async Task DeleteUser(int userId)
        {
            await userRepository.DeleteAsync(userId);
            await userRepository.SaveAsync();
        }

        public async Task<User> GetUserForDelete(int userId)
        {
            return await userRepository.GetUserFullDataAsync(userId);
        }

        public async Task<AdminEditUserViewModel> GetUserForEditAsync(int userId)
        {
            var user = await userRepository.GetUserFullDataAsync(userId);
            if (user == null)
                throw new Exception("کاربر یافت نشد");

            var editUser = UserMapper.MapToEditUser(user);
            editUser.Roles = await roleRepository.GetAllRoleAsync();
            return editUser;
        }

        public async Task<IEnumerable<UserViewModel>> ListUserForAdmin()
        {
            var list = userRepository.GetAllUserForAdminAsync().Result
                .Select(u => new UserViewModel
                {
                    Avatar = u.Avatar,
                    CreateDate = u.CreateDate,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    Id = u.Id,
                    IsActive = u.IsActive,
                    LastName = u.LastName,
                    Mobile = u.Mobile,
                    NationalCode = u.NationalCode,
                    Password = u.Password,
                    UpdateDate = u.UpdateDate,
                    UserName = u.UserName,
                    IsDelete = u.IsDelete,
                }).ToList();
            return list;
        }

        #region Utilities
        private async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {

            if (imageFile == null) return "NoPhoto.jpg";

            var imageName = NameGenerator.GenerateUnicName() +
                Path.GetExtension(imageFile.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars", imageName);
            using (var stream = System.IO.File.Create(savePath))
            {
                await imageFile.CopyToAsync(stream);
            }

            return imageName;

        }
        #endregion

        public async Task<AdminFilterUserViewModel> AdminFilterAsync(AdminFilterUserViewModel filter)
        {
            #region Query
            var query = await userRepository.FilterAsync();
            #endregion

            #region Filters
            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(u => EF.Functions.Like(u.FirstName, $"%{filter.FirstName}%"));

            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(u => EF.Functions.Like(u.LastName, $"%{filter.LastName}%"));

            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email.ToLower()}%"));

            if (!string.IsNullOrEmpty(filter.UserName))
                query = query.Where(u => EF.Functions.Like(u.UserName, $"%{filter.UserName}%"));

            if (!string.IsNullOrEmpty(filter.Mobile))
                query = query.Where(u => EF.Functions.Like(u.Mobile, $"%{filter.Mobile}%"));

            switch (filter.DeleteStatus)
            {
                case FilterDeleteStatus.All:
                    {
                        break;
                    }
                case FilterDeleteStatus.Deleted:
                    {
                        query = query.Where(u => u.IsDelete);
                        break;
                    }
                case FilterDeleteStatus.NotDeleted:
                    {
                        query = query.Where(u => !u.IsDelete);
                        break;
                    }
            }
            #endregion

            #region Sort
            query.OrderByDescending(u => u.CreateDate);
            #endregion

            #region Paging
            await filter.Paging(UserMapper.MapToUserViewModel(query));
            #endregion

            return filter;
        }
    }
}
