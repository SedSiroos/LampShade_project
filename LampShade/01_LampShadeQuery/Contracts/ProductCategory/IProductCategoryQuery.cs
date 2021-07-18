using System.Collections.Generic;

namespace _01_LampShadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug);
        List<ProductCategoryQueryModel> GetProductCategory();
        List<ProductCategoryQueryModel> GetProductCategoryWithProducts();
    }
}