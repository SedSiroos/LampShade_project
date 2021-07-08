using System.Collections.Generic;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Adminstration.Pages.Discounts.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerDiscountApplication _customerDiscount;
        private readonly IProductApplication _productApplication;

        public IndexModel(ICustomerDiscountApplication customerDiscount, IProductApplication productApplication)
        {
            _customerDiscount = customerDiscount;
            _productApplication = productApplication;
        } 

        public List<CustomerDiscountViewModel> CustomerDiscount;
        public CustomerDiscountSearchModel SearchModel;
        public SelectList ProductList;

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            ProductList = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscount = _customerDiscount.SearchModel(searchModel);
        }


        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscount.Define(command);
            return new JsonResult(result);
        }


        public IActionResult OnGetEdit(long id)
        {
            var customerId=_customerDiscount.GetDetails(id);
            customerId.Products = _productApplication.GetProducts();

            return Partial("./Edit",customerId);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var customeResult = _customerDiscount.Edit(command);
            return new JsonResult(customeResult);
        }
    }
}