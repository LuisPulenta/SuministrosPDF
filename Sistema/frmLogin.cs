using CADSistema;
using System;
using System.Windows.Forms;

namespace Sistema
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtUsuario.Text == string.Empty)
            {
                errorProvider1.SetError(txtUsuario, "Debe ingresar un Usuario");
                txtUsuario.Focus();
                return;
            }
            if (txtClave.Text == string.Empty)
            {
                errorProvider1.SetError(txtClave, "Debe ingresar una Clave");
                txtClave.Focus();
                return;
            }
            if (!CADUsuario.ValidaUsuario(txtUsuario.Text, txtClave.Text))
            {
                MessageBox.Show("Usuario o Clave no válidos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsuario.Text = string.Empty;
                txtClave.Text = string.Empty;
                txtUsuario.Focus();
                return;
            }

            frmPrincipal miForm = new frmPrincipal();
            miForm.Show();
            this.Hide();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}