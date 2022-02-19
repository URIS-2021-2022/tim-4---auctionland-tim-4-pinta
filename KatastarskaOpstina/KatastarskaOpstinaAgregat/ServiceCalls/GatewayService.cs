using KatastarskaOpstinaAgregat.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.ServiceCalls
{
    public class GatewayService : IGatewayService
    {
        private readonly IConfiguration configuration;

        public GatewayService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<GatewayDto> GetUrl(string servis)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:GatewayService"];
                Uri url = new Uri($"{ configuration["Services:GatewayService"] }{servis}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var gateway = JsonConvert.DeserializeObject<GatewayDto>(responseContent);

                return gateway;
            }
        }
    }
}
