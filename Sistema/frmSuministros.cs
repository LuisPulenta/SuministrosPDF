using iTextSharp.text;
using iTextSharp.text.pdf;
using Sistema.DSSistemaTableAdapters;
using System;
using System.IO;
using System.Windows.Forms;

namespace Sistema
{
    public partial class frmSuministros : Form
    {

        private static readonly string _dataDir = "..\\Samples";
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
            ObrasNuevoSuministrosTableAdapter obrasNuevoSuministrosTableAdapter = new ObrasNuevoSuministrosTableAdapter();


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
            obrasNuevoSuministrosTableAdapter.FillByFecha(this.dSSistema.ObrasNuevoSuministros,fecha);
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

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (txtNroSuministro.Text == "")
            {
                MessageBox.Show("Debe seleccionar un Suministro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //**************************************************************************************************************

            String Ruta = "C://Rowing/";
            String NombreArchivo = "Suministro N° " + txtNroSuministro.Text + ".pdf";

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.A4);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@Ruta+NombreArchivo, FileMode.Create));

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Creamos la imagen y le ajustamos el tamaño
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C://Rowing//logo.png");
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_RIGHT;
            float percentage = 0.0f;
            percentage = 150 / imagen.Width;
            imagen.ScalePercent(percentage * 100);

            // Insertamos la imagen en el documento
            doc.Add(imagen);
            doc.Add(Chunk.NEWLINE);

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("SUMINISTRO N° " + txtNroSuministro.Text));
            doc.Add(Chunk.NEWLINE);



            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 18f, BaseColor.BLUE);
            title.Add("Hola Mundo!!");
            doc.Add(title);


            doc.Add(new Paragraph("Hola Mundo!!"));
            doc.Add(new Paragraph("Parrafo 1"));
            doc.Add(new Paragraph("Parrafo 2"));

            // Creamos una tabla que contendrá el nombre, apellido y país
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(3);
            tblPrueba.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", _standardFont));
            clNombre.BorderWidth = 0;
            clNombre.BorderWidthBottom = 0.75f;

            PdfPCell clApellido = new PdfPCell(new Phrase("Apellido", _standardFont));
            clApellido.BorderWidth = 0;
            clApellido.BorderWidthBottom = 0.75f;

            PdfPCell clPais = new PdfPCell(new Phrase("País", _standardFont));
            clPais.BorderWidth = 0;
            clPais.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clApellido);
            tblPrueba.AddCell(clPais);

            // Llenamos la tabla con información
            clNombre = new PdfPCell(new Phrase("Roberto", _standardFont));
            clNombre.BorderWidth = 0;

            clApellido = new PdfPCell(new Phrase("Torres", _standardFont));
            clApellido.BorderWidth = 0;

            clPais = new PdfPCell(new Phrase("Puerto Rico", _standardFont));
            clPais.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clApellido);
            tblPrueba.AddCell(clPais);


            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(tblPrueba);

            


            doc.Close();
            writer.Close();



            //**************************************************************************************************************

            MessageBox.Show("PDF grabado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
