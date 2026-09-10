using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Products;
using ShopStock.Domain.ViewModels.Product;

namespace ShopStock.Application.Services.Implementation
{
    public class ProductColorService : IProductColorService
    {
        private readonly IProductColorsRepository _productColorsRepository;

        public ProductColorService(IProductColorsRepository productColorsRepository)
        {
            _productColorsRepository = productColorsRepository;
        }

        public async Task CreateProductColor(ProductColorViewModel model)
        {
            await _productColorsRepository.CreateProductColorAsync(new ProductColor
            {
                ProductId = model.ProductId,
                Name = model.Name,
                Code = model.Code,
                Price = model.Price,
                IsDefault = false,
                Quantity = model.Quantity,
                CreateDate = DateTime.Now,
                IsDelete = false  
            });
            await _productColorsRepository.SaveChangesAsync();
        }

        public async Task DeleteProductColor(int colorId)
        {
            await _productColorsRepository.DeleteById(colorId);
            await _productColorsRepository.SaveChangesAsync();
        }

        public async Task EditProductColor(ProductColorViewModel model)
        {
            var productColor = await _productColorsRepository.GetProductColorByIdAsync(model.Id);
            if (productColor == null) return;

            productColor.ProductId = model.ProductId;
            productColor.Name = model.Name;
            productColor.Code = model.Code;
            productColor.Price = model.Price;
            productColor.Quantity = model.Quantity;
            productColor.UpdateDate = DateTime.Now;

            await _productColorsRepository.UpdateAsync(productColor);
            await _productColorsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductColor>> GetAllProductColors(int productId)
        {
            return await _productColorsRepository.GetAllProductColorsAsync(productId);
        }

        public async Task<ProductColor?> GetProductColorById(int colorId)
        {
            return await _productColorsRepository.GetProductColorByIdAsync(colorId);
        }

        public async Task<int> SetDefaultColorAsync(int colorId)
        {
            var productcolor=await _productColorsRepository.GetProductColorByIdAsync(colorId);
            var colors=await _productColorsRepository.GetAllProductColorsAsync(productcolor.ProductId);
            foreach (var color in colors)
            {
                color.IsDefault = false;
            }
            productcolor.IsDefault = true;

            await _productColorsRepository.SaveChangesAsync();
            return productcolor.ProductId;
        }
    }
}
