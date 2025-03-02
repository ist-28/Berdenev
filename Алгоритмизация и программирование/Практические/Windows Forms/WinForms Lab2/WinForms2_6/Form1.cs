using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms2_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult aResult;
            Form2 aForm2 = new Form2();
            aResult = aForm2.ShowDialog();
            if (aResult == DialogResult.OK)
            {
                MessageBox.Show("Your name is " + aForm2.textBox1.Text + " " + aForm2.textBox2.Text);
                MessageBox.Show("Your phone number is " + aForm2.maskedTextBox1.Text);
                MessageBox.Show("Your assress is " + aForm2.textBox3.Text);
            }
            linkLabel1.LinkVisited = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
            linkLabel2.LinkVisited = true;
        }
    }
}
