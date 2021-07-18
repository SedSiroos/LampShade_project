using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {

        private readonly IProductQuery _productQuery;
        public ProductModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public ProductQueryModel Product;

        public void OnGet(string id)
        {
            Product = _productQuery.GetProductDetails(id);
        }
    }
}