using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Domain.Models.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Display(Name ="تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime? UpdateDate { get; set; }
        [Display(Name = "تاریخ حذف")]
        public DateTime? DeleteDate { get; set; }
        [Display(Name = "حذف شده؟")]
        public bool IsDelete { get; set; }

    }
}
