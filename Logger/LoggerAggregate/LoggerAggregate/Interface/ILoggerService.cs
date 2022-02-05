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
       
       string CheckHttpStatus(HttpContext http);    

    }
}
