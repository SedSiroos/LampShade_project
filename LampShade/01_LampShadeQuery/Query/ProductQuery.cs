using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductPictureAgg;
using Shopmanagement.infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestProduct()
        {
            var productInventory = _inventoryContext.Inventory.Select(I => new
            {
                I.ProductId,
                I.UnitPrice,
            }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(d => new
                {
                    d.DiscountRate,
                    d.EndDate,
                    d.ProductId
                });

            var products = _shopContext.Products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Category = x.ProductCategory.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Name = x.Name,
            }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();

            foreach (var product in products)
            {
                var inventory = productInventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (inventory != null)
                {
                    var price = inventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        int discountRate = discount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }
            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new
            {
                x.ProductId,
                x.UnitPrice
            }).ToList();
            var discount = _discountContext.CustomerDiscounts
                .Where(d => d.StartDate < DateTime.Now && d.EndDate > DateTime.Now)
                .Select(x => new
                {
                    x.DiscountRate,
                    x.ProductId,
                    x.EndDate
                }).ToList();

            var query = _shopContext.Products
                .Include(x => x.ProductCategory)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.ProductCategory.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    CategorySlug = x.ProductCategory.Slug,
                    Slug = x.Slug
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);

                    if (productDiscount == null) continue;

                    var discountRate = productDiscount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;

                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
            return products;
        }

        public ProductQueryModel GetProductDetails(string slug)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).ToList();
            var discountCustomer = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.EndDate, x.ProductId, x.DiscountRate }).ToList();


            var product = _shopContext.Products
                .Include(x => x.ProductCategory)
                .Include(x => x.Comment)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    CategorySlug = x.ProductCategory.Slug,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    Slug = x.Slug,
                    Category = x.ProductCategory.Name,
                    ProductPictures = MapProductPictures(x.ProductPictures),
                    Comments = MapComments(x.Comment)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (product == null)
                return new ProductQueryModel();

            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                product.IsInStock = productInventory.InStock;
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();

                var productDiscount = discountCustomer.FirstOrDefault(x => x.ProductId == product.Id);
                if (productDiscount != null)
                {
                    var discountRate = productDiscount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;

                    product.ExpiryDate = productDiscount.EndDate.ToDiscountFormat();

                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
            return product;
        }


        private static List<CommentQueryModel> MapComments(List<Comment> comment)
        {
            return comment.Where(x => x.IsCanceled).Where(x => x.IsConfirmed)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Message = x.Message
                }).ToList();
        }
        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> productPictures)
        {
            return productPictures.Select(x => new ProductPictureQueryModel
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId,
                IsRemove = x.IsRemove
            }).Where(x => !x.IsRemove).ToList();
        }
    }
}
