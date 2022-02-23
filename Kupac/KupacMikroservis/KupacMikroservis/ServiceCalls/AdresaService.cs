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

        public async Task<AdresaKupcaDto> GetAdresaKupcaAsync(Guid AdresaID,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:AdresaService"];
                Uri url = new Uri($"{ configuration["Services:AdresaService"] }api/adrese/{AdresaID}");

                client.DefaultRequestHeaders.Add("token", token);
                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var adresa = JsonConvert.DeserializeObject<AdresaKupcaDto>(responseContent);

                return adresa;
            }
        }

        public async Task<AdresaOvlascenogLicaDto> GetAdresaOvlLicaAsync(Guid AdresaID,string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:AdresaService"];
                Uri url = new Uri($"{ configuration["Services:AdresaService"] }api/adrese/{AdresaID}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                client.DefaultRequestHeaders.Add("token", token);
                var responseContent = await response.Content.ReadAsStringAsync();
                var adresa = JsonConvert.DeserializeObject<AdresaOvlascenogLicaDto>(responseContent);

                return adresa;
            }
        }
    }
}