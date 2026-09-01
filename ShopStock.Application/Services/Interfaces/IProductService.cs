using ShopStock.Domain.Models.Products;
using ShopStock.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
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
    }
}
