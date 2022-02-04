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

        OvlascenoLiceEntity UpdateOvlascenoLice(OvlascenoLiceEntity ovlascenoLice);

        void DeleteOvlascenoLice(Guid ovlascenoLiceID);
    }
}
