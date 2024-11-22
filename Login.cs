using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace POS
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
        {
            InitializeComponent();
        }

        public string user1;

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            SqlConnection conn = new SqlConnection("data source=LAPTOP-JL7A7VB3\\SQLEXPRESS; initial catalog=Mini2CPR; integrated security=true");
            SqlCommand cmd = new SqlCommand("select * from tbUser where username = '" + txtName.Text + "' and userpassword = '" + txtPass.Text + "'", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            string cmbItemValue = comboBox1.SelectedItem.ToString();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["userauther"].ToString() == cmbItemValue)
                    {
                        MessageBox.Show("you are login as " + dt.Rows[i][2]);
                        if (comboBox1.SelectedIndex == 0)
                        {

                            frmHome fh = new frmHome();
                            fh.Show();
                            Visible = false;
                            
                        }
                        else 
                        {

                            UserHome uh = new UserHome();
                            uh.Show();
                            Visible = false;

                        }
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Error");
            }

            user1 = txtName.Text;
            frmSell.username = user1;
            frmsellUser.username = user1;
            

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
