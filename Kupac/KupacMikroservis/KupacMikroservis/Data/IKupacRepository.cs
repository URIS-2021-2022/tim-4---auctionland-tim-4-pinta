using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
    public interface IKupacRepository
    {
        List<KupacEntity> GetKupci();

        KupacEntity GetKupacById(Guid kupacID);

        KupacEntity CreateKupac(KupacEntity kupac);

        KupacEntity UpdateKupac(KupacEntity kupac);

        void DeleteKupac(Guid kupacID);
    }
}
