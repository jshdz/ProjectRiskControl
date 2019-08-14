using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRC.Model;

namespace PRC.AccesoDatos
{
    public interface IImpactoRepository
    {
        Impacto insert(Impacto pobj);
        void update(Impacto pobj);
        void delete(Impacto pobj);
        Impacto getById(int pid);
        Impacto getByValor(int pvalor);
        Impacto getByCategoria(string pcategoria);
        List<Impacto> getAll();
    }
}