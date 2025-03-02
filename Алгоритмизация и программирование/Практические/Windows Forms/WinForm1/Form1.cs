using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm1
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
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new Size(300, 500);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2();
            form2.Left = Left;
            form2.Top = Top;
            form2.Show();
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form nForm = new nForms();
            nForm.Left = Left; nForm.Top = Top;
            nForm.Show();
            Hide();
        }
    }
}
