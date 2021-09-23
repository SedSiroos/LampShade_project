using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.inventory
{
    [Authorize(Roles = Roles.Administrator)]
    public class IndexModel : PageModel
    {

        [TempData]public string Message { get; set; }
        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventory;
        public SelectList ProductsList;

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            ProductsList = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create",command);
        }

        [NeedsPermission(ShopPermission.CreateProduct)]
        public JsonResult OnPostCreate(CreateInventory entity)
        {
            var result = _inventoryApplication.Create(entity);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
           var inventoryId = _inventoryApplication.GetDetails(id);
           inventoryId.Products = _productApplication.GetProducts();
           return Partial("./Edit",inventoryId);
        }

        public JsonResult OnPostEdit(EditInventory entity)
        {
            var result = _inventoryApplication.Edit(entity);
            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory
            {
                InventoryId = id
            };
            return Partial("Increase", command);
        }

        public IActionResult OnPostIncrease(IncreaseInventory entity)
        {
            var result = _inventoryApplication.Increase(entity);
            return new JsonResult(result);
        }

        public IActionResult OnGetReduce(long id)
        {
            var entity = new ReduceInventory
            {
                InventoryId = id
            };
            return Partial("Reduce", entity);
        }

        public IActionResult OnPostReduce(ReduceInventory entity)
        {
            var result = _inventoryApplication.Reduce(entity);
            return new JsonResult(result);
        }

        public IActionResult OnGetLog(long id)
        {
            var log = _inventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", log);
        }
    }
}