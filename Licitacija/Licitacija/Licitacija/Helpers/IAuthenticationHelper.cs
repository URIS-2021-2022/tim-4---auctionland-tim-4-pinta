using Licitacija.Models;

namespace Licitacija.Helpers
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);

        public string GenerateJwt(Principal principal);
    }
}