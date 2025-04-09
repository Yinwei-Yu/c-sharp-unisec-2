namespace Week6
{
    public partial class Form2 : Form
    {
        public event Action<string> OnDataReady;
        
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("请填写顾客姓名和商品名称");
                return;
            }

            try
            {
                string customerName = textBox1.Text;
                string goodsName = textBox2.Text;
                Program.orderService.AddOrder(customerName, goodsName);
                
                string data = "新的订单已创建";
                OnDataReady?.Invoke(data); // 触发事件
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"创建订单失败: {ex.Message}");
            }
        }
    }
}