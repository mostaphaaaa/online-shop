using Microsoft.AspNetCore.Http;
using OnlineShop.Domain.Models.Ads;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Interfaces
{
    public interface IAdsService
    {
        Task<IEnumerable<Ads>> GetAllAdsAsync();
        Task<Ads> GetAdsByIdAsync(int id);
        Task<IEnumerable<Ads>> GetAdsForViewComponent(int zoneId);
        Task CreateAdsAsync(Ads ads, IFormFile imgUp, string? inputStartDate, string? inputExpireDate);
        Task EditAdsAsync(Ads ads, IFormFile imgUp, string? inputStartDate, string? inputExpireDate);
        Task SoftDeleteAdsAsync(int id);
        Task PremanentDeleteAsync(Ads ads);
        Task<bool> ExistAdsAsync(int id);
    }
}
