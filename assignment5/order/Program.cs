using System;

namespace OrderManagement
{
    internal class Program
    {
        private static void Main()
        {
            var service = new OrderService();

            // 测试用例集合
            TestAddOrder(service);
            TestRemoveOrder(service);
            TestUpdateOrder(service);
            TestSearchOrders(service);
            TestSorting(service);
        }

        private static void TestAddOrder(OrderService service)
        {
            Console.WriteLine("测试添加订单...");
            var order = CreateTestOrder("1001", "张三", "笔记本", 1, 1000);
            service.AddOrder(order);

            try
            {
                service.AddOrder(order); // 重复添加
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("重复添加测试通过：" + ex.Message);
            }
        }

        private static void TestRemoveOrder(OrderService service)
        {
            Console.WriteLine("\n测试删除订单...");
            var order = CreateTestOrder("1002", "李四", "手机", 2, 500);
            service.AddOrder(order);

            service.RemoveOrder("1002"); // 正常删除
            try
            {
                service.RemoveOrder("1002"); // 删除不存在的订单
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("删除不存在订单测试通过：" + ex.Message);
            }
        }

        private static void TestUpdateOrder(OrderService service)
        {
            Console.WriteLine("\n测试修改订单...");
            var original = CreateTestOrder("1003", "王五", "键盘", 3, 100);
            service.AddOrder(original);

            var updated = CreateTestOrder("1003", "王五修改", "键盘", 3, 100);
            service.UpdateOrder(updated); // 正常更新

            try
            {
                service.UpdateOrder(new Order { OrderID = "1004" }); // 修改不存在订单
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("修改不存在订单测试通过：" + ex.Message);
            }
        }

        private static void TestSearchOrders(OrderService service)
        {
            Console.WriteLine("\n测试查询订单...");
            // 准备测试数据
            var orders = new List<Order>
            {
                CreateTestOrder("2001", "测试客户1", "笔记本", 1, 1000),
                CreateTestOrder("2002", "测试客户2", "手机", 2, 500),
                CreateTestOrder("2003", "测试客户1", "显示器", 1, 300)
            };

            // 通过公共方法添加订单
            foreach (var order in orders)
                service.AddOrder(order);

            // 查询测试
            PrintResults("按订单号 2001 查询", service.SearchByOrderID("2001"));
            PrintResults("按客户测试客户1查询", service.SearchByCustomer("测试客户1"));
            PrintResults("按商品手机查询", service.SearchByProductName("手机"));
            PrintResults("金额500-1500查询", service.SearchByAmount(500, 1500));
        }

        private static void TestSorting(OrderService service)
        {
            Console.WriteLine("\n测试排序...");
            var testOrders = new List<Order>
            {
                CreateTestOrder("C", "排序测试", "C", 1, 300),
                CreateTestOrder("A", "排序测试", "A", 1, 100),
                CreateTestOrder("B", "排序测试", "B", 1, 200)
            };

            // 通过公共方法添加订单
            foreach (var order in testOrders)
                service.AddOrder(order);

            service.SortOrders();
            Console.WriteLine("默认排序结果（按订单号）：");
            foreach (var o in service.Orders) // 直接访问私有字段需改为通过公共方法获取
                Console.WriteLine(o.OrderID);

            service.SortOrders(o => -o.TotalAmount);
            Console.WriteLine("按总金额降序排序结果：");
            foreach (var o in service.Orders)
                Console.WriteLine($"{o.OrderID} - {o.TotalAmount:C}");
        }

        private static void PrintResults(string title, IEnumerable<Order> results)
        {
            Console.WriteLine($"--- {title} ---");
            foreach (var order in results)
            {
                Console.WriteLine(order);
                Console.WriteLine(new string('-', 40));
            }
        }

        private static Order CreateTestOrder(string orderID, string customerName,
                                    string product, int quantity, decimal price)
        {
            var customer = new Customer { Name = customerName };
            var details = new List<OrderDetails>
            {
                new OrderDetails { ProductName = product, Quantity = quantity, UnitPrice = price }
            };
            return new Order { OrderID = orderID, Customer = customer, Details = details };
        }
    }
}