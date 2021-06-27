using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPictures;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository:IRepository<long,ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> SearchModel(ProductPictureSearchModel searchModel);
    }
}
