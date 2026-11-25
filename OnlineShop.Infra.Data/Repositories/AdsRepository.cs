using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Ads;
using OnlineShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Repositories
{
    public class AdsRepository : IAdsRepository
    {
        private readonly EshopDbContext _context;

        public AdsRepository(EshopDbContext context)
        {
            _context = context;
        }
        public async Task CreateAdsAsync(Ads ads)
        {
            await _context.Ads.AddAsync(ads);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAdsAsync(int id)
        {
            return await _context.Ads.AnyAsync(a => a.Id == id);
        }

        public async Task<Ads> GetAdsByIdAsync(int id)
        {
            return await _context.Ads
                 .Include(a => a.AdsZone)
                 .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Ads>> GetAdsForViewComponentAsync(int zoneId)
        {
            DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime dtEx = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var ads = await _context.Ads.Where(s => !s.IsDelete && s.IsActive && s.ZoneId == zoneId).ToListAsync();
            ads = ads.Where(s => s.StartDate == null || s.StartDate <= dtStart).ToList();
            ads = ads.Where(s => s.ExpireDate == null || s.ExpireDate >= dtEx).ToList();
            return ads;
        }

        public async Task<IEnumerable<Ads>> GetAllAdsAsync()
        {
            return await _context.Ads
                .Include(a => a.AdsZone)
                .OrderBy(a => a.DeleteDate)
                .ThenBy(a => a.CreateDate)
                .ToListAsync();
        }

        public async Task PremanentDeleteAsync(Ads ads)
        {
             _context.Ads.Remove(ads);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdsAsync(Ads ads)
        {
            _context.Ads.Update(ads);
            await _context.SaveChangesAsync();
        }
    }
}
