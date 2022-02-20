using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
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
