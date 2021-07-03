using System.Collections.Generic;
using _0_Framework.Domain;
using DiscountManagement.Application.Contract.CustomerDiscount;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
   public interface ICustomerDiscountRepository : IRepository<long,CustomerDiscount>
   {
       List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
       EditCustomerDiscount GetDetails(long id);
   }
}
