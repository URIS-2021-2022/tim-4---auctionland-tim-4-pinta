using Uplata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavnoNadmetanjeAgregat.Models;

namespace Uplata.ServiceCalls
{
    public interface IJavnoNadmetanjeService
    {
        Task<JavnoNadmetanjeUplateDto> GetJavnoNadmetanjeByIdAsync(Guid? javnoNadmetanjeID, string token);
    }
}
