using OnlineShop.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Domain.ViewModels.Product
{
    public class AdminFilterProductViewModel:BasePaging<ProductViewModel>
    {
        [Display(Name = "عنوان محصول")]
        public string? Title { get; set; }
    }
}
