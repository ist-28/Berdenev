using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Button a = new Button();
            a.Parent = this;
            a.Size = new Size(100, 30);
            a.Location = new Point(e.X, e.Y);
            a.Text = e.Location.ToString();
            Controls.Add(a);


        }
    }
}
