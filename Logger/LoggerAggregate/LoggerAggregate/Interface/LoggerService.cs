using LoggerAggregate.Models;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LoggerAggregate.Interface
{
    public class LoggerService : ILoggerService
    {

        private static ILogger logger = LogManager.GetCurrentClassLogger();
       

        public string CheckHttpStatus(HttpContext http)
        {
            if (http.Response.StatusCode < 200)
            {
                return "Informational response";
            }
            else if (http.Response.StatusCode > 200 && http.Response.StatusCode < 300)
            {
                return "Successful response";
            }
            else if (http.Response.StatusCode > 300 && http.Response.StatusCode < 400)
            {
                return "Redirected response";
            }
            else if (http.Response.StatusCode > 400 && http.Response.StatusCode < 500)
            {
                return "Client error response";
            }
            else return "Server error response";
        }

       
    }
}
