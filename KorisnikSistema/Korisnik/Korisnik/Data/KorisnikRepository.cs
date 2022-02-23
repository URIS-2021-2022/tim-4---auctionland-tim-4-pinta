using AutoMapper;
using Korisnik.Entities;
using Korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Korisnik.Data
{
    public class KorisnikRepository : IKorisnikRepository
    {

        private readonly KorisnikContext context;
        private readonly IMapper mapper;
        private readonly static int iterations = 1000;
        public KorisnikRepository(KorisnikContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<KorisnikModel> GetKorisniks(string KorisnickoIme = null, string Ime = null, string Prezime = null)
        {
            return context.KorisnikModels.Where(e => (KorisnickoIme == null || e.KorisnickoIme == KorisnickoIme) &&
                                                        (Ime == null || e.Ime == Ime) &&
                                                        (Prezime == null || e.Prezime == Prezime)).ToList();
        }

        public KorisnikModel GetKorisnikById(int korisnikId)
        {
            return context.KorisnikModels.FirstOrDefault(e => e.KorisnikId == korisnikId);
        }

        public KorisnikModel CreateKorisnik(KorisnikModel korisnik)
        {
            var createdEntity = context.Add(korisnik);
            return mapper.Map<KorisnikModel>(createdEntity.Entity);
        }

        public void UpdateKorisnik(KorisnikModel korisnik)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteKorisnik(int korisnikId)
        {
            var registration = GetKorisnikById(korisnikId);
            context.Remove(registration);
        }

        public bool Authorize(String token)
        {
            var retToken = context.Tokens.FirstOrDefault(e => e.token == token);
            TimeSpan difference = DateTime.Now - retToken.time;
            if (difference.TotalMinutes > 60) 
            {
                return false;
            }
            return retToken!=null;
           
        }

        public bool UserWithCredentialsExists(string username, string password)
        {
           
            KorisnikModel user = context.KorisnikModels.FirstOrDefault(u => u.KorisnickoIme == username);

            if (user == null)
            {
                return false;
            }

            return true;
          
        }


        /// <summary>
        /// Proverava validnost prosleđene lozinke sa prosleđenim hash-om
        /// </summary>
        /// <param name="password">Korisnička lozinka</param>
        /// <param name="savedHash">Sačuvan hash</param>
        /// <param name="savedSalt">Sačuvan salt</param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
            {
                return true;
            }
            return false;
        }

    }
}
