using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textX.Text = "14,26";
            textY.Text = "-1,22";
            textZ.Text = "0,035";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x = double.Parse(textX.Text);
            double y = double.Parse(textY.Text);
            double z = double.Parse(textZ.Text);

            textResult.Text += Environment.NewLine + "X = " + x.ToString();
            textResult.Text += Environment.NewLine + "Y = " + y.ToString();
            textResult.Text += Environment.NewLine + "Z = " + z.ToString();

            double a = (2 * Math.Cos(x - 3.14 / 6)) / (0.5 + (Math.Sin(y) * Math.Sin(y)));
            double b = 1 + (Math.Pow(z,2) / (3 - (Math.Pow(z,2)/5)));
            double c = a * b; 

            textResult.Text += Environment.NewLine + "Результат t = " + c.ToString();
        }
    }
}
