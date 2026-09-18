using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Configurations.CategoryConfig
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(250).IsRequired();
            builder.Property(x => x.ImageName).IsRequired();
        }
    }
}
