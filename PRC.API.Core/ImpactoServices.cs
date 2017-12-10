using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRC.AccesoDatos;
using PRC.Model;

namespace PRC.API.Core
{
    public static class ImpactoServices
    {
        static IImpactoRepository repository;

        static ImpactoServices()
        {
            repository = new ImpactoRepository();
        }

        public static Impacto insert(Impacto pobj)
        {
            return repository.insert(pobj);
        }

        public static void update(Impacto pobj)
        {
            repository.update(pobj);
        }

        public static void delete(Impacto pobj)
        {
            repository.delete(pobj);
        }

        public static Impacto getById(int pid)
        {
            return repository.getById(pid);
        }

        public static List<Impacto> getAll()
        {
            return repository.getAll();
        }
    }
}
