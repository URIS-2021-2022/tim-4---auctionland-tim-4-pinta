using AutoMapper;
using Parcela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KlasaRepository : IKlasaRepository
    {
        private readonly ParcelaContext context;

        public KlasaRepository(ParcelaContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public KlasaEntity CreateKlasa(KlasaEntity klasa)
        {
            klasa.KlasaID = Guid.NewGuid();
            context.Klase.Add(klasa);
            return klasa;
        }

        public void DeleteKlasa(Guid klasaID)
        {
            context.Klase.Remove(context.Klase.FirstOrDefault(k => k.KlasaID == klasaID));
        }

        public KlasaEntity GetKlasaById(Guid klasaID)
        {
            return context.Klase.FirstOrDefault(k => k.KlasaID == klasaID);
        }

        public List<KlasaEntity> GetKlase()
        {
            return (from k in context.Klase select k).ToList();
        }

        public void UpdateKlasa(KlasaEntity klasa)
        {
            
        }
    }
}
