using OnlineShop.Domain.Enums.Common;
using OnlineShop.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Domain.ViewModels.Category
{
    public class AdminFilterCategoryViewModel : BasePaging<CategoryViewModel>
    {
        public int? ParentId { get; set; }
        [Display(Name = "عنوان دسته بندی")]
        public string? Title { get; set; }
        [Display(Name = "وضغیت حذف")]
        public FilterDeleteStatus DeleteStatus { get; set; }

    }
}
