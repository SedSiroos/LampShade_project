using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CollegueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Shopmanagement.infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long,ColleagueDiscount>,IColleagueDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;

        public ColleagueDiscountRepository(DiscountContext discountContext, ShopContext shopContext):base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }



        public EditColleagueDiscount GetDetails(long id)
        {
            return _discountContext.ColleagueDiscounts.Select(x => new EditColleagueDiscount
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
            }).FirstOrDefault(x=>x.Id==id);
        }
        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name });

            var query = _discountContext.ColleagueDiscounts
                .Select(x => new ColleagueDiscountViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    DiscountRate = x.DiscountRate,
                    IsRemove = x.IsRemoved,
                    CreationDate = x.CreationDate.ToFarsi(),
                });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var discounts = query.OrderByDescending(x =>
                x.Id).ToList();
            discounts.ForEach(d=>d.Product= products.FirstOrDefault(x=>x.Id==d.ProductId)?.Name);
            return discounts;
        }
    }
}
