using System.Collections.Generic;
using System.Linq;

namespace OrderManagement
{
    public class OrderService
    {
        private List<Order> orders = new List<Order>();

        // 添加订单
        public void AddOrder(Order newOrder)
        {
            if (orders.Contains(newOrder))
                throw new InvalidOperationException("订单已存在");
            orders.Add(newOrder);
        }

        // 删除订单
        public void RemoveOrder(string orderID)
        {
            var order = orders.FirstOrDefault(o => o.OrderID == orderID);
            if (order == null)
                throw new KeyNotFoundException($"订单 {orderID} 不存在");
            orders.Remove(order);
        }

        // 修改订单
        public void UpdateOrder(Order updatedOrder)
        {
            var existing = orders.FirstOrDefault(o => o.OrderID == updatedOrder.OrderID);
            if (existing == null)
                throw new KeyNotFoundException($"订单 {updatedOrder.OrderID} 不存在");
            orders.Remove(existing);
            orders.Add(updatedOrder);
        }

        // 查询订单
        public IEnumerable<Order> SearchByOrderID(string id)
            => QueryBy(o => o.OrderID == id);

        public IEnumerable<Order> SearchByCustomer(string name)
            => QueryBy(o => o.Customer.Name == name);

        public IEnumerable<Order> SearchByProductName(string product)
            => QueryBy(o => o.Details.Any(d => d.ProductName == product));

        public IEnumerable<Order> SearchByAmount(decimal min, decimal max)
            => QueryBy(o => o.TotalAmount >= min && o.TotalAmount <= max);

        private IEnumerable<Order> QueryBy(Func<Order, bool> predicate)
            => orders.Where(predicate).OrderByDescending(o => o.TotalAmount);

        // 排序方法
        public void SortOrders<TKey>(Func<Order, TKey> keySelector)
            => orders = orders.OrderBy(keySelector).ToList();

        public void SortOrders()
            => SortOrders(o => o.OrderID);

        //外部读取
        public IReadOnlyList<Order> Orders => orders.AsReadOnly();
    }
}