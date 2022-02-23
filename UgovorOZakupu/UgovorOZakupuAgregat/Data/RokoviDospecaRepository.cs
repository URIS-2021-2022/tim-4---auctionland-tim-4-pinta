using System;
using System.Collections.Generic;
using System.Linq;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public class RokoviDospecaRepository : IRokoviDospecaRepository
    {
        private readonly UgovorOZakupuContext context;

        public RokoviDospecaRepository(UgovorOZakupuContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<RokoviDospeca> GetRokovi()
        {
            return (from r in context.RokoviDospeca select r).ToList();
        }

        public RokoviDospeca GetRokById(Guid rokId)
        {
            return context.RokoviDospeca.FirstOrDefault(r => r.RokId == rokId);
        }

        public RokoviDospeca CreateRok(RokoviDospeca rokModel)
        {
            rokModel.RokId = Guid.NewGuid();
            context.RokoviDospeca.Add(rokModel);
            return rokModel;
        }

        public void UpdateRok(RokoviDospeca rokModel)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteRok(Guid rokId)
        {
            var rok = GetRokById(rokId);
            context.Remove(rok);
        }
    }
}