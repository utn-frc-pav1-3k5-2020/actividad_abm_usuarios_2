using BugTracker.BusinessLayer;
using BugTracker.Entities;
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
    public partial class frmConsultaBugs : Form
    {
        private readonly BugService bugService;
        private readonly CriticidadService criticidadService;
        private readonly EstadoService estadoService;
        private readonly PrioridadService prioridadService;
        private readonly ProductoService productoService;
        private readonly UsuarioService usuarioService;

        public frmConsultaBugs()
        {
            InitializeComponent();
            // Inicializamos la grilla de bugs
            InitializeDataGridView();
            bugService = new BugService();
            criticidadService = new CriticidadService();
            estadoService = new EstadoService();
            prioridadService = new PrioridadService();
            productoService = new ProductoService();
            usuarioService = new UsuarioService();

        }

        private void frmBugs_Load(object sender, EventArgs e)
        {

            //LLenar combos y limpiar grid
            LlenarCombo(cboEstados, estadoService.ObtenerTodos(), "Nombre", "IdEstado");

            LlenarCombo(cboPrioridades, prioridadService.ObtenerTodos(), "Nombre", "IDPrioridad");

            LlenarCombo(cboCriticidades, criticidadService.ObtenerTodos(), "Nombre", "IDCriticidad");

            LlenarCombo(cboAsignadoA, usuarioService.ObtenerTodos(), "nombreusuario", "IDUsuario");

            LlenarCombo(cboProductos, productoService.ObtenerTodos(), "Nombre", "IDProducto");

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {

            // Dictionary: Representa una colección de claves y valores.
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            DateTime fechaDesde;
            DateTime fechaHasta;
            if (DateTime.TryParse(txtFechaDesde.Text, out fechaDesde) &&
                DateTime.TryParse(txtFechaHasta.Text, out fechaHasta))
            {
                parametros.Add("fechaDesde", txtFechaDesde.Text);
                parametros.Add("fechaHasta", txtFechaHasta.Text);
            }


            if (!string.IsNullOrEmpty(cboEstados.Text))
            {
                var idEstado = cboEstados.SelectedValue.ToString();
                parametros.Add("idEstado", idEstado);
            }

            if (!string.IsNullOrEmpty(cboAsignadoA.Text))
            {
                var asignadoA = cboAsignadoA.SelectedValue.ToString();
                parametros.Add("idUsuarioAsignado", asignadoA);
            }

            if (!string.IsNullOrEmpty(cboPrioridades.Text))
            {
                var prioridad = cboPrioridades.SelectedValue.ToString();
                parametros.Add("idPrioridad", prioridad);
            }

            if (!string.IsNullOrEmpty(cboCriticidades.Text))
            {
                var criticidad = cboCriticidades.SelectedValue.ToString();
                parametros.Add("idCriticidad", criticidad);
            }

            if (!string.IsNullOrEmpty(cboProductos.Text))
            {
                var producto = cboProductos.SelectedValue.ToString();
                parametros.Add("idProducto", producto);
            }

            IList<Bug> listadoBugs = bugService.ConsultarBugsConFiltros(parametros);

            dgvBugs.DataSource = listadoBugs;
            
            if (dgvBugs.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron coincidencias para el/los filtros ingresados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            // Datasource: establece el origen de datos de este objeto.
            cbo.DataSource = source;
            // DisplayMember: establece la propiedad que se va a mostrar para este ListControl.
            cbo.DisplayMember = display;
            // ValueMember: establece la ruta de acceso de la propiedad que se utilizará como valor real para los elementos de ListControl.
            cbo.ValueMember = value;
            //SelectedIndex: establece el índice que especifica el elemento seleccionado actualmente.
            cbo.SelectedIndex = -1;
        }


        private void InitializeDataGridView()
        {
            // Cree un DataGridView no vinculado declarando un recuento de columnas.
            dgvBugs.ColumnCount = 10;
            dgvBugs.ColumnHeadersVisible = true;

            // Configuramos la AutoGenerateColumns en false para que no se autogeneren las columnas
            dgvBugs.AutoGenerateColumns = false;

            // Cambia el estilo de la cabecera de la grilla.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgvBugs.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Definimos el nombre de la columnas y el DataPropertyName que se asocia a DataSource
            dgvBugs.Columns[0].Name = "ID";
            dgvBugs.Columns[0].DataPropertyName = "id_bug";
            // Definimos el ancho de la columna.
            dgvBugs.Columns[0].Width = 50;

            dgvBugs.Columns[1].Name = "Título";
            dgvBugs.Columns[1].DataPropertyName = "titulo";
            
            dgvBugs.Columns[2].Name = "Descripción";
            dgvBugs.Columns[2].DataPropertyName = "descripcion";

            dgvBugs.Columns[3].Name = "Responsable";
            dgvBugs.Columns[3].DataPropertyName = "UsuarioResponsable";

            dgvBugs.Columns[4].Name = "Asignado";
            dgvBugs.Columns[4].DataPropertyName = "UsuarioAsignado";
            
            dgvBugs.Columns[5].Name = "Prioridad";
            dgvBugs.Columns[5].DataPropertyName = "prioridad";
            
            dgvBugs.Columns[6].Name = "Criticidad";
            dgvBugs.Columns[6].DataPropertyName = "criticidad";
            
            dgvBugs.Columns[7].Name = "Producto";
            dgvBugs.Columns[7].DataPropertyName = "producto";

            dgvBugs.Columns[8].Name = "Fecha Alta";
            dgvBugs.Columns[8].DataPropertyName = "fechaAlta";

            dgvBugs.Columns[9].Name = "Estado";
            dgvBugs.Columns[9].DataPropertyName = "Estado";

            // Cambia el tamaño de la altura de los encabezados de columna.
            dgvBugs.AutoResizeColumnHeadersHeight();

            // Cambia el tamaño de todas las alturas de fila para ajustar el contenido de todas las celdas que no sean de encabezado.
            dgvBugs.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }

        private void btnDetalleBug_Click(object sender, EventArgs e)
        {
            if (dgvBugs.CurrentRow != null)
            {
                frmDetalleBug frmDetalle = new frmDetalleBug();
                Bug selectedItem = (Bug) dgvBugs.CurrentRow.DataBoundItem;
                frmDetalle.InicializarDetalleBug(selectedItem.IdBug);
                frmDetalle.ShowDialog();
            }
        }

        private void dgvBugs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Cuando seleccionamos una fila de la grilla habilitamos el boton btnDetalleBug.
            btnDetalleBug.Enabled = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
