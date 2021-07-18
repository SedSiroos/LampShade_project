using System.Collections.Generic;

namespace _01_LampShadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProduct();
        List<ProductQueryModel> Search(string value);
        ProductQueryModel GetProductDetails(string slug);
    }
}
