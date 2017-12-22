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
        private static string[,] infoMPI;

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

        public static string[,] calcularMPI()
        {
            infoMPI = null;

            if (ProbabilidadServices.getAll().Count() > 0 && ImpactoServices.getAll().Count() > 0) {
                Impacto impTemp = null;
                Probabilidad probTemp = null;
                int contImps1 = 1;
                int contImps2 = 1;
                int contProbs1 = ProbabilidadServices.getAll().Count();
                int contProbs2 = ProbabilidadServices.getAll().Count();
                int MAX_ROWS_INFOMPI = ProbabilidadServices.getAll().Count() + 2; //5
                int MAX_COLS_INFOMPI = ImpactoServices.getAll().Count() + 2; //6
                infoMPI = new string[MAX_ROWS_INFOMPI, MAX_COLS_INFOMPI];  //matriz de 5*6

                //for, impactos predefinidos
                for (int i = 0; i < 2; i++) {
                    for (int j = 2; j < MAX_COLS_INFOMPI; j++) {
                        if (i == 0) {
                            impTemp = ImpactoServices.getByValor(contImps1);
                            infoMPI[i, j] = Convert.ToString(impTemp.valor);
                            contImps1++;
                        } else if (i == 1) {
                            impTemp = ImpactoServices.getByValor(contImps2);
                            infoMPI[i, j] = impTemp.categoria;
                            contImps2++;
                        }
                    }
                }

                //for, probabilidades predefinidas
                for (int n = 0; n < 2; n++) { //cols
                    for (int y = 2; y < MAX_ROWS_INFOMPI; y++) { //rows
                        if (n == 0) {
                            probTemp = ProbabilidadServices.getByValor(contProbs1);
                            infoMPI[y, n] = Convert.ToString(probTemp.valor);
                            contProbs1--;
                        } else if (n == 1) {
                            probTemp = ProbabilidadServices.getByValor(contProbs2);
                            infoMPI[y, n] = probTemp.categoria;
                            contProbs2--;
                        }
                    }
                }

                infoMPI = calcularValoresMPI(infoMPI);
            }

            return infoMPI;
        }

        private static string[,] calcularValoresMPI(string[,] pinfoMPI)
        {
            List<string> valsImpactos = new List<string>();
            List<string> valsProbabilidades = new List<string>();
            int MAX_ROWS_INFOMPI = ProbabilidadServices.getAll().Count() + 2;
            int MAX_COLS_INFOMPI = ImpactoServices.getAll().Count() + 2;

            for (int i = 2; i < MAX_COLS_INFOMPI; i++) {
                valsImpactos.Add(pinfoMPI[0, i]);
            }

            for (int j = 2; j < MAX_ROWS_INFOMPI; j++) {
                valsProbabilidades.Add(pinfoMPI[j, 0]);
            }

            for (int x = 2; x < MAX_ROWS_INFOMPI; x++) {
                for (int y = 2; y < MAX_COLS_INFOMPI; y++) {
                    pinfoMPI[x, y] = Convert.ToString(Convert.ToInt32(valsProbabilidades[x-2]) * Convert.ToInt32(valsImpactos[y-2]));
                }
            }
       
            return pinfoMPI;
        }

        public static string[,] definirColoresValoresMPI(string[,] pinfoMPI)
        {
            List<string> valsImpactos = new List<string>();
            List<string> valsProbabilidades = new List<string>();
            int MAX_ROWS_INFOMPI = ProbabilidadServices.getAll().Count() + 2;
            int MAX_COLS_INFOMPI = ImpactoServices.getAll().Count() + 2;
            string[,] colorsValsMPI = new string[ProbabilidadServices.getAll().Count(), ImpactoServices.getAll().Count()];
            string colorRiesgoTemp = "";

            for (int i = 2; i < MAX_COLS_INFOMPI; i++) {
                valsImpactos.Add(pinfoMPI[0, i]);
            }

            for (int j = 2; j < MAX_ROWS_INFOMPI; j++) {
                valsProbabilidades.Add(pinfoMPI[j, 0]);
            }

            for (int x = 2; x < MAX_ROWS_INFOMPI; x++) {
                for (int y = 2; y < MAX_COLS_INFOMPI; y++) {
                    colorRiesgoTemp = calcularColorRiesgo(Convert.ToInt32(valsProbabilidades[x - 2]), Convert.ToInt32(valsImpactos[y - 2]));
                    pinfoMPI[x, y] = colorRiesgoTemp;
                }
            }

            return pinfoMPI;
        }

        public static string calcularColorRiesgo(int pvalorProb, int pvalorImp)
        {
            string colorRiesgo = "";
            string verde = "#4CAF50";
            string amarillo = "#FFEB3B";
            string rojo = "#F44336";

            if (pvalorProb == 1 && pvalorImp == 1) {
                colorRiesgo = verde;
            } else if (pvalorProb == 1 && pvalorImp == 2 || pvalorProb == 2 && pvalorImp == 1) {
                colorRiesgo = verde;
            } else if (pvalorProb == 1 && pvalorImp == 3 || pvalorProb == 3 && pvalorImp == 1) {
                colorRiesgo = amarillo;
            } else if (pvalorProb == 1 && pvalorImp == 4 || pvalorProb == 4 && pvalorImp == 1) {
                colorRiesgo = amarillo;
            } else if (pvalorProb == 2 && pvalorImp == 2) {
                colorRiesgo = amarillo;
            } else if (pvalorProb == 2 && pvalorImp == 3 || pvalorProb == 3 && pvalorImp == 2) {
                colorRiesgo = rojo;
            } else if (pvalorProb == 2 && pvalorImp == 4 || pvalorProb == 4 && pvalorImp == 2) {
                colorRiesgo = rojo;
            } else if (pvalorProb == 3 && pvalorImp == 3) {
                colorRiesgo = rojo;
            } else if (pvalorProb == 3 && pvalorImp == 4 || pvalorProb == 4 && pvalorImp == 3) {
                colorRiesgo = rojo;
            }

            return colorRiesgo;
        }

        public static string calcularNivelRiesgo(string pcolorRiesgo)
        {
            string nivelRiesgo = "";
            string lvl1 = ProbabilidadServices.getByValor(1).categoria;
            string lvl2 = ProbabilidadServices.getByValor(2).categoria;
            string lvl3 = ProbabilidadServices.getByValor(3).categoria;
            string verde = "#4CAF50";
            string amarillo = "#FFEB3B";
            string rojo = "#F44336";

            if (pcolorRiesgo == verde) {
                nivelRiesgo = lvl1;
            } else if (pcolorRiesgo == amarillo) {
                nivelRiesgo = lvl2;
            } else if (pcolorRiesgo == rojo) {
                nivelRiesgo = lvl3;
            }

            return nivelRiesgo;
        }

        public static bool validarCamposRiesgo(Riesgo pobj)
        {
            if (!String.IsNullOrEmpty(pobj.descripcion) && !String.IsNullOrEmpty(Convert.ToString(pobj.idProbabilidad)) && !String.IsNullOrEmpty(Convert.ToString(pobj.idImpacto))) {
                return true;
            }

            return false;
        }
    }
}