using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BugTracker.BusinessLayer;
using BugTracker.Entities;

namespace BugTracker.GUILayer.Usuarios
{
    public partial class frmUsuarios : Form
    {

        private UsuarioService oUsuarioService;
        private PerfilService oPerfilService;

        public frmUsuarios()
        {
            InitializeComponent();
            InitializeDataGridView();
            oUsuarioService = new UsuarioService();
            oPerfilService = new PerfilService();

        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            LlenarCombo(cboPerfiles, oPerfilService.ObtenerTodos(), "Nombre", "IdPerfil");
            this.CenterToParent();
        }

        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            cbo.DataSource = source;
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
            cbo.SelectedIndex = -1;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmABMUsuario formulario = new frmABMUsuario();
            formulario.ShowDialog();
            btnConsultar_Click(sender, e);
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (chkTodos.Checked)
                {
                    txtNombre.Enabled = false;
                    cboPerfiles.Enabled = false;
                }
                else
                {
                    txtNombre.Enabled = true;
                    cboPerfiles.Enabled = true;
                }
            }
        }

        private void btnSalir_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnConsultar_Click(System.Object sender, System.EventArgs e)
        {
            var filters = new Dictionary<string, object>();

            if (!chkTodos.Checked)
            {
                // Validar si el combo 'Perfiles' esta seleccionado.
                if (cboPerfiles.Text != string.Empty)
                {
                    // Si el cbo tiene un texto no vacìo entonces recuperamos el valor de la propiedad ValueMember
                    filters.Add("idPerfil", cboPerfiles.SelectedValue);
                }

                // Validar si el textBox 'Nombre' esta vacio.
                if (txtNombre.Text != string.Empty)
                {
                    // Si el textBox tiene un texto no vacìo entonces recuperamos el valor del texto
                    filters.Add("usuario", txtNombre.Text);
                }

                if (filters.Count > 0)
                    dgvUsers.DataSource = oUsuarioService.ConsultarConFiltro(filters);
                else
                    MessageBox.Show("Debe ingresar al menos un criterio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                dgvUsers.DataSource = oUsuarioService.ObtenerTodos();
        }

        private void btnEditar_Click(System.Object sender, System.EventArgs e)
        {
            frmABMUsuario formulario = new frmABMUsuario();
            var usuario = (Usuario)dgvUsers.CurrentRow.DataBoundItem;
            formulario.InicializarFormulario(frmABMUsuario.FormMode.actualizar, usuario);
            formulario.ShowDialog();
            btnConsultar_Click(sender, e);
        }

        private void dgvUsers_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnQuitar.Enabled = true;
        }

        private void btnQuitar_Click(System.Object sender, System.EventArgs e)
        {
            frmABMUsuario formulario = new frmABMUsuario();
            var usuario = (Usuario)dgvUsers.CurrentRow.DataBoundItem;
            formulario.InicializarFormulario(frmABMUsuario.FormMode.eliminar, usuario);
            formulario.ShowDialog();
            btnConsultar_Click(sender, e);
        }

        private void InitializeDataGridView()
        {
            // Cree un DataGridView no vinculado declarando un recuento de columnas.
            dgvUsers.ColumnCount = 3;
            dgvUsers.ColumnHeadersVisible = true;

            // Configuramos la AutoGenerateColumns en false para que no se autogeneren las columnas
            dgvUsers.AutoGenerateColumns = false;

            // Cambia el estilo de la cabecera de la grilla.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgvUsers.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Definimos el nombre de la columnas y el DataPropertyName que se asocia a DataSource
            dgvUsers.Columns[0].Name = "Usuario";
            dgvUsers.Columns[0].DataPropertyName = "NombreUsuario";
            // Definimos el ancho de la columna.

            dgvUsers.Columns[1].Name = "Email";
            dgvUsers.Columns[1].DataPropertyName = "Email";

            dgvUsers.Columns[2].Name = "Perfil";
            dgvUsers.Columns[2].DataPropertyName = "Perfil";

            // Cambia el tamaño de la altura de los encabezados de columna.
            dgvUsers.AutoResizeColumnHeadersHeight();

            // Cambia el tamaño de todas las alturas de fila para ajustar el contenido de todas las celdas que no sean de encabezado.
            dgvUsers.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }


    }
}
