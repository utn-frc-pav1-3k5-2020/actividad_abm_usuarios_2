

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
    public class ProductoDao
    {
        public IList<Producto> GetAll()
        {
            List<Producto> listadoBugs = new List<Producto>();

            var strSql = "SELECT id_producto, nombre from Productos";

            var resultadoConsulta = DataManager.GetInstance().ConsultaSQL(strSql);

            foreach (DataRow row in resultadoConsulta.Rows)
            {
                listadoBugs.Add(ObjectMapping(row));
            }

            return listadoBugs;
        }

        private Producto ObjectMapping(DataRow row)
        {
            Producto oProducto = new Producto
            {
                IdProducto = Convert.ToInt32(row["id_producto"].ToString()),
                Nombre = row["nombre"].ToString()
            };

            return oProducto;
        }
    }

}