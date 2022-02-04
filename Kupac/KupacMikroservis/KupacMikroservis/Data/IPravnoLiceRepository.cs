using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
    public interface IPravnoLiceRepository
    {
        List<PravnoLiceEntity> GetPravnaLica();

        PravnoLiceEntity GetPravnoLiceById(Guid pravnoLiceID); 

        PravnoLiceEntity CreatePravnoLice(PravnoLiceEntity pravnoLice);

        PravnoLiceEntity UpdatePravnoLice(PravnoLiceEntity pravnoLice);

        void DeletePravnoLice(Guid pravnoLiceID);
    }
}
