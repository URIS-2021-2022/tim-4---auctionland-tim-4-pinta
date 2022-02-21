using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public class KorisnikSistemaService : IKorisnikSistemaService
    {
        public async Task<KorisnikSistemaDto> GetKorisnikAsync()
        {
            /*   var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:42001/api/korisnik");
               var httpClient = _httpClientFactory.CreateClient();
               var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);


               var response = await httpResponseMessage.Content.ReadAsStringAsync();
               return response;*/
            return new KorisnikSistemaDto();
        }
        
    }
}
