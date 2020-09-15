

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

namespace BugTracker.DataAccessLayer
{
    public class BugDao
    {
        public Bug GetBugById(int idBug)
        {
            var strSql = String.Concat("SELECT bug.id_bug, ",
                                      "        bug.titulo,",
                                      "        bug.descripcion,",
                                      "        bug.fecha_alta,",
                                      "        bug.id_usuario_responsable,",
                                      "        responsable.usuario as responsable,  ",
                                      "        bug.id_usuario_asignado,",
                                      "        asignado.usuario as asignado, ",
                                      "        bug.id_producto,",
                                      "        producto.nombre as producto, ",
                                      "        bug.id_prioridad,",
                                      "        prioridad.nombre as prioridad, ",
                                      "        bug.id_criticidad,",
                                      "        criticidad.nombre as criticidad, ",
                                      "        bug.id_estado,",
                                      "        estado.nombre as estado",
                                       "   FROM Bugs as bug",
                                       "   LEFT JOIN Usuarios as responsable ON responsable.id_usuario = bug.id_usuario_responsable",
                                       "   LEFT JOIN Usuarios as asignado ON asignado.id_usuario = bug.id_usuario_asignado",
                                       "  INNER JOIN Productos as producto ON producto.id_producto = bug.id_producto",
                                       "  INNER JOIN Prioridades as prioridad ON  prioridad.id_prioridad = bug.id_prioridad",
                                       "  INNER JOIN Criticidades as criticidad ON criticidad.id_criticidad = bug.id_criticidad",
                                       "  INNER JOIN Estados as estado ON estado.id_estado = bug.id_estado",
                                       " WHERE borrado = 0 and id_bug = " + idBug.ToString());

            return ObjectMapping(DataManager.GetInstance().ConsultaSQL(strSql).Rows[0]);
        }

        public IList<Bug> GetBugByFilters(Dictionary<string, object> parametros)
        {
            List<Bug> listadoBugs = new List<Bug>();

            var strSql = String.Concat("SELECT bug.id_bug, ",
                                      "        bug.titulo,",
                                      "        bug.descripcion,",
                                      "        bug.fecha_alta,",
                                      "        bug.id_usuario_responsable,",
                                      "        responsable.usuario as responsable,  ",
                                      "        bug.id_usuario_asignado,",
                                      "        asignado.usuario as asignado, ",
                                      "        bug.id_producto,",
                                      "        producto.nombre as producto, ",
                                      "        bug.id_prioridad,",
                                      "        prioridad.nombre as prioridad, ",
                                      "        bug.id_criticidad,",
                                      "        criticidad.nombre as criticidad, ",
                                      "        bug.id_estado,",
                                      "        estado.nombre as estado",
                                      "   FROM Bugs as bug",
                                      "   LEFT JOIN Usuarios as responsable ON responsable.id_usuario = bug.id_usuario_responsable",
                                      "   LEFT JOIN Usuarios as asignado ON asignado.id_usuario = bug.id_usuario_asignado",
                                      "  INNER JOIN Productos as producto ON producto.id_producto = bug.id_producto",
                                      "  INNER JOIN Prioridades as prioridad ON  prioridad.id_prioridad = bug.id_prioridad",
                                      "  INNER JOIN Criticidades as criticidad ON criticidad.id_criticidad = bug.id_criticidad",
                                      "  INNER JOIN Estados as estado ON estado.id_estado = bug.id_estado",
                                      "  WHERE borrado = 0 ");

            if (parametros.ContainsKey("fechaDesde") && parametros.ContainsKey("fechaHasta"))
                strSql += " AND (fecha_alta>=@fechaDesde AND fecha_alta<=@fechaHasta) ";
            if (parametros.ContainsKey("idPrioridad"))
                strSql += " AND (prioridad.id_prioridad=@idPrioridad) ";
            if (parametros.ContainsKey("idCriticidad"))
                strSql += " AND (criticidad.id_criticidad=@idCriticidad) ";
            if (parametros.ContainsKey("idProducto"))
                strSql += " AND (producto.id_producto=@idProducto) ";
            if (parametros.ContainsKey("idEstado"))
                strSql += " AND (estado.id_estado=@idEstado)  ";
            if (parametros.ContainsKey("idUsuarioAsignado"))
                strSql += " AND (id_usuario_asignado=@idUsuarioAsignado) ";
            strSql += " ORDER BY bug.fecha_alta DESC";

            var resultadoConsulta = DataManager.GetInstance().ConsultaSQL(strSql, parametros).Rows;

            foreach (DataRow row in resultadoConsulta)
            {
                listadoBugs.Add(ObjectMapping(row));
            }

            return listadoBugs;
        }

        private Bug ObjectMapping(DataRow row)
        {
            Bug oBug = new Bug();
            oBug.IdBug = Convert.ToInt32(row["id_bug"].ToString());
            oBug.Titulo = row["titulo"].ToString();
            oBug.Descripcion = row["descripcion"].ToString();
            oBug.FechaAlta = Convert.ToDateTime(row["fecha_alta"].ToString());
            oBug.Producto = new Producto();
            oBug.Producto.IdProducto = Convert.ToInt32(row["id_producto"].ToString());
            oBug.Producto.Nombre = row["producto"].ToString();

            oBug.Prioridad = new Prioridad();
            oBug.Prioridad.IdPrioridad = Convert.ToInt32(row["id_prioridad"].ToString());
            oBug.Prioridad.Nombre = row["prioridad"].ToString();

            oBug.Criticidad = new Criticidad();
            oBug.Criticidad.IdCriticidad = Convert.ToInt32(row["id_criticidad"].ToString());
            oBug.Criticidad.Nombre = row["criticidad"].ToString();


            oBug.Estado = new Estado();
            oBug.Estado.IdEstado = Convert.ToInt32(row["id_estado"].ToString());
            oBug.Estado.Nombre = row["estado"].ToString();

            oBug.UsuarioResponsable = new Usuario();
            oBug.UsuarioResponsable.IdUsuario = Convert.ToInt32(row["id_usuario_responsable"].ToString());
            oBug.UsuarioResponsable.NombreUsuario = row["responsable"].ToString();

            oBug.UsuarioAsignado = new Usuario();
            oBug.UsuarioAsignado.IdUsuario = Convert.ToInt32(row["id_usuario_asignado"].ToString());
            oBug.UsuarioAsignado.NombreUsuario = row["responsable"].ToString();


            return oBug;
        }
    }

}