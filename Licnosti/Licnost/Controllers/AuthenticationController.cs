//using Licnost.Helpers;
//using Licnost.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Licnost.Controllers
//{
//    [ApiController]
//    [Route("api/licnost")]
//    [Produces("application/json", "application/xml")]
//    public class AuthenticationController : ControllerBase
//    {
//        private readonly IAuthenticationHelper authenticationHelper;

//        public AuthenticationController(IAuthenticationHelper authenticationHelper)
//        {
//            this.authenticationHelper = authenticationHelper;
//        }

//        /// <summary>
//        /// Sluzi za autentifikaciju korisnika
//        /// </summary>
//        /// <param name="principal">Model sa podacima na osnovu kojih se vrsi autentifikacija</param>
//        /// <returns></returns>
//        [HttpPost("authenticate")]
//        public IActionResult Authenticate(Principal principal)
//        {
//            if (authenticationHelper.AuthenticatePrincipal(principal))
//            {
//                var tokenString = authenticationHelper.GenerateJwt(principal);
//                return Ok(new { token = tokenString });
//            }
//            return Unauthorized();
//        }
//    }
//}
