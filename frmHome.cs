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
    public partial class frmHome : MetroFramework.Forms.MetroForm
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void metroTile6_Click(object sender, EventArgs e)
        {
            frmDataAdd frmAdd = new frmDataAdd();
            frmAdd.Show();
            Visible = false;
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            frmDataAdd frmAdd = new frmDataAdd();
            frmAdd.Show();
            Visible = false;
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            frmSell frmSell = new frmSell();  
            frmSell.Show();
            Visible = false;
        }

        private void gunaCircleButton6_Click(object sender, EventArgs e)
        {

        }

        private void metroTile5_Click(object sender, EventArgs e)
        {
            Report rp = new Report();
            rp.Show();

        }
    }
}
