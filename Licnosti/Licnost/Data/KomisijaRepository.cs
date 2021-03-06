using Licnost.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Licnost.Data
{
    public class KomisijaRepository : IKomisijaRepository
    {
        private readonly LicnostContext context;

        public KomisijaRepository(LicnostContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Komisija> GetKomisije()
        {
            return (from p in context.Komisije select p).ToList();
        }

        public Komisija GetKomisijaById(Guid komisijaId)
        {
            return context.Komisije.FirstOrDefault(k => k.KomisijaId == komisijaId);
        }

        public Komisija CreateKomisija(Komisija komisija)
        {
            komisija.KomisijaId = Guid.NewGuid();
            context.Komisije.Add(komisija);
            return komisija;
        }

        public void UpdateKomisija(Komisija komisija)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteKomisija(Guid komisijaId)
        {
            var komisija = GetKomisijaById(komisijaId);
            context.Remove(komisija);
        }
    }
}