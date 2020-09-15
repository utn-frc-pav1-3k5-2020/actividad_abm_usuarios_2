

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using BugTracker.Entities;
using System.Data;
using System.Data.SqlClient;

namespace BugTracker.DataAccessLayer
{
    public class UsuarioDao
    {
        public IList<Usuario> GetAll()
        {
            List<Usuario> listadoBugs = new List<Usuario>();

            String strSql = string.Concat(" SELECT id_usuario, ",
                                          "        usuario, ",
                                          "        email, ",
                                          "        password, ",
                                          "        p.id_perfil, ",
                                          "        p.nombre perfil ",
                                          "   FROM Usuarios u",
                                          "  INNER JOIN Perfiles p ON u.id_perfil= p.id_perfil " +
                                          "  WHERE u.borrado = 0 ");

            var resultadoConsulta = DataManager.GetInstance().ConsultaSQL(strSql);

            foreach (DataRow row in resultadoConsulta.Rows)
            {
                listadoBugs.Add(ObjectMapping(row));
            }

            return listadoBugs;
        }
        public Usuario GetUser(string nombreUsuario)
        {
            //Construimos la consulta sql para buscar el usuario en la base de datos.
            String strSql = string.Concat(" SELECT id_usuario, ",
                                          "        usuario, ",
                                          "        email, ",
                                          "        password, ",
                                          "        p.id_perfil, ",
                                          "        p.nombre perfil ",
                                          "   FROM Usuarios u",
                                          "  INNER JOIN Perfiles p ON u.id_perfil= p.id_perfil ",
                                          "  WHERE usuario = @usuario");

            var parametros = new Dictionary<string, object>();
            parametros.Add("usuario", nombreUsuario);
            //Usando el método GetDBHelper obtenemos la instancia unica de DBHelper (Patrón Singleton) y ejecutamos el método ConsultaSQL()
            var resultado = DataManager.GetInstance().ConsultaSQL(strSql, parametros);

            // Validamos que el resultado tenga al menos una fila.
            if (resultado.Rows.Count > 0)
            {
                return ObjectMapping(resultado.Rows[0]);
            }

            return null;
        }

        public IList<Usuario> GetByFilters(Dictionary<string, object> parametros)
        {
            List<Usuario> lst = new List<Usuario>();
            String strSql = string.Concat(" SELECT id_usuario, ",
                                              "        usuario, ",
                                              "        email, ",
                                              "        password, ",
                                              "        p.id_perfil, ",
                                              "        p.nombre perfil ",
                                              "   FROM Usuarios u",
                                              "  INNER JOIN Perfiles p ON u.id_perfil= p.id_perfil ",
                                              "  WHERE u.borrado = 0");

            if (parametros.ContainsKey("idPerfil"))
                strSql += " AND (u.id_perfil = @idPerfil) ";


            if (parametros.ContainsKey("usuario"))
                strSql += " AND (u.usuario LIKE '%' + @usuario + '%') ";

            var resultado = DataManager.GetInstance().ConsultaSQL(strSql, parametros);

            foreach (DataRow row in resultado.Rows)
                lst.Add(ObjectMapping(row));

            return lst;
        }

        internal bool Create(Usuario oUsuario)
        {
            string str_sql = "     INSERT INTO Usuarios (usuario, password, email, id_perfil, borrado)" +
                             "     VALUES (@usuario, @password, @email, @id_perfil, 0)";

            var parametros = new Dictionary<string, object>();
            parametros.Add("usuario", oUsuario.NombreUsuario);
            parametros.Add("password", oUsuario.Password);
            parametros.Add("email", oUsuario.Email);
            parametros.Add("id_perfil", oUsuario.Perfil.IdPerfil);

            // Si una fila es afectada por la inserción retorna TRUE. Caso contrario FALSE
            return (DataManager.GetInstance().EjecutarSQL(str_sql, parametros) == 1);
        }

        internal bool Update(Usuario oUsuario)
        {
            string str_sql = "  UPDATE Usuarios" +
                            "     SET usuario = @usuario," +
                            "         password = @password, " +
                            "         email = @email, " +
                            "         id_perfil = @id_perfil" +
                            "   WHERE id_usuario = @id_usuario";

            var parametros = new Dictionary<string, object>();
            parametros.Add("id_usuario", oUsuario.IdUsuario);
            parametros.Add("usuario", oUsuario.NombreUsuario);
            parametros.Add("password", oUsuario.Password);
            parametros.Add("email", oUsuario.Email);
            parametros.Add("id_perfil", oUsuario.Perfil.IdPerfil);

            // Si una fila es afectada por la actualización retorna TRUE. Caso contrario FALSE
            return (DataManager.GetInstance().EjecutarSQL(str_sql, parametros) == 1);
        }

        internal bool Delete(Usuario oUsuario)
        {
            string str_sql = "  UPDATE Usuarios" +
                            "     SET borrado = 1" +
                            "   WHERE id_usuario = @id_usuario";

            var parametros = new Dictionary<string, object>();
            parametros.Add("id_usuario", oUsuario.IdUsuario);

            // Si una fila es afectada por la actualización retorna TRUE. Caso contrario FALSE
            return (DataManager.GetInstance().EjecutarSQL(str_sql, parametros) == 1);
        }


        private Usuario ObjectMapping(DataRow row)
        {
            Usuario oUsuario = new Usuario
            {
                IdUsuario = Convert.ToInt32(row["id_usuario"].ToString()),
                NombreUsuario = row["usuario"].ToString(),
                Email = row["email"].ToString(),
                Password = row.Table.Columns.Contains("password") ? row["password"].ToString() : null,
                Perfil = new Perfil() {
                    IdPerfil = Convert.ToInt32(row["id_perfil"].ToString()),
                    Nombre = row["perfil"].ToString(),
                }                
            };

            return oUsuario;
        }
    }

}