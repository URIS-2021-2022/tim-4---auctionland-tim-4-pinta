using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
   public interface IOvlascenoLiceRepository
    {
        List<OvlascenoLiceModel> GetOvlascenaLica();

        OvlascenoLiceModel GetOvlascenoLiceById(Guid ovlascenoLiceID);

        OvlascenoLiceModel CreateOvlascenoLice(OvlascenoLiceModel ovlascenoLice);

        OvlascenoLiceModel UpdateOvlascenoLice(OvlascenoLiceModel ovlascenoLice);

        void DeleteOvlascenoLice(Guid ovlascenoLiceID);
    }
}
