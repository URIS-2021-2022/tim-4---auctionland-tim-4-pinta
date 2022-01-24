using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KlasaRepository : IKlasaRepository
    {
        public static List<KlasaModel> Klase { get; set; } = new List<KlasaModel>();

        public KlasaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Klase.AddRange(new List<KlasaModel>
            {
                new KlasaModel
                {
                    KlasaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    KlasaOznaka = 1
                },
                new KlasaModel
                {
                    KlasaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    KlasaOznaka = 2
                }
            });
        }
        public KlasaModel CreateKlasa(KlasaModel klasa)
        {
            klasa.KlasaID = Guid.NewGuid();
            Klase.Add(klasa);
            KlasaModel k = GetKlasaById(klasa.KlasaID);
            return k;
        }

        public void DeleteKlasa(Guid klasaID)
        {
            Klase.Remove(Klase.FirstOrDefault(k => k.KlasaID == klasaID));
        }

        public KlasaModel GetKlasaById(Guid klasaID)
        {
            return Klase.FirstOrDefault(k => k.KlasaID == klasaID);
        }

        public List<KlasaModel> GetKlase()
        {
            return (from k in Klase select k).ToList();
        }

        public KlasaModel UpdateKlasa(KlasaModel klasa)
        {
            KlasaModel k = GetKlasaById(klasa.KlasaID);

            k.KlasaOznaka = klasa.KlasaOznaka;

            return k;
        }
    }
}
