using OnlineShop.Domain.Models.Ads;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Contracts
{
    public interface IAdsZoneRepository
    {
        Task<IEnumerable<AdsZone>> GetAllAdsZonesAsync();
        Task<AdsZone> GetAdsZoneByIdAsync(int adaZoneId);
        Task CreateAdsZoneAsync(AdsZone adsZone);
        Task UpdateAdZoneAsync(AdsZone adsZone);
        Task DeleteAdsZoneAsync(int adsZoneId);

        Task<bool> AdsZoneExists(int id);

    }
}
