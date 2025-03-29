using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace order_winform
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            var service = new OrderService();
        }

        public static void TestAddOrder(OrderService service)
        {
            MessageBox.Show("测试添加订单...");
            var order = CreateTestOrder("1001", "张三", "笔记本", 1, 1000);
            service.AddOrder(order);

            try
            {
                service.AddOrder(order); // 重复添加
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("重复添加测试通过：" + ex.Message);
            }
        }

        public static void TestRemoveOrder(OrderService service)
        {
            MessageBox.Show("\n测试删除订单...");
            var order = CreateTestOrder("1002", "李四", "手机", 2, 500);
            service.AddOrder(order);

            service.RemoveOrder("1002"); // 正常删除
            try
            {
                service.RemoveOrder("1002"); // 删除不存在的订单
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show("删除不存在订单测试通过：" + ex.Message);
            }
        }

        public static void TestUpdateOrder(OrderService service)
        {
            MessageBox.Show("\n测试修改订单...");
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
                MessageBox.Show("修改不存在订单测试通过：" + ex.Message);
            }
        }

        public static void TestSearchOrders(OrderService service)
        {
            MessageBox.Show("\n测试查询订单...");
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

        public static void PrintResults(string title, IEnumerable<Order> results)
        {
            System.Text.StringBuilder message = new System.Text.StringBuilder();
            message.AppendLine($"--- {title} ---");

            // 遍历每个订单并添加到消息中
            foreach (var order in results)
            {
                message.AppendLine(order.ToString());
                message.AppendLine(new string('-', 40));
            }

            // 显示消息框
            MessageBox.Show(message.ToString());
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