using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.ServiceCalls
{
    public interface IGatewayService
    {
        Task<GatewayDto> GetUrl(string servis);
    }
}
