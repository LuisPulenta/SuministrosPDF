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
            //this.obrasNuevoSuministrosTableAdapter3.Fill(this.dSSistema.ObrasNuevoSuministros);
            ActualizarDatos();
            dgvDatos.AutoResizeColumns();
        }

        private void obrasNuevoSuministrosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.obrasNuevoSuministrosBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dSSistema);

        }

        private void ActualizarDatos()
        {
           


            string dia = dtpFecha.Value.Day.ToString();
            if (dia.Length == 1)
            {
                dia = '0' + dia;
            }
            string mes = dtpFecha.Value.Month.ToString();
            if (mes.Length == 1)
            {
                mes = '0' + mes;
            }
            string ano = dtpFecha.Value.Year.ToString();
            string fecha = dia + '-' + mes + '-' + ano;

            this.obrasNuevoSuministrosTableAdapter3.FillByFecha(this.dSSistema.ObrasNuevoSuministros,fecha);
            dgvDatos.AutoResizeColumns();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            ActualizarDatos();
        }

        private void obrasNuevoSuministrosBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.obrasNuevoSuministrosBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dSSistema);

        }
    }
}
