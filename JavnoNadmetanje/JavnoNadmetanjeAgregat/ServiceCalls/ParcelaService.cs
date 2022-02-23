using JavnoNadmetanjeAgregat.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.ServiceCalls
{
    public class ParcelaService : IParcelaService
    {
        private readonly IConfiguration configuration;

        public ParcelaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<ParcelaJavnoNadmetanjeDto> GetParcelaByIdAsync(Guid parcelaID, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                
                Uri url = new Uri($"{ configuration["Services:ParcelaService"] }api/parcele/{parcelaID}");
                client.DefaultRequestHeaders.Add("token", token);
                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var parcela = JsonConvert.DeserializeObject<ParcelaJavnoNadmetanjeDto>(responseContent);

                return parcela;
            }
        }
    }
}
