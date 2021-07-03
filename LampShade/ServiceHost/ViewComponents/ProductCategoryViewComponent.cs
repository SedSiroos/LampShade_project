﻿using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Pages;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent: ViewComponent
    {
        private readonly IProductCategoryQuery _productCategory;

        public ProductCategoryViewComponent(IProductCategoryQuery productCategory)
        {
            _productCategory = productCategory;
        }

        public IViewComponentResult Invoke()
        {
            var category = _productCategory.GetProductCategory();
            return View(category);
        }
    }
}
