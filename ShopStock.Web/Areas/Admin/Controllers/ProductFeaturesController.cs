
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Models.Products;
using ShopStock.Domain.ViewModels.Product;
using ShopStock.Infra.Data.Context;
using ShopStock.Web.Areas.Admin.Controllers;

public class ProductFeaturesController : AdminBaseController
{
    private readonly EshopDbContext _context;
    private readonly IProductService _productService;
    private readonly IProductFeatureService _productFeatureService;

    public ProductFeaturesController(IProductService productService, IProductFeatureService productFeatureService)
    {
        _productService = productService;
        _productFeatureService = productFeatureService;
    }


    public async Task<IActionResult> Index(int? id)
    {
        if (id == null)
            return BadRequest();

        var productTitle = await _productService.GetProductTitleAsync(id.Value);
        if(productTitle == null)
            return NotFound();

        ViewBag.Product = "ویژگی ها " + productTitle;
        ViewBag.ProductId = id.Value;

        var features = await _productFeatureService.GetAllProductFeatures(id.Value);

        return View(features);
    }



    public IActionResult Create(int? id)
    {
        if (id == null) return BadRequest();

        var model = new ProductFeatureViewModel
        {
            ProductId = id.Value
        };

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFeatureViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _productFeatureService.CreateProductFeature(model);
        return RedirectToAction(nameof(Index), new { id = model.ProductId });
    }


    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var productFeature = await _productFeatureService.GetProductFeatureById(id.Value);
        if (productFeature == null)
        {
            return NotFound();
        }

        ProductFeatureViewModel model = new ProductFeatureViewModel();

        model.Id = productFeature.Id;
        model.Name = productFeature.Name;
        model.Value = productFeature.Value;
        model.ProductId = productFeature.ProductId;

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, ProductFeatureViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _productFeatureService.EditProductFeature(model);
        return RedirectToAction(nameof(Index), new { id = model.ProductId });
    }


    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var feature = await _productFeatureService.GetProductFeatureById(id.Value);
        if (feature == null)
            return NotFound();

        return View(feature);
    }


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
            return NotFound();

        var feature = await _productFeatureService.GetProductFeatureById(id.Value);
        if (feature == null)
            return NotFound();
        await _productFeatureService.DeleteProductFeature(id.Value);

        return RedirectToAction(nameof(Index), new { id = feature.ProductId });

    }
}
