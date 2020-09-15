using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;
using BugTracker.DataAccessLayer;

namespace BugTracker.BusinessLayer
{
    public class CriticidadService
    {
        private CriticidadDao oCriticidadDao;
        public CriticidadService()
        {
            oCriticidadDao = new CriticidadDao();
        }
        public IList<Criticidad> ObtenerTodos()
        {
            return oCriticidadDao.GetAll();
        }

    }
}
