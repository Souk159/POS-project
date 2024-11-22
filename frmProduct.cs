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

namespace POS
{
    public partial class frmProduct : MetroFramework.Forms.MetroForm
    {
        public frmProduct()
        {
            InitializeComponent();
        }

        classConnectDatabase ccd = new classConnectDatabase();

        private void frmProduct_Load(object sender, EventArgs e)
        {
            ccd.ConnectDatabase();
            SqlDataAdapter daU = new SqlDataAdapter("select * from tbUnit", ccd.conn);
            DataSet dsU = new DataSet();
            daU.Fill(dsU, "Unit");
            dsU.Tables[0].Clear();
            daU.Fill(dsU, "Unit");
            cbUnit.DataSource = dsU.Tables[0];
            cbUnit.DisplayMember = "UnitName";
            cbUnit.ValueMember = "UnitID";

            SqlDataAdapter daC = new SqlDataAdapter("select * from tbcategory", ccd.conn);
            DataSet dsC = new DataSet();
            daC.Fill(dsC, "category");
            dsC.Tables[0].Clear();
            daC.Fill(dsC, "category");
            cbCategory.DataSource = dsC.Tables[0];
            cbCategory.DisplayMember = "categoryName";
            cbCategory.ValueMember = "categoryID";
            ShowData();
            DGV.Columns[0].HeaderText = "ລະຫັດສິນຄ້າ";
            //DGV.Columns[0].Width = 150;
            DGV.Columns[1].HeaderText = "ຊື່ສິນຄ້າ";
            DGV.Columns[1].Width = 250;
            DGV.Columns[2].HeaderText = "ລາຄາ";
            //DGV.Columns[2].Width = 150;
            DGV.Columns[3].HeaderText = "ຈຳນວນ";
            //DGV.Columns[3].Width = 150;
            DGV.Columns[4].HeaderText = "ຫົວໜ່ວຍ";
            //DGV.Columns[4].Width = 150;
            DGV.Columns[5].HeaderText = "ປະເພດສິນຄ້າ";
            //DGV.Columns[5].Width = 300;
            DGV.Columns[6].HeaderText = "ເງື່ອນໄຂສັ່ງຊື້";
            //DGV.Columns[6].Width = 250;
            lbCount.Text = (DGV.RowCount - 1).ToString();
        }

        void ShowData()
        {
            ccd.da = new SqlDataAdapter("SELECT dbo.tbProduct.ProductID, dbo.tbProduct.ProductName, dbo.tbProduct.price, dbo.tbProduct.qty, dbo.tbUnit.UnitName, dbo.tbcategory.categoryName, dbo.tbProduct.conditionCheck FROM dbo.tbcategory INNER JOIN dbo.tbProduct ON dbo.tbcategory.categoryID = dbo.tbProduct.categoryID INNER JOIN dbo.tbUnit ON dbo.tbProduct.UnitID = dbo.tbUnit.UnitID", ccd.conn);
            ccd.da.Fill(ccd.ds);
            ccd.ds.Tables[0].Clear();
            ccd.da.Fill(ccd.ds);
            DGV.DataSource = ccd.ds.Tables[0];
            DGV.Refresh();

        }

        void AllClear()
        {
            txtProductID.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtConditionCheck.Clear();
            cbUnit.SelectedIndex = 0;
            cbCategory.SelectedIndex = 0;
            txtProductID.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ccd.da = new SqlDataAdapter("SELECT dbo.tbProduct.ProductID, dbo.tbProduct.ProductName, dbo.tbProduct.price, dbo.tbProduct.qty, dbo.tbUnit.UnitName, dbo.tbcategory.categoryName, dbo.tbProduct.conditionCheck FROM dbo.tbcategory INNER JOIN dbo.tbProduct ON dbo.tbcategory.categoryID = dbo.tbProduct.categoryID INNER JOIN dbo.tbUnit ON dbo.tbProduct.UnitID = dbo.tbUnit.UnitID where dbo.tbProduct.ProductID='" + txtSearch.Text + "' or dbo.tbProduct.ProductName like N'%" + txtSearch.Text + "%'", ccd.conn);
                ccd.da.Fill(ccd.ds);
                ccd.ds.Tables[0].Clear();
                ccd.da.Fill(ccd.ds);
                DGV.DataSource = ccd.ds.Tables[0];
                DGV.Refresh();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ccd.cmd = new SqlCommand("insert into tbProduct values(@ProductID,@ProductName,@price,@qty,@UnitID,@categoryID,@conditionCheck)", ccd.conn);
            ccd.cmd.Parameters.AddWithValue("ProductID", txtProductID.Text);
            ccd.cmd.Parameters.Add("ProductName", SqlDbType.NVarChar).Value = txtProductName.Text;
            ccd.cmd.Parameters.AddWithValue("price", txtPrice.Text);
            ccd.cmd.Parameters.AddWithValue("qty", txtQty.Text);
            ccd.cmd.Parameters.AddWithValue("UnitID", cbUnit.SelectedValue);
            ccd.cmd.Parameters.AddWithValue("categoryID", cbCategory.SelectedValue);
            ccd.cmd.Parameters.AddWithValue("conditionCheck", txtConditionCheck.Text);
            ccd.cmd.ExecuteNonQuery();
            ShowData();
            AllClear();
            lbCount.Text = (DGV.RowCount - 1).ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ccd.cmd = new SqlCommand("update tbProduct set ProductName=@ProductName,price=@price,qty=@qty,UnitID=@UnitID,categoryID=@categoryID,conditionCheck = @conditionCheck where ProductID=@ProductID", ccd.conn);
            ccd.cmd.Parameters.AddWithValue("ProductID", txtProductID.Text);
            ccd.cmd.Parameters.Add("ProductName", SqlDbType.NVarChar).Value = txtProductName.Text;
            ccd.cmd.Parameters.AddWithValue("price", txtPrice.Text);
            ccd.cmd.Parameters.AddWithValue("qty", txtQty.Text);
            ccd.cmd.Parameters.AddWithValue("UnitID", cbUnit.SelectedValue);
            ccd.cmd.Parameters.AddWithValue("categoryID", cbCategory.SelectedValue);
            ccd.cmd.Parameters.AddWithValue("conditionCheck", txtConditionCheck.Text);
            ccd.cmd.ExecuteNonQuery();
            ShowData();
            AllClear();
        }

        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ccd.cmd = new SqlCommand("delete from tbProduct where ProductID=@ProductID", ccd.conn);
            ccd.cmd.Parameters.AddWithValue("ProductID", txtProductID.Text);
            ShowData();
            AllClear();
            lbCount.Text = (DGV.RowCount - 1).ToString();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            frmDataAdd dad = new frmDataAdd();
            dad.Show();
            Visible = false;
        }

        private void DGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtProductID.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[0].Value.ToString();
            txtProductName.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[1].Value.ToString();
            txtPrice.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[2].Value.ToString();
            txtQty.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[3].Value.ToString();
            cbUnit.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[4].Value.ToString();
            cbCategory.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[5].Value.ToString();
            txtConditionCheck.Text = DGV.Rows[DGV.CurrentRow.Index].Cells[6].Value.ToString();
        }
    }
}
