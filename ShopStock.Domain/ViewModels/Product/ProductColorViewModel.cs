using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Product
{
    public class ProductColorViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "عنوان رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Name { get; set; }

        [Display(Name = "کد رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Code { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public double Price { get; set; }

        [Display(Name = "پیش فرض")]
        public bool IsDefault { get; set; }
        
        [Display(Name = "موجودی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int Quantity { get; set; }

    }
}
