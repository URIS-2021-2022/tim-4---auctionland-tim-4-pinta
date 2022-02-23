using KupacMikroservis.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KupacMikroservis.ServiceCalls
{
    public interface IKorisnikSistemaService
    {
    
        Task<HttpStatusCode> AuthorizeAsync(string token);
    }
}
