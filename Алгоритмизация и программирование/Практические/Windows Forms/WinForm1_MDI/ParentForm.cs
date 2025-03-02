using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm1_MDI
{
    public partial class ParentForm : Form
    {
        public ParentForm()
        {
            InitializeComponent();
            spData.Text = Convert.ToString(DateTime.Today.ToLongDateString());
        }

        private int OpenDocuments = 0;
        

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WindowCascadeMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi (System.Windows.Forms.MdiLayout.Cascade);
            spWin.Text = "Windows is Cascade";
        }

        private void WindowTitleMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi (System.Windows.Forms.MdiLayout.TileHorizontal);
            spWin.Text = "Windows is Horizontal";
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm newChild = new ChildForm();
            newChild.MdiParent = this;
            newChild.Show();
            newChild.Text = newChild.Text + " " + ++OpenDocuments;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "NewDoc":
                    ChildForm newChild = new ChildForm();
                    newChild.MdiParent = this;
                    newChild.Show();
                    newChild.Text = newChild.Text + " " + ++OpenDocuments;
                    break;
                case "Cascade":
                    this.LayoutMdi(MdiLayout.Cascade);
                    spWin.Text = "Windows is Cascade";
                    break;
                case "Title":
                    this.LayoutMdi(MdiLayout.TileHorizontal);
                    spWin.Text = "Windows is Horizontal";
                    break;
            }
        }
    }
}
