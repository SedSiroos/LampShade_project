using System.Collections.Generic;
using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public long AccountId { get; private set; }
        //public int PaymentMethod { get; private set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public Order(long accountId, double totalAmount, double discountAmount)
        {
            AccountId = accountId;

            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
            OrderItems = new List<OrderItem>();
        }
        public void PaymentSucceeded(long refId)
        {
            IsPaid = true;
            if (RefId != 0)
                RefId = refId;
        }
        public void Cancel()
        {
            IsCanceled = true;
        }
        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo = number;
        }

        public void AddItem(OrderItem item)
        {
            OrderItems.Add(item);
        }
    }
}