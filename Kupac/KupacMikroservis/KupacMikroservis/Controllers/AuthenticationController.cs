using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KupacMikroservis.Helpers;
using KupacMikroservis.Models;

namespace KupacMikroservis.Controllers
{
    [ApiController]
    [Route("api/kupac")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// Sluzi za autentifikaciju korisnika
        /// </summary>
        /// <param name="principal">Model sa podacima na osnovu kojih se vrši autentifikacija</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate(Principal principal)
        {
           
            if (authenticationHelper.AuthenticatePrincipal(principal))
            {
                var tokenString = authenticationHelper.GenerateJwt(principal);
                return Ok(new { token = tokenString });
            }

          
            return Unauthorized();
        }
    }
}
