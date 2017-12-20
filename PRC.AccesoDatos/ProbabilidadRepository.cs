using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRC.Model;

namespace PRC.AccesoDatos
{
    public class ProbabilidadRepository : IProbabilidadRepository
    {
        public Probabilidad insert(Probabilidad pobj)
        {
            using (PRCEntities db = new PRCEntities()) {
                db.Probabilidades.Add(pobj);
                db.SaveChanges();
                return pobj;
            }
        }

        public void update(Probabilidad pobj)
        {
            using (PRCEntities db = new PRCEntities()) {
                db.Probabilidades.Attach(pobj);
                db.Entry(pobj).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void delete(Probabilidad pobj)
        {
            using (PRCEntities db = new PRCEntities()) {
                db.Probabilidades.Attach(pobj);
                db.Probabilidades.Remove(pobj);
                db.SaveChanges();
            }
        }

        public Probabilidad getById(int pid)
        {
            using (PRCEntities db = new PRCEntities()) {
                return db.Probabilidades.Find(pid);
            }
        }

        public Probabilidad getByValor(int pvalor)
        {
            using (PRCEntities db = new PRCEntities()) {

                var data = from probabilidad in db.Probabilidades
                           where probabilidad.valor == pvalor
                           select probabilidad;

                return data.FirstOrDefault();
            }
        }

        public List<Probabilidad> getAll()
        {
            using (PRCEntities db = new PRCEntities()) {
                return db.Probabilidades.ToList();
            }
        }
    }
}
