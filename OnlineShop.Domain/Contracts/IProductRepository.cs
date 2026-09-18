using OnlineShop.Domain.Models.Products;
using OnlineShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Contracts
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<IQueryable<Product>> FilterAsync();
        Task<bool> IsExistAsync(int productId);
        Task AddProductGalleryAsync(ProductGallery productGallery);

        Task AddProductTagsAsync(int productId, List<ProductTagViewModel> list);
        Task<Product> GetProductForEditInAdminAsync(int productId);
        Task<Product> GetProductForShortDescByIdAsync(int productId);
        Task<IEnumerable<ProductTag>> GetAllTagsAsync(string query);

        Task<IEnumerable<ProductGallery>> GetProductGalleriesAsync(int productId);

        Task DeleteProductGalleryAsync(ProductGallery gallery);
        Task<ProductGallery> GetProductGalleryById(int galleryId);

        Task AddProductGalleryAsync(int productId,string imageName);

        Task DeleteAllProductTagsAsunc(int productId);

        Task <IQueryable<Product>> GetTopProductsForShowAsync();
    }
}
