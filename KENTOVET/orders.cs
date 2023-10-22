using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KENTOVET
{
    public partial class orders : Form
    {
        public orders()
        {
            InitializeComponent();
        }
        private void refresh()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("select * from orders", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();

            con.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from orders where itemName in (select Pname from product where Pid in (select Pid from product where Cid = dbo.filterOrdes('animal vaccine')))", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void orders_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from orders where Cusid = dbo.searchOrder(@SearchText);", con);


            cmd.Parameters.Add(new SqlParameter("@SearchText", guna2TextBox7.Text));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from orders where itemName in (select Pname from product where Pid in (select Pid from product where Cid = dbo.filterOrdes('animal medicine')))", con);
          
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from orders where itemName in (select Pname from product where Pid in (select Pid from product where Cid = dbo.filterOrdes('dog food')))", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            a.Show();
            this.Hide();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            orders b = new orders();
            b.Show();
            this.Hide();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            addItem a = new addItem();
            a.Show();
            this.Hide();
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            itemList a = new itemList();
            a.Show();
            this.Hide();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            dashboard a = new dashboard();
            a.Show();
            this.Hide();
        }
    }
}
