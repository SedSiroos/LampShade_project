using InventoryManagement.Application.Contract.Inventory;
using System.Collections.Generic;
using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long,Inventory>
    {
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        EditInventory GetDetails(long id);
        Inventory GetDetailsInventory(long productId);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
