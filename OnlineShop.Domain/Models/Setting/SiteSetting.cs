using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Domain.Models.Setting
{
    public class SiteSetting
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        public string? SiteUrl { get; set; }
        [MaxLength(50)]
        public string? LogoName { get; set; }
        [MaxLength(800)]
        public string? CopyRightText { get; set; }
    }
}
