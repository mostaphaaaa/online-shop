using Microsoft.AspNetCore.Http;
using OnlineShop.Application.Convertors;
using OnlineShop.Application.Generator;
using OnlineShop.Application.Mapper;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Application.Utilities;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Products;
using OnlineShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace OnlineShop.Application.Services.Implementation
{
    public class ProductService(IProductRepository _productRepository) : IProductService
    {
        public async Task<AdminFilterProductViewModel> FilterProductsAsync(AdminFilterProductViewModel filter)
        {
            var query = await _productRepository.FilterAsync();
            query = query.Where(p => !p.IsDelete);

            //Filter the query based on the filter.Title property
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(p => p.Title.Contains(filter.Title));
            }

            //Sort the query based on the filter.SortBy property
            query = query.OrderByDescending(p => p.CreateDate);

            await filter.Paging(ProductMapper.MapToProductViewModel(query));

            return filter;
        }
        public async Task CreateProductAsync(AdminCreateProductViewModel model)
        {
            var imageName = await SaveImageFileAsync(model.ImageFile);

            var product = ProductMapper.MapToProduct(model, imageName);
            _productRepository.Add(product);
            await _productRepository.SaveAsync();

            #region Save Product Tags
            if (!string.IsNullOrEmpty(model.Tags))
            {
                var tags = JsonSerializer.Deserialize<List<ProductTagViewModel>>(model.Tags);
                await _productRepository.AddProductTagsAsync(product.Id, tags);
                await _productRepository.SaveAsync();
            }
            #endregion


            #region Add Product Galleries
            if (model.Galleries != null && model.Galleries.Any())
            {
                foreach (var img in model.Galleries)
                {
                    string galleryImageName = await SaveImageFileAsync(img);
                    ProductGallery gallery = new ProductGallery
                    {
                        ProductId = product.Id,
                        Alt = product.Title,
                        CreateDate = DateTime.Now,
                        ImageName = galleryImageName,
                        IsDelete = false,
                    };
                    await _productRepository.AddProductGalleryAsync(gallery);
                    await _productRepository.SaveAsync();
                }
            }
            #endregion
        }



        public Task<IEnumerable<ProductTag>> GetTagsAsync(string query)
        {
            return _productRepository.GetAllTagsAsync(query);
        }

        public async Task<AdminEditProductViewModel> GetProductForEditAsync(int productId)
        {
            AdminEditProductViewModel model = new AdminEditProductViewModel();
            var product = await _productRepository.GetProductForEditInAdminAsync(productId);


            model.ProductGalleries = product.ProductGalleries?.ToList();

            model.Tags = string.Join(",", product.ProductTags.Select(t => t.TagTitle));

            model.Review = product.Review;
            model.Price = product.Price;
            model.CategoryId = product.CategoryId;
            model.Count = product.Count;
            model.DetailReview = product.DetailReview;
            model.ImageName = product.ImageName;
            model.IsActive = product.IsActive;
            model.ShortDescription = product.ShortDescription;
            model.Title = product.Title;

            //if (product.Category.Parent?.Parent == null)
            //{
            //    model.MainCategoryId = product.Category.Parent.Id;
            //    model.SubCategoryId = product.Category.Id;
            //    model.CategoryId = product.Category.Id;  // سطح دوم
            //}
            //else
            //{
            model.MainCategoryId = product.Category.Parent.Parent.Id;
            model.SubCategoryId = product.Category.Parent.Id;
            model.CategoryId = product.Category.Id;  // سطح سوم
            //}

            return model;
        }

        public async Task EditProductAsync(AdminEditProductViewModel model)
        {
            var product = await _productRepository.GetProductForEditInAdminAsync(model.Id);

            #region  Map Product
            product.Title = model.Title;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.Count = model.Count;
            product.DetailReview = model.DetailReview;
            product.IsActive = model.IsActive;
            product.ShortDescription = model.ShortDescription;
            product.Review = model.Review;
            product.UpdateDate = DateTime.Now;
            #endregion

            #region Save Product Image
            // Save Product Image
            if (model.ImageFile != null)
            {
                // delete existing only if there is a real image
                if (!string.IsNullOrEmpty(product.ImageName) && product.ImageName != "NoPhoto.jpg")
                {
                    DeleteImageFile(product.ImageName);
                }

                // always save the new uploaded image
                product.ImageName = await SaveImageFileAsync(model.ImageFile);

            }
            else
            {
                // if no new image is uploaded, keep the existing image name
                model.ImageName = product.ImageName;
            }
            #endregion

            #region Edit Product Tags
            await _productRepository.DeleteAllProductTagsAsunc(product.Id);   //حذف همه تگ‌های محصول قبل از افزودن تگ‌های جدید
            if (!string.IsNullOrEmpty(model.Tags))
            {
                var tags = JsonSerializer.Deserialize<List<ProductTagViewModel>>(model.Tags);
                await _productRepository.AddProductTagsAsync(product.Id, tags);
            }   
            #endregion

            #region Edit Product Galleries 
            if (model.DeletedGalleryIds?.Any() == true)
            {
                foreach (var galleryId in model.DeletedGalleryIds)
                {
                    var gallery = product.ProductGalleries?
                        .FirstOrDefault(g => g.Id == galleryId);

                    if (gallery == null)
                        continue;

                    // حذف فایل تصویر اصلی و تصویر بندانگشتی
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages", gallery.ImageName);
                    var thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/Thumb", gallery.ImageName);
                    FileHelper.DeleteFile(path);
                    FileHelper.DeleteFile(thumbPath);

                    // حذف از دیتابیس
                    await _productRepository.DeleteProductGalleryAsync(gallery);
                }
            }



            //Add new photo to gallery
            if (model.Galleries != null && model.Galleries.Any())
            {
                foreach (var img in model.Galleries)
                {
                    string galleryImageName = await SaveImageFileAsync(img);
                    ProductGallery gallery = new ProductGallery
                    {
                        ProductId = product.Id,
                        Alt = product.Title,
                        CreateDate = DateTime.Now,
                        ImageName = galleryImageName,
                        IsDelete = false,
                    };
                    await _productRepository.AddProductGalleryAsync(gallery);
                }
            }
            #endregion

            _productRepository.Update(product);
            await _productRepository.SaveAsync();
        }
            

            #region Utilities (Save & Delete Image File)
        private async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {

            if (imageFile == null) return "NoPhoto.jpg";

            var imageName = NameGenerator.GenerateUnicName() +
                Path.GetExtension(imageFile.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages", imageName);
            using (var stream = System.IO.File.Create(savePath))
            {
                await imageFile.CopyToAsync(stream);
            }

            ImageResizer imageResizer = new ImageResizer();
            var thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImages/Thumb", imageName);

            imageResizer.ImageResize(savePath, thumbPath, 120, 210);

            return imageName;
        }


        private void DeleteImageFile(string imageName)
        {
            string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages", imageName);
            string thumbDeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages", "Thumb", imageName);
            FileHelper.DeleteFile(deletePath);
            FileHelper.DeleteFile(thumbDeletePath);
        }
        #endregion

        public async Task DeleteImageGallery(int galleryId)
        {
            var gallery = await _productRepository.GetProductGalleryById(galleryId);
            DeleteImageFile(gallery.ImageName);
            await _productRepository.DeleteProductGalleryAsync(gallery);
            await _productRepository.SaveAsync();
        }

        public async Task<string?> GetProductTitleAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            return product?.Title;
        }

        public async Task<IEnumerable<Product>> GetTopProductsForShowAsync()
        {
            var data =await _productRepository.GetTopProductsForShowAsync();
            return data;
        }

        public async Task<Product> GetProductForShortDescById(int productId)
        {
            return await _productRepository.GetProductForShortDescByIdAsync(productId);
        }
    }
}
