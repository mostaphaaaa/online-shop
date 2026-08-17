using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Category
{
    public class AdminEditCategoryViewModel
    {
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }

        [DisplayName("عنوان دسته بندی")]
        [Required(ErrorMessage = ("لطفا {0} را وارد کنید"))]
        public string Title { get; set; }


        [DisplayName("عنوان آدرس بار")]
        [Required(ErrorMessage = ("لطفا {0} را وارد کنید"))]
        public string Slug { get; set; }


        public string? CategoryParentTitle { get; set; }

        [DisplayName("تصویر گروه")]
        public IFormFile? Image { get; set; }

        public string ImageName { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
