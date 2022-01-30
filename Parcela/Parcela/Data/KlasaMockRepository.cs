using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KlasaMockRepository : IKlasaRepository
    {
        public static List<KlasaEntity> Klase { get; set; } = new List<KlasaEntity>();

        public KlasaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Klase.AddRange(new List<KlasaEntity>
            {
                new KlasaEntity
                {
                    KlasaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    KlasaOznaka = 1
                },
                new KlasaEntity
                {
                    KlasaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    KlasaOznaka = 2
                }
            });
        }
        public KlasaEntity CreateKlasa(KlasaEntity klasa)
        {
            klasa.KlasaID = Guid.NewGuid();
            Klase.Add(klasa);
            KlasaEntity k = GetKlasaById(klasa.KlasaID);
            return k;
        }

        public void DeleteKlasa(Guid klasaID)
        {
            Klase.Remove(Klase.FirstOrDefault(k => k.KlasaID == klasaID));
        }

        public KlasaEntity GetKlasaById(Guid klasaID)
        {
            return Klase.FirstOrDefault(k => k.KlasaID == klasaID);
        }

        public List<KlasaEntity> GetKlase()
        {
            return (from k in Klase select k).ToList();
        }

        public KlasaEntity UpdateKlasa(KlasaEntity klasa)
        {
            KlasaEntity k = GetKlasaById(klasa.KlasaID);

            k.KlasaOznaka = klasa.KlasaOznaka;

            return k;
        }
    }
}
