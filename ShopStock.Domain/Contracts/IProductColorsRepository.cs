using ShopStock.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Contracts
{
    public interface IProductColorsRepository
    {
        Task<IEnumerable<ProductColor>> GetAllProductColorsAsync(int productId);
        Task<ProductColor?> GetProductColorByIdAsync(int colorId);

        Task CreateProductColorAsync(ProductColor color);
        Task UpdateAsync(ProductColor color);
        Task DeleteById(int colorId);
        Task SaveChangesAsync();
    }
}
