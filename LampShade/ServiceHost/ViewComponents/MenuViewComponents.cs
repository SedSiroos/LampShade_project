using _01_LampShadeQuery;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponents : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategory;

        public MenuViewComponents(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategory)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategory = articleCategory;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel
            {
                ArticleCategories = _articleCategory.GetArticleCategory(),
                ProductCategories = _productCategoryQuery.GetProductCategory()
            };
            return View(result);
        }
    }
}
