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

        public string CheckHttpLevel(ILogger http)
        {
           
            if(http.IsDebugEnabled)
            {
                return "Debug level";
            }
          else  if (http.IsInfoEnabled)
            {
                return "Info level";
            }
            else if(http.IsTraceEnabled)
            {
                return "Trace level";
            }
            else if(http.IsErrorEnabled)
            {
                
                return "Error level";
            }
            else
            return "Warn level";
        }

        public string CheckHttpMethod(HttpContext http)
        {
            if (http.Request.Method == "POST")
            {
                return "POST metoda";
            }
            else if (http.Request.Method == "GET")
            {
                return "GET metoda";
            }
            else if (http.Request.Method == "PUT")
            {
                return "PUT metoda";
            }
            else
            {
                return "DELETE metoda";
            }
        }

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
