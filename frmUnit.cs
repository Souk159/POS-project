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
    public partial class frmUnit : MetroFramework.Forms.MetroForm
    {
        public frmUnit()
        {
            InitializeComponent();
        }

        string strcon = "data source=LAPTOP-JL7A7VB3\\SQLEXPRESS; initial catalog=Mini2CPR; integrated security=true";
        SqlConnection conn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();

        private void frmUnit_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.ConnectionString = strcon;
            conn.Open();
            ShowData();
            DGV.Columns[0].HeaderText = "ລະຫັດຫົວໜ່ວຍສິນຄ້າ";
            DGV.Columns[1].HeaderText = "ຊື່ຫົວໜ່ວຍສິນຄ້າ";
        }

        void ShowData()
        {
            da = new SqlDataAdapter("select * from tbUnit", conn);
            da.Fill(ds, "Unit");
            ds.Tables[0].Clear();
            da.Fill(ds, "Unit");
            DGV.DataSource = ds.Tables[0];
            DGV.Refresh();

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            frmDataAdd fad = new frmDataAdd();
            fad.Show();
            Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into tbUnit values(@UnitID,@UnitName)", conn);
            cmd.Parameters.AddWithValue("UnitID", txtunitID.Text);
            cmd.Parameters.AddWithValue("UnitName", txtunitName.Text);
            cmd.ExecuteNonQuery();
            ShowData();
            AllClear();
        }

        void AllClear()
        {
            txtunitID.Clear();
            txtunitName.Clear();
            txtunitID.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("update tbUnit set UnitName=@UnitName where UnitID=@UnitID", conn);
            cmd.Parameters.AddWithValue("UnitID", txtunitID.Text);
            cmd.Parameters.AddWithValue("UnitName", txtunitName.Text);
            cmd.ExecuteNonQuery();
            ShowData();
            AllClear();
        }

        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtunitID.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[0].Value.ToString();
            txtunitName.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[1].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("delete from tbUnit where UnitID=@UnitID", conn);
            cmd.Parameters.AddWithValue("UnitID", txtunitID.Text);
            cmd.ExecuteNonQuery();
            ShowData();
            AllClear();
        }
    }
}
