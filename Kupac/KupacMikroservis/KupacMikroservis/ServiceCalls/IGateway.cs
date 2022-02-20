
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KupacMikroservis.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDTO> GetUrl(string servis);
    }
}
