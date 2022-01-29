using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public interface IZasticenaZonaRepository
    {
        List<ZasticenaZonaEntity> GetZasticeneZone();

        ZasticenaZonaEntity GetZasticenaZonaById(Guid zasticenaZonaID);

        ZasticenaZonaEntity CreateZasticenaZona(ZasticenaZonaEntity zasticenaZona);

        ZasticenaZonaEntity UpdateZasticenaZona(ZasticenaZonaEntity zasticenaZona);

        void DeleteZasticenaZona(Guid zasticenaZonaID);
    }
}
