using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public interface IGatewayService
    {
        /// <summary>
        /// Zahtev za gateway servis
        /// </summary>
        /// <param name="servis"></param>
        /// <returns></returns>
        Task<GatewayDto> GetUrl(string servis);
    }
}
