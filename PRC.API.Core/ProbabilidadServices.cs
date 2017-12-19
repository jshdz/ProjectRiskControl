using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRC.AccesoDatos;
using PRC.Model;

namespace PRC.API.Core
{
    public static class ProbabilidadServices
    {
        static IProbabilidadRepository repository;
        private static int MAX_CATEGORIAS = 4;
        private static int MAX_PORC_RANGO = 99;
        private static int MIN_PORC_RANGO = 1;

        static ProbabilidadServices()
        {
            repository = new ProbabilidadRepository();
        }

        public static Probabilidad insert(Probabilidad pobj)
        {
            if (validarProbabilidad(pobj)) {
                repository.insert(pobj);
            } else {
                pobj = null;
            }

            return pobj;
        }

        public static void update(Probabilidad pobj)
        {
            repository.update(pobj);
        }

        public static void delete(Probabilidad pobj)
        {
            repository.delete(pobj);
        }

        public static Probabilidad getById(int pid)
        {
            return repository.getById(pid);
        }

        public static List<Probabilidad> getAll()
        {
            return repository.getAll();
        }

        /// <summary>
        /// Método que hace la llamada a todas las verificaciones que se deben hacer para una Probabilidad.
        /// </summary>
        /// <param name="pobj"></param>
        /// <returns>true: las validaciones fueron exitosas y false: las validaciones no fueron exitosas</returns>
        public static bool validarProbabilidad(Probabilidad pobj)
        {
            if (validarMaxValor(pobj) && validarMaxCategoria(pobj) && validarRango(pobj)) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método que valida que una Probabilidad no tenga una categoria ya ingresada en el sistema.
        /// </summary>
        /// <param name="pobj"></param>
        /// <returns>true: no hay categorias repetidas y false: hay una categoria igual</returns>
        private static bool validarMaxCategoria(Probabilidad pobj)
        {
            List<Probabilidad> listaProbabilidades = getAll();

            if (listaProbabilidades.Count() < MAX_CATEGORIAS) {

                foreach (Probabilidad probabilidad in listaProbabilidades) {
                    if (pobj.categoria == probabilidad.categoria) {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Método que valida que una Probabilidad no tenga un valor ya ingresado en el sistema.
        /// </summary>
        /// <param name="pobj"></param>
        /// <returns>true: no hay valores repetidos y false hay un valor igual</returns>
        private static bool validarMaxValor(Probabilidad pobj)
        {
            if (pobj.valor > 0) {
                List<Probabilidad> listaProbabilidades = getAll();

                foreach (Probabilidad probabilidad in listaProbabilidades) {
                    if (pobj.valor == probabilidad.valor) {
                        return false;
                    }
                }

                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Método que valida que el rango de una Probabilidad cumpla con los porcentajes minimos y máximos.
        /// </summary>
        /// <param name="pobj"></param>
        /// <returns>true: el rango es válido y false: el rango no es válido </returns>
        private static bool validarRango(Probabilidad pobj)
        {
            if (pobj.rangoInicio >= MIN_PORC_RANGO && pobj.rangoInicio <= MAX_PORC_RANGO && pobj.rangoFin >= MIN_PORC_RANGO && pobj.rangoFin <= MAX_PORC_RANGO) {
                if (pobj.rangoInicio != pobj.rangoFin) {
                    return true;
                }
            }

            return false;
        }
    }
}
