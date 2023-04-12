using CADSistema;
using System;
using System.Windows.Forms;

namespace Sistema
{
    public partial class frmPrincipal : Form
    {

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void suministrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSuministros miForm = new frmSuministros();
            miForm.MdiParent = this;
            miForm.Show();

        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
        }
   }
}
