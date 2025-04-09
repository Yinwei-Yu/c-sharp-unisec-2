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
                MessageBox.Show("����д�˿���������Ʒ����");
                return;
            }

            try
            {
                string customerName = textBox1.Text;
                string goodsName = textBox2.Text;
                Program.orderService.AddOrder(customerName, goodsName);
                
                string data = "�µĶ����Ѵ���";
                OnDataReady?.Invoke(data); // �����¼�
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������ʧ��: {ex.Message}");
            }
        }
    }
}