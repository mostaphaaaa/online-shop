using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configurations.ProductConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Title).IsRequired().HasMaxLength(300);
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Price).IsRequired();


        }
    }
}
