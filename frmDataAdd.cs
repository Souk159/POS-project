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
    public partial class frmDataAdd : MetroFramework.Forms.MetroForm
    {
        public frmDataAdd()
        {
            InitializeComponent();
        }

        private void frmDataAdd_Load(object sender, EventArgs e)
        {

        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            /*frmCategory fc = new frmCategory();
            fc.Show();
            Visible = false;*/
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            /*frmHome fh = new frmHome();
            fh.Show();
            Visible = false;*/
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            frmHome fh = new frmHome();
            fh.Show();
            Visible = false;
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            frmCategory fc = new frmCategory();
            fc.Show();
            Visible = false;
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            frmUnit fu = new frmUnit();
            fu.Show();
            Visible = false;
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            frmProduct pd = new frmProduct();
            pd.Show();
            Visible = false;
        }
    }
}
