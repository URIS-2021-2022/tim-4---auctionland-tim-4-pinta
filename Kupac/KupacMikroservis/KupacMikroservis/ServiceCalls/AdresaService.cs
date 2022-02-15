using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using KupacMikroservis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KupacMikroservis.ServiceCalls
{
    public class AdresaService : IAdresaService
    {
        private readonly IConfiguration configuration;

        public AdresaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<AdresaKupcaDTO> GetAdresaKupcaAsync(Guid adresaID)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:AdresaService"];
                Uri url = new Uri($"{ configuration["Services:AdresaService"] }api/adresa/{adresaID}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var adresa = JsonConvert.DeserializeObject<AdresaKupcaDTO>(responseContent);

                return adresa;
            }
        }

        public async Task<AdresaOvlascenogLicaDTO> GetAdresaOvlLicaAsync(Guid adresaID)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:AdresaService"];
                Uri url = new Uri($"{ configuration["Services:AdresaService"] }api/adresa/{adresaID}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var adresa = JsonConvert.DeserializeObject<AdresaOvlascenogLicaDTO>(responseContent);

                return adresa;
            }
        }
    }
}