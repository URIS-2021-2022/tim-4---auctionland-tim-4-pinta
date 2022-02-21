using Microsoft.AspNetCore.Mvc;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Parcela.ServiceCals
{
    public interface IKorisnikSistemaService
    {
        Task<HttpStatusCode> AuthorizeAsync(string token);
    }
}
