namespace Week6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Order.OrderCreated += OnOrderCreated;
        }

        private void OnOrderCreated(Order newOrder)
        {
            string message = $"新的订单已创建，订单号为{newOrder.OrderId}";
            MessageBox.Show(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Form2 frm2 = new Form2())
            {
                frm2.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Form3 frm3 = new Form3())
            {
                frm3.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Form5 frm5 = new Form5())
            {
                frm5.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (Form4 frm4 = new Form4())
            {
                frm4.ShowDialog();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Order.OrderCreated -= OnOrderCreated;
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}