using Licitacija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Licitacija.ServiceCalls
{
    public interface IGatewayService
    {
        Task<GatewayDto> GetUrl(string servis);
    }
}
