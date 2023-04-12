using System;
namespace WIN
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }
        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listaDeSuministrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSuministros miForm = new frmSuministros();
            miForm.MdiParent = this;
            miForm.Show();

        }
    }
}
