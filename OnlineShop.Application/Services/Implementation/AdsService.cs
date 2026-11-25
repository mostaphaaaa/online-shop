using Microsoft.AspNetCore.Http;
using OnlineShop.Application.Convertors;
using OnlineShop.Application.Generator;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Ads;
using OnlineShop.Domain.Models.Slider;
using OnlineShop.Infra.Data.Repositories;

namespace OnlineShop.Application.Services.Implementation
{
    public class AdsService : IAdsService
    {
        private readonly IAdsRepository _adsRepository;

        public AdsService(IAdsRepository adsRepository)
        {
            _adsRepository = adsRepository;
        }

        public async Task CreateAdsAsync(Ads ads, IFormFile imgUp, string? inputStartDate, string? inputExpireDate)
        {
            if (!string.IsNullOrEmpty(inputStartDate))
            {
                ads.StartDate = DateConvertor.ToMiladi(inputStartDate);
            }

            if (!string.IsNullOrEmpty(inputExpireDate))
            {
                ads.ExpireDate = DateConvertor.ToMiladi(inputExpireDate);
            }
            ads.VisitCount = 0;
            ads.CreateDate = DateTime.Now;
            ads.IsDelete = false;
            ads.ImageName = await SaveImageFileAsync(imgUp);
            await _adsRepository.CreateAdsAsync(ads);
        }


        #region Utilities(Save and Delete Image File)
        private async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {

            if (imageFile == null) return "NoPhoto.jpg";

            var imageName = NameGenerator.GenerateUnicName() +
                Path.GetExtension(imageFile.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AdsImages", imageName);
            using (var stream = System.IO.File.Create(savePath))
            {
                await imageFile.CopyToAsync(stream);
            }

            return imageName;
        }

        private void DeleteImageFile(string imageName)
        {
            if (imageName != "NoPhoto.jpg")
            {
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AdsImages", imageName);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
            }
        }
        #endregion

        public async Task<IEnumerable<Ads>> GetAllAdsAsync()
        {
            return await _adsRepository.GetAllAdsAsync();
        }

        public async Task EditAdsAsync(Ads ads, IFormFile? imgUp, string? inputStartDate, string? inputExpireDate)
        {
            if(imgUp != null)
            {
                DeleteImageFile(ads.ImageName);
                ads.ImageName = await SaveImageFileAsync(imgUp);
            }


            ads.UpdateDate = DateTime.Now;
            if (!string.IsNullOrEmpty(inputStartDate))
            {
                ads.StartDate = DateConvertor.ToMiladi(inputStartDate);
            }
            else
            {
                ads.StartDate = null;
            }
            if (!string.IsNullOrEmpty(inputExpireDate))
            {
                ads.ExpireDate = DateConvertor.ToMiladi(inputExpireDate);
            }
            else
            {
                ads.ExpireDate = null;
            }


            await _adsRepository.UpdateAdsAsync(ads);
        }

        public Task<Ads> GetAdsByIdAsync(int id)
        {
            return _adsRepository.GetAdsByIdAsync(id);
        }

        public async Task SoftDeleteAdsAsync(int id)
        {
            var ads = await _adsRepository.GetAdsByIdAsync(id);
            ads.IsDelete = true;
            ads.DeleteDate = DateTime.Now;

            await _adsRepository.UpdateAdsAsync(ads);
        }

        public async Task<bool> ExistAdsAsync(int id)
        {
            return await _adsRepository.ExistAdsAsync(id);
        }

        public async Task<IEnumerable<Ads>> GetAdsForViewComponent(int zoneId)
        {
            return await _adsRepository.GetAdsForViewComponentAsync(zoneId);
        }

        public async Task PremanentDeleteAsync(Ads ads)
        {
            DeleteImageFile(ads.ImageName);
            await _adsRepository.PremanentDeleteAsync(ads);
        }
    }
}
