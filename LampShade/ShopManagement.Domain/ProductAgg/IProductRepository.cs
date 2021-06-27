using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategorys;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository:IRepository<long,Product>
    {
        List<ProductViewModel> GetProducts();
        EditProduct GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel model);

    }
}
