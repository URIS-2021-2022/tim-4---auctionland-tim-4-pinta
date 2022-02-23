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
    public class KatastarskaOpstinaService : IKatastarskaOpstinaService
    {
        private readonly IConfiguration configuration;

        public KatastarskaOpstinaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<KatastarskaOpstinaJavnoNadmetanjeDto> GetKatastarskaOpstinaByIdAsync(Guid katastarskaOpstinaID, string token)
        {
            using (HttpClient client = new HttpClient())
            {
             
                Uri url = new Uri($"{ configuration["Services:KatastarskaOpstinaService"] }api/katastarskeOpstine/{katastarskaOpstinaID}");
                client.DefaultRequestHeaders.Add("token", token);
                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var opstina = JsonConvert.DeserializeObject<KatastarskaOpstinaJavnoNadmetanjeDto>(responseContent);

                return opstina;
            }
        }
    }
}
