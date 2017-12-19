using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRC.Model;

namespace PRC.AccesoDatos
{
    public interface IProbabilidadRepository
    {
        Probabilidad insert(Probabilidad pobj);
        void update(Probabilidad pobj);
        void delete(Probabilidad pobj);
        Probabilidad getById(int pid);
        Probabilidad getByCategoria(string pcategoria);
        List<Probabilidad> getAll();
    }
}