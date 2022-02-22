using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdresaServis.ServiceCalls
{
    public class KorisnikSistemaService : IKorisnikSistemaService
    {
        private readonly IConfiguration configuration;

        public KorisnikSistemaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<HttpStatusCode> AuthorizeAsync(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{ configuration["Services:KorisnikSistemaService"] }api/korisnik/authorize/{token}");

                HttpResponseMessage response = client.GetAsync(url).Result;
                var responseContent = response.StatusCode;

                return await Task.FromResult(responseContent);
            }
        }
    }
}
