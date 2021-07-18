using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class PCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery _productCategory;
        public PCategoryModel(IProductCategoryQuery productCategory)
        {
            _productCategory = productCategory;
        }

        public ProductCategoryQueryModel ProductCategory;

        public void OnGet(string id)
        {
            ProductCategory = _productCategory.GetProductCategoryWithProductsBy(id);
        }
    }
}
