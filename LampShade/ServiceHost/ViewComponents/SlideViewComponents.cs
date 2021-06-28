using _01_LampShadeQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponents : ViewComponent
    {
        private readonly ISlideQuery _slide;

        public SlideViewComponents(ISlideQuery slide)
        {
            _slide = slide;
        }

        public IViewComponentResult Invoke()
        {
            var slides = _slide.GetSlide();
            return View(slides);
        }
    }
}
