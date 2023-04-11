using CAD;
using System;
using System.Windows.Forms;

namespace WIN
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }




        private void btnAceptar_Click_1(object sender, EventArgs e)
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
                errorProvider1.SetError(txtClave, "Debe ingresar una Contraseña");
                txtClave.Focus();
                return;
            }

            if (!CADUsuarios.ValidaUsuario(txtUsuario.Text, txtClave.Text))
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

    

    private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
