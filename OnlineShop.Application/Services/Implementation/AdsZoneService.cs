using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Ads;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Implementation
{
    public class AdsZoneService : IAdsZoneService
    {
        private readonly IAdsZoneRepository _adsZoneRepository;

        public AdsZoneService(IAdsZoneRepository adsZoneRepository)
        {
            _adsZoneRepository = adsZoneRepository;
        }

        public async Task<bool> AdsZoneExistsAsync(int zoneId)
        {
            return await _adsZoneRepository.AdsZoneExists(zoneId);
        }

        public async Task CreateAdsZoneAsync(AdsZone adsZone)
        {
            await _adsZoneRepository.CreateAdsZoneAsync(adsZone);
        }

        public async Task DeleteAdsZoneAsync(int zoneId)
        {
             await _adsZoneRepository.DeleteAdsZoneAsync(zoneId);
        }

        public async Task EditAdsZoneAsync(AdsZone model)
        {
            var adsZone=await _adsZoneRepository.GetAdsZoneByIdAsync(model.Id);

            adsZone.Title=model.Title;

            await _adsZoneRepository.UpdateAdZoneAsync(adsZone);
        }

        public async Task<AdsZone> GetAdsZoneByIdAsync(int zoneId)
        {
            return await _adsZoneRepository.GetAdsZoneByIdAsync(zoneId);
        }

        public async Task<IEnumerable<AdsZone>> GetAllAdsZonesAsync()
        {
            return await _adsZoneRepository.GetAllAdsZonesAsync();
        }
    }
}
