namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(CartCheckout cart);
        void PaymentSucceeded(long orderId, long refId);
    }
}
