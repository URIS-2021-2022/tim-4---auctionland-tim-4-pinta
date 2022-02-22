using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ParcelaRepository : IParcelaRepository
    {
        private readonly ParcelaContext context;

        public ParcelaRepository(ParcelaContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ParcelaEntity CreateParcela(ParcelaEntity parcela)
        {
            parcela.ParcelaID = Guid.NewGuid();
            context.Parcele.Add(parcela);
            return parcela;
        }

        public void DeleteParcela(Guid parcelaID)
        {
            context.Parcele.Remove(context.Parcele.FirstOrDefault(p => p.ParcelaID == parcelaID));
        }

        public ParcelaEntity GetParcelaById(Guid parcelaID)
        {
            return context.Parcele.FirstOrDefault(p => p.ParcelaID == parcelaID);
        }

        public List<ParcelaEntity> GetParcele()
        {
            return (from p in context.Parcele select p).ToList();
        }

        public void UpdateParcela(ParcelaEntity parcela)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
    }
}
