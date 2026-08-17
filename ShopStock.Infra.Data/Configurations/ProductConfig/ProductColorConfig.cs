using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configurations.ProductConfig
{
    public class ProductColorConfig : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Code).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        }
    }
}
