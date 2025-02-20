using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace window
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            combOperator.SelectedIndex = 0;
        }

        private void combOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lbResult_Click(object sender, EventArgs e)
        {
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtNum1.Text, out double num1) || !double.TryParse(txtNum2.Text, out double num2))
            {
                MessageBox.Show("输入的不是有效数字!");
                return;
            }
            try
            {
                //解析数字和运算符
                num1 = double.Parse(txtNum1.Text);
                num2 = double.Parse(txtNum2.Text);
                string op = combOperator.SelectedItem.ToString();

                //计算
                double result = 0;
                switch (op)
                {
                    case "+":
                        result = num1 + num2;
                        break;

                    case "-":
                        result = num1 - num2;
                        break;

                    case "*":
                        result = num1 * num2;
                        break;

                    case "/":
                        result = num1 / num2;
                        break;

                    case "%":
                        result = num1 % num2;
                        break;

                    case "^":
                        result = Math.Pow(num1, num2);
                        break;
                }
                lbResult.Text = $"结果：{result}";
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入正确的数字!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("除数不能为0!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}