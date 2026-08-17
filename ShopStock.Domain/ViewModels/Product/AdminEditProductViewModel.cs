using Microsoft.AspNetCore.Http;
using ShopStock.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Product
{
    public class AdminEditProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Title { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public double Price { get; set; }

        [Display(Name = "توضیح مختصر")]
        public string? ShortDescription { get; set; }

        [Display(Name = "نقد و بررسی")]
        public string? Review { get; set; }

        [Display(Name = "بررسی تخصصی")]
        public string? DetailReview { get; set; }

        [Display(Name = "انتخاب تصویر")]
        public string? ImageName { get; set; }

        public IFormFile? ImageFile { get; set; }

        [Display(Name = "گالری تصاویر")]
        public IFormFile[]? Galleries { get; set; }

        [Display(Name = "فعال است؟")]
        public bool IsActive { get; set; }

        [Display(Name = "موجودی انبار")]
        public int Count { get; set; } = 0;

        public string? Tags { get; set; }

        public List<int> DeletedGalleryIds { get; set; } = [];

        public List<ProductGallery>? ProductGalleries { get; set; }

        public int? SubCategoryId { get; set; }

        public int? MainCategoryId { get; set; }
    }
}
