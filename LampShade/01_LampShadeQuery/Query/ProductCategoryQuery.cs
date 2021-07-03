using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contracts.ProductCategory;
using Shopmanagement.infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class ProductCategoryQuery:IProductCategoryQuery
    {
        private readonly ShopContext _context;

        public ProductCategoryQuery(ShopContext context)
        {
            _context = context;
        }

        public List<ProductCategoryModel> GetProductCategory()
        {
            return _context.ProductCategories
                .Select(x => new ProductCategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Slug = x.Slug,
                }).ToList();
        }
    }
}
