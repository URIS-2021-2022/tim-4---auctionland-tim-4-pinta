using KupacMikroservis.Models;
using KupacMikroservis.ServiceCalls;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KupacMikroservis.ServiceCalls
{
    public class Logger : ILogger
    {
        private readonly IConfiguration configuration;
       private readonly IGateway gateway;

        public Logger(IConfiguration configuration, IGateway gateway)
        {
            this.configuration = configuration;
            this.gateway = gateway;
        }

        public void Log(LogDto log)
        {
            using (HttpClient client = new HttpClient())
            {
                

                var x = configuration["Services:LoggerService"];
                
                Uri url = new Uri($"{configuration["Services:LoggerService"]}api/logger");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
            }
        }
    }
}
