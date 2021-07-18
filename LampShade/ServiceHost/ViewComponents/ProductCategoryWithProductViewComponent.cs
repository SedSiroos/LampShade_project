using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategory;

        public ProductCategoryWithProductViewComponent(IProductCategoryQuery productCategory)
        {
            _productCategory = productCategory;
        }


        public IViewComponentResult Invoke()
        {
            var category = _productCategory.GetProductCategoryWithProducts();
            return View(category);
        }
    }
}
