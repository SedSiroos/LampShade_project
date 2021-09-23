using System.Collections.Generic;
using ShopManagement.Application.Contracts.Order;

namespace _01_LampShadeQuery.Contracts
{
    public interface ICartCalculatorService
    {
        CartCheckout ComputeCartCheckout(List<CartItem> cartItems);     
    }
}
