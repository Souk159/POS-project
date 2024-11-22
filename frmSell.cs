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
    public partial class frmSell : MetroFramework.Forms.MetroForm

    {
        public frmSell()
        {
            InitializeComponent();
        }

        public static string username;

        classConnectDatabase ccd = new classConnectDatabase();
        private void frmSell_Load(object sender, EventArgs e)
        {
            ccd.ConnectDatabase();
            lbDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            timer1.Start();
            LV.Columns.Add("ລະຫັດສິນຄ້າ",150,HorizontalAlignment.Center);
            LV.Columns.Add("ຊື່ສິນຄ້າ", 300, HorizontalAlignment.Center);
            LV.Columns.Add("ຈຳນວນສິນຄ້າ", 120, HorizontalAlignment.Center);
            LV.Columns.Add("ລາຄາສິນຄ້າ", 100, HorizontalAlignment.Center);
            LV.Columns.Add("ຫົວໜ່ວຍ", 100, HorizontalAlignment.Center);
            LV.Columns.Add("ລວມເປັນເງິນ", 150, HorizontalAlignment.Center);
            LV.View = View.Details;
            AutoBill();
        }

        private void gunaTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.Show();
            Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void txtProductID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtProductName.Clear();
                txtPrice.Clear();
                lbUnit.Text = "ຫົວໜ່ວຍ";
                txtQty.Clear();
                txtTotal.Clear();
                ccd.da = new SqlDataAdapter("Select p.ProductName,p.price,u.UnitName from tbProduct p inner join tbUnit u ON p.UnitID=u.UnitID where p.ProductID = '" + txtProductID.Text + "'", ccd.conn);
                ccd.da.Fill(ccd.ds, "product");
                ccd.ds.Tables[0].Clear();
                ccd.da.Fill(ccd.ds, "product");
                txtProductName.Text = ccd.ds.Tables[0].Rows[0].ItemArray[0].ToString();
                txtPrice.Text = (int.Parse(ccd.ds.Tables[0].Rows[0].ItemArray[1].ToString())).ToString("#,###");
                lbUnit.Text = ccd.ds.Tables[0].Rows[0].ItemArray[2].ToString();
                txtQty.Text = "1";
                txtTotal.Text = txtPrice.Text;
                if (cbAuto.Checked == false)
                {
                    btnAdd.PerformClick();
                    txtProductID.Clear();
                    txtProductName.Clear();
                    txtPrice.Clear();
                    txtQty.Clear();
                    lbUnit.Text = "ຫົວໜ່ວຍ";
                    txtTotal.Clear();
                    txtProductID.Focus();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void cbAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAuto.Checked == true)
            {
                txtQty.Enabled = true;
            }
            else
            {
                txtQty.Enabled = false;
            }
        }
        string total, amount;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = new ListViewItem();
            int i;
            for (i = 0; i < LV.SelectedItems.Count; i++)
            {
                lvi = LV.SelectedItems[i];
                total = LV.SelectedItems[i].SubItems[5].Text;
            }
            LV.Items.Remove(lvi);
            total = total.Replace(",", "");
            amount = txtAmount.Text;
            amount = amount.Replace(",", "");
            if (int.Parse(amount) <= int.Parse(total))
            {
                txtAmount.Text = "0";
            }
            else
            {
                txtAmount.Text = (int.Parse(amount) - int.Parse(total)).ToString("#,###");

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            ccd.cmd = new SqlCommand("Insert into tbSell values(@billNo,@sellDate,@sellTime,@username)", ccd.conn);
            ccd.cmd.Parameters.AddWithValue("@billNo", lbBillNo.Text);
            ccd.cmd.Parameters.AddWithValue("@sellDate", Convert.ToDateTime(lbDate.Text).ToString("yyyy-MM-dd"));
            ccd.cmd.Parameters.AddWithValue("@sellTime", lbTime.Text);
            ccd.cmd.Parameters.AddWithValue("@username", username);
            ccd.cmd.ExecuteNonQuery();
            int i;
            for (i = 0; i < LV.Items.Count; i++)
            {
                ccd.cmd = new SqlCommand("insert into tbSellDetail values(@billNo,@productID,@price,@qty,@total)", ccd.conn);
                ccd.cmd.Parameters.AddWithValue("@billNo", lbBillNo.Text);
                ccd.cmd.Parameters.AddWithValue("@productID", LV.Items[i].SubItems[0].Text);
                string price = LV.Items[i].SubItems[2].Text;
                price = price.Replace(",", "");
                ccd.cmd.Parameters.AddWithValue("@price", price);
                ccd.cmd.Parameters.AddWithValue("@qty", LV.Items[i].SubItems[3].Text);
                string total = LV.Items[i].SubItems[5].Text;
                total = total.Replace(",", "");
                ccd.cmd.Parameters.AddWithValue("@total", total);
                ccd.cmd.ExecuteNonQuery();
                
                SqlDataAdapter daQ = new SqlDataAdapter("Select qty from tbProduct where productID='" + LV.Items[i].SubItems[0].Text + "'", ccd.conn);
                DataSet dsQ = new DataSet();
                daQ.Fill(dsQ);
                string qty = dsQ.Tables[0].Rows[0].ItemArray[0].ToString();
                
                qty = (float.Parse(qty) - float.Parse(LV.Items[i].SubItems[3].Text)).ToString();
                
                ccd.cmd = new SqlCommand("Update tbProduct Set qty=@qty where productID=@productID", ccd.conn);
                ccd.cmd.Parameters.AddWithValue("@qty", qty);
                ccd.cmd.Parameters.AddWithValue("@productID", LV.Items[i].SubItems[0].Text);
                ccd.cmd.ExecuteNonQuery();
            }

            MessageBox.Show("ບັນທຶກຂໍ້ມູນການຂາຍແລ້ວ");
            LV.Items.Clear();
            txtAmount.Text = "0";
            AutoBill();
            txtProductID.Focus();

        }

        void AutoBill()
        {
            SqlDataAdapter daB = new SqlDataAdapter("select Max(billNo) from tbSell", ccd.conn);
            DataSet dsB = new DataSet();
            daB.Fill(dsB);
            string billNo = "";
            if (dsB.Tables[0].Rows[0].ItemArray[0].ToString() == "")
            {
                billNo = "00001";
            }
            else
            {
                billNo = dsB.Tables[0].Rows[0].ItemArray[0].ToString();
                billNo = (int.Parse(billNo) + 1).ToString("00000");
            }
            lbBillNo.Text = billNo.ToString();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool ch = false; ;
                int num;
                if (txtQty.Text != "")
                {
                    ch = int.TryParse(txtQty.Text, out num);
                    if (ch == false)
                    {
                        MessageBox.Show("ກະລຸນາປ້ອນຈຳນວນສະນຄ້າເປັນຕົວເລກເທົ່ານັ້ນ!!!", "ຜົນການກວດສອບ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtQty.Text = "";
                        txtTotal.Text = "";
                        txtQty.Focus();
                    }
                    else
                    {
                        string price = txtPrice.Text;
                        price = price.Replace(",", "");
                        txtTotal.Text = (int.Parse(price) * int.Parse(txtQty.Text)).ToString("#,###");
                    }
                }
                else
                {
                    txtTotal.Text = "";
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            double price = 0;
            if (txtPrice.Text.Length > 0)
            {
                price = Convert.ToDouble(txtPrice.Text);
                txtPrice.Text = string.Format("{0:N0}", price);
                txtPrice.Select(txtPrice.Text.Length,0);
            }
        }

        private void lbBillNo_Click(object sender, EventArgs e)
        {

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string[] AllData;
            AllData = new string[] { txtProductID.Text, txtProductName.Text, txtPrice.Text, txtQty.Text, lbUnit.Text, txtTotal.Text };
            ListViewItem lvi = new ListViewItem(AllData);
            LV.Items.Add(lvi);
            total = txtTotal.Text;
            total = total.Replace(",", "");
            amount = txtAmount.Text;
            amount = amount.Replace(",", "");
            txtAmount.Text = (int.Parse(amount) + int.Parse(total)).ToString("#,###");
        }
    }
}
