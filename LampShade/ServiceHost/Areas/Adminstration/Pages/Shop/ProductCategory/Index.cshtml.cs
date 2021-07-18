using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategorys;

namespace ServiceHost.Areas.Adminstration.Pages.Shop.ProductCategory
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryApplication _categoryApplication;
        public IndexModel(IProductCategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        public List<ProductCategoryViewModel> ProductCategory { get; set; }
        public ProductCategorySearchModel SearchModel { get; set; }


        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategory = _categoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate() 
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _categoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
           var productCategories = _categoryApplication.GetDetails(id);

            return Partial("Edit", productCategories);
        }

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result=_categoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}