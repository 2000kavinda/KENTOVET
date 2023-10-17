using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Guna.UI2.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KENTOVET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int selectedRow;
        private void LoadDistributor()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("select Did from distributor", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);  
            guna2ComboBox1.DataSource = dt;
            guna2ComboBox1.ValueMember = "Did";
            con.Close();
        }

        private void LoadCusCat()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("select CusCatname from cusCategory", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            guna2ComboBox2.DataSource = dt;
            guna2ComboBox2.ValueMember = "CusCatname";
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCusCat();
            LoadDistributor();
            guna2TextBox4.Text=DateTime.Now.ToLongDateString();
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con.Open(); 
            SqlCommand cmd = new SqlCommand("displayStock", con);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            guna2TextBox3.Text = row.Cells[0].Value.ToString();
            guna2TextBox1.Text = row.Cells[5].Value.ToString();
            guna2TextBox6.Text = row.Cells[4].Value.ToString();
            //guna2TextBox5.Text = row.Cells[0].Value.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("addCart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Assuming types for parameters, adjust as needed
                    cmd.Parameters.Add("@Cusid", SqlDbType.VarChar).Value = guna2TextBox2.Text;
                    cmd.Parameters.Add("@Did", SqlDbType.VarChar).Value = guna2ComboBox1.Text;
                    cmd.Parameters.Add("@itemName", SqlDbType.VarChar).Value = guna2TextBox3.Text;
                    cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = Int32.Parse(guna2TextBox1.Text);
                    cmd.Parameters.Add("@total", SqlDbType.Int).Value = Int32.Parse(guna2TextBox6.Text) * Int32.Parse(guna2TextBox5.Text);
                    cmd.Parameters.Add("@Odate", SqlDbType.Date).Value = DateTime.Now.Date;
                    if (Int32.Parse(guna2TextBox1.Text) > Int32.Parse(guna2TextBox5.Text)) {
                        cmd.ExecuteNonQuery();  // Execute the stored procedure
                        MessageBox.Show("Added sucessfully");
                    }
                    else { MessageBox.Show("Availability is not enough"); }
                    
                    
                    SqlCommand cmd1 = new SqlCommand("displayCart", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    dt.Load(cmd1.ExecuteReader());
                    dataGridView2.DataSource = dt;
                    
                    guna2TextBox5.Clear();
                    guna2TextBox3.Clear();
                    guna2TextBox1.Clear();
                    guna2TextBox6.Clear();
                }
                con.Close();
            }
        }

    }
}
