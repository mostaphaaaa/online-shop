using OnlineShop.Domain.Models.Ads;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Interfaces
{
    public interface IAdsZoneService
    {
        Task<IEnumerable<AdsZone>> GetAllAdsZonesAsync();
        Task<AdsZone> GetAdsZoneByIdAsync(int zoneId);
        Task CreateAdsZoneAsync(AdsZone adsZone);
        Task EditAdsZoneAsync(AdsZone adsZone);
        Task<bool> AdsZoneExistsAsync(int zoneId);

        Task DeleteAdsZoneAsync(int zoneId);
    }
}
