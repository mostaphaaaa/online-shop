using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Products;
using OnlineShop.Infra.Data.Context;

namespace OnlineShop.Infra.Data.Repositories
{
    public class ProductColorsRepository : IProductColorsRepository
    {
        private readonly EshopDbContext _context;

        public ProductColorsRepository(EshopDbContext context)
        {
            _context = context;
        }
        public async Task CreateProductColorAsync(ProductColor color)
        {
            await _context.AddAsync(color);
        }

        public async Task DeleteById(int colorId)
        {
            var color =await _context.ProductColors.FindAsync(colorId);
            if (color == null)
                return;
            _context.ProductColors.Remove(color);
        }

        public async Task<IEnumerable<ProductColor>> GetAllProductColorsAsync(int productId)
        {
            return await _context.ProductColors.Where(c => c.ProductId == productId).ToListAsync();
        }

        public async Task<ProductColor?> GetProductColorByIdAsync(int colorId)
        {
            return await _context.ProductColors.FindAsync(colorId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductColor color)
        {
            _context.ProductColors.Update(color);
        }
    }
}
