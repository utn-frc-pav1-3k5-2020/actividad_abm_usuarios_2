

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
    public class BugHistoricoDao
    {
        public IList<BugHistorico> GetByIdBug(int idBug)
        {
            List<BugHistorico> listadoBugs = new List<BugHistorico>();

            var strSql = String.Concat("SELECT historico.id_bug_historico, ",
                                      "        historico.id_bug,",
                                      "        historico.titulo,",
                                      "        historico.descripcion,",
                                      "        historico.fecha_historico,",
                                      "        historico.id_usuario_responsable,",
                                      "        responsable.usuario as responsable,  ",
                                      "        historico.id_usuario_asignado,",
                                      "        asignado.usuario as asignado, ",
                                      "        historico.id_producto,",
                                      "        producto.nombre as producto, ",
                                      "        historico.id_prioridad,",
                                      "        prioridad.nombre as prioridad, ",
                                      "        historico.id_criticidad,",
                                      "        criticidad.nombre as criticidad, ",
                                      "        historico.id_estado,",
                                      "        estado.nombre as estado",
                                      "   FROM BugsHistorico as historico",
                                      "   LEFT JOIN Usuarios as responsable ON responsable.id_usuario = historico.id_usuario_responsable",
                                      "   LEFT JOIN Usuarios as asignado ON asignado.id_usuario = historico.id_usuario_asignado",
                                      "  INNER JOIN Productos as producto ON producto.id_producto = historico.id_producto",
                                      "  INNER JOIN Prioridades as prioridad ON  prioridad.id_prioridad = historico.id_prioridad",
                                      "  INNER JOIN Criticidades as criticidad ON criticidad.id_criticidad = historico.id_criticidad",
                                      "  INNER JOIN Estados as estado ON estado.id_estado = historico.id_estado",
                                      "  WHERE id_bug = " + idBug.ToString());

            var resultado = DataManager.GetInstance().ConsultaSQL(strSql);

            foreach (DataRow row in resultado.Rows)
            {
                listadoBugs.Add(ObjectMapping(row));
            }

            return listadoBugs;
        }

        private BugHistorico ObjectMapping(DataRow row)
        {
            BugHistorico oBug = new BugHistorico();
            oBug.IdBugHistorico = Convert.ToInt32(row["id_bug_historico"].ToString());
            oBug.Titulo = row["titulo"].ToString();
            oBug.Descripcion = row["descripcion"].ToString();
            oBug.FechaHistorico = Convert.ToDateTime(row["fecha_historico"].ToString());
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