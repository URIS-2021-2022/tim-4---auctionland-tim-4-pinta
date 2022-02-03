using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerAggregate.Models
{
    public class LogModel
    {
       

        //odredjuje koja je HTTP metoda u pitanju
        public HttpContext HttpMethodIdentifier;

        //status Http zahtjeva tj.nivo
        public ILogger HttpLevel;

        //odredjuje tip greske
        public HttpContext Error;
    }
}
