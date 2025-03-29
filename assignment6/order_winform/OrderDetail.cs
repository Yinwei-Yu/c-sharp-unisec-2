using System;

namespace order_winform
{
    public class OrderDetails
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;

        public override bool Equals(object obj)
            => obj is OrderDetails details && ProductName == details.ProductName;

        public override int GetHashCode() => ProductName.GetHashCode();

        public override string ToString()
            => $"{ProductName} x{Quantity} @ {UnitPrice:C} = {SubTotal:C}";
    }
}