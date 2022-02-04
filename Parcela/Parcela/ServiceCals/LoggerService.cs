using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration configuration;

        public LoggerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //public void CreateLog(LogDto log)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var x = configuration["Services:LoggerService"];
        //        Uri url = new Uri($"{ configuration["Services:LoggerService"] }api/logovi");

        //        HttpContent content = new StringContent(JsonConvert.SerializeObject(log));
        //        content.Headers.ContentType.MediaType = "application/json";

        //        HttpResponseMessage response = client.PostAsync(url, content).Result;
        //    }
        //}
    }
}
