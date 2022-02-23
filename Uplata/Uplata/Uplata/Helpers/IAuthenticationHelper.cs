using Uplata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Helpers
{
    public interface IAuthenticationHelper
    {
        
           public bool AuthenticatePrincipal(Principal principal);
           public string GenerateJwt(Principal principal);
        
    }
}
