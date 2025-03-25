using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            int zero = 0;
            int one = 0;
            string str = (string)listBox1.Items[i];
            for (int j = 0; j < str.Length; j++) {
                if (str[j] == '0') { 
                    zero++;
                } else
                {
                    one++;
                }
            }
            label1.Text = "Кол-во 1 = " + Convert.ToString(one) + "; Кол-во 0 = " + Convert.ToString(zero);
        }
    }
}
