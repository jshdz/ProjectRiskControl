using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRC.AccesoDatos;
using PRC.Model;

namespace PRC.API.Core
{
    public static class RiesgoServices
    {
        static IRiesgoRepository repository;
        private static List<string> listaCategoriasRiesgo = new List<string> {"bajo", "medio", "alto"};

        static RiesgoServices()
        {
            repository = new RiesgoRepository();
        }

        public static Riesgo insert(Riesgo pobj)
        {
            repository.insert(pobj);

            return pobj;
        }

        public static void update(Riesgo pobj)
        {
            repository.update(pobj);
        }

        public static void delete(Riesgo pobj)
        {
            repository.delete(pobj);
        }

        public static Riesgo getById(int pid)
        {
            return repository.getById(pid);
        }

        public static List<Riesgo> getAll()
        {
            return repository.getAll();
        }

        public static int calcularRiesgoPI()
        {
            return 0;
        }
        
        public static bool validarRiesgo(Riesgo pobj)
        {
            return false;
        }
    }
}