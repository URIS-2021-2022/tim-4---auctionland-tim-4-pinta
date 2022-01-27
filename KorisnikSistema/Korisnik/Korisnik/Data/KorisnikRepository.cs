using Korisnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korisnik.Data
{
    public class KorisnikRepository : IKorisnikRepository
    {

        public static List<KorisnikModel> Korisniks { get; set; } = new List<KorisnikModel>();



        public KorisnikModel CreateKorisniks(KorisnikModel korisnikModel)
        {
            //korisnikModel.KorisnikId = Guid.NewGuid();
            Korisniks.Add(korisnikModel);
            KorisnikModel korisnik = GetKorisniksById(korisnikModel.KorisnikId);

            return new KorisnikModel
            {
                KorisnikId = korisnik.KorisnikId,
                Ime = korisnik.Ime,
                Prezime=korisnik.Prezime,
                KorisnickoIme=korisnik.KorisnickoIme,
                Lozinka = korisnik.Lozinka
            };
        }

        public void DeleteKorisnik(int korisnikId)
        {
            Korisniks.Remove(Korisniks.FirstOrDefault(e => e.KorisnikId == korisnikId));
        }

        public List<KorisnikModel> GetKorisniks()
        {
            return Korisniks;
        }

        public KorisnikModel GetKorisniksById(int korisnikId)
        {
            return Korisniks.FirstOrDefault(e => e.KorisnikId == korisnikId);
        }

        public KorisnikModel UpdateKorisniks(KorisnikModel korisnikModel)
        {
            KorisnikModel korisnik = GetKorisniksById(korisnikModel.KorisnikId);
            korisnik.KorisnikId = korisnikModel.KorisnikId;
            korisnik.Ime = korisnikModel.Ime;
            korisnik.Prezime = korisnikModel.Prezime;
            korisnik.KorisnickoIme = korisnikModel.KorisnickoIme;
            korisnik.Lozinka = korisnikModel.Lozinka;

            return korisnik;
        }
    }
}
