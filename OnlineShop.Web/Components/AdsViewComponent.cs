using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Interfaces;

namespace OnlineShop.Web.Components
{
    public class AdsViewComponent : ViewComponent
    {
        private readonly IAdsService _adsService;

        public AdsViewComponent(IAdsService adsService)
        {
            _adsService = adsService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int zoneId)
        {
            var slides=await _adsService.GetAdsForViewComponent(zoneId);
            ViewBag.ZoneId = zoneId;
            return View(slides);
        }
    }
}
