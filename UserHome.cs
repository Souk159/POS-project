using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class UserHome : MetroFramework.Forms.MetroForm
    {
        public UserHome()
        {
            InitializeComponent();
        }

        private void UserHome_Load(object sender, EventArgs e)
        {

        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            frmsellUser fS = new frmsellUser();
            fS.Show();
            Visible = false;
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void metroTile5_Click(object sender, EventArgs e)
        {
            Report rp = new Report();
            rp.Show();
        }
    }
}
