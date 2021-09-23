using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository: RepositoryBase<long,CustomerDiscount>,ICustomerDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext discountContext, ShopContext shopContext):base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {

            var product = _shopContext.Products.Select(p => new
            {
                p.Id,
                p.Name,
            }).ToList();

            var query = _discountContext.CustomerDiscounts
                .Select(x=>new CustomerDiscountViewModel
                {
                    Id = x.Id,
                    Reason = x.Reason,
                    DiscountRate = x.DiscountRate,
                    ProductId = x.ProductId,
                    StartDate = x.StartDate.ToFarsi(),
                    StartDateGr = x.StartDate,
                    EndDate = x.EndDate.ToFarsi(),
                    EndDateGr = x.EndDate,
                    CreationDate = x.CreationDate.ToFarsi(),
                });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            { 
                query = query.Where(x => x.StartDateGr < searchModel.StartDate.ToGeorgianDateTime());
            }
            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            { 
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());
            }

            var discount = query.OrderByDescending(x => x.Id).ToList();

            discount.ForEach(d=>
                d.Product = product.FirstOrDefault(p=>p.Id==d.ProductId)?.Name);

            return discount;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _discountContext.CustomerDiscounts
                .Select(x => new EditCustomerDiscount()
                {
                    Id = x.Id,
                    DiscountRate = x.DiscountRate,
                    Reason = x.Reason,
                    ProductId = x.ProductId,
                    StartDate = x.StartDate.ToString(CultureInfo.InvariantCulture),
                    EndDate = x.EndDate.ToString(CultureInfo.InvariantCulture),
                }).FirstOrDefault(x => x.Id == id);
        }
    }
}
