using AutoMapper;
using Licnost.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Data
{
    public class ClanKomisijeRepository : IClanKomisijeRepository
    {
        private readonly LicnostContext context;
        private readonly IMapper mapper;

        public ClanKomisijeRepository(LicnostContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<ClanKomisije> GetClanoviKomisije()
        {
            return (from p in context.ClanoviKomisije select p).ToList();

        }

        public ClanKomisije GetClanKomisijetById(Guid clanId)
        {
            return context.ClanoviKomisije.FirstOrDefault(c => c.ClanKomisijeId == clanId);
        }

        public ClanKomisije CreateClanKomisije(ClanKomisije clan)
        {
            var createdEntity = context.Add(clan);
            return mapper.Map<ClanKomisije>(createdEntity.Entity);
        }

        public void UpdateClanKomisije(ClanKomisije clan)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteClanKomisije(Guid clanId)
        {
            var clan = GetClanKomisijetById(clanId);
            context.Remove(clanId);
        }
    }
}

