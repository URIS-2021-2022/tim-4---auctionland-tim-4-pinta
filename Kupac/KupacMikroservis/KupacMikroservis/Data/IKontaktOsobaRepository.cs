using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
   public interface IKontaktOsobaRepository
    {
        List<KontaktOsobaEntity> GetKontaktOsobe();

        KontaktOsobaEntity GetKontaktOsoba(Guid kontaktOsobaID);

        KontaktOsobaEntity CreateKontaktOsoba(KontaktOsobaEntity kontaktOsoba);

        KontaktOsobaEntity UpdateKontaktOsoba(KontaktOsobaEntity kontaktOsoba);

        void DeleteKontaktOsoba(Guid kontaktOsobaID);
    }
}
