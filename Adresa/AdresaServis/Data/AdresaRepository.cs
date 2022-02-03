using AdresaServis.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdresaServis.Data
{
    public class AdresaRepository : IAdresaRepository
    {
        private readonly AdresaContext context;
        private readonly IMapper mapper;

        public AdresaRepository(AdresaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public AdresaEntity CreateAdresa(AdresaEntity adresa)
        {
            adresa.AdresaID = Guid.NewGuid();
            context.Adrese.Add(adresa);
            AdresaEntity a = GetAdresaById(adresa.AdresaID);
            return a;
        }

        public void DeleteAdresa(Guid adresaID)
        {
            context.Adrese.Remove(context.Adrese.FirstOrDefault(a => a.AdresaID == adresaID));
        }

        public AdresaEntity GetAdresaById(Guid adresaID)
        {
            return context.Adrese.FirstOrDefault(a => a.AdresaID == adresaID);
        }

        public List<AdresaEntity> GetAdrese()
        {
            return (from a in context.Adrese select a).ToList();
        }

        public AdresaEntity UpdateAdresa(AdresaEntity adresa)
        {
            throw new NotImplementedException();
        }
    }
}
