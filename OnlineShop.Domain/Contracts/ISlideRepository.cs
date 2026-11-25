using Microsoft.AspNetCore.Http;
using OnlineShop.Domain.Models.Slider;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Contracts
{
    public interface ISlideRepository
    {
       Task CreateSlideAsync(Slide slide);
        Task UpdateSlideAsync(Slide slide);
        Task<Slide> GetSlideByIdAsync(int id);
        Task<Slide> GetSlideForVisitCountAsync(int slideId);
        Task<bool> SlideExists(int? id);
        Task<IEnumerable<Slide>> GetAllSlidesAsync();
        Task<IEnumerable<Slide>> GetSlidesForViewComponentAsync();
        Task PremanentDeleteAsync(Slide slide);

    }
}
