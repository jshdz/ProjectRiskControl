using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRC.Model;

namespace PRC.AccesoDatos
{
    public class ImpactoRepository : IImpactoRepository
    {
        public Impacto insert(Impacto pobj)
        {
            using (PRCEntities db = new PRCEntities()) {
                db.Impactos.Add(pobj);
                db.SaveChanges();
                return pobj;
            }
        }

        public void update(Impacto pobj)
        {
            using (PRCEntities db = new PRCEntities()) {
                db.Impactos.Attach(pobj);
                db.Entry(pobj).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void delete(Impacto pobj)
        {
            using (PRCEntities db = new PRCEntities()) {
                db.Impactos.Attach(pobj);
                db.Impactos.Remove(pobj);
                db.SaveChanges();
            }
        }

        public Impacto getById(int pid)
        {
            using (PRCEntities db = new PRCEntities()) {
                return db.Impactos.Find(pid);
            }
        }

        public Impacto getByValor(int pvalor)
        {
            using (PRCEntities db = new PRCEntities()) {

                var data = from impacto in db.Impactos
                           where impacto.valor == pvalor
                           select impacto;

                return data.FirstOrDefault();
            }
        }

        public Impacto getByCategoria(string pcategoria)
        {
            using (PRCEntities db = new PRCEntities()) {

                var data = from impacto in db.Impactos
                           where impacto.categoria == pcategoria
                           select impacto;

                return data.FirstOrDefault();
            }
        }

        public List<Impacto> getAll()
        {
            using (PRCEntities db = new PRCEntities()) {
                return db.Impactos.ToList();
            }
        }
    }
}