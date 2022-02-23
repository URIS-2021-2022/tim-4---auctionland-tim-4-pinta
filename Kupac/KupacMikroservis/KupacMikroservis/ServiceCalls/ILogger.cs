
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KupacMikroservis.ServiceCalls
{
    public interface ILogger
    {
        void Log(LogDto log);
    }
}
