using OnlineShop.Domain.Models.Common;
using OnlineShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Domain.Models.Products
{
    public class ProductComment:BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        [Display(Name ="عنوان  نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(700,ErrorMessage ="{0} نمیتواند بیشتر از {1} باشد")]
        public string Text { get; set; }


        //Relations
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
