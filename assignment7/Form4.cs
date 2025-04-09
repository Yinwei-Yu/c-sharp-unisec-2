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
            // ����Ƿ�������д��һ��
            if (string.IsNullOrWhiteSpace(textBox1.Text) && 
                string.IsNullOrWhiteSpace(textBox2.Text) && 
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("��������дһ���ѯ����");
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
                        MessageBox.Show("��������Ч�Ķ���ID��������");
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
                MessageBox.Show($"��ѯ����ʧ��: {ex.Message}");
            }
        }

        private void DisplayOrderDetails(List<Order> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                return;
            }

            string message = "��ѯ���:\n\n";
            foreach (var order in orders)
            {
                message += $"����ID: {order.OrderId}\n" +
                           $"�˿�: {order.OrderDetails.CustomerName}\n" +
                           $"��Ʒ: {order.OrderDetails.GoodsName}\n" +
                           $"�۸�: {order.OrderDetails.Price}\n" +
                           $"����: {order.OrderDetails.OrderDate}\n\n";
            }

            MessageBox.Show(message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // ��ӿ�ʵ����ƥ��InitializeComponent��İ�
        }
    }
}