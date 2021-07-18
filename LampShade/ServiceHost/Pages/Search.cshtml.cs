using System.Collections.Generic;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IProductQuery _productQuery;

        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }


        public string Value;
        public List<ProductQueryModel> ProductQuery;
        public void OnGet(string value)
        {
            Value = value;
            ProductQuery = _productQuery.Search(value);
        }
    }
}
