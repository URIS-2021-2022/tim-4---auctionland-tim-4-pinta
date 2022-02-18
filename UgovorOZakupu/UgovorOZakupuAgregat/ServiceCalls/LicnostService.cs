using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UgovorOZakupuAgregat.Models;

namespace UgovorOZakupuAgregat.ServiceCalls
{
    public class LicnostService : ILicnostService
    {
        
            private readonly IConfiguration configuration;

            public LicnostService(IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            public async Task<LicnostUgovoraDto> GetLicnostByIdAsync(Guid licnostId)
            {
                using (HttpClient client = new HttpClient())
                {
                    var x = configuration["Services:LicnostService"];
                    Uri url = new Uri($"{ configuration["Services:LicnostService"] }api/licnosti/{licnostId}");

                    HttpResponseMessage response = client.GetAsync(url).Result;

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var licnost = JsonConvert.DeserializeObject<LicnostUgovoraDto>(responseContent);

                    return licnost;
                }
            }
           
    }  
}
