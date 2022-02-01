using KupacMikroservis.Models;
using System;
using System.Collections.Generic;

namespace KupacMikroservis.Data
{
    public interface IPravnoLiceRepository
    {
        List<PravnoLiceModel> GetPravnaLica();

        PravnoLiceModel GetPravnoLiceById(Guid pravnoLiceID); 

        PravnoLiceModel CreatePravnoLice(PravnoLiceModel pravnoLice);

        PravnoLiceModel UpdatePravnoLice(PravnoLiceModel pravnoLice);

        void DeletePravnoLice(Guid pravnoLiceID);
    }
}
