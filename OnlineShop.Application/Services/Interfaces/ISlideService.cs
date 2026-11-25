using Microsoft.AspNetCore.Http;
using OnlineShop.Domain.Models.Slider;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Interfaces
{
    public interface ISlideService
    {
        Task CreateSlideAsync(Slide slide, IFormFile imgUp,string? inputStartDate,string? inputExpireDate);
        Task UpdateSlideAsync(Slide slide, IFormFile imgUp,string? inputStartDate,string? inputExpireDate);
        Task UpdateSlideAsync(Slide slide);
        Task<Slide> GetSlideByIdAsync(int slideId);
        Task<Slide> GetSlideForVisitCountAsync(int slideId);
        Task<IEnumerable<Slide>> GetSlidesForViewComponent();
        Task<IEnumerable<Slide>> GetAllSlidesAsync();
        Task SoftDeleteSlide(int slideId);
        Task PremanentDelete(Slide slide);
        Task<string> VisitSlideAsync(int id, bool increment);
    }
}
