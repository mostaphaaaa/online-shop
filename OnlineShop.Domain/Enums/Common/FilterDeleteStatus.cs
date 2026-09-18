using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Domain.Enums.Common
{
    public enum FilterDeleteStatus
    {
        [Display(Name = "همه")] All,
        [Display(Name = "حذف نشده")] NotDeleted,
        [Display(Name = "حذف شده")] Deleted,
    }
}
