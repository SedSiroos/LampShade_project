using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slides;

namespace ServiceHost.Areas.Adminstration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        private readonly ISlideApplication _slideApplication;
        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        [TempData] public string Message { get; set; }
        public List<SlideViewMode> Slides;

        public void OnGet()
        {
            Slides = _slideApplication.GetList();
        }

        public IActionResult OnGetCreate()
        {
            
            return Partial("./Create", new CreateSlide());
        }

        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
           var product = _slideApplication.GetDetails(id);
           return Partial("Edit", product);
        }

        public JsonResult OnPostEdit(EditSlide command)
        {
            var result=_slideApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var picture = _slideApplication.Remove(id);
            if (picture.IsSucceeded)
                return RedirectToPage("./Index");

            Message = picture.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var picture = _slideApplication.Restore(id);
            if (picture.IsSucceeded)
                return RedirectToPage("./Index");

            Message = picture.Message;
            return RedirectToPage("./Index");
        }
    }
}
