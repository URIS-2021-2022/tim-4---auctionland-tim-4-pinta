using Licitacija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija.ServiceCalls
{
    public interface IKupacService
    {
        /// <summary>
        /// Zahtev za kupca po ID-ju
        /// </summary>
        /// <param name="kupacID"></param>
        /// <returns></returns>
        Task<KupacLicitacijeDto> GetKupacByIdAsync(Guid? kupacID, string token);
    }
}
