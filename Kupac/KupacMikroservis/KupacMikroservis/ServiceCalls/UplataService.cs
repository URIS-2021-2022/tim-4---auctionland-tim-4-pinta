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
    public class UplataService : IUplataService
    {
        private readonly IConfiguration configuration;

        public UplataService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<UplataKupcaDTO> GetUplataKupcaAsync(Guid uplataID)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:UplataService"];
                Uri url = new Uri($"{ configuration["Services:UplataService"] }api/uplata/{uplataID}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var uplata = JsonConvert.DeserializeObject<UplataKupcaDTO>(responseContent);

                return uplata;
            }
        }
    }
}