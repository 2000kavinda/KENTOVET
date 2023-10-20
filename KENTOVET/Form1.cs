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
        private void calTotal()
        {
            using (SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT SUM(total) FROM cart", con);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    guna2HtmlLabel10.Text = "Total: Rs." + Convert.ToDecimal(result).ToString("0.00");
                }
                else
                {
                    guna2HtmlLabel10.Text = "Total: Rs.0.00";
                }

                con.Close();
            }
        }

        private void LoadRefresh()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("displayStock", con);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void LoadCart()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con.Open();
            SqlCommand cmd1 = new SqlCommand("displayCart", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(cmd1.ExecuteReader());
            dataGridView2.DataSource = dt;
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
            guna2TextBox8.Text = row.Cells[1].Value.ToString();
            guna2TextBox9.Text = row.Cells[2].Value.ToString();
            guna2TextBox1.Text = row.Cells[5].Value.ToString();
            guna2TextBox6.Text = row.Cells[4].Value.ToString();
            //guna2TextBox5.Text = row.Cells[0].Value.ToString();

            string productName = guna2TextBox3.Text;

            
            string query = "SELECT Pid FROM product WHERE Pname = @ProductName";

            using (SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add a parameter for the product name
                    cmd.Parameters.AddWithValue("@ProductName", productName);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            // Retrieve the Pid and fill guna2TextBox13
                            guna2TextBox13.Text = dt.Rows[0]["Pid"].ToString();
                        }
                        else
                        {
                            guna2TextBox13.Text = "Product not found";
                        }
                    }
                }
            }


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
                    cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = Int32.Parse(guna2TextBox5.Text);
                    cmd.Parameters.Add("@total", SqlDbType.Int).Value = Int32.Parse(guna2TextBox6.Text) * Int32.Parse(guna2TextBox5.Text);
                    cmd.Parameters.Add("@Odate", SqlDbType.Date).Value = DateTime.Now.Date;
                    cmd.Parameters.Add("@Bid", SqlDbType.VarChar).Value = guna2TextBox9.Text;
                    cmd.Parameters.Add("@Comid", SqlDbType.VarChar).Value = guna2TextBox8.Text;
                    if (Int32.Parse(guna2TextBox1.Text) > Int32.Parse(guna2TextBox5.Text)) {
                        cmd.ExecuteNonQuery();  // Execute the stored procedure

                        SqlCommand cmd2 = new SqlCommand("updateAvailability", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@Pid", SqlDbType.VarChar).Value = guna2TextBox13.Text;
                        cmd2.Parameters.Add("@Comid", SqlDbType.VarChar).Value = guna2TextBox8.Text;
                        cmd2.Parameters.Add("@Bid", SqlDbType.VarChar).Value = guna2TextBox9.Text;
                        cmd2.Parameters.Add("@qty", SqlDbType.Int).Value = Int32.Parse(guna2TextBox1.Text) - Int32.Parse(guna2TextBox5.Text);
                        cmd2.ExecuteNonQuery();
                        LoadCart();
                        LoadRefresh();
                        guna2TextBox5.Clear();
                        guna2TextBox3.Clear();
                        guna2TextBox1.Clear();
                        guna2TextBox6.Clear();
                        guna2TextBox8.Clear();
                        guna2TextBox9.Clear();
                        guna2TextBox13.Clear();
                        calTotal();
                        MessageBox.Show("Added sucessfully");
                    }
                    else { MessageBox.Show("Availability is not enough"); }
                    
                    
                    /*SqlCommand cmd1 = new SqlCommand("displayCart", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    dt.Load(cmd1.ExecuteReader());
                    dataGridView2.DataSource = dt;
                    LoadRefresh();*/


                    /*guna2TextBox5.Clear();
                    guna2TextBox3.Clear();
                    guna2TextBox1.Clear();
                    guna2TextBox6.Clear();
                    guna2TextBox8.Clear();
                    guna2TextBox9.Clear();*/
                }
                con.Close();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[selectedRow];
            guna2TextBox10.Text = row.Cells[2].Value.ToString();
            guna2TextBox11.Text = row.Cells[6].Value.ToString();
            guna2TextBox12.Text = row.Cells[7].Value.ToString();
            guna2TextBox14.Text = row.Cells[3].Value.ToString();

            string productName = guna2TextBox10.Text;

            
            string query = "SELECT Pid FROM product WHERE Pname = @ProductName";

            using (SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add a parameter for the product name
                    cmd.Parameters.AddWithValue("@ProductName", productName);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            // Retrieve the Pid and fill guna2TextBox13
                            guna2TextBox13.Text = dt.Rows[0]["Pid"].ToString();
                        }
                        else
                        {
                            guna2TextBox13.Text = "Product not found";
                        }
                    }
                }
            }

            string Pid = guna2TextBox13.Text;
            string Comid = guna2TextBox12.Text;
            string Bid = guna2TextBox11.Text;
            string query1 = "SELECT qty FROM productStock WHERE Pid = @Pid AND Comid = @Comid AND Bid=@Bid ";

            using (SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    // Add a parameter for the product name
                    cmd.Parameters.AddWithValue("@Pid", Pid);
                    cmd.Parameters.AddWithValue("@Comid", Comid);
                    cmd.Parameters.AddWithValue("@Bid", Bid);

                    using (SqlDataAdapter da1 = new SqlDataAdapter(cmd))
                    {
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);

                        if (dt1.Rows.Count > 0)
                        {
                            // Retrieve the Pid and fill guna2TextBox13
                            guna2TextBox15.Text = dt1.Rows[0]["qty"].ToString();
                        }
                        else
                        {
                            guna2TextBox15.Text = "Product not found";
                        }
                    }
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("deleteCartItem", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Assuming types for parameters, adjust as needed
                    
                    cmd.Parameters.Add("@itemName", SqlDbType.VarChar).Value = guna2TextBox10.Text;
                    cmd.Parameters.Add("@Comid", SqlDbType.VarChar).Value = guna2TextBox12.Text;
                    cmd.Parameters.Add("@Bid", SqlDbType.VarChar).Value = guna2TextBox11.Text;
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("updateAvailability", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@Pid", SqlDbType.VarChar).Value = guna2TextBox13.Text;
                        cmd2.Parameters.Add("@Comid", SqlDbType.VarChar).Value = guna2TextBox12.Text;
                        cmd2.Parameters.Add("@Bid", SqlDbType.VarChar).Value = guna2TextBox11.Text;
                        cmd2.Parameters.Add("@qty", SqlDbType.Int).Value = Int32.Parse(guna2TextBox15.Text) + Int32.Parse(guna2TextBox14.Text);
                        cmd2.ExecuteNonQuery();
                        guna2TextBox10.Clear();
                        guna2TextBox11.Clear();
                        guna2TextBox12.Clear();
                        guna2TextBox13.Clear();
                        guna2TextBox14.Clear();
                        guna2TextBox15.Clear();
                        LoadRefresh();
                        LoadCart();
                        calTotal();
                        MessageBox.Show("Removed from cart");

                    /*if (Int32.Parse(guna2TextBox1.Text) > Int32.Parse(guna2TextBox5.Text))
                    {
                        // Execute the stored procedure

                        SqlCommand cmd2 = new SqlCommand("updateAvailability", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@Pid", SqlDbType.VarChar).Value = guna2TextBox13.Text;
                        cmd2.Parameters.Add("@Comid", SqlDbType.VarChar).Value = guna2TextBox8.Text;
                        cmd2.Parameters.Add("@Bid", SqlDbType.VarChar).Value = guna2TextBox9.Text;
                        cmd2.Parameters.Add("@qty", SqlDbType.Int).Value = Int32.Parse(guna2TextBox1.Text) - Int32.Parse(guna2TextBox5.Text);
                        cmd2.ExecuteNonQuery();

                        MessageBox.Show("Added sucessfully");
                    }
                    else { MessageBox.Show("Availability is not enough"); }*/


                    /*SqlCommand cmd1 = new SqlCommand("displayCart", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    dt.Load(cmd1.ExecuteReader());
                    dataGridView2.DataSource = dt;
                    LoadRefresh();


                    guna2TextBox5.Clear();
                    guna2TextBox3.Clear();
                    guna2TextBox1.Clear();
                    guna2TextBox6.Clear();
                    guna2TextBox8.Clear();
                    guna2TextBox9.Clear();*/
                }
                con.Close();
            }
        }
    }
}
