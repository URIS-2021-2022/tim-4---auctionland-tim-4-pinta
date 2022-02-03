using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Korisnik.Models;

namespace Korisnik.Data
{
    public interface IKorisnikRepository
    {
        List<KorisnikModel> GetKorisniks();

        KorisnikModel GetKorisniksById(int korisnikId);

        KorisnikModel CreateKorisniks(KorisnikModel korisnikModel);

        KorisnikModel UpdateKorisniks(KorisnikModel korisnikModel);

        void DeleteKorisnik(int korisnikId);
    }
}
