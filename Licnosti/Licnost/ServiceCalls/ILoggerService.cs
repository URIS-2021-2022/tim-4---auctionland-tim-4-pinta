using Licnost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.ServiceCalls
{
    public interface ILoggerService
    {
        /// <summary>
        /// Zahttev za kreiranje novog loga
        /// </summary>
        /// <param name="log"></param>
        void CreateLog(LogDto log);
    }
}
