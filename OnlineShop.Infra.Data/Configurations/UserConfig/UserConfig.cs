using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Models.Roles;
using OnlineShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Configurations.UserConfig
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Key
            builder.HasKey(u=>u.Id);
            #endregion

            #region Validations
            builder.Property(u=>u.FirstName).HasMaxLength(200);
            builder.Property(u=>u.LastName).HasMaxLength(200);
            builder.Property(u=>u.UserName).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.Property(u => u.NationalCode).HasMaxLength(15);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(400);
            builder.Property(u => u.EmailActiveCode).HasMaxLength(50);


            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => p.UserName).IsUnique();
            builder.HasIndex(p => p.Mobile).IsUnique();
            #endregion

            #region Relation
            builder.HasMany(u=>u.UserInRoles)
                .WithOne(r=>r.User)
                .HasForeignKey(r=>r.UserId);
            #endregion
        }
    }
}
