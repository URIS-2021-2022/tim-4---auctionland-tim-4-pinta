using KatastarskaOpstinaAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.ServiceCalls
{
    public interface ILoggerService
    {
        void CreateLog(LogDto log);
    }
}
