

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
    public class PrioridadDao
    {
        public IList<Prioridad> GetAll()
        {
            List<Prioridad> listadoBugs = new List<Prioridad>();

            var strSql = "SELECT id_prioridad, nombre from Prioridades";

            var resultadoConsulta = DataManager.GetInstance().ConsultaSQL(strSql);

            foreach (DataRow row in resultadoConsulta.Rows)
            {
                listadoBugs.Add(ObjectMapping(row));
            }

            return listadoBugs;
        }

        private Prioridad ObjectMapping(DataRow row)
        {
            Prioridad oPrioridad = new Prioridad
            {
                IdPrioridad = Convert.ToInt32(row["id_prioridad"].ToString()),
                Nombre = row["nombre"].ToString()
            };

            return oPrioridad;
        }
    }

}