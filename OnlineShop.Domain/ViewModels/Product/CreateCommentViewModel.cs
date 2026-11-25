using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Domain.ViewModels.Product
{
    public class CreateCommentViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "عنوان  نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(700, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Text { get; set; }
    }
}
