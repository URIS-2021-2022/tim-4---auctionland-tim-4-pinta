using Microsoft.AspNetCore.Mvc;
using Uplata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Uplata.ServiceCalls
{
    public interface IKorisnikSistemaService
    {
        Task<HttpStatusCode> AuthorizeAsync(string token);
    }
}
