using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Licitacija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Licitacija.ServiceCalls
{
    public class JavnoNadmetanjeService : IJavnoNadmetanjeService
    {
        private readonly IConfiguration configuration;

        public JavnoNadmetanjeService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<JavnoNadmetanjeLicitacijeDto> GetJavnoNadmetanjeByIdAsync(Guid? javnoNadmetanjeID,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:JavnoNadmetanjeService"];
                Uri url = new Uri($"{ configuration["Services:JavnoNadmetanjeService"] }api/javnaNadmetanja/{javnoNadmetanjeID}");

                client.DefaultRequestHeaders.Add("token", token);
                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var javnoNadmetanje = JsonConvert.DeserializeObject<JavnoNadmetanjeLicitacijeDto>(responseContent);

                return javnoNadmetanje;
            }
        }
    }
}
