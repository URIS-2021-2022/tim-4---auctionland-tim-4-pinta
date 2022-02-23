using System;
using System.Collections.Generic;
using System.Linq;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public class TipGarancijeRepository : ITipGarancijeRepository
    {
        private readonly UgovorOZakupuContext context;

        public TipGarancijeRepository(UgovorOZakupuContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<TipGarancije> GetTipovi()
        {
            return (from t in context.TipoviGarancije select t).ToList();
        }

        public TipGarancije GetTipGarancijeById(Guid tipId)
        {
            return context.TipoviGarancije.FirstOrDefault(t => t.TipId == tipId);
        }

        public TipGarancije CreateTipGarancije(TipGarancije tipModel)
        {
            tipModel.TipId = Guid.NewGuid();
            context.TipoviGarancije.Add(tipModel);
            return tipModel;
        }

        public void UpdateTipGarancije(TipGarancije tipModel)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteTipGarancije(Guid tipId)
        {
            var tipGarancije = GetTipGarancijeById(tipId);
            context.Remove(tipGarancije);
        }
    }
}