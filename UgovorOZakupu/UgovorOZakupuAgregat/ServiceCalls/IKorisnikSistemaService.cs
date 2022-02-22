using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UgovorOZakupuAgregat.ServiceCalls
{
    public interface IKorisnikSistemaService
    {
        /// <summary>
        /// Zahtev za korisnik servis
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<HttpStatusCode> AuthorizeAsync(string token);
    }
}
