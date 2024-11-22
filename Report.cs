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

namespace POS
{
    public partial class Report : MetroFramework.Forms.MetroForm
    {
        public Report()
        {
            InitializeComponent();
        }
        classConnectDatabase ccd = new classConnectDatabase();

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {

        }

        private void reportDocument1_InitReport(object sender, EventArgs e)
        {

        }

        private void CrystalReport11_InitReport(object sender, EventArgs e)
        {

        }

        private void Report_Load(object sender, EventArgs e)
        {
            ccd.ConnectDatabase();
            ccd.da = new SqlDataAdapter("select * from tbUser",ccd.conn); // where userauther='Admin'
            ccd.da.Fill(ccd.ds);
            comboBox1.DataSource = ccd.ds.Tables[0];
            comboBox1.DisplayMember = "username";
            comboBox1.ValueMember = "username";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.SelectionFormula = "{tbSell.username}= '" + comboBox1.SelectedValue + "' and {tbSell.sellDate}= '"+ Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy-MM-dd")+"' ";
            crystalReportViewer1.RefreshReport();
        }
    }
}
