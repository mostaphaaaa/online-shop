using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Roles;
using OnlineShop.Domain.Models.Users;
using OnlineShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Repositories
{
    public class UserRepository(EshopDbContext context) : IUserRepository
    {
        public async Task AddUserToRoles(int userId, List<int> roleIds)
        {
            foreach (int roleId in roleIds)
            {
                context.UserInRoles.Add(new UserInRoles()
                {
                    RoleId = roleId,
                    UserId = userId,
                });
            }
        }

        public async Task CreateAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            user.IsDelete = true;
            //user.IsActive = false;
            user.DeleteDate = DateTime.Now;
            await UpdateAsync(user);
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                await DeleteAsync(user);
            }
        }

        public async Task<IQueryable<User>> FilterAsync()
        {
            return context.Users.IgnoreQueryFilters().AsQueryable();
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUserForAdminAsync()
        {
            return await context.Users.IgnoreQueryFilters().ToListAsync();
        }

        public async Task<User?> GetUserByActiveCodeAsync(string activeCode)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.EmailActiveCode == activeCode);

        }

        public async Task<User?> GetUserByEmailOrUserName(string emailOrUserName)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Email == emailOrUserName ||
            u.UserName == emailOrUserName);
        }

        public async Task<User?> GetUserByIdAsync(int UserId)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Id == UserId);
        }

        public async Task<User?> GetUserFullDataAsync(int userId)
        {
            return await context.Users.IgnoreQueryFilters().Include(u => u.UserInRoles)
                .ThenInclude(r => r.Role).SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsExistEmailAsync(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsExistEmailAsync(string email, int userId)
        {
            return await context.Users.AnyAsync(u => u.Email == email && u.Id != userId);
        }

        public async Task<bool> IsExistMobileAsync(string mobile)
        {
            return await context.Users.AnyAsync(u => u.Mobile == mobile);
        }

        public async Task<bool> IsExistMobileAsync(string mobile, int userId)
        {
            return await context.Users.AnyAsync(u => u.Mobile == mobile&& u.Id!=userId);
        }

        public async Task<bool> IsExistUserNameAsync(string userName)
        {
            return await context.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<bool> IsExistUserNameAsync(string userName, int userId)
        {
            return await context.Users.AnyAsync(u => u.UserName == userName&&u.Id!=userId);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            context.Users.Update(user);
        }
    }
}
