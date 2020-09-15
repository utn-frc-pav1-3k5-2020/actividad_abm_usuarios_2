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

namespace BugTracker.GUILayer.Usuarios
{
    public partial class frmABMUsuario : Form
    {

        private FormMode formMode = FormMode.nuevo;

        private readonly UsuarioService oUsuarioService;
        private readonly PerfilService oPerfilService;
        private Usuario oUsuarioSelected;

        public frmABMUsuario()
        {
            InitializeComponent();
            oUsuarioService = new UsuarioService();
            oPerfilService = new PerfilService();
        }

        public enum FormMode
        {
            nuevo,
            actualizar,
            eliminar
        }


        private void frmABMUsuario_Load(System.Object sender, System.EventArgs e)
        {
            LlenarCombo(cboPerfil, oPerfilService.ObtenerTodos(), "Nombre", "IdPerfil");
            switch (formMode)
            {
                case FormMode.nuevo:
                    {
                        this.Text = "Nuevo Usuario";
                        break;
                    }

                case FormMode.actualizar:
                    {
                        this.Text = "Actualizar Usuario";
                        // Recuperar usuario seleccionado en la grilla 
                        MostrarDatos();
                        txtNombre.Enabled = true;
                        txtEmail.Enabled = true;
                        txtEmail.Enabled = true;
                        txtPassword.Enabled = true;
                        txtConfirmarPass.Enabled = true;
                        cboPerfil.Enabled = true;
                        break;
                    }

                case FormMode.eliminar:
                    {
                        MostrarDatos();
                        this.Text = "Habilitar/Deshabilitar Usuario";
                        txtNombre.Enabled = false;
                        txtEmail.Enabled = false;
                        txtEmail.Enabled = false;
                        txtPassword.Enabled = false;
                        cboPerfil.Enabled = false;
                        txtConfirmarPass.Enabled = false;
                        break;
                    }
            }
        }

        public void InicializarFormulario(FormMode op, Usuario usuarioSelected)
        {
            formMode = op;
            oUsuarioSelected = usuarioSelected;
        }

        private void MostrarDatos()
        {
            if (oUsuarioSelected != null)
            {
                txtNombre.Text = oUsuarioSelected.NombreUsuario;
                txtEmail.Text = oUsuarioSelected.Email;
                txtPassword.Text = oUsuarioSelected.Password;
                txtConfirmarPass.Text = txtPassword.Text;
                cboPerfil.Text = oUsuarioSelected.Perfil.Nombre;
            }
        }

        private void btnAceptar_Click(System.Object sender, System.EventArgs e)
        {
            switch (formMode)
            {
                case FormMode.nuevo:
                    {
                        if (ExisteUsuario() == false)
                        {
                            if (ValidarCampos())
                            {
                                var oUsuario = new Usuario();
                                oUsuario.NombreUsuario = txtNombre.Text;
                                oUsuario.Password = txtPassword.Text;
                                oUsuario.Email = txtEmail.Text;
                                oUsuario.Perfil = new Perfil();
                                oUsuario.Perfil.IdPerfil = (int)cboPerfil.SelectedValue;

                                if (oUsuarioService.CrearUsuario(oUsuario))
                                {
                                    MessageBox.Show("Usuario insertado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                        }
                        else
                            MessageBox.Show("Nombre de usuario encontrado!. Ingrese un nombre diferente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                case FormMode.actualizar:
                    {
                        if (ValidarCampos())
                        {
                            oUsuarioSelected.NombreUsuario = txtNombre.Text;
                            oUsuarioSelected.Password = txtPassword.Text;
                            oUsuarioSelected.Email = txtEmail.Text;
                            oUsuarioSelected.Perfil = new Perfil();
                            oUsuarioSelected.Perfil.IdPerfil = (int)cboPerfil.SelectedValue;

                            if (oUsuarioService.ActualizarUsuario(oUsuarioSelected))
                            {
                                MessageBox.Show("Usuario actualizado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Dispose();
                            }
                            else
                                MessageBox.Show("Error al actualizar el usuario!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }

                case FormMode.eliminar:
                    {
                        if (MessageBox.Show("Seguro que desea habilitar/deshabilitar el usuario seleccionado?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {                         
                            if (oUsuarioService.EliminarUsuario(oUsuarioSelected))
                            {
                                MessageBox.Show("Usuario Habilitado/Deshabilitado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                                MessageBox.Show("Error al actualizar el usuario!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
            }
        }

        private bool ValidarCampos()
        {
            // campos obligatorios
            if (txtNombre.Text == string.Empty)
            {
                txtNombre.BackColor = Color.Red;
                txtNombre.Focus();
                return false;
            }
            else
                txtNombre.BackColor = Color.White;

            if (txtPassword.Text == string.Empty)
            {
                txtPassword.BackColor = Color.Red;
                txtPassword.Focus();
                return false;
            }
            else
                txtPassword.BackColor = Color.White;

            if (txtConfirmarPass.Text == string.Empty)
            {
                txtConfirmarPass.BackColor = Color.Red;
                txtConfirmarPass.Focus();
                return false;
            }
            else
                txtConfirmarPass.BackColor = Color.White;

            if (cboPerfil.Text == string.Empty)
            {
                cboPerfil.BackColor = Color.Red;
                cboPerfil.Focus();
                return false;
            }
            else
                cboPerfil.BackColor = Color.White;

            if (txtConfirmarPass.Text != txtPassword.Text)
            {
                txtConfirmarPass.BackColor = Color.Red;
                txtPassword.BackColor = Color.Red;
                txtConfirmarPass.Focus();
                return false;
            }
            else
            {
                txtConfirmarPass.BackColor = Color.White;
                txtPassword.BackColor = Color.White;
            }

            return true;
        }

        private bool ExisteUsuario()
        {
            return oUsuarioService.ObtenerUsuario(txtNombre.Text) != null;
        }


        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            cbo.DataSource = source;
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
            cbo.SelectedIndex = -1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

    }
}
