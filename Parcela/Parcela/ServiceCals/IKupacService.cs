using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public interface IKupacService
    {
        /// <summary>
        /// Zahtev za kupca po ID-ju
        /// </summary>
        /// <param name="kupacID"></param>
        /// <returns></returns>
        Task<KupacParceleDto> GetKupacByIdAsync(Guid kupacID, string token);
    }
}
