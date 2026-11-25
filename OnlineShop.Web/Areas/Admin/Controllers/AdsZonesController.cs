
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Services.Interfaces;
using OnlineShop.Domain.Models.Ads;
using OnlineShop.Infra.Data.Context;
using OnlineShop.Web.Areas.Admin.Controllers;

public class AdsZonesController : AdminBaseController
{
    private readonly IAdsZoneService _adsZoneService;

    public AdsZonesController(EshopDbContext context, IAdsZoneService adsZoneService)
    {
        _adsZoneService = adsZoneService;
    }

    // GET: ADSZONES
    public async Task<IActionResult> Index()
    {
        var adsZones = await _adsZoneService.GetAllAdsZonesAsync();
        return View(adsZones);
    }

    // GET: ADSZONES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ADSZONES/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdsZone adszone)
    {
        if (ModelState.IsValid)
        {
            await _adsZoneService.CreateAdsZoneAsync(adszone);
            return RedirectToAction(nameof(Index));
        }
        return View(adszone);
    }

    // GET: ADSZONES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var adszone = await _adsZoneService.GetAdsZoneByIdAsync(id.Value);
        if (adszone == null)
        {
            return NotFound();
        }
        return View(adszone);
    }

    // POST: ADSZONES/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, AdsZone adszone)
    {
        if (id != adszone.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _adsZoneService.EditAdsZoneAsync(adszone);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _adsZoneService.AdsZoneExistsAsync(adszone.Id))
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
        return View(adszone);
    }

    // GET: ADSZONES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var adszone = await _adsZoneService.GetAdsZoneByIdAsync(id.Value);
        if (adszone == null)
        {
            return NotFound();
        }

        return View(adszone);
    }

    // POST: ADSZONES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var adszone = await _adsZoneService.GetAdsZoneByIdAsync(id.Value);
        if (adszone == null)
        {
            return NotFound();
        }

        await _adsZoneService.DeleteAdsZoneAsync(id.Value);


        return RedirectToAction(nameof(Index));
    }
}
