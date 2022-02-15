using Uplata.Models;

namespace Uplata.Helpers
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);

        public string GenerateJwt(Principal principal);
    }
}