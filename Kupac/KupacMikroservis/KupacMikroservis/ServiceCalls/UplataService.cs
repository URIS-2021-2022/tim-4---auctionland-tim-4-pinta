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

        public async Task<UplataKupcaDto> GetUplataKupcaAsync(Guid UplataID, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:UplataService"];
                Uri url = new Uri($"{ configuration["Services:UplataService"] }api/uplate/{UplataID}");

                client.DefaultRequestHeaders.Add("token", token);
                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var uplata = JsonConvert.DeserializeObject<UplataKupcaDto>(responseContent);

                return uplata;
            }
        }
    }
}