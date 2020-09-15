

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
    public class CriticidadDao
    {
        public IList<Criticidad> GetAll()
        {
            List<Criticidad> listadoBugs = new List<Criticidad>();

            var strSql = "SELECT id_criticidad, nombre from Criticidades";

            var resultadoConsulta = DataManager.GetInstance().ConsultaSQL(strSql);

            foreach (DataRow row in resultadoConsulta.Rows)
            {
                listadoBugs.Add(ObjectMapping(row));
            }

            return listadoBugs;
        }

        private Criticidad ObjectMapping(DataRow row)
        {
            Criticidad oCriticidad = new Criticidad
            {
                IdCriticidad = Convert.ToInt32(row["id_criticidad"].ToString()),
                Nombre = row["nombre"].ToString()
            };

            return oCriticidad;
        }
    }

}