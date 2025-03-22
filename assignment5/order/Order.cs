using System.Collections.Generic;

namespace OrderManagement
{
    public class Order
    {
        public string OrderID { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetails> Details { get; set; } = new List<OrderDetails>();

        public decimal TotalAmount => Details.Sum(d => d.SubTotal);

        public override bool Equals(object obj)
            => obj is Order order && OrderID == order.OrderID;

        public override int GetHashCode() => OrderID.GetHashCode();

        public override string ToString()
            => $"订单号：{OrderID}\n客户：{Customer}\n总金额：{TotalAmount:C}\n明细：\n" +
               string.Join("\n", Details.Select(d => $"  {d}"));

        public void AddDetail(OrderDetails detail)
        {
            if (Details.Any(d => d.Equals(detail)))
                throw new InvalidOperationException("该商品已存在订单明细中");
            Details.Add(detail);
        }
    }
}