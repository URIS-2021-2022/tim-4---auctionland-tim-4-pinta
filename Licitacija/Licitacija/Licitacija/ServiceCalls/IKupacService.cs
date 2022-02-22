using Licitacija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija.ServiceCalls
{
    public interface IKupacService
    {
        Task<KupacLicitacijeDto> GetKupacByIdAsync(Guid? kupacID);
    }
}
