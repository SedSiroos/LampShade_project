using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Application
{
    public class CartService : ICartService
    {
        public CartCheckout CartCheckout { get; set; }
        public CartCheckout Get()
        {
           return CartCheckout;
        }

        public void Set(CartCheckout cart)
        {
            CartCheckout = cart;
        }
    }
}
