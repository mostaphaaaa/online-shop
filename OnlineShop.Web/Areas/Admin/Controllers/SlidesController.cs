
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Contracts;
using OnlineShop.Domain.Models.Slider;
using OnlineShop.Infra.Data.Context;
using OnlineShop.Web.Areas.Admin.Controllers;

public class SlidesController : AdminBaseController
{
    private readonly ISlideService _slideService;

    public SlidesController(EshopDbContext context, ISlideService slideService, ISlideRepository slideRepository)
    {
        _slideService = slideService;
    }

    // GET: SLIDES
    public async Task<IActionResult> Index()
    {
        var slides = await _slideService.GetAllSlidesAsync();
        return View(slides);
    }

    // GET: SLIDES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slide = await _slideService.GetSlideByIdAsync(id.Value);
        if (slide == null)
        {
            return NotFound();
        }

        return View(slide);
    }

    // GET: SLIDES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SLIDES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Slide slide, IFormFile? imgUp, string? inputStartDate, string? inputExpireDate)
    {

        if (ModelState.IsValid)
        {
            if (imgUp == null)
            {
                ModelState.AddModelError("ImageName", "لطفا تصویر اسلاید را انتخاب کنید");
                return View(slide);
            }

            await _slideService.CreateSlideAsync(slide, imgUp, inputStartDate, inputExpireDate);
            return RedirectToAction(nameof(Index));
        }
        return View(slide);
    }

    // GET: SLIDES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slide = await _slideService.GetSlideByIdAsync(id.Value);
        if (slide == null)
        {
            return NotFound();
        }
        return View(slide);
    }

    // POST: SLIDES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Slide slide
       , IFormFile? imgUp, string? inputStartDate, string? inputExpireDate)
    {
        if (id != slide.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            await _slideService.UpdateSlideAsync(slide, imgUp, inputStartDate, inputExpireDate);
            return RedirectToAction(nameof(Index));
        }
        return View(slide);
    }

    // GET: SLIDES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var slide = await _slideService.GetSlideByIdAsync(id.Value);
        if (slide == null)
        {
            return NotFound();
        }

        return View(slide);
    }

    // POST: SLIDES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var slide = await _slideService.GetSlideByIdAsync(id.Value);
        if (slide != null)
        {
            await _slideService.SoftDeleteSlide(id.Value);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PermenantDelete(int? id)
    {
        if(id==null)
        {
            return NotFound();
        }
        var slide=await _slideService.GetSlideByIdAsync(id.Value);
        await _slideService.PremanentDelete(slide);

        return RedirectToAction(nameof(Index));
    }
}
