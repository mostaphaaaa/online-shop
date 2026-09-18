using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application.Services.Implementation;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Contracts;
using OnlineShop.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infra.IOC
{
    public static class DIContainers
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductFeaturesRepository, ProductFeaturesRepository>();
            services.AddScoped<IProductColorsRepository, ProductColorsRepository>();
            #endregion

            #region Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductFeatureService, ProductFeatureService>();
            services.AddScoped<IProductColorService, ProductColorService>();
            #endregion
        }
    }
}
