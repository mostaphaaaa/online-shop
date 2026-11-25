using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Convertors;
using OnlineShop.Application.Generator;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Slider;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Implementation
{
    public class SlideService(ISlideRepository _slideRepository) : ISlideService
    {
        public async Task CreateSlideAsync(Slide slide, IFormFile imgUp, string? inputStartDate, string? inputExpireDate)
        {
            if (!string.IsNullOrEmpty(inputStartDate))
            {
                slide.StartDate = DateConvertor.ToMiladi(inputStartDate);
            }

            if (!string.IsNullOrEmpty(inputExpireDate))
            {
                slide.ExpireDate = DateConvertor.ToMiladi(inputExpireDate);
            }


            slide.CreateDate = DateTime.Now;
            slide.VisitCount = 0;
            slide.ImageName = await SaveImageFileAsync(imgUp);
            await _slideRepository.CreateSlideAsync(slide);
        }

        public async Task UpdateSlideAsync(Slide slide, IFormFile imgUp, string? inputStartDate, string? inputExpireDate)
        {
            if (imgUp != null)
            {
                DeleteImageFile(slide.ImageName);
                slide.ImageName = await SaveImageFileAsync(imgUp);
            }
            slide.UpdateDate = DateTime.Now;
            if (!string.IsNullOrEmpty(inputStartDate))
            {
                slide.StartDate = DateConvertor.ToMiladi(inputStartDate);
            }
            if (!string.IsNullOrEmpty(inputExpireDate))
            {
                slide.ExpireDate = DateConvertor.ToMiladi(inputExpireDate);
            }
            try
            {
                await _slideRepository.UpdateSlideAsync(slide);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _slideRepository.SlideExists(slide.Id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        #region Utilities (Save & Delete Image File)
        private async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {

            if (imageFile == null) return "NoPhoto.jpg";

            var imageName = NameGenerator.GenerateUnicName() +
                Path.GetExtension(imageFile.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Sliders", imageName);
            using (var stream = System.IO.File.Create(savePath))
            {
                await imageFile.CopyToAsync(stream);
            }

            return imageName;
        }

        private void DeleteImageFile(string imageName)
        {
            if (imageName != "NoPhoto.jpg" && imageName != null)
            {
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Sliders", imageName);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
            }
        }
        #endregion
        public async Task SoftDeleteSlide(int slideId)
        {
            var slide = await _slideRepository.GetSlideByIdAsync(slideId);
            slide.IsDelete = true;
            slide.DeleteDate = DateTime.Now;
            await _slideRepository.UpdateSlideAsync(slide);
        }

        public async Task<Slide> GetSlideByIdAsync(int slideId)
        {
            return await _slideRepository.GetSlideByIdAsync(slideId);
        }

        public async Task<IEnumerable<Slide>> GetAllSlidesAsync()
        {
            return await _slideRepository.GetAllSlidesAsync();
        }

        public async Task<IEnumerable<Slide>> GetSlidesForViewComponent()
        {
            return await _slideRepository.GetSlidesForViewComponentAsync();
        }

        public async Task PremanentDelete(Slide slide)
        {
            DeleteImageFile(slide.ImageName);
            await _slideRepository.PremanentDeleteAsync(slide);
        }

        public Task<Slide> GetSlideForVisitCountAsync(int slideId)
        {
            return _slideRepository.GetSlideForVisitCountAsync(slideId);
        }

        public async Task UpdateSlideAsync(Slide slide)
        {
            await _slideRepository.UpdateSlideAsync(slide);
        }

        public async Task<string> VisitSlideAsync(int id, bool increment)
        {
            //GetSlideForVisitCountAsync returns an active slide by id
            var slide = await _slideRepository.GetSlideForVisitCountAsync(id);
            if (slide == null)
            {
                return "";
            }
            if (increment)
            {
                slide.VisitCount += 1;
                await _slideRepository.UpdateSlideAsync(slide);
            }

            return slide.Url;
        }
    }
}