using ShopStock.Domain.Models.Products;
using ShopStock.Domain.ViewModels.Product;
namespace ShopStock.Application.Services.Interfaces
{
    public interface IProductColorService
    {
        Task CreateProductColor(ProductColorViewModel model);

        Task EditProductColor(ProductColorViewModel model);

        Task DeleteProductColor(int colorId);
        Task<IEnumerable<ProductColor>> GetAllProductColors(int productId);

        Task<ProductColor?> GetProductColorById(int colorId);

        Task<int> SetDefaultColorAsync(int colorId);
    }
}
