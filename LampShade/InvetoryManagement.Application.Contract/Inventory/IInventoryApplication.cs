using System.Collections.Generic;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory entity);
        OperationResult Reduce(List<ReduceInventory> entity);
        OperationResult Reduce(ReduceInventory entity);
        EditInventory GetDetails(long id);  
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);

    }
}
