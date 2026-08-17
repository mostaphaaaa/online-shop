using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configurations.RoleConfig
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoleName).IsRequired()
                .HasMaxLength(200);


            #region Relation
            builder.HasMany(x => x.UserInRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);
            #endregion

        }
    }
}
