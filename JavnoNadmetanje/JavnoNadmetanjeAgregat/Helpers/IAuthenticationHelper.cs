using JavnoNadmetanjeAgregat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanjeAgregat.Helpers
{
    public interface IAuthenticationHelper
    {
        
           public bool AuthenticatePrincipal(Principal principal);
           public string GenerateJwt(Principal principal);
        
    }
}
