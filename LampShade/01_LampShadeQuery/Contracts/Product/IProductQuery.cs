using System.Collections.Generic;
using ShopManagement.Application.Contracts.Order;

namespace _01_LampShadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProduct();
        List<ProductQueryModel> Search(string value);
        ProductQueryModel GetProductDetails(string slug);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
    }
}
