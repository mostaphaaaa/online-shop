using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Models.Categories;
using OnlineShop.Domain.Models.Permission;
using OnlineShop.Domain.Models.Products;
using OnlineShop.Domain.Models.Roles;
using OnlineShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.Data.Context
{
    public class EshopDbContext(DbContextOptions<EshopDbContext> options) :
        DbContext(options)
    {
        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        #endregion

        #region Roles
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRoles> UserInRoles { get; set; }
        #endregion

        #region Permissions
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }
        #endregion

        #region Category
        public DbSet<Category> Categories { get; set; }
        #endregion

        #region Product
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Query Filters
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(u => !u.IsDelete);
            //modelBuilder.Entity<Category>().HasQueryFilter(u => !u.IsDelete);
            #endregion
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
