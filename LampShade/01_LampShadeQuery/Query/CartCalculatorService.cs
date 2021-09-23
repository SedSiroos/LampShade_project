using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;

namespace _01_LampShadeQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;

        public CartCalculatorService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public CartCheckout ComputeCartCheckout(List<CartItem> cartItems)
        {   
            var cartCheckout = new CartCheckout();

            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x=> !x.IsRemoved)
                .Select(x=>new {x.ProductId,x.DiscountRate}).ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate > DateTime.Now && x.EndDate < DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscountRate}).ToList();

            var currentAccountRole = _authHelper.CurrentAccountRole();

            foreach (var item in cartItems)
            {
                if (currentAccountRole == Roles.ColleagueUser)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                    if (colleagueDiscount != null)
                        item.DiscountRate = colleagueDiscount.DiscountRate;
                }
                else
                {
                    var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                    if (customerDiscount != null)
                        item.DiscountRate = customerDiscount.DiscountRate;
                }

                item.DiscountAmount = ((item.TotalUnitPrice * item.DiscountRate) / 100);
                item.ItemPayAmount = item.TotalUnitPrice - item.DiscountAmount;
                cartCheckout.Add(item);
            }

            return cartCheckout;
        }
    }
}