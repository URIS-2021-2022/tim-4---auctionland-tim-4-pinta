using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Korisnik.Models;

namespace Korisnik.Data
{
    public interface IKorisnikRepository
    {
        List<KorisnikModel> GetKorisniks(string KorisnickoIme = null, string Ime = null, string Prezime = null);

        KorisnikModel GetKorisnikById(int korisnikId);

        KorisnikModel CreateKorisnik(KorisnikModel korisnik);

        void UpdateKorisnik(KorisnikModel korisnik);

        void DeleteKorisnik(int korisnikId);

        bool SaveChanges();

        bool Authorize(string token);

        public bool UserWithCredentialsExists(string username, string password);
    }
}
