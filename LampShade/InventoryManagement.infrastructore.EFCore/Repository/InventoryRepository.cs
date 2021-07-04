using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Shopmanagement.infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long,Inventory>,IInventoryRepository
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;
        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext):base(inventoryContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
            }).FirstOrDefault(x => x.Id == id);
        }

        public Inventory GetDetailsInventory(long productId)
        {
            return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var product = _shopContext.Products.Select(x => new {x.Id, x.Name});

            var query = _inventoryContext.Inventory
                .Select(x => new InventoryViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    UnitPrice = x.UnitPrice,
                    InStock = x.InStock,
                    CurrentCount = x.CalculateCurrentCount(),
                    CreationDate = x.CreationDate.ToFarsi(),
                });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            if (searchModel.InStock)
                query = query.Where(x => !x.InStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item=>
                item.Product= product.FirstOrDefault(x=>x.Id==item.ProductId)?.Name);

            return inventory;
        }

    }
}
