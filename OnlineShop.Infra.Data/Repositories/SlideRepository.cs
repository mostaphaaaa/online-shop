using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Slider;
using OnlineShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Repositories
{
    public class SlideRepository : ISlideRepository
    {
        private readonly EshopDbContext _context;

        public SlideRepository(EshopDbContext context)
        {
            _context = context;
        }
        public async Task CreateSlideAsync(Slide slide)
        {
            await _context.AddAsync(slide);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Slide>> GetAllSlidesAsync()
        {
            return await _context.Sliders.OrderBy(s => s.DeleteDate).ThenBy(s => s.CreateDate).ToListAsync();
        }

        public async Task<Slide> GetSlideByIdAsync(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }

        public async Task<Slide> GetSlideForVisitCountAsync(int slideId)
        {
            return await _context.Sliders.FirstOrDefaultAsync(s => s.Id == slideId && s.IsActive);
        }

        public async Task<IEnumerable<Slide>> GetSlidesForViewComponentAsync()
        {
            DateTime dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime dtEx = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var slides = await _context.Sliders.Where(s => !s.IsDelete && s.IsActive).ToListAsync();
            slides = slides.Where(s => s.StartDate == null || s.StartDate <= dtStart).ToList();
            slides = slides.Where(s => s.ExpireDate == null || s.ExpireDate >= dtEx).ToList();
            return slides;
        }

        public async Task PremanentDeleteAsync(Slide slide)
        {
            _context.Sliders.Remove(slide);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SlideExists(int? id)
        {
            return _context.Sliders.Any(e => e.Id == id);
        }

        public async Task UpdateSlideAsync(Slide slide)
        {
            _context.Update(slide);
            await _context.SaveChangesAsync();
        }
    }

}
