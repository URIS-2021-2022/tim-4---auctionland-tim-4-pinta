using Microsoft.AspNetCore.Mvc;
using Licitacija.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Licitacija.ServiceCalls
{
    public interface IKorisnikSistemaService
    {
        Task<HttpStatusCode> AuthorizeAsync(string token);
    }
}
