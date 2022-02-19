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
            return (from c in context.ClanoviKomisije select c).ToList();

        }

        public ClanKomisije GetClanKomisijeById(Guid clanId)
        {
            return context.ClanoviKomisije.FirstOrDefault(c => c.ClanKomisijeId == clanId);
        }

        public ClanKomisije CreateClanKomisije(ClanKomisije clan)
        {
            clan.ClanKomisijeId = Guid.NewGuid();
            context.ClanoviKomisije.Add(clan);
            ClanKomisije c = GetClanKomisijeById(clan.ClanKomisijeId);
            return c;
            //var createdEntity = context.Add(clan);
            //return mapper.Map<ClanKomisije>(createdEntity.Entity);
        }

        public void UpdateClanKomisije(ClanKomisije clan)
        {
            throw new NotImplementedException();
        }

        public void DeleteClanKomisije(Guid clanId)
        {
            var clan = GetClanKomisijeById(clanId);
            context.Remove(clanId);
        }
    }
}

