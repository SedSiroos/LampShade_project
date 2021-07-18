using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPictures;
using ShopManagement.Domain.ProductPictureAgg;

namespace Shopmanagement.infrastructure.EFCore.Repository
{
   public class ProductPictureRepository : RepositoryBase<long,ProductPicture> , IProductPictureRepository
   {
       private readonly ShopContext _context;

       public ProductPictureRepository(ShopContext context):base(context)
       {
           _context = context;
       }

       public ProductPicture GetProductAndCategory(long id)
       {
           return _context.ProductPictures
               .Include(x => x.Product)
               .ThenInclude(x => x.ProductCategory)
               .FirstOrDefault(x => x.Id == id);
       }

       public EditProductPicture GetDetails(long id)
       {
              return  _context.ProductPictures
                  .Select(x => new EditProductPicture
                   {
                       Id = x.Id,
                       ProductId = x.ProductId,
                       PictureAlt = x.PictureAlt,
                       PictureTitle = x.PictureTitle
                   }
               
               ).FirstOrDefault(x => x.Id == id);
       }

       public List<ProductPictureViewModel> SearchModel(ProductPictureSearchModel searchModel)
       {
           var query = _context.ProductPictures
               .Include(x => x.Product)
               .Select(x => new ProductPictureViewModel
               {
                   Id = x.Id,
                   Picture = x.Picture,
                   CreationDate = x.CreationDate.ToFarsi(),
                   Product = x.Product.Name,
                   ProductId = x.ProductId,
                   IsRemove = x.IsRemove,
               });
           if (searchModel.ProductId != 0)
               query = query.Where(x => x.ProductId == searchModel.ProductId);

           return query.OrderByDescending(x=>x.Id).ToList();
       }
   }
}
