using BugTracker.BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTracker.GUILayer
{
    public partial class frmDetalleBug : Form
    {
        private BugService bugService;
        private BugHistoricoService bugHistoricoService;
        public frmDetalleBug()
        {
            InitializeComponent();

            bugService = new BugService();
            bugHistoricoService = new BugHistoricoService();
        }

        internal void InicializarDetalleBug(int idBug)
        {


            var resultado = bugService.ConsultarBugsPorId(idBug);

            if (resultado != null)
            {
                txtNroBug.Text = resultado.IdBug.ToString();
                txtTitulo.Text = resultado.Titulo;
                txtDescripcion.Text = resultado.Descripcion;
                txtFechaAlta.Text = resultado.FechaAlta.ToString();
                txtProducto.Text = resultado.Producto.Nombre;
                txtPrioridad.Text = resultado.Prioridad.Nombre;
                txtCriticidad.Text = resultado.Criticidad.Nombre;
                txtEstado.Text = resultado.Estado.Nombre;
            }

            InicializarHistoricoBug(idBug);
        }


        internal void InicializarHistoricoBug(int idBug)
        {
            InitializeDataGridView();


            dgvHistoricoBug.DataSource = bugHistoricoService.ConsultarPorIdBug(idBug);
        }

        private void InitializeDataGridView()
        {
            // Cree un DataGridView no vinculado declarando un recuento de columnas.
            dgvHistoricoBug.ColumnCount = 10;
            dgvHistoricoBug.ColumnHeadersVisible = true;

            // Configuramos la AutoGenerateColumns en false para que no se autogeneren las columnas
            dgvHistoricoBug.AutoGenerateColumns = false;

            // Cambia el estilo de la cabecera de la grilla.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgvHistoricoBug.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Definimos el nombre de la columnas y el DataPropertyName que se asocia a DataSource
            dgvHistoricoBug.Columns[0].Name = "ID";
            dgvHistoricoBug.Columns[0].DataPropertyName = "idBugHistorico";
            // Definimos el ancho de la columna.
            dgvHistoricoBug.Columns[0].Width = 50;

            dgvHistoricoBug.Columns[1].Name = "Título";
            dgvHistoricoBug.Columns[1].DataPropertyName = "Titulo";

            dgvHistoricoBug.Columns[2].Name = "Descripción";
            dgvHistoricoBug.Columns[2].DataPropertyName = "Descripcion";

            dgvHistoricoBug.Columns[3].Name = "Responsable";
            dgvHistoricoBug.Columns[3].DataPropertyName = "UsuarioResponsable";

            dgvHistoricoBug.Columns[4].Name = "Asignado";
            dgvHistoricoBug.Columns[4].DataPropertyName = "UsuarioAsignado";

            dgvHistoricoBug.Columns[5].Name = "Prioridad";
            dgvHistoricoBug.Columns[5].DataPropertyName = "Prioridad";

            dgvHistoricoBug.Columns[6].Name = "Criticidad";
            dgvHistoricoBug.Columns[6].DataPropertyName = "Criticidad";

            dgvHistoricoBug.Columns[7].Name = "Producto";
            dgvHistoricoBug.Columns[7].DataPropertyName = "Producto";

            dgvHistoricoBug.Columns[8].Name = "Fecha Histórico";
            dgvHistoricoBug.Columns[8].DataPropertyName = "FechaHistorico";

            dgvHistoricoBug.Columns[9].Name = "Estado";
            dgvHistoricoBug.Columns[9].DataPropertyName = "Estado";

            // Cambia el tamaño de la altura de los encabezados de columna.
            dgvHistoricoBug.AutoResizeColumnHeadersHeight();

            // Cambia el tamaño de todas las alturas de fila para ajustar el contenido de todas las celdas que no sean de encabezado.
            dgvHistoricoBug.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
