using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;
using BugTracker.DataAccessLayer;

namespace BugTracker.BusinessLayer
{
    public class UsuarioService
    {
        private UsuarioDao oUsuarioDao;
        public UsuarioService()
        {
            oUsuarioDao = new UsuarioDao();
        }
        public IList<Usuario> ObtenerTodos()
        {
            return oUsuarioDao.GetAll();
        }

        public Usuario ValidarUsuario(string usuario, string password)
        {
            var usr = oUsuarioDao.GetUser(usuario);

            if (usr.Password != null && usr.Password.Equals(password))
            {
                return usr;
            }

            return null;
        }

        internal bool CrearUsuario(Usuario oUsuario)
        {
            return oUsuarioDao.Create(oUsuario);
        }

        internal bool ActualizarUsuario(Usuario oUsuarioSelected)
        {
            return oUsuarioDao.Update(oUsuarioSelected);
        }

        internal bool EliminarUsuario(Usuario oUsuarioSelected)
        {
            return oUsuarioDao.Delete(oUsuarioSelected);
        }

        internal object ObtenerUsuario(string usuario)
        {

            return oUsuarioDao.GetUser(usuario);
        }

        internal IList<Usuario> ConsultarConFiltro(Dictionary<string, object> filtros)
        {
            return oUsuarioDao.GetByFilters(filtros);
        }
    }
}
