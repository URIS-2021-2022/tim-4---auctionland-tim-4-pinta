using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licitacija.Models;

namespace Licitacija.ServiceCalls
{
    public interface ILoggerService
    {
        void CreateLog(LogDto log);
    }
}
