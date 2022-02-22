﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Licitacija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Licitacija.ServiceCalls
{
    public class KupacService : IKupacService
    {
        private readonly IConfiguration configuration;

        public KupacService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<KupacLicitacijeDto> GetKupacByIdAsync(Guid? kupacID)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:KupacService"];
                Uri url = new Uri($"{ configuration["Services:KupacService"] }api/kupac/{kupacID}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var kupac = JsonConvert.DeserializeObject<KupacLicitacijeDto>(responseContent);

                return kupac;
            }
        }
    }
}