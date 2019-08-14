using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRC.API.Core
{
    public class RiesgoTabla
    {
        public int idRiesgo { get; set; }
        public int probabilidad { get; set; }
        public int impacto { get; set; }
        public int valorPI { get; set; }
        public string nivel { get; set; }
        public string color { get; set; }
    }
}