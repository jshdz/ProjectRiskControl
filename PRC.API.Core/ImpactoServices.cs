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
        private static int MAX_CATEGORIAS = 4;

        static ImpactoServices()
        {
            repository = new ImpactoRepository();
        }

        public static Impacto insert(Impacto pobj)
        {
            if (validarImpacto(pobj)) {
                repository.insert(pobj);
            } else {
                pobj = null;
            }

            return pobj;
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

        public static Impacto getByCategoria(string pcategoria)
        {
            return repository.getByCategoria(pcategoria);
        }

        public static List<Impacto> getAll()
        {
            return repository.getAll();
        }

        /// <summary>
        /// Método que hace la llamada a todas las verificaciones que se deben hacer para un Impacto.
        /// </summary>
        /// <param name="pobj"></param>
        /// <returns>true: las validaciones fueron exitosas y false: las validaciones no fueron exitosas</returns>
        public static bool validarImpacto(Impacto pobj)
        {
            if (validarMaxValor(pobj) && validarMaxCategoria(pobj)) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método que valida que un Impacto no tenga una categoria ya ingresada en el sistema.
        /// </summary>
        /// <param name="pobj"></param>
        /// <returns>true: no hay categorias repetidas y false: hay una categoria igual</returns>
        private static bool validarMaxCategoria(Impacto pobj)
        {
            List<Impacto> listaImpactos = getAll();

            if (listaImpactos.Count()< MAX_CATEGORIAS) {

                foreach (Impacto impacto in listaImpactos) {
                    if (pobj.categoria == impacto.categoria) {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Método que valida que un Impacto no tenga un valor ya ingresado en el sistema.
        /// </summary>
        /// <param name="pobj"></param>
        /// <returns>true: no hay valores repetidos y false hay un valor igual</returns>
        private static bool validarMaxValor(Impacto pobj)
        {
            if (pobj.valor > 0) { 
                List<Impacto> listaImpactos = getAll();

                foreach (Impacto impacto in listaImpactos) {     
                    if (pobj.valor == impacto.valor) {
                        return false;
                    } 
                }

                return true;
            } else {
                return false;
            }
        }
    }
}