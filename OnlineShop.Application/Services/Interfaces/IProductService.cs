using OnlineShop.Domain.Models.Products;
using OnlineShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<AdminFilterProductViewModel> FilterProductsAsync(AdminFilterProductViewModel filter);
        Task CreateProductAsync(AdminCreateProductViewModel model);
        Task<AdminEditProductViewModel> GetProductForEditAsync(int productId);

        Task DeleteImageGallery(int galleryId);
        Task<IEnumerable<ProductTag>> GetTagsAsync(string query);

        Task EditProductAsync(AdminEditProductViewModel model);

        Task<string?> GetProductTitleAsync(int productId);

        Task<IEnumerable<Product>> GetTopProductsForShowAsync();
        Task<Product> GetProductForShortDescById(int productId);
    }
}
