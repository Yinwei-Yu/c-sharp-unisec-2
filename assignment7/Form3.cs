namespace Week6
{
    public partial class Form3 : Form
    {
        public event Action<string> OnDataReady;

        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("请输入订单ID");
                return;
            }

            try
            {
                int orderId = int.Parse(textBox3.Text);
                Program.orderService.DeleteOrder(orderId);
                
                string data = "订单已删除";
                OnDataReady?.Invoke(data);
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入有效的订单ID（整数）");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除订单失败: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}