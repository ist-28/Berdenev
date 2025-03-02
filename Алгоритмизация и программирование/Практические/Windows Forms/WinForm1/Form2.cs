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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form1 = Application.OpenForms[0];
            form1.StartPosition = FormStartPosition.Manual;
            form1.Left = Left;
            form1.Top = Top;
            form1.Show();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath myPath = new System.Drawing.Drawing2D.GraphicsPath();
            myPath.AddPolygon(new Point[] {new Point(0,0),new Point(0,this.Height), new Point(this.Width,0)});
            Region myRegion = new Region(myPath);
            this.Region = myRegion;
        }
    }
}
