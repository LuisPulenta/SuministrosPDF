using System;
using System.Windows.Forms;

namespace Sistema
{
    public partial class frmSuministros : Form
    {
        public frmSuministros()
        {
            InitializeComponent();
        }

        //------------------- frmUsuarios_Load -------------------------
        private void frmSuministros_Load(object sender, EventArgs e)
        {
            this.obrasNuevoSuministrosTableAdapter2.Fill(this.dSSistema.ObrasNuevoSuministros);
            dgvDatos.AutoResizeColumns();
        }

        private void obrasNuevoSuministrosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.obrasNuevoSuministrosBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dSSistema);

        }
    }
}
