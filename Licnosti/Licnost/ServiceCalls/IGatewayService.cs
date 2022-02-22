using Licnost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.ServiceCalls
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
