﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KENTOVET
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "12345")
            {
                Form1 stocks = new Form1();
                stocks.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("error");
            }
        }
    }
}
