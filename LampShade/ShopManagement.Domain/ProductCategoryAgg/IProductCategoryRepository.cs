using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategorys;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long,ProductCategory>
    {
        string GetSlugById(long id);
        List<ProductCategoryViewModel> GetProductsCategory();
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> SearchModel(ProductCategorySearchModel searchModel);
    }
}
