namespace Week6
{
    public partial class Form4 : Form
    {
        public event Action<string> OnDataReady;

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 检查是否至少填写了一项
            if (string.IsNullOrWhiteSpace(textBox1.Text) && 
                string.IsNullOrWhiteSpace(textBox2.Text) && 
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("请至少填写一项查询条件");
                return;
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    if (int.TryParse(textBox1.Text, out int orderId))
                    {
                        var order = Program.orderService.FindAOrderById(orderId);
                        if (order != null)
                        {
                            DisplayOrderDetails(new List<Order> { order });
                        }
                    }
                    else
                    {
                        MessageBox.Show("请输入有效的订单ID（整数）");
                    }
                }
                else if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    var orders = Program.orderService.FindAOrderByGoodsname(textBox2.Text);
                    DisplayOrderDetails(orders);
                }
                else if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    var orders = Program.orderService.FindAOrderByCustomer(textBox3.Text);
                    DisplayOrderDetails(orders);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询订单失败: {ex.Message}");
            }
        }

        private void DisplayOrderDetails(List<Order> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                return;
            }

            string message = "查询结果:\n\n";
            foreach (var order in orders)
            {
                message += $"订单ID: {order.OrderId}\n" +
                           $"顾客: {order.OrderDetails.CustomerName}\n" +
                           $"商品: {order.OrderDetails.GoodsName}\n" +
                           $"价格: {order.OrderDetails.Price}\n" +
                           $"日期: {order.OrderDetails.OrderDate}\n\n";
            }

            MessageBox.Show(message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // 添加空实现以匹配InitializeComponent里的绑定
        }
    }
}