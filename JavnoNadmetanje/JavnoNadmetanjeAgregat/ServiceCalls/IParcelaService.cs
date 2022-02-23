using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.ServiceCalls
{
     public interface IParcelaService
    {
       Task <ParcelaJavnoNadmetanjeDto> GetParcelaByIdAsync(Guid parcelaID, string token);
    }
}
