using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct entity);
        EditProduct GetDetails(long id);
        List<ProductViewModel> SearchModel(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
    }
}
