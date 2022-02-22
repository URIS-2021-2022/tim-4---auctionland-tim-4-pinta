using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ZasticenaZonaRepository : IZasticenaZonaRepository
    {
        private readonly ParcelaContext context;

        public ZasticenaZonaRepository(ParcelaContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ZasticenaZonaEntity CreateZasticenaZona(ZasticenaZonaEntity zasticenaZona)
        {
            zasticenaZona.ZasticenaZonaID = Guid.NewGuid();
            context.ZasticeneZone.Add(zasticenaZona);
            return zasticenaZona;
        }

        public void DeleteZasticenaZona(Guid zasticenaZonaID)
        {
            context.ZasticeneZone.Remove(context.ZasticeneZone.FirstOrDefault(z => z.ZasticenaZonaID == zasticenaZonaID));
        }

        public ZasticenaZonaEntity GetZasticenaZonaById(Guid zasticenaZonaID)
        {
            return context.ZasticeneZone.FirstOrDefault(z => z.ZasticenaZonaID == zasticenaZonaID);
        }

        public List<ZasticenaZonaEntity> GetZasticeneZone()
        {
            return (from z in context.ZasticeneZone select z).ToList();
        }

        public void UpdateZasticenaZona(ZasticenaZonaEntity zasticenaZona)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
    }
}
