using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using Shopmanagement.infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x =>new
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


            var category = _shopContext.ProductCategories
                .Include(x=>x.Products)
                .ThenInclude(x=>x.ProductCategory)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    KeyWords = x.KeyWords,
                    Products = MapProduct(x.Products),
                    Slug = x.Slug
                }).FirstOrDefault(p => p.Slug == slug);

            foreach (var product in category.Products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);

                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var productDiscount = discount.FirstOrDefault(d => d.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        int discountRate = productDiscount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        product.ExpiryDate = productDiscount.EndDate.ToDiscountFormat();
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }
            return category;
        }

        public List<ProductCategoryQueryModel> GetProductCategory()
        {
            return _shopContext.ProductCategories
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Slug = x.Slug,
                }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoryWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(d => new { d.DiscountRate, d.ProductId, d.EndDate }).ToList();

            var categories = _shopContext.ProductCategories
                .Include(p => p.Products)
                .ThenInclude(p => p.ProductCategory)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProduct(x.Products),
                }).ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        var price = productInventory.UnitPrice;
                        product.Price = price.ToMoney();

                        var discount = discounts.FirstOrDefault(d => d.ProductId == product.Id);
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
            }

            return categories;
        }

        private static List<ProductQueryModel> MapProduct(List<Product> products)
        {
            return products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Category = x.ProductCategory.Name,
                Slug = x.Slug
            }).ToList();
        }
    }
}
