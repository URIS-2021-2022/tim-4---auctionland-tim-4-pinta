using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    interface IZasticenaZonaRepository
    {
        List<ZasticenaZonaModel> GetZasticeneZone();

        ZasticenaZonaModel GetZasticenaZonaById(Guid zasticenaZonaID);

        ZasticenaZonaModel CreateZasticenaZona(ZasticenaZonaModel zasticenaZona);

        ZasticenaZonaModel UpdateZasticenaZona(ZasticenaZonaModel zasticenaZona);

        void DeleteZasticenaZona(Guid zasticenaZonaID);
    }
}
