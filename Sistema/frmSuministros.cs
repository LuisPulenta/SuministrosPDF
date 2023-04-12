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
            dgvDatos.Columns[1].Visible = false;
            dgvDatos.Columns[2].Visible = false;
            dgvDatos.Columns[3].Visible = false;
            dgvDatos.Columns[4].Visible = false;
            dgvDatos.Columns[5].Visible = false;
            dgvDatos.Columns[6].Visible = false;
            dgvDatos.Columns[7].Visible = false;
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

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNroSuministro.Text = dgvDatos.Rows[e.RowIndex].Cells[0].Value.ToString();

            String imageUrlDNIFrente = "";
            String imageUrlDNIDorso = "";
            String imageUrlFirma = "";
            String imageUrlAntes1 = "";
            String imageUrlAntes2 = "";
            String imageUrlDespues1 = "";
            String imageUrlDespues2 = "";


            String parcialImageUrlDNIFrente = dgvDatos.Rows[e.RowIndex].Cells[1].Value.ToString();
            String parcialImageUrlDNIDorso = dgvDatos.Rows[e.RowIndex].Cells[2].Value.ToString();
            String parcialImageUrlFirma = dgvDatos.Rows[e.RowIndex].Cells[3].Value.ToString();
            String parcialImageUrlAntes1 = dgvDatos.Rows[e.RowIndex].Cells[4].Value.ToString();
            String parcialImageUrlAntes2 = dgvDatos.Rows[e.RowIndex].Cells[5].Value.ToString();
            String parcialImageUrlDespues1 = dgvDatos.Rows[e.RowIndex].Cells[6].Value.ToString();
            String parcialImageUrlDespues2 = dgvDatos.Rows[e.RowIndex].Cells[7].Value.ToString();

            if (parcialImageUrlDNIFrente.Length>0) {
                imageUrlDNIFrente = $"http://190.111.249.225/RowingAppApi{parcialImageUrlDNIFrente.Substring(1)}";
            }
            if (parcialImageUrlDNIDorso.Length > 0)
            {
                imageUrlDNIDorso = $"http://190.111.249.225/RowingAppApi{parcialImageUrlDNIDorso.Substring(1)}";
            }
            if (parcialImageUrlFirma.Length > 0)
            {
                imageUrlFirma = $"http://190.111.249.225/RowingAppApi{parcialImageUrlFirma.Substring(1)}";
            }
            if (parcialImageUrlAntes1.Length > 0)
            {
                imageUrlAntes1 = $"http://190.111.249.225/RowingAppApi{parcialImageUrlAntes1.Substring(1)}";
            }
            if (parcialImageUrlAntes2.Length > 0)
            {
                imageUrlAntes2 = $"http://190.111.249.225/RowingAppApi{parcialImageUrlAntes2.Substring(1)}";
            }
            if (parcialImageUrlDespues1.Length > 0)
            {
                imageUrlDespues1 = $"http://190.111.249.225/RowingAppApi{parcialImageUrlDespues1.Substring(1)}";
            }
            if (parcialImageUrlDespues2.Length > 0)
            {
                imageUrlDespues2 = $"http://190.111.249.225/RowingAppApi{parcialImageUrlDespues2.Substring(1)}";
            }


            pbDNIFrente.ImageLocation = "";
            pbDNIDorso.ImageLocation = "";
            pbFirma.ImageLocation = "";
            pbAntes1.ImageLocation = "";
            pbAntes2.ImageLocation = "";
            pbDespues1.ImageLocation = "";
            pbDespues2.ImageLocation = "";

            pbDNIFrente.ImageLocation = imageUrlDNIFrente;
            pbDNIDorso.ImageLocation = imageUrlDNIDorso;
            pbFirma.ImageLocation = imageUrlFirma;
            pbAntes1.ImageLocation = imageUrlAntes1;
            pbAntes2.ImageLocation = imageUrlAntes1;
            pbDespues1.ImageLocation = imageUrlDespues1;
            pbDespues2.ImageLocation = imageUrlDespues2;



        }
    }
}
