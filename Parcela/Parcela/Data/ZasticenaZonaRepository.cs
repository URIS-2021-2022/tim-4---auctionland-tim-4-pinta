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
        private readonly IMapper mapper;

        public ZasticenaZonaRepository(ParcelaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ZasticenaZonaEntity CreateZasticenaZona(ZasticenaZonaEntity zasticenaZona)
        {
            zasticenaZona.ZasticenaZonaID = Guid.NewGuid();
            context.ZasticeneZone.Add(zasticenaZona);
            ZasticenaZonaEntity z = GetZasticenaZonaById(zasticenaZona.ZasticenaZonaID);
            return z;
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

        public ZasticenaZonaEntity UpdateZasticenaZona(ZasticenaZonaEntity zasticenaZona)
        {
            throw new NotImplementedException();
        }
    }
}
