using LoggerAggregate.Models;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerAggregate.Interface
{
    public interface ILoggerService
    {
        string CheckHttpLevel(ILogger http);
        string CheckHttpMethod(HttpContext http);
       string CheckHttpStatus(HttpContext http);
      

    }
}
