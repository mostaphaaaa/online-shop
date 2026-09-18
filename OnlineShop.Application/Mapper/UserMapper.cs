using OnlineShop.Application.TextFixer;
using OnlineShop.Application.Generator;
using OnlineShop.Application.Security;
using OnlineShop.Domain.Models.Users;
using OnlineShop.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineShop.Domain.ViewModels.User;

namespace OnlineShop.Application.Mapper
{
    public static class UserMapper
    {
        public static IQueryable<UserViewModel> MapToUserViewModel(IQueryable<User> query)
        {
            return query.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Mobile = user.Mobile,
                Password = user.Password,
                Avatar = user.Avatar,
                CreateDate = user.CreateDate,
                IsActive = user.IsActive,
                IsDelete = user.IsDelete,
            });
        }
        public static User MapToUser(RegisterViewModel model)
        {
            return new User()
            {
                UserName = model.UserName.FixUserName(),
                Email = model.Email.FixEmail(),
                Avatar = "NoPhoto.jpg",
                CreateDate = DateTime.Now,
                EmailActiveCode = NameGenerator.GenerateUnicName(),
                IsActive = true,
                IsEmailActive = false,
                Password = PasswordHelper.EncodePasswordMd5(model.Password),
            };
        }
        public static User MapToUser(AdminCreateUserViewModel model)
        {
            return new User()
            {
                FirstName = model.FirstName,
                Avatar = model.Avatar,
                CreateDate = DateTime.Now,
                Email = model.Email.FixEmail(),
                EmailActiveCode = NameGenerator.GenerateUnicName(),
                IsActive = model.IsActive,
                IsEmailActive = true,
                LastName = model.LastName,
                Mobile = model.Mobile,
                NationalCode = model.NationalCode,
                Password = PasswordHelper.EncodePasswordMd5(model.Password),
                UserName = model.UserName.FixUserName(),
                IsDelete = false,
            };
        }

        public static AdminEditUserViewModel MapToEditUser(User user)
        {
            return new AdminEditUserViewModel()
            {
                Id = user.Id,
                Avatar = user.Avatar,
                Email = user.Email,
                FirstName = user.FirstName,
                IsActive = user.IsActive,
                IsDelete = user.IsDelete,
                LastName = user.LastName,
                Mobile = user.Mobile,
                NationalCode = user.NationalCode,
                UserName = user.UserName,
                UserSelectedRoles=user.UserInRoles?.Select(x=>x.RoleId).ToList(),
            };

        }

    }
}
