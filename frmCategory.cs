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
    public partial class frmCategory : MetroFramework.Forms.MetroForm
    {
        public frmCategory()
        {
            InitializeComponent();
        }
        string strcon = "data source=LAPTOP-JL7A7VB3\\SQLEXPRESS; initial catalog=Mini2CPR; integrated security=true";
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();

        private void frmCategory_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.ConnectionString = strcon;
            conn.Open();
            ShowData();
            DGV.Columns[0].HeaderText = "ລະຫັດປະເພດສິນຄ້າ";
            DGV.Columns[1].HeaderText = "ຊື່ປະເພດສິນຄ້າ";
            
            //DGV.Columns[0].Width = 150;
            //DGV.Columns[1].Width = 200;
        }

        void ShowData()
        {
            da = new SqlDataAdapter("select * from tbcategory", conn);
            da.Fill(ds, "category");
            ds.Tables[0].Clear();
            da.Fill(ds, "category");
            DGV.DataSource = ds.Tables[0];
            DGV.Refresh();

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            /*frmDataAdd fad = new frmDataAdd();
            fad.Show();
            Visible = false;*/
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            frmDataAdd fad = new frmDataAdd();
            fad.Show();
            Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into tbcategory values(@categoryID,@categoryName)", conn);
            cmd.Parameters.AddWithValue("categoryID", txtcategoryID.Text);
            cmd.Parameters.AddWithValue("categoryName", txtcategoryName.Text);
            cmd.ExecuteNonQuery();
            ShowData();
            AllClear();
        }

        void AllClear()
        {
            txtcategoryID.Clear();
            txtcategoryName.Clear();
            txtcategoryID.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("update tbcategory set categoryName=@categoryName where categoryID=@categoryID", conn);
            cmd.Parameters.AddWithValue("categoryID", txtcategoryID.Text);
            cmd.Parameters.AddWithValue("categoryName", txtcategoryName.Text);
            cmd.ExecuteNonQuery();
            ShowData();
            AllClear();
        }

        private void DGV_Click(object sender, EventArgs e)
        {
            txtcategoryID.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[0].Value.ToString();
            txtcategoryName.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[1].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("delete from tbcategory where categoryID=@categoryID", conn);
            cmd.Parameters.AddWithValue("categoryID", txtcategoryID.Text);
            cmd.ExecuteNonQuery();
            ShowData();
            AllClear();
        }

        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
