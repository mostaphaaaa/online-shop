using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Domain.Models.Ads
{
    public class AdsZone
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="محل تبلیغات")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "حداکثر طول محل تبلیغات 200 کاراکتر می باشد")]
        public string Title { get; set; }


        //Navigation Properties
        public List<Ads>? Ads { get; set; }
    }
}
