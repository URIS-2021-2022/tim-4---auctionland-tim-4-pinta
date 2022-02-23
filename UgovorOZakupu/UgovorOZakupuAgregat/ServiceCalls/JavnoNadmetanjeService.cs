using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.ServiceCalls
{
    public class JavnoNadmetanjeService : IJavnoNadmetanjeService
    {
        private readonly IConfiguration configuration;

        public JavnoNadmetanjeService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<JavnoNadmetanjeUgovoraDto> GetJavnoNadmetanjeByIdAsync(Guid javnoNadmetanjeId, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ configuration["Services:JavnoNadmetanjeService"] }api/javnaNadmetanja/{javnoNadmetanjeId}");

                client.DefaultRequestHeaders.Add("token", token);
                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var javnoNadmetanje = JsonConvert.DeserializeObject<JavnoNadmetanjeUgovoraDto>(responseContent);

                return javnoNadmetanje;
            }
        }
    }
}