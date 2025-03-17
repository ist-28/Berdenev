using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";

            double x = Convert.ToDouble(textBox1.Text);
            double y = Convert.ToDouble(textBox2.Text);
            double fx = 0;
            string func = "";
            double result = 0;

            if (radioButton1.Checked)
            {
                fx = Math.Sinh(x);
                func = "sh(x)";
            }
            else if (radioButton2.Checked)
            {
                fx = Math.Pow(x, 2);
                func = "x^2";
            }
            else if (radioButton3.Checked)
            {
                fx = Math.Exp(x);
                func = "e^x";
            }

            textBox3.Text += "При X = " + textBox1.Text + Environment.NewLine;
            textBox3.Text += "При Y = " + textBox2.Text + Environment.NewLine;
            textBox3.Text += "При Функции = " + func + Environment.NewLine;

            if (x * y > 0)
            {
                result = Math.Pow(fx + y, 2) - Math.Sqrt(fx * y);
            }
            else if (x * y < 0)
            {
                result = Math.Pow(fx + y, 2) + Math.Sqrt(Math.Abs(fx * y));
            } 
            else
            {
                result = Math.Pow(fx + y, 2) + 1;
            }

            textBox3.Text += "Результат = " + result + Environment.NewLine;
        }
    }
}
