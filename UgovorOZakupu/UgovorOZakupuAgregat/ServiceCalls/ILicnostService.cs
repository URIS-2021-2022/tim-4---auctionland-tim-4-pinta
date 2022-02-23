using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.ServiceCalls
{
    public interface ILicnostService
    {
        Task<LicnostUgovoraDto> GetLicnostByIdAsync(Guid licnostId, string token);
    }
}
