using Korisnik.Data;
using Korisnik.Entities;
using Korisnik.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korisnik.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IConfiguration configuration;
        private readonly IKorisnikRepository userRepository;
        private readonly KorisnikContext context;

        public AuthenticationHelper(IConfiguration configuration, IKorisnikRepository userRepository, KorisnikContext context)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.context = context;
        }

        public bool AuthenticatePrincipal(Principal principal)
        {
            if (userRepository.UserWithCredentialsExists(principal.Username, principal.Password))
            {
               
                return true;
            }

            return false;
        }

        public string GenerateJwt(Principal principal)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Issuer"],
                                             null,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            TokenTime tokenTime = new TokenTime();
            KorisnikModel korisnik=context.KorisnikModels.FirstOrDefault(e => e.KorisnickoIme == principal.Username);
            Random rand = new Random();
   
            tokenTime.tokenId = rand.Next();
            tokenTime.korisnikId = korisnik.KorisnikId;
            tokenTime.time = DateTime.Now;
            tokenTime.token = new JwtSecurityTokenHandler().WriteToken(token).ToString();

            var createdEntity = context.Add(tokenTime);

            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            string finalToken = tokenResult.ToString() + "#" + korisnik.TipKorisnika; 

            return finalToken;
        }


        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
