using CADSistema;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Word;
using Sistema.DSSistemaTableAdapters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Document = iTextSharp.text.Document;
using Paragraph = iTextSharp.text.Paragraph;

namespace Sistema
{
    public partial class frmSuministros : Form
    {
        List<Suministro> suministros = new List<Suministro>();
        String Ruta = "C://Rowing/";
        WebClient client = new WebClient();
        String NombreArchivo = "";

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
            obrasNuevoSuministrosTableAdapter.FillByFecha(this.dSSistema.ObrasNuevoSuministros, fecha);
            dgvDatos.AutoResizeColumns();


            suministros.Clear();

            foreach (DataGridViewRow dr in dgvDatos.Rows)
            {
                Suministro suministro = new Suministro();
                suministro.ANTESFOTO1 = (string)dr.Cells[4].Value;
                suministro.ANTESFOTO2 = (string)dr.Cells[5].Value;
                suministro.APELLIDONOMBRE = (string)dr.Cells[9].Value;
                suministro.BARRIO = (string)dr.Cells[17].Value;
                suministro.CAUSANTEC = "";
                suministro.CONEXIONDIRECTA = (string)dr.Cells[27].Value;
                suministro.CORTE = (string)dr.Cells[23].Value;
                suministro.CUADRILLA = (string)dr.Cells[13].Value;
                suministro.DENUNCIA = (string)dr.Cells[24].Value;
                suministro.DESPUESFOTO1 = (string)dr.Cells[6].Value;
                suministro.DESPUESFOTO2 = (string)dr.Cells[7].Value;
                suministro.DIRECTA = "";
                suministro.DNI = (string)dr.Cells[10].Value;
                suministro.DOMICILIO = (string)dr.Cells[14].Value;
                suministro.EMAIL = (string)dr.Cells[12].Value;
                suministro.ENRE = (string)dr.Cells[25].Value;
                suministro.ENTRECALLES1 = (string)dr.Cells[15].Value;
                suministro.ENTRECALLES2 = (string)dr.Cells[16].Value;
                suministro.FECHA = (DateTime)dr.Cells[8].Value;
                suministro.FIRMACLIENTE = (string)dr.Cells[3].Value;
                suministro.FOTODNIFRENTE = (string)dr.Cells[1].Value;
                suministro.FOTODNIREVERSO = (string)dr.Cells[2].Value;
                suministro.GRUPOC = "";
                suministro.IDCERTIFBAREMO = 0;
                suministro.IDCERTIFMATERIALES = 0;
                suministro.IDUserCarga = 0;
                suministro.KITNRO = 0;
                suministro.LOCALIDAD = (string)dr.Cells[18].Value;
                suministro.MEDIDORCOLOCADO = (string)dr.Cells[20].Value;
                suministro.MEDIDORVECINO = (string)dr.Cells[21].Value;
                suministro.MTSCABLERETIRADO = (int)dr.Cells[30].Value;
                suministro.NROOBRA = 0;
                suministro.NROSUMINISTRO = (int)dr.Cells[0].Value;
                suministro.OBSERVACIONES = (string)dr.Cells[33].Value;
                suministro.OTRO = (string)dr.Cells[26].Value;
                suministro.PARTIDO = (string)dr.Cells[19].Value;
                suministro.POSTEPODRIDO = (string)dr.Cells[32].Value;
                suministro.POTENCIACONTRATADA = (int)dr.Cells[34].Value;
                suministro.RETIROCONEXION = (string)dr.Cells[28].Value;
                suministro.RETIROCRUCECALLE = (string)dr.Cells[29].Value;
                suministro.TELEFONO = (string)dr.Cells[11].Value;
                suministro.TENSIONCONTRATADA = (int)dr.Cells[35].Value;
                suministro.TIPORED = (string)dr.Cells[22].Value;
                suministro.TRABAJOCONHIDRO = (string)dr.Cells[31].Value;

                suministros.Add(suministro);
            }
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

            if (parcialImageUrlDNIFrente.Length > 0)
            {
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
            pbAntes2.ImageLocation = imageUrlAntes2;
            pbDespues1.ImageLocation = imageUrlDespues1;
            pbDespues2.ImageLocation = imageUrlDespues2;



            BorraArchivo(Ruta + "Antes1.png");
            BorraArchivo(Ruta + "Antes2.png");
            BorraArchivo(Ruta + "Despues1.png");
            BorraArchivo(Ruta + "Despues2.png");
            BorraArchivo(Ruta + "DNIDorso.png");
            BorraArchivo(Ruta + "DNIFrente  .png");
            BorraArchivo(Ruta + "Firma.png");

            if (imageUrlDNIFrente != "")
            {
                client.DownloadFile(imageUrlDNIFrente, @"C://Rowing//DNIFrente.png");
            }
            if (imageUrlDNIDorso != "")
            {
                client.DownloadFile(imageUrlDNIDorso, @"C://Rowing//DNIDorso.png");
            }
            if (imageUrlFirma != "")
            {
                client.DownloadFile(imageUrlFirma, @"C://Rowing//Firma.png");
            }
            if (imageUrlAntes1 != "")
            {
                client.DownloadFile(imageUrlAntes1, @"C://Rowing//Antes1.png");
            }
            if (imageUrlAntes2 != "")
            {
                client.DownloadFile(imageUrlAntes2, @"C://Rowing//Antes2.png");
            }
            if (imageUrlDespues1 != "")
            {
                client.DownloadFile(imageUrlDespues1, @"C://Rowing//Despues1.png");
            }
            if (imageUrlDespues2 != "")
            {
                client.DownloadFile(imageUrlDespues2, @"C://Rowing//Despues2.png");
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (txtNroSuministro.Text == "")
            {
                MessageBox.Show("Debe seleccionar un Suministro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //**************************************************************************************************************


            NombreArchivo = "Suministro N° " + txtNroSuministro.Text + ".pdf";

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.A4);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@Ruta + NombreArchivo, FileMode.Create));

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Creamos la imagen y le ajustamos el tamaño
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("C://Rowing//logo.png");
            logo.BorderWidth = 0;
            logo.Alignment = Element.ALIGN_RIGHT;
            float percentage = 0.0f;
            percentage = 150 / logo.Width;
            logo.ScalePercent(percentage * 100);

            // Insertamos la imagen en el documento
            doc.Add(logo);
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


            var ancho = 50;
            //DNI Frente
            // Creamos la imagen y le ajustamos el tamaño
            doc.Add(new Paragraph("DNI Frente"));
            iTextSharp.text.Image DNIFrente = iTextSharp.text.Image.GetInstance("C://Rowing//DNIFrente.png");
            DNIFrente.BorderWidth = 0;
            DNIFrente.Alignment = Element.ALIGN_LEFT;
            percentage = ancho / DNIFrente.Width;
            DNIFrente.ScalePercent(percentage * 100);
            // Insertamos la imagen en el documento
            doc.Add(DNIFrente);
            doc.Add(Chunk.NEWLINE);

            //DNI Dorso
            // Creamos la imagen y le ajustamos el tamaño
            doc.Add(new Paragraph("DNI Dorso"));
            iTextSharp.text.Image DNIDorso = iTextSharp.text.Image.GetInstance("C://Rowing//DNIDorso.png");
            DNIDorso.BorderWidth = 0;
            DNIDorso.Alignment = Element.ALIGN_LEFT;
            percentage = ancho / DNIDorso.Width;
            DNIDorso.ScalePercent(percentage * 100);
            // Insertamos la imagen en el documento
            doc.Add(DNIDorso);
            doc.Add(Chunk.NEWLINE);

            //Firma
            // Creamos la imagen y le ajustamos el tamaño
            doc.Add(new Paragraph("Firma"));
            iTextSharp.text.Image Firma = iTextSharp.text.Image.GetInstance("C://Rowing//Firma.png");
            Firma.BorderWidth = 0;
            Firma.Alignment = Element.ALIGN_LEFT;
            percentage = ancho / Firma.Width;
            Firma.ScalePercent(percentage * 100);
            // Insertamos la imagen en el documento
            doc.Add(Firma);
            doc.Add(Chunk.NEWLINE);

            //Antes1
            // Creamos la imagen y le ajustamos el tamaño
            doc.Add(new Paragraph("Antes 1"));
            iTextSharp.text.Image Antes1 = iTextSharp.text.Image.GetInstance("C://Rowing//Antes1.png");
            Antes1.BorderWidth = 0;
            Antes1.Alignment = Element.ALIGN_LEFT;
            percentage = ancho / Antes1.Width;
            Antes1.ScalePercent(percentage * 100);
            // Insertamos la imagen en el documento
            doc.Add(Antes1);
            doc.Add(Chunk.NEWLINE);

            //Antes2
            // Creamos la imagen y le ajustamos el tamaño
            doc.Add(new Paragraph("Antes 2"));
            iTextSharp.text.Image Antes2 = iTextSharp.text.Image.GetInstance("C://Rowing//Antes2.png");
            Antes2.BorderWidth = 0;
            Antes2.Alignment = Element.ALIGN_LEFT;
            percentage = ancho / Antes2.Width;
            Antes2.ScalePercent(percentage * 100);
            // Insertamos la imagen en el documento
            doc.Add(Antes2);
            doc.Add(Chunk.NEWLINE);

            //Despues1
            // Creamos la imagen y le ajustamos el tamaño
            doc.Add(new Paragraph("Despues 1"));
            iTextSharp.text.Image Despues1 = iTextSharp.text.Image.GetInstance("C://Rowing//Despues1.png");
            Despues1.BorderWidth = 0;
            Despues1.Alignment = Element.ALIGN_LEFT;
            percentage = ancho / Despues1.Width;
            Despues1.ScalePercent(percentage * 100);
            // Insertamos la imagen en el documento
            doc.Add(Despues1);
            doc.Add(Chunk.NEWLINE);

            //Despues2
            // Creamos la imagen y le ajustamos el tamaño
            doc.Add(new Paragraph("Despues 2"));
            iTextSharp.text.Image Despues2 = iTextSharp.text.Image.GetInstance("C://Rowing//Despues2.png");
            Despues2.BorderWidth = 0;
            Despues2.Alignment = Element.ALIGN_LEFT;
            percentage = ancho / Despues2.Width;
            Despues2.ScalePercent(percentage * 100);
            // Insertamos la imagen en el documento
            doc.Add(Despues2);
            doc.Add(Chunk.NEWLINE);

            doc.Close();
            writer.Close();



            //**************************************************************************************************************

            MessageBox.Show("PDF grabado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void BorraArchivo(String ruta)
        {
            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }
        }

        private void btnGenerarTodosPDF_Click(object sender, EventArgs e)
        {
            if (suministros.Count == 0)
            {
                MessageBox.Show("No hay Suministros en la lista", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (Suministro suministro in suministros)
            {
                btnGenerarPDF(suministro);
            }
            MessageBox.Show("Se generaron " + suministros.Count.ToString() + " PDF.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGenerarPDF(Suministro suministro)
        {
            _borrarArchivos();


            if (suministro.FOTODNIFRENTEImageFullPath != "")
            {
                client.DownloadFile(suministro.FOTODNIFRENTEImageFullPath, @"C://Rowing//DNIFrente.png");
            }
            if (suministro.FOTODNIREVERSOImageFullPath != "")
            {
                client.DownloadFile(suministro.FOTODNIREVERSOImageFullPath, @"C://Rowing//DNIDorso.png");
            }
            if (suministro.FIRMACLIENTEImageFullPath != "")
            {
                client.DownloadFile(suministro.FIRMACLIENTEImageFullPath, @"C://Rowing//Firma.png");
            }
            if (suministro.ANTESFOTO1ImageFullPath != "")
            {
                client.DownloadFile(suministro.ANTESFOTO1ImageFullPath, @"C://Rowing//Antes1.png");
            }
            if (suministro.ANTESFOTO2ImageFullPath != "")
            {
                client.DownloadFile(suministro.ANTESFOTO2ImageFullPath, @"C://Rowing//Antes2.png");
            }
            if (suministro.DESPUESFOTO1ImageFullPath != "")
            {
                client.DownloadFile(suministro.DESPUESFOTO1ImageFullPath, @"C://Rowing//Despues1.png");
            }
            if (suministro.DESPUESFOTO2ImageFullPath != "")
            {
                client.DownloadFile(suministro.DESPUESFOTO2ImageFullPath, @"C://Rowing//Despues2.png");
            }

            NombreArchivo = "Suministro N° " + suministro.NROSUMINISTRO.ToString() + ".pdf";

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.A4);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@Ruta + NombreArchivo, FileMode.Create));

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _standardBoldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            // Creamos la imagen y le ajustamos el tamaño
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Ruta + "logo.png");
            logo.BorderWidth = 0;
            logo.Alignment = Element.ALIGN_RIGHT;
            float percentage = 0.0f;
            percentage = 150 / logo.Width;
            logo.ScalePercent(percentage * 100);

            // Insertamos la imagen en el documento
            doc.Add(logo);
            doc.Add(Chunk.NEWLINE);

            // Escribimos el encabezamiento en el documento
            Paragraph titulo = new Paragraph("SUMINISTRO N° " + suministro.NROSUMINISTRO.ToString());
            titulo.Alignment = (int)WdParagraphAlignment.wdAlignParagraphCenter;
            titulo.Font = _titleFont;
            doc.Add(titulo);
            doc.Add(Chunk.NEWLINE);

            // Dibujando una línea
            PdfContentByte linea = writer.DirectContent; //'declaración de la linea     
            linea.SetLineWidth(0.75);// 'configurando el ancho de linea
            linea.MoveTo(35, 740); //'MoveTo indica el punto de inicio
            linea.LineTo(560, 740);//'LineTo indica hacia donde se dibuja la linea 
            linea.Stroke(); //'traza la linea actual y se puede iniciar una nueva


            // Creamos una tabla que contendrá los datos del cliente
            PdfPTable tblCliente = new PdfPTable(2);
            tblCliente.WidthPercentage = 100;
            float[] widths = new float[] { 1f, 4f };
            tblCliente.SetWidths(widths);

            // Configuramos el título de las columnas de la tabla
            PdfPCell clTitulo = new PdfPCell(new Phrase("", _standardFont));
            clTitulo.BorderWidth = 0;
            clTitulo.BorderWidthBottom = 0f;
            

            PdfPCell clDato = new PdfPCell(new Phrase("", _standardFont));
            clDato.BorderWidth = 0;
            clDato.BorderWidthBottom = 0f;
            

            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);


            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("FECHA: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(((DateTime) suministro.FECHA).Day.ToString()+"/"+ ((DateTime)suministro.FECHA).Month.ToString()+ ((DateTime)suministro.FECHA).Year.ToString(), _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);
            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(" ", _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);


            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("NOMBRE Y APELLIDO: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(suministro.APELLIDONOMBRE, _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);
            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("DOMICILIO: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(suministro.DOMICILIO, _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);
            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("ENTRE CALLES: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(suministro.ENTRECALLES1 + " y " + suministro.ENTRECALLES2, _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);
            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("BARRIO: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(suministro.BARRIO, _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);
            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("LOCALIDAD: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(suministro.LOCALIDAD, _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);
            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("PARTIDO: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(suministro.PARTIDO, _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);
            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("TELEFONO: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(suministro.TELEFONO, _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);
            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("MAIL: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(suministro.EMAIL, _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);
            // Llenamos la tabla con información
            clTitulo = new PdfPCell(new Phrase("DNI: ", _standardBoldFont));
            clTitulo.BorderWidth = 0;
            clDato = new PdfPCell(new Phrase(suministro.DNI, _standardFont));
            clDato.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblCliente.AddCell(clTitulo);
            tblCliente.AddCell(clDato);


            // Añadimos la tabla al documento PDF
            doc.Add(tblCliente);



            // Creamos una tabla que contendrá los títulos de las fotos del DNI
            PdfPTable tblDNI = new PdfPTable(2);
            tblCliente.WidthPercentage = 100;
            float[] widthsDNI = new float[] { 1f, 1f };
            tblCliente.SetWidths(widthsDNI);

            // Configuramos el título de las columnas de la tabla
            PdfPCell clDNIFrente = new PdfPCell(new Phrase("", _standardFont));
            clDNIFrente.BorderWidth = 0;
            clDNIFrente.BorderWidthBottom = 0f;


            PdfPCell clDNIDorso = new PdfPCell(new Phrase("", _standardFont));
            clDNIDorso.BorderWidth = 0;
            clDNIDorso.BorderWidthBottom = 0f;


            // Añadimos las celdas a la tabla
            tblDNI.AddCell(clDNIFrente);
            tblDNI.AddCell(clDNIDorso);


            // Llenamos la tabla con información
            clDNIFrente = new PdfPCell(new Phrase("DNI FRENTE", _standardBoldFont));
            clDNIFrente.BorderWidth = 0;
            clDNIFrente.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDNIDorso = new PdfPCell(new Phrase("DNI DORSO", _standardBoldFont));
            clDNIDorso.BorderWidth = 0;
            clDNIDorso.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblDNI.AddCell(clDNIFrente);
            tblDNI.AddCell(clDNIDorso);
            // Llenamos la tabla con información
            clDNIFrente = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDNIFrente.BorderWidth = 0;
            clDNIFrente.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDNIDorso = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDNIDorso.BorderWidth = 0;
            clDNIDorso.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblDNI.AddCell(clDNIFrente);
            tblDNI.AddCell(clDNIDorso);


            // Llenamos la tabla con información
            var ancho = 150;
            iTextSharp.text.Image DNIFrente = iTextSharp.text.Image.GetInstance("C://Rowing//DNIFrente.png");
            DNIFrente.BorderWidth = 0;
            DNIFrente.Alignment = Element.ALIGN_JUSTIFIED_ALL;
            //percentage = ancho / DNIFrente.Width;
            //DNIFrente.ScalePercent(percentage * 100);
            DNIFrente.ScaleToFit(160f, 120f);

            iTextSharp.text.Image DNIDorso = iTextSharp.text.Image.GetInstance("C://Rowing//DNIDorso.png");
            DNIDorso.BorderWidth = 0;
            DNIDorso.Alignment = Element.ALIGN_JUSTIFIED_ALL;
            //percentage = ancho / DNIDorso.Width;
            //DNIDorso.ScalePercent(percentage * 100);
            DNIDorso.ScaleToFit(160f, 120f);

            clDNIFrente = new PdfPCell(DNIFrente);
            clDNIFrente.BorderWidth = 0;
            clDNIDorso = new PdfPCell(DNIDorso);
            clDNIDorso.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblDNI.AddCell(clDNIFrente);
            tblDNI.AddCell(clDNIDorso);

            // Añadimos la tabla al documento PDF
            doc.Add(tblDNI);

            // Dibujando una línea
            PdfContentByte linea2 = writer.DirectContent; //'declaración de la linea     
            linea2.SetLineWidth(0.75);// 'configurando el ancho de linea
            linea2.MoveTo(35, 420); //'MoveTo indica el punto de inicio
            linea2.LineTo(560, 420);//'LineTo indica hacia donde se dibuja la linea 
            linea2.Stroke(); //'traza la linea actual y se puede iniciar una nueva




            // Creamos una tabla que contendrá los datos del cliente
            PdfPTable tblSuministro = new PdfPTable(4);
            tblSuministro.WidthPercentage = 100;
            float[] widths3= new float[] { 5f, 3f, 5f, 3f };
            tblSuministro.SetWidths(widths3);

            // Configuramos el título de las columnas de la tabla
            PdfPCell clTitulo1 = new PdfPCell(new Phrase("", _standardFont));
            clTitulo1.BorderWidth = 0;
            clTitulo1.BorderWidthBottom = 0f;

            PdfPCell clDato1 = new PdfPCell(new Phrase("", _standardFont));
            clDato1.BorderWidth = 0;
            clDato1.BorderWidthBottom = 0f;

            PdfPCell clTitulo2 = new PdfPCell(new Phrase("", _standardFont));
            clTitulo2.BorderWidth = 0;
            clTitulo2.BorderWidthBottom = 0f;

            PdfPCell clDato2 = new PdfPCell(new Phrase("", _standardFont));
            clDato2.BorderWidth = 0;
            clDato2.BorderWidthBottom = 0f;


            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);


            // Llenamos la tabla con información
            clTitulo1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(" ", _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(" ", _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            clTitulo1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(" ", _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(" ", _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            clTitulo1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(" ", _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(" ", _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

          

            clTitulo1 = new PdfPCell(new Phrase("POTENCIA CONTRATADA: ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(suministro.POTENCIACONTRATADA.ToString(), _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase("MEDIDOR COLOCADO: ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(suministro.MEDIDORCOLOCADO.ToString(), _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            clTitulo1 = new PdfPCell(new Phrase("TENSION CONTRATADA: ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(suministro.TENSIONCONTRATADA.ToString(), _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase("MEDIDOR VECINO: ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(suministro.MEDIDORVECINO.ToString(), _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            clTitulo1 = new PdfPCell(new Phrase("TIPO RED: ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(suministro.TIPORED.ToString(), _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase("ENRE: ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(suministro.ENRE.ToString(), _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            clTitulo1 = new PdfPCell(new Phrase("CONEXION DIRECTA: ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(suministro.CONEXIONDIRECTA.ToString(), _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase("RETIRO CONEXION: ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(suministro.RETIROCONEXION.ToString(), _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            clTitulo1 = new PdfPCell(new Phrase("RETIRO CRUCE CALLE: ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(suministro.RETIROCRUCECALLE.ToString(), _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase("MTS. CABLE RETIRADO: ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(suministro.MTSCABLERETIRADO.ToString(), _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            clTitulo1 = new PdfPCell(new Phrase("POSTE PODRIDO: ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(suministro.POSTEPODRIDO.ToString(), _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase("TRABAJO CON HIDRO: ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(suministro.TRABAJOCONHIDRO.ToString(), _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            clTitulo1 = new PdfPCell(new Phrase("CORTE: ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(suministro.CORTE.ToString(), _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase("DENUNCIA: ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(suministro.DENUNCIA.ToString(), _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            clTitulo1 = new PdfPCell(new Phrase("DIRECTA: ", _standardBoldFont));
            clTitulo1.BorderWidth = 0;
            clDato1 = new PdfPCell(new Phrase(suministro.DIRECTA.ToString(), _standardFont));
            clDato1.BorderWidth = 0;
            clTitulo2 = new PdfPCell(new Phrase("OTRO: ", _standardBoldFont));
            clTitulo2.BorderWidth = 0;
            clDato2 = new PdfPCell(new Phrase(suministro.OTRO.ToString(), _standardFont));
            clDato2.BorderWidth = 0;
            // Añadimos las celdas a la tabla
            tblSuministro.AddCell(clTitulo1);
            tblSuministro.AddCell(clDato1);
            tblSuministro.AddCell(clTitulo2);
            tblSuministro.AddCell(clDato2);

            // Añadimos la tabla al documento PDF
            doc.Add(tblSuministro);

            Paragraph observacionesTitulo = new Paragraph();
            Paragraph observacionesDato = new Paragraph();

            observacionesTitulo.Font = _standardBoldFont;
            observacionesDato.Font = _standardFont;

            observacionesTitulo.Add("OBSERVACIONES: ");
            observacionesDato.Add(suministro.OBSERVACIONES);

            doc.Add(observacionesTitulo);
            doc.Add(observacionesDato);

            // Dibujando una línea
            PdfContentByte linea3 = writer.DirectContent; //'declaración de la linea     
            linea3.SetLineWidth(0.75);// 'configurando el ancho de linea
            linea3.MoveTo(35, 250); //'MoveTo indica el punto de inicio
            linea3.LineTo(560, 250);//'LineTo indica hacia donde se dibuja la linea 
            linea3.Stroke(); //'traza la linea actual y se puede iniciar una nueva


            // Creamos una tabla que contendrá los títulos de las fotos Antes1 y Despues1
            PdfPTable tblAntesDespues1 = new PdfPTable(2);
            tblAntesDespues1.WidthPercentage = 100;
            float[] widthsAntesDespues1 = new float[] { 1f, 1f };
            tblAntesDespues1.SetWidths(widthsAntesDespues1);

            // Configuramos el título de las columnas de la tabla
            PdfPCell clAntes1 = new PdfPCell(new Phrase("", _standardFont));
            clAntes1.BorderWidth = 0;
            clAntes1.BorderWidthBottom = 0f;


            PdfPCell clDespues1 = new PdfPCell(new Phrase("", _standardFont));
            clDespues1.BorderWidth = 0;
            clDespues1.BorderWidthBottom = 0f;


            // Añadimos las celdas a la tabla
            tblDNI.AddCell(clAntes1);
            tblDNI.AddCell(clDespues1);


            // Llenamos la tabla con información
            clAntes1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes1.BorderWidth = 0;
            clAntes1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues1.BorderWidth = 0;
            clDespues1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues1.AddCell(clAntes1);
            tblAntesDespues1.AddCell(clDespues1);

            clAntes1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes1.BorderWidth = 0;
            clAntes1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues1.BorderWidth = 0;
            clDespues1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues1.AddCell(clAntes1);
            tblAntesDespues1.AddCell(clDespues1);

            clAntes1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes1.BorderWidth = 0;
            clAntes1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues1.BorderWidth = 0;
            clDespues1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues1.AddCell(clAntes1);
            tblAntesDespues1.AddCell(clDespues1);

            clAntes1 = new PdfPCell(new Phrase("ANTES 1", _standardBoldFont));
            clAntes1.BorderWidth = 0;
            clAntes1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues1 = new PdfPCell(new Phrase("DESPUES 1", _standardBoldFont));
            clDespues1.BorderWidth = 0;
            clDespues1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues1.AddCell(clAntes1);
            tblAntesDespues1.AddCell(clDespues1);

            // Llenamos la tabla con información
            clAntes1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes1.BorderWidth = 0;
            clAntes1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues1 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues1.BorderWidth = 0;
            clDespues1.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues1.AddCell(clAntes1);
            tblAntesDespues1.AddCell(clDespues1);


            // Llenamos la tabla con información
            var ancho2 = 150;
            iTextSharp.text.Image Antes1 = iTextSharp.text.Image.GetInstance("C://Rowing//Antes1.png");
            Antes1.BorderWidth = 0;
            Antes1.Alignment = Element.ALIGN_JUSTIFIED_ALL;
            //percentage = ancho / DNIFrente.Width;
            //DNIFrente.ScalePercent(percentage * 100);
            Antes1.ScaleToFit(160f, 120f);

            iTextSharp.text.Image Despues1 = iTextSharp.text.Image.GetInstance("C://Rowing//Despues1.png");
            Despues1.BorderWidth = 0;
            Despues1.Alignment = Element.ALIGN_JUSTIFIED_ALL;
            //percentage = ancho / DNIDorso.Width;
            //DNIDorso.ScalePercent(percentage * 100);
            Despues1.ScaleToFit(160f, 120f);

            clAntes1 = new PdfPCell(Antes1);
            clAntes1.BorderWidth = 0;
            clDespues1 = new PdfPCell(Despues1);
            clDespues1.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblAntesDespues1.AddCell(clAntes1);
            tblAntesDespues1.AddCell(clDespues1);

            // Añadimos la tabla al documento PDF
            doc.Add(tblAntesDespues1);

            // Creamos una tabla que contendrá los títulos de las fotos Antes2 y Despues2
            PdfPTable tblAntesDespues2 = new PdfPTable(2);
            tblAntesDespues1.WidthPercentage = 100;
            float[] widthsAntesDespues2 = new float[] { 1f, 1f };
            tblAntesDespues1.SetWidths(widthsAntesDespues1);

            // Configuramos el título de las columnas de la tabla
            PdfPCell clAntes2 = new PdfPCell(new Phrase("", _standardFont));
            clAntes2.BorderWidth = 0;
            clAntes2.BorderWidthBottom = 0f;


            PdfPCell clDespues2 = new PdfPCell(new Phrase("", _standardFont));
            clDespues2.BorderWidth = 0;
            clDespues2.BorderWidthBottom = 0f;


            // Añadimos las celdas a la tabla
            tblDNI.AddCell(clAntes2);
            tblDNI.AddCell(clDespues2);


            // Llenamos la tabla con información
            clAntes2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes2.BorderWidth = 0;
            clAntes2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues2.BorderWidth = 0;
            clDespues2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues2.AddCell(clAntes2);
            tblAntesDespues2.AddCell(clDespues2);

            clAntes2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes2.BorderWidth = 0;
            clAntes2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues2.BorderWidth = 0;
            clDespues2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues2.AddCell(clAntes2);
            tblAntesDespues2.AddCell(clDespues2);

            clAntes2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes2.BorderWidth = 0;
            clAntes2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues2.BorderWidth = 0;
            clDespues2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues2.AddCell(clAntes2);
            tblAntesDespues2.AddCell(clDespues2);

            

            // Llenamos la tabla con información
            clAntes2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes2.BorderWidth = 0;
            clAntes2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues2.BorderWidth = 0;
            clDespues2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues2.AddCell(clAntes2);
            tblAntesDespues2.AddCell(clDespues2);

            // Llenamos la tabla con información
            clAntes2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes2.BorderWidth = 0;
            clAntes2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues2.BorderWidth = 0;
            clDespues2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues2.AddCell(clAntes2);
            tblAntesDespues2.AddCell(clDespues2);

            clAntes2 = new PdfPCell(new Phrase("ANTES 2", _standardBoldFont));
            clAntes2.BorderWidth = 0;
            clAntes2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues2 = new PdfPCell(new Phrase("DESPUES 2", _standardBoldFont));
            clDespues2.BorderWidth = 0;
            clDespues2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues2.AddCell(clAntes2);
            tblAntesDespues2.AddCell(clDespues2);

            // Llenamos la tabla con información
            clAntes2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clAntes2.BorderWidth = 0;
            clAntes2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            clDespues2 = new PdfPCell(new Phrase(" ", _standardBoldFont));
            clDespues2.BorderWidth = 0;
            clDespues2.HorizontalAlignment = Element.ALIGN_CENTER; ;
            // Añadimos las celdas a la tabla
            tblAntesDespues2.AddCell(clAntes2);
            tblAntesDespues2.AddCell(clDespues2);



            // Llenamos la tabla con información
            var ancho3 = 150;
            iTextSharp.text.Image Antes2 = iTextSharp.text.Image.GetInstance("C://Rowing//Antes2.png");
            Antes2.BorderWidth = 0;
            Antes2.Alignment = Element.ALIGN_JUSTIFIED_ALL;
            //percentage = ancho / DNIFrente.Width;
            //DNIFrente.ScalePercent(percentage * 100);
            Antes2.ScaleToFit(160f, 120f);

            iTextSharp.text.Image Despues2 = iTextSharp.text.Image.GetInstance("C://Rowing//Despues2.png");
            Despues2.BorderWidth = 0;
            Despues2.Alignment = Element.ALIGN_JUSTIFIED_ALL;
            //percentage = ancho / DNIDorso.Width;
            //DNIDorso.ScalePercent(percentage * 100);
            Despues2.ScaleToFit(160f, 120f);

            clAntes2 = new PdfPCell(Antes2);
            clAntes2.BorderWidth = 0;
            clDespues2 = new PdfPCell(Despues2);
            clDespues2.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblAntesDespues2.AddCell(clAntes2);
            tblAntesDespues2.AddCell(clDespues2);

            // Añadimos la tabla al documento PDF
            doc.Add(tblAntesDespues2);
            //doc.Add(Chunk.NEWLINE);

            // Dibujando una línea
            PdfContentByte linea4 = writer.DirectContent; //'declaración de la linea     
            linea4.SetLineWidth(0.75);// 'configurando el ancho de linea
            linea4.MoveTo(35, 650); //'MoveTo indica el punto de inicio
            linea4.LineTo(560, 650);//'LineTo indica hacia donde se dibuja la linea 
            linea4.Stroke(); //'traza la linea actual y se puede iniciar una nueva


            iTextSharp.text.Image Firma = iTextSharp.text.Image.GetInstance("C://Rowing//Firma.png");
            Firma.BorderWidth = 0;
            Firma.Alignment = Element.ALIGN_RIGHT;
            //percentage = ancho / DNIFrente.Width;
            //DNIFrente.ScalePercent(percentage * 100);
            Firma.ScaleToFit(100f, 60f);
            doc.Add(Firma);


            Paragraph firma = new Paragraph();
            firma.Font = _standardBoldFont;
            firma.Add("FIRMA DEL CLIENTE");
            firma.Alignment= Element.ALIGN_RIGHT;
            doc.Add(firma);

            //Cerramos el PDF
            doc.Close();
            writer.Close();

            _borrarArchivos();
        }

        private void _borrarArchivos()

        {
            BorraArchivo(Ruta + "Antes1.png");
            BorraArchivo(Ruta + "Antes2.png");
            BorraArchivo(Ruta + "Despues1.png");
            BorraArchivo(Ruta + "Despues2.png");
            BorraArchivo(Ruta + "DNIDorso.png");
            BorraArchivo(Ruta + "DNIFrente.png");
            BorraArchivo(Ruta + "Firma.png");
        }
    }
}
