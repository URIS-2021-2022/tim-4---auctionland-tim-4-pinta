using Microsoft.AspNetCore.Mvc;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public class KorisnikSistemaService : IKorisnikSistemaService
    {
        public async Task<HttpStatusCode> AuthorizeAsync(string token)
        {

            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = client.GetAsync("http://localhost:44500/api/korisnik/authorize/" + token).Result;

                var responseContent = response.StatusCode;

                return responseContent;
            }
        }
        
    }
}
