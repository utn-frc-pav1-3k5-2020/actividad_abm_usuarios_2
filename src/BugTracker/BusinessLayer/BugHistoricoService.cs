using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;
using BugTracker.DataAccessLayer;

namespace BugTracker.BusinessLayer
{
    public class BugHistoricoService
    {
        private BugHistoricoDao oBugHistoricoDao;
        public BugHistoricoService()
        {
            oBugHistoricoDao = new BugHistoricoDao();
        }
        public IList<BugHistorico> ConsultarPorIdBug(int idBug)
        {
            return oBugHistoricoDao.GetByIdBug(idBug);
        }
    }
}
