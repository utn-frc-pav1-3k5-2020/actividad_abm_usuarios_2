

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
    public class PerfilDao
    {
        public IList<Perfil> GetAll()
        {
            List<Perfil> listadoBugs = new List<Perfil>();

            var strSql = "SELECT * From Perfiles WHERE id_perfil <> 1";

            var resultadoConsulta = DataManager.GetInstance().ConsultaSQL(strSql);

            foreach (DataRow row in resultadoConsulta.Rows)
            {
                listadoBugs.Add(ObjectMapping(row));
            }

            return listadoBugs;
        }

        private Perfil ObjectMapping(DataRow row)
        {
            Perfil oPerfil = new Perfil
            {
                IdPerfil = Convert.ToInt32(row["id_Perfil"].ToString()),
                Nombre = row["nombre"].ToString()
            };

            return oPerfil;
        }
    }

}