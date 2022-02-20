using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Entities;

namespace UgovorOZakupuAgregat.Data
{
    public class TipGarancijeRepository : ITipGarancijeRepository
    {
        private readonly UgovorOZakupuContext context;
        private readonly IMapper mapper;

        public TipGarancijeRepository(UgovorOZakupuContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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

        public TipGarancije CreateTipGarancije(TipGarancije tipGarancije)
        {
            tipGarancije.TipId = Guid.NewGuid();
            context.TipoviGarancije.Add(tipGarancije);
            TipGarancije t = GetTipGarancijeById(tipGarancije.TipId);
            return t;

        }

        public void UpdateTipGarancije(TipGarancije tip)
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
