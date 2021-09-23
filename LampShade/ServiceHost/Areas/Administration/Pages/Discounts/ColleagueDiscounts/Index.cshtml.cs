using System.Collections.Generic;
using DiscountManagement.Application.Contract.CollegueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {

        [TempData]public string Message { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscount;
        public ColleagueDiscountSearchModel Search;
        public SelectList Products;


        private readonly IColleagueDisCountApplication _colleagueDisCountApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IColleagueDisCountApplication colleagueDisCountApplication, IProductApplication productApplication)
        {
            _colleagueDisCountApplication = colleagueDisCountApplication;
            _productApplication = productApplication;
        }


        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscount = _colleagueDisCountApplication.Search(searchModel);

        }
        public IActionResult OnGetCreate()
        {
            var entity = new DefineColleagueDiscount {Products = _productApplication.GetProducts()};
            return Partial("./Create", entity);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDisCountApplication.Define(command);
            return new JsonResult(result);
        }


        public IActionResult OnGetEdit(long id)
        {
            var colleagueId = _colleagueDisCountApplication.GetDetails(id);
            colleagueId.Products = _productApplication.GetProducts();
            return Partial("./Edit", colleagueId);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDisCountApplication.Edit(command);
          return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            _colleagueDisCountApplication.Removed(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            _colleagueDisCountApplication.Restored(id);
            return RedirectToPage("./Index");
        }
    }
}