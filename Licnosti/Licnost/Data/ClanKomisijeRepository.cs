using Licnost.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Licnost.Data
{
    public class ClanKomisijeRepository : IClanKomisijeRepository
    {
        private readonly LicnostContext context;

        public ClanKomisijeRepository(LicnostContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<ClanKomisije> GetClanoviKomisije()
        {
            return (from c in context.ClanoviKomisije select c).ToList();
        }

        public ClanKomisije GetClanKomisijeById(Guid clanKomisijeId)
        {
            return context.ClanoviKomisije.FirstOrDefault(c => c.ClanKomisijeId == clanKomisijeId);
        }

        public ClanKomisije CreateClanKomisije(ClanKomisije clanKomisije)
        {
            clanKomisije.ClanKomisijeId = Guid.NewGuid();
            context.ClanoviKomisije.Add(clanKomisije);
            return clanKomisije;
        }

        public void UpdateClanKomisije(ClanKomisije clanKomisije)
        {
            throw new NotImplementedException();
        }

        public void DeleteClanKomisije(Guid clanKomisijeId)
        {
            var clan = GetClanKomisijeById(clanKomisijeId);
            context.Remove(clan);
        }
    }
}