using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Configurations.ProductConfig
{
    public class ProductFeatureConfig : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(pf => pf.Id);
            builder.Property(pf => pf.ProductId).IsRequired();
            builder.Property(pf => pf.Name).IsRequired().HasMaxLength(200);
            builder.Property(pf => pf.Value).IsRequired();
        }
    }
}
