using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
   public interface IOvlascenoLiceRepository
    {
        List<OvlascenoLiceEntity> GetOvlascenaLica();

        OvlascenoLiceEntity GetOvlascenoLiceById(Guid ovlascenoLiceID);

        OvlascenoLiceEntity CreateOvlascenoLice(OvlascenoLiceEntity ovlascenoLice);

        void UpdateOvlascenoLice(OvlascenoLiceEntity ovlascenoLice);

        void DeleteOvlascenoLice(Guid ovlascenoLiceID);

        bool SaveChanges();
    }
}
