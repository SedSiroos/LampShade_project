using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository discountRepository)
        {
            _customerDiscountRepository = discountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exists(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessage.RecordNotFound);


            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            var customerDiscount = new CustomerDiscount(command.ProductId,command.DiscountRate,
                endDate,startDate,command.Reason);

            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerId = _customerDiscountRepository.Get(command.Id);

            if (customerId ==null)
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId &&
                                                        x.DiscountRate == command.DiscountRate &&
                                                        x.Id != command.Id))
                return operation.Failed(ApplicationMessage.RecordNotFound);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();
            customerId.Edit(command.ProductId, command.DiscountRate,
                startDate,endDate,command.Reason);
            _customerDiscountRepository.SaveChanges();
            return operation.Succeeded();

        }

        public List<CustomerDiscountViewModel> SearchModel(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }
    }
}
