using System.Collections.Generic;
using _01_LampShadeQuery.Contracts.Inventory;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryQuery _inventoryQuery;
        private readonly IInventoryApplication _inventoryApplication;

        public InventoryController(IInventoryApplication inventoryApplication, IInventoryQuery inventoryQuery)
        {
            _inventoryApplication = inventoryApplication;
            _inventoryQuery = inventoryQuery;
        }


        [HttpGet("{id}")]
        public List<InventoryOperationViewModel> GetOperationBy(long id)
        {
          return _inventoryApplication.GetOperationLog(id);
        }

        [HttpPost]
        public StockStatus CheStockStatus(IsInStock command)
        {
          return  _inventoryQuery.CheckStatus(command);
        }
    }
}
