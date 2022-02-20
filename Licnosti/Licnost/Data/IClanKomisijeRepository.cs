using Licnost.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Data
{
    public interface IClanKomisijeRepository
    {
        List<ClanKomisije> GetClanoviKomisije();

        ClanKomisije GetClanKomisijeById(Guid clanKomisijeId);

        ClanKomisije CreateClanKomisije(ClanKomisije clanKomisije);

        void UpdateClanKomisije(ClanKomisije clanKomisije);

        void DeleteClanKomisije(Guid clanKomisijeId);

        bool SaveChanges();
    }
}
