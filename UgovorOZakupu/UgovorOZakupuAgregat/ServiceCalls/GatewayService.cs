using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.ServiceCalls
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
                Uri url = new Uri($"{ configuration["Services:GatewayService"] }{servis}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var gateway = JsonConvert.DeserializeObject<GatewayDto>(responseContent);

                return gateway;
            }
        }
    }
}