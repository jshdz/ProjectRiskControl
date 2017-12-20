using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRC.Model;

namespace PRC.AccesoDatos
{
    public interface IRiesgoRepository
    {
        Riesgo insert(Riesgo pobj);
        void update(Riesgo pobj);
        void delete(Riesgo pobj);
        Riesgo getById(int pid);
        List<Riesgo> getAll();
    }
}
