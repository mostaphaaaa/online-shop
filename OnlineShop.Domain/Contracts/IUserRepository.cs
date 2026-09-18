using OnlineShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> FilterAsync();
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<IEnumerable<User>> GetAllUserForAdminAsync();
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserFullDataAsync(int userId);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task DeleteAsync(int UserId);

        Task AddUserToRoles(int userId,List<int> roleIds);

        Task<bool> IsExistUserNameAsync(string userName);
        Task<bool> IsExistUserNameAsync(string userName,int userId);
        Task<bool> IsExistEmailAsync(string email);
        Task<bool> IsExistEmailAsync(string email,int userId);
        Task<bool> IsExistMobileAsync(string mobile);
        Task<bool> IsExistMobileAsync(string mobile,int userId);
        Task<User?> GetUserByActiveCodeAsync(string activeCode); 
        Task<User?>GetUserByEmailOrUserName(string emailOrUserName);
        Task SaveAsync();
    }
}
