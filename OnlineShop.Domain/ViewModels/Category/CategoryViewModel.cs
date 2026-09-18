using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace OnlineShop.Domain.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        [Display(Name = "عنوان دسته بندی")]
        public string Title { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
