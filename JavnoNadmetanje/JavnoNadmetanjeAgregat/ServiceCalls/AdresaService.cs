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
    public class AdresaService : IAdresaService
    {
        private readonly IConfiguration configuration;

        public AdresaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<AdresaJavnoNadmetanjeDto> GetAdresaByIdAsync(Guid adresaID)
        {
            using (HttpClient client = new HttpClient())
            {
                
                Uri url = new Uri($"{ configuration["Services:AdresaService"] }api/adrese/{adresaID}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var adresa = JsonConvert.DeserializeObject<AdresaJavnoNadmetanjeDto>(responseContent);

                return adresa;
            }
        }
    }
}
