using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.ServiceCalls
{
    public interface IKupacService
    {
        Task<KupacUgovoraDto> GetKupacByIdAsync(Guid kupacId);
    }
}
