using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using KupacMikroservis.ServiceCalls;

namespace KupacMikroservis.ServiceCalls
{
    public class Gateway : IGateway
    {
        private readonly IConfiguration configuration;

        public Gateway(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<GatewayDTO> GetUrl(string servis)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:GatewayService"];
                Uri url = new Uri($"{ configuration["Services:GatewayService"] }{servis}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var gateway = JsonConvert.DeserializeObject<GatewayDTO>(responseContent);

                return gateway;
            }
        }
    }
}
