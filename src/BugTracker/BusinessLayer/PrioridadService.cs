using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;
using BugTracker.DataAccessLayer;

namespace BugTracker.BusinessLayer
{
    public class PrioridadService
    {
        private PrioridadDao oPrioridadDao;
        public PrioridadService()
        {
            oPrioridadDao = new PrioridadDao();
        }
        public IList<Prioridad> ObtenerTodos()
        {
            return oPrioridadDao.GetAll();
        }

    }
}
