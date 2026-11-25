using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Interfaces;

namespace OnlineShop.Web.Components
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly ISlideService _slideService;

        public SliderViewComponent(ISlideService slideService)
        {
            _slideService = slideService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var slides=await _slideService.GetSlidesForViewComponent();
            return View(slides);
        }
    }
}
