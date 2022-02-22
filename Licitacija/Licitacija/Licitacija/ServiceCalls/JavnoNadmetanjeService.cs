﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Licitacija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Licitacija.Models;

namespace Licitacija.ServiceCalls
{
    public class JavnoNadmetanjeService : IJavnoNadmetanjeService
    {
        private readonly IConfiguration configuration;

        public JavnoNadmetanjeService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<JavnoNadmetanjeLicitacijeDto> GetJavnoNadmetanjeByIdAsync(Guid? javnoNadmetanjeID)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:JavnoNadmetanjeService"];
                Uri url = new Uri($"{ configuration["Services:JavnoNadmetanjeService"] }api/javnaNadmetanja/{javnoNadmetanjeID}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var javnoNadmetanje = JsonConvert.DeserializeObject<JavnoNadmetanjeLicitacijeDto>(responseContent);

                return javnoNadmetanje;
            }
        }
    }
}