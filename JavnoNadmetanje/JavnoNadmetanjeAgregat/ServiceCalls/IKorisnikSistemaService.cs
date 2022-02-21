﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.ServiceCalls
{
    public interface IKorisnikSistemaService
    {
        Task<HttpStatusCode> AuthorizeAsync(string token);
    }
}
