using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public class KatastarskaOpstinaService : IKatastarskaOpstinaService
    {
        private readonly IConfiguration configuration;

        public KatastarskaOpstinaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<OpstinaParceleDto> GetKatastarskaOpstinaByIdAsync(Guid katastarskaOpstinaID)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ configuration["Services:KatastarskaOpstinaService"] }api/katastarskeOpstine/{katastarskaOpstinaID}");

                HttpResponseMessage response = client.GetAsync(url).Result;
                
                var responseContent = await response.Content.ReadAsStringAsync();
                var opstina = JsonConvert.DeserializeObject<OpstinaParceleDto>(responseContent);

                return opstina;
            }
        }
    }
}
