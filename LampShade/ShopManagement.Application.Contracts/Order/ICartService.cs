namespace ShopManagement.Application.Contracts.Order
{
    public interface ICartService
    {
        CartCheckout Get();
        void Set(CartCheckout cart);
    }
}
