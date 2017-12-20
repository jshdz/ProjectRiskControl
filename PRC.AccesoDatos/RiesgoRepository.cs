using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRC.Model;

namespace PRC.AccesoDatos
{
    public class RiesgoRepository : IRiesgoRepository
    {
        public Riesgo insert(Riesgo pobj)
        {
            using (PRCEntities db = new PRCEntities()) {
                db.Riesgos.Add(pobj);
                db.SaveChanges();
                return pobj;
            }
        }

        public void update(Riesgo pobj)
        {
            using (PRCEntities db = new PRCEntities()) {
                db.Riesgos.Attach(pobj);
                db.Entry(pobj).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void delete(Riesgo pobj)
        {
            using (PRCEntities db = new PRCEntities()) {
                db.Riesgos.Attach(pobj);
                db.Riesgos.Remove(pobj);
                db.SaveChanges();
            }
        }

        public Riesgo getById(int pid)
        {
            using (PRCEntities db = new PRCEntities()) {
                return db.Riesgos.Find(pid);
            }
        }

        public List<Riesgo> getAll()
        {
            using (PRCEntities db = new PRCEntities()) {
                return db.Riesgos.ToList();
            }
        }
    }
}