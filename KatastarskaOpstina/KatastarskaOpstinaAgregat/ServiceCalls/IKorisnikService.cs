using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KatastarskaOpstinaAgregat.ServiceCalls
{
    public interface IKorisnikService
    {
        Task<HttpStatusCode> AuthorizeAsync(string token);
    }
}
