using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace InventoryManagement.Infrastructure.Configuration.InventoryPermission
{
    public class InventoryPermissionExpose : IPermissionExpose
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermission.ListInventory,"ListInventory"),
                        new PermissionDto(InventoryPermission.SearchInventory,"SearchInventory"),
                        new PermissionDto(InventoryPermission.CreateInventory,"CreateInventory"),
                        new PermissionDto(InventoryPermission.EditInventory,"EditInventory"),
                        new PermissionDto(InventoryPermission.IncreaseInventory,"IncreaseInventory"),
                        new PermissionDto(InventoryPermission.ReduceInventory,"ReduceInventory"),
                        new PermissionDto(InventoryPermission.OperationLogInventory,"OperationLogInventory"),
                    }
                }
            };
        }
    }
}
