using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.ServiceCalls
{
    public interface IAdresaService
    {
        Task<AdresaKupcaDto> GetAdresaKupcaAsync(Guid AdresaID,string token);
        Task<AdresaOvlascenogLicaDto> GetAdresaOvlLicaAsync(Guid AdresaID,string token);
    }
}