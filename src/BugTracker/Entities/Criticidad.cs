using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Entities
{
    public class Criticidad

    {
        public int IdCriticidad { get; set; }
        public string Nombre { get; set; }
        
        public override string ToString()
        {
            return Nombre;
        }
    }
}
