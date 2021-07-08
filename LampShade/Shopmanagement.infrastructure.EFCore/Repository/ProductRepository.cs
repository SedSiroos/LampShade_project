using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace Shopmanagement.infrastructure.EFCore.Repository
{
    public class ProductRepository:RepositoryBase<long,Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CategoryId = x.CategoryId
            }).FirstOrDefault(p => p.Id == id);
        }


        public List<ProductViewModel> Search(ProductSearchModel model)
        {
            var query = _context.Products.Include(p=>p.ProductCategory)
                .Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Picture = x.Picture,
                CategoryId = x.CategoryId,
                CreationDate = x.CreationDate.ToFarsi(),
                Category = x.ProductCategory.Name,
            });

            if (!string.IsNullOrWhiteSpace(model.Name))
                query = query.Where(p => p.Name.Contains(model.Name));

            if (!string.IsNullOrWhiteSpace(model.Code))
                query = query.Where(p => p.Code.Contains(model.Code));

            if (model.CategoryId != 0)
                query = query.Where(p => p.CategoryId == model.CategoryId);

            return query.OrderByDescending(p => p.Id).ToList();
        }
    }
}
