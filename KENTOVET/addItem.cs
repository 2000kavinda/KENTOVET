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
    public partial class addItem : Form
    {
        public addItem()
        {
            InitializeComponent();
        }
        private void LoadComid()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("select Comid from company", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            comidCombo.DataSource = dt;
            comidCombo.ValueMember = "Comid";
            con.Close();
        }
        private void LoadWid()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("select Wid from warehouse", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            whouse.DataSource = dt;
            whouse.ValueMember = "Wid";
            con.Close();
        }
        private void LoadCategory()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("select Cid from category", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            typeCombo.DataSource = dt;
            typeCombo.ValueMember = "Cid";
            con.Close();
        }
        private void LoadBatch()
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand("select Bid from batch", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            bidCombo.DataSource = dt;
            bidCombo.ValueMember = "Bid";
            con.Close();
        }

        private void description_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void addItem_Load(object sender, EventArgs e)
        {
            LoadComid();
            LoadCategory();
            LoadBatch();
            LoadWid();
            guna2Button6.Hide();
            guna2Button7.Hide();
            guna2Button8.Hide();
            
            guna2HtmlLabel6.Hide();
            guna2HtmlLabel7.Hide();
            guna2HtmlLabel8.Hide();
            guna2HtmlLabel9.Hide();
            //guna2HtmlLabel5.Hide();
            guna2Button9.Hide();
            guna2Button10.Hide();
            bprice.Hide();
            rprice.Hide();
            qty.Hide();
            whouse.Hide();

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //guna2HtmlLabel1.Hide();
            //guna2HtmlLabel5.Show();
            guna2Button1.Hide();
            guna2Button2.Hide();
            guna2Button3.Hide();
            guna2Button6.Show();
            guna2Button7.Show();
            guna2Button8.Show();
            name.Hide();
            type.Hide();
            id.Hide();
            guna2HtmlLabel3.Hide();
            guna2HtmlLabel4.Hide();
            description.Hide();
            nameText.Hide();
            idText.Hide();
            descriptionText.Hide();
            typeCombo.Hide();
            comidCombo.Hide();
            bidCombo.Hide();
            guna2HtmlLabel8.Show();
            guna2HtmlLabel9.Show();
            guna2HtmlLabel7.Show();
            guna2HtmlLabel6.Show();
            bprice.Show();
            rprice.Show(); 
            qty.Show(); 
            whouse.Show();
            guna2Button5.Hide();
            guna2Button9.Show();
            guna2Button10.Show();
            guna2Button4.Hide();



        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into product (Pid,Cid,Pname,descrip) values(@Pid,@Cid,@Pname,@descrip)", con);
            SqlCommand cmd1 = new SqlCommand("insert into productStock (Pid,Comid,Bid,Wid,Bprice,Sprice,qty) values(@Pid,@Comid,@Bid,@Wid,@Bprice,@Sprice,@qty)", con);
            cmd.Parameters.AddWithValue("@Pid", idText.Text);
            cmd.Parameters.AddWithValue("@Cid", typeCombo.Text);
            cmd.Parameters.AddWithValue("@Pname", nameText.Text);
            cmd.Parameters.AddWithValue("@descrip", descriptionText.Text);
            cmd1.Parameters.AddWithValue("@Pid", idText.Text);
            cmd1.Parameters.AddWithValue("@Comid", comidCombo.Text);
            cmd1.Parameters.AddWithValue("@Bid", bidCombo.Text);
            cmd1.Parameters.AddWithValue("@Wid", whouse.Text);
            cmd1.Parameters.AddWithValue("@Bprice", Int32.Parse(bprice.Text));
            cmd1.Parameters.AddWithValue("@Sprice", Int32.Parse(rprice.Text));
            cmd1.Parameters.AddWithValue("@Qty", Int32.Parse(qty.Text));
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            con.Close();
            successmsg msg = new successmsg();
            msg.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            dashboard a = new dashboard();
            a.Show();
            this.Hide();
        }
    }
}
