using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPictures;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public IndexModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }

        [TempData] public string Message { get; set; }
        public List<ProductPictureViewModel> ProductPictures { get; set; }
        public ProductPictureSearchModel SearchModel { get; set; }
        public SelectList Products;

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ProductPictures = _productPictureApplication.SearchModel(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture
            {
                ListProduct = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
           var product = _productPictureApplication.GetDetails(id);
           product.ListProduct = _productApplication.GetProducts();

            return Partial("Edit", product);
        }

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result=_productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var picture = _productPictureApplication.Remove(id);
            if (picture.IsSucceeded)
                return RedirectToPage("./Index");

            Message = picture.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var picture = _productPictureApplication.Restore(id);
            if (picture.IsSucceeded)
                return RedirectToPage("./Index");

            Message = picture.Message;
            return RedirectToPage("./Index");
        }
    }
}