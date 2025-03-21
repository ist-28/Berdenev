﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms2_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Label lbl = new Label();
                lbl.Location = new Point(16,36);
                lbl.Size = new Size(32,23);
                lbl.Name = "label1";
                lbl.TabIndex = 2;
                lbl.Text = "PIN2";
                groupBox1.Controls.Add(lbl);
                TextBox txt = new TextBox();
                txt.Location = new Point(96, 36);
                txt.Size = new Size(184, 20);
                txt.Name = "textboxx";
                txt.TabIndex = 1;
                txt.Text = "";
                txt.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
                groupBox1.Controls.Add(txt);                
            } else
            {
                int lcv;
                lcv = groupBox1.Controls.Count;
                while (lcv > 0)
                {
                    groupBox1.Controls.RemoveAt(lcv - 1);
                    lcv--;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Поле Name не может содержать цифры");
                errorProvider1.SetError(textBox1, "Must provide letter");
            }
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar)) {
            //    e.Handled = true;
            //    MessageBox.Show("Поле PIN не может содержать буквы");
            //}
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if(textBox2.Text == "")
            {
                e.Cancel = false;
            }
            else
            {
                try
                {
                    double.Parse(textBox2.Text);
                    e.Cancel = false;
                }
                catch
                {
                    e.Cancel= true;
                    MessageBox.Show("Поле PIN не может содержать буквы");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
