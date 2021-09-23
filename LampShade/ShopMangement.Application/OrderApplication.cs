using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;

        public OrderApplication(IAuthHelper authHelper, IConfiguration configuration, IOrderRepository orderRepository)
        {
            _authHelper = authHelper;
            _configuration = configuration;
            _orderRepository = orderRepository;
        }

        public long PlaceOrder(CartCheckout cart)
        {
            var currentAccountId = _authHelper.CurrentAccountId();
            var order = new Order(currentAccountId, cart.TotalAmount, cart.DiscountAmount);

            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);
                order.AddItem(orderItem);
            }
            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public void PaymentSucceeded(long orderId, long refId)
        {
            var symbol = _configuration.GetValue<string>("Symbol");
            var issueTrackingNo = CodeGenerator.Generate(symbol);

            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            order.SetIssueTrackingNo(issueTrackingNo);

            // Reduce OrderItems From Inventory
            _orderRepository.SaveChanges();
            
        }
    }
}
