
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Services.Implementation;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Models.Ads;
using OnlineShop.Infra.Data.Context;
using OnlineShop.Web.Areas.Admin.Controllers;

public class AdsController : AdminBaseController
{
    private readonly IAdsZoneService _adsZoneService;
    private readonly IAdsService _adsService;

    public AdsController(EshopDbContext context, IAdsZoneService adsZoneService, IAdsService adsService)
    {
        _adsZoneService = adsZoneService;
        _adsService = adsService;
    }

    // GET: ADSS
    public async Task<IActionResult> Index()
    {
        var ads = await _adsService.GetAllAdsAsync();
        return View(ads);
    }

    // GET: ADSS/Create
    public async Task<IActionResult> Create()
    {
        var zones = await _adsZoneService.GetAllAdsZonesAsync();
        ViewBag.Zones = zones.Select(z => new SelectListItem
        {
            Value = z.Id.ToString(),
            Text = z.Title
        }).ToList();
        return View();
    }

    // POST: ADSS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ads ads, IFormFile imgUp, string? inputStartDate, string? inputExpireDate)
    {
        if (ModelState.IsValid)
        {
            await _adsService.CreateAdsAsync(ads, imgUp, inputStartDate, inputExpireDate);
            return RedirectToAction(nameof(Index));
        }
        return View(ads);
    }

    // GET: ADSS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ads = await _adsService.GetAdsByIdAsync(id.Value);
        if (ads == null)
        {
            return NotFound();
        }
        var zones = await _adsZoneService.GetAllAdsZonesAsync();
        ViewBag.Zones = zones.Select(z => new SelectListItem
        {
            Value = z.Id.ToString(),
            Text = z.Title,
            Selected = z.Id == ads.ZoneId

        }).ToList();
        return View(ads);
    }

    // POST: ADSS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Ads ads, IFormFile? imgUp, string? inputStartDate, string? inputExpireDate)
    {
        if (id != ads.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _adsService.EditAdsAsync(ads, imgUp, inputStartDate, inputExpireDate);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _adsService.ExistAdsAsync(ads.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(ads);
    }

    // GET: ADSS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ads = await _adsService.GetAdsByIdAsync(id.Value);
        if (ads == null)
        {
            return NotFound();
        }

        return View(ads);
    }

    // POST: ADSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var ads = await _adsService.GetAdsByIdAsync(id.Value);
        if (ads == null)
        {
            return NotFound();
        }
        await _adsService.SoftDeleteAdsAsync(id.Value);
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PermenantDelete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var slide = await _adsService.GetAdsByIdAsync(id.Value);
        await _adsService.PremanentDeleteAsync(slide);

        return RedirectToAction(nameof(Index));
    }
}
