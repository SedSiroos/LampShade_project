﻿using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestProductViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LatestProductViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var product = _productQuery.GetLatestProduct();

            return View(product);
        }
    }
}
