using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Ads;
using OnlineShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Infra.Data.Repositories
{
    public class AdsZoneRepository : IAdsZoneRepository
    {
        private readonly EshopDbContext _context;

        public AdsZoneRepository(EshopDbContext context)
        {
            _context = context;
        }

        public async Task CreateAdsZoneAsync(AdsZone adsZone)
        {
            await _context.AdsZones.AddAsync(adsZone);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdZoneAsync(AdsZone adsZone)
        {
            _context.AdsZones.Update(adsZone);
            await _context.SaveChangesAsync();
        }

        public async Task<AdsZone> GetAdsZoneByIdAsync(int adaZoneId)
        {
            return await _context.AdsZones.FindAsync(adaZoneId);
        }

        public async Task<IEnumerable<AdsZone>> GetAllAdsZonesAsync()
        {
            return await _context.AdsZones.ToListAsync();
        }

        public async Task<bool> AdsZoneExists(int id)
        {
            return await _context.AdsZones.AnyAsync(a=>a.Id==id);
        }

        public async Task DeleteAdsZoneAsync(int adsZoneId)
        {
            var adsZone = await GetAdsZoneByIdAsync(adsZoneId);
             _context.AdsZones.Remove(adsZone);
            await _context.SaveChangesAsync();
        }
    }
}
