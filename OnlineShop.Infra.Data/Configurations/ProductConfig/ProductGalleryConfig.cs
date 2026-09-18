using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Configurations.ProductConfig
{
    public class ProductGalleryConfig : IEntityTypeConfiguration<ProductGallery>
    {
        public void Configure(EntityTypeBuilder<ProductGallery> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.ImageName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Alt).IsRequired().HasMaxLength(200);
        }
    }
}
