using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
    public interface IKupacRepository
    {
        List<KupacModel> GetKupci();

        KupacModel GetKupacById(Guid kupacID);

        KupacModel CreateKupac(KupacModel kupac);

        KupacModel UpdateKupac(KupacModel kupac);

        void DeleteKupac(Guid kupacID);
    }
}
