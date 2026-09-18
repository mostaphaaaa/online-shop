using OnlineShop.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Domain.Models.Users
{
    public class UserAddress:BaseEntity
    {
        public int UserId { get; set; }

        [MaxLength(300)]
        public required string Title { get; set; }

        [MaxLength(300)]
        public required string PostalCode { get; set; }

        [MaxLength(1500)]
        public required string Address { get; set; }


        #region Relations
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        #endregion
    }
}
