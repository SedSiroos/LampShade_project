﻿namespace ShopManagement.Application.Contracts.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public double UnitPrice { get; set; }
        public double TotalUnitPrice { get; set; }
        public int Count { get; set; }
        public bool IsInStock { get; set; }
        public int DiscountRate { get; set; }
        public double DiscountAmount { get; set; }
        public double ItemPayAmount { get; set; }       

        public CartItem()
        {
            TotalUnitPrice = UnitPrice * Count;
        }

        public void CalculatorTotalItemPrice()
        {
            TotalUnitPrice = UnitPrice * Count;
        }
    }
}
