using Licnost.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Licnost.ServiceCalls
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration configuration;
        private readonly IGatewayService gatewayService;

        public LoggerService(IConfiguration configuration, IGatewayService gatewayService)
        {
            this.configuration = configuration;
            this.gatewayService = gatewayService;
        }

        public void CreateLog(LogDto log)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{configuration["Services:LoggerService"]}");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
            }
        }
    }
}
