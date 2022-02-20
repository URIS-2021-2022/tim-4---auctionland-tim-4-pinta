using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.ServiceCalls
{
    public interface IAdresaService
    {
        Task<AdresaKupcaDTO> GetAdresaKupcaAsync(Guid AdresaID);
        Task<AdresaOvlascenogLicaDTO> GetAdresaOvlLicaAsync(Guid AdresaID);
    }
}