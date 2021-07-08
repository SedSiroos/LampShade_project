using System.Collections.Generic;
using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }


        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(X => X.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventoryId = _inventoryRepository.Get(command.Id);

            if (inventoryId==null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            inventoryId.Edit(command.ProductId,command.UnitPrice);
            _inventoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Increase(IncreaseInventory entity)
        {
            var operation = new OperationResult();
            var inventoryId = _inventoryRepository.Get(entity.InventoryId);

            if (inventoryId == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            const long operatorId = 1;
            inventoryId.Increase(entity.Count,operatorId,entity.Description);
            _inventoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> entity)
        {
            var operation = new OperationResult();
            const long operatorId = 1;

            foreach (var item in entity)
            {
                var inventory = _inventoryRepository.GetDetailsInventory(item.ProductId);
                inventory.Reduce(item.Count,operatorId,item.Description,item.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(ReduceInventory entity)
        {
            var operation = new OperationResult();
            var inventoryId = _inventoryRepository.Get(entity.InventoryId);

            if (inventoryId == null)
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            const long operatorId = 0;
            inventoryId.Reduce(entity.Count,operatorId,entity.Description,entity.OrderId);
            _inventoryRepository.SaveChanges();

            return operation.Succeeded();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            return _inventoryRepository.GetOperationLog(inventoryId);
        }
    }
}
