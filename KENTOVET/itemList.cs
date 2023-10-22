using Guna.UI2.WinForms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KENTOVET
{
    public partial class itemList : Form
    {
        public itemList()
        {
            InitializeComponent();
        }
        int selectedRow;

        private void itemList_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("select productStock.Pid,product.Pname,productStock.Bprice,productStock.Sprice,productStock.qty,productStock.Comid,productStock.Bid,productStock.Wid from productStock,product where product.Pid=productStock.Pid;", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();
            
            con.Close();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            guna2TextBox1.Text = row.Cells[0].Value.ToString();
            guna2TextBox2.Text = row.Cells[5].Value.ToString();
            guna2TextBox3.Text = row.Cells[6].Value.ToString();
            guna2TextBox4.Text = row.Cells[2].Value.ToString();
            guna2TextBox5.Text = row.Cells[3].Value.ToString();
            guna2TextBox6.Text = row.Cells[4].Value.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("Update productStock set Bprice=@Bprice,Sprice=@Sprice,qty=@qty where Pid=@Pid AND Comid=@Comid AND Bid=@Bid ", con);
            cmd.Parameters.AddWithValue("@Pid", guna2TextBox1.Text);
            cmd.Parameters.AddWithValue("@Comid", guna2TextBox2.Text);
            cmd.Parameters.AddWithValue("@Bid", guna2TextBox3.Text);
            cmd.Parameters.AddWithValue("@Bprice", Int32.Parse(guna2TextBox4.Text));
            cmd.Parameters.AddWithValue("@Sprice", Int32.Parse(guna2TextBox5.Text));
            cmd.Parameters.AddWithValue("@qty", Int32.Parse(guna2TextBox6.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Sucessfully Updated");
            con.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("Delete productStock where Pid=@Pid AND Comid=@Comid AND Bid=@Bid ", con);
            cmd.Parameters.AddWithValue("@Pid", guna2TextBox1.Text);
            cmd.Parameters.AddWithValue("@Comid", guna2TextBox2.Text);
            cmd.Parameters.AddWithValue("@Bid", guna2TextBox3.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Sucessfully Deleted");
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT PS.Pid, P.Pname, PS.Bprice, PS.Sprice, PS.qty, PS.Comid, PS.Bid, PS.Wid " +
                                            "FROM productStock PS " +
                                            "INNER JOIN product P ON P.Pid = PS.Pid " +
                                            "WHERE PS.Pid = dbo.searchItem(@SearchText);", con);

            
            cmd.Parameters.Add(new SqlParameter("@SearchText", guna2TextBox7.Text));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
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
    }
}
