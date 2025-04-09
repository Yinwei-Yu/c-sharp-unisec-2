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
                MessageBox.Show("�����붩��ID");
                return;
            }

            try
            {
                int orderId = int.Parse(textBox3.Text);
                Program.orderService.DeleteOrder(orderId);
                
                string data = "������ɾ��";
                OnDataReady?.Invoke(data);
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("��������Ч�Ķ���ID��������");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ɾ������ʧ��: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}