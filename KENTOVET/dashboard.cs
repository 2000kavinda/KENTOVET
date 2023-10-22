using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data;
using System.Data.SqlClient;

namespace KENTOVET
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }
        public void expiry() {
            SqlConnection con1 = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con1.Open();
            SqlCommand cmd1 = new SqlCommand("remindExpiry", con1);
            cmd1.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(cmd1.ExecuteReader());
            dataGridView2.DataSource = dt;
            con1.Close();
        }
        public void lowStock()
        {
            SqlConnection con1 = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con1.Open();
            SqlCommand cmd1 = new SqlCommand("lowStock", con1);
            cmd1.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(cmd1.ExecuteReader());
            dataGridView3.DataSource = dt;
            con1.Close();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("dailyIncome", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        ChartValues<int> values = new ChartValues<int>();
                        List<string> labels = new List<string>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            int total = Convert.ToInt32(row["TotalQuantity"]);
                            DateTime oDate = Convert.ToDateTime(row["Odate"]);

                            values.Add(total);
                            labels.Add(oDate.ToString("yyyy-MM-dd"));
                        }

                        ColumnSeries col = new ColumnSeries
                        {
                            DataLabels = true,
                            Values = values,
                            LabelPoint = point => point.Y.ToString()
                        };

                        Axis ax = new Axis
                        {
                            Separator = new Separator { Step = 1, IsEnabled = false },
                            Labels = labels
                        };

                        cartesianChart1.Series.Add(col);
                        cartesianChart1.AxisX.Add(ax);
                        cartesianChart1.AxisY.Add(new Axis
                        {
                            LabelFormatter = value => value.ToString(),
                            Separator = new Separator()
                        });
                    }
                }
            }
            expiry();
            lowStock();

            SqlConnection con1 = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=KENTOVET;Integrated Security=True");
            con1.Open();
            SqlCommand cmd1 = new SqlCommand("topSellings", con1);
            cmd1.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(cmd1.ExecuteReader());
            dataGridView1.DataSource = dt;
            con1.Close();


        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // Handle chart click event here if needed.
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            dashboard a = new dashboard();
            a.Show();
            this.Hide();
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
