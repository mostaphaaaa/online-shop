using OnlineShop.Domain.Models.Ads;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Contracts
{
    public interface IAdsRepository
    {
        Task<IEnumerable<Ads>> GetAllAdsAsync();
        Task<IEnumerable<Ads>> GetAdsForViewComponentAsync(int zoneId);
        Task CreateAdsAsync(Ads ads);
        Task UpdateAdsAsync(Ads ads);
        Task<Ads> GetAdsByIdAsync(int id);
        Task<bool> ExistAdsAsync(int id);
        Task PremanentDeleteAsync(Ads ads);
    }
}
