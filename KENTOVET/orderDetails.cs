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
    public partial class orderDetails : Form
    {
        public orderDetails()
        {
            InitializeComponent();
        }

        private void orderDetails_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("select  from productStock,product where product.Pid=productStock.Pid;", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();

            con.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }
    }
}
