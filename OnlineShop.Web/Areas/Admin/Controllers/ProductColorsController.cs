
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Services.Implementation;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Models.Products;
using OnlineShop.Domain.ViewModels.Product;
using OnlineShop.Infra.Data.Context;
using OnlineShop.Web.Areas.Admin.Controllers;

public class ProductColorsController : AdminBaseController
{
    private readonly EshopDbContext _context;
    private readonly IProductColorService _productColorService;
    private readonly IProductService productService;

    public ProductColorsController(IProductColorService productColorService, IProductService productService)
    {
        _productColorService = productColorService;
        this.productService = productService;
    }

    // GET: PRODUCTCOLORS
    public async Task<IActionResult> Index(int? id)    
    {
        if (id==null)
            return BadRequest("Product ID is required to view colors.");

        ViewBag.ProductId = id.Value;
        ViewBag.ProductTitle= await productService.GetProductTitleAsync(id.Value);

        var productColors = await _productColorService.GetAllProductColors(id.Value);
        if(productColors==null)
            return NotFound();

        return View(productColors);
    }

    // GET: PRODUCTCOLORS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var productcolor = await _context.ProductColors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (productcolor == null)
        {
            return NotFound();
        }

        return View(productcolor);
    }

    // GET: PRODUCTCOLORS/Create
    public IActionResult Create(int? id)
    {
        if (id == null)
            return BadRequest();
        var model = new ProductColorViewModel
        {
            ProductId = id.Value
        };

        return View(model);
    }

    // POST: PRODUCTCOLORS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductColorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _productColorService.CreateProductColor(model);
        return RedirectToAction(nameof(Index), new { id = model.ProductId });
    }

    // GET: PRODUCTCOLORS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var productcolor = await _productColorService.GetProductColorById(id.Value);
        if (productcolor == null)
        {
            return NotFound();
        }
        ProductColorViewModel model = new ProductColorViewModel
        {
            Id = productcolor.Id,
            ProductId = productcolor.ProductId,
            Name = productcolor.Name,
            Code = productcolor.Code,
            Price = productcolor.Price,
            IsDefault = productcolor.IsDefault,
            Quantity = productcolor.Quantity
        };
        return View(model);
    }

    // POST: PRODUCTCOLORS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, ProductColorViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _productColorService.EditProductColor(model);
        return RedirectToAction(nameof(Index), new { id = model.ProductId });
    }

    // GET: PRODUCTCOLORS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var color = await _productColorService.GetProductColorById(id.Value);
        if (color == null)
            return NotFound();

        return View(color);
    }

    // POST: PRODUCTCOLORS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
            return NotFound();

        var color = await _productColorService.GetProductColorById(id.Value);
        if (color == null)
            return NotFound();
        await _productColorService.DeleteProductColor(id.Value);

        return RedirectToAction(nameof(Index), new { id = color.ProductId });
    }


    public async Task<IActionResult> IsDefault(int? id)
    {
        if(id== null)
            return BadRequest();

        //Sets the default color for a product and returns the product ID to redirect to the Index action.
        var productId = await _productColorService.SetDefaultColorAsync(id.Value);
        return RedirectToAction(nameof(Index), new { id = productId });
    }

}
