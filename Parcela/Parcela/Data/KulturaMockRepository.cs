using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KulturaMockRepository : IKulturaRepository
    {
        public static List<KulturaEntity> Kulture { get; set; } = new List<KulturaEntity>();

        public KulturaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Kulture.AddRange(new List<KulturaEntity>
            {
                new KulturaEntity
                {
                    KulturaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    KulturaNaziv = "Kukuruz"
                },
                new KulturaEntity
                {
                    KulturaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    KulturaNaziv = "Soja"
                }
            });
        }

        public KulturaEntity CreateKultura(KulturaEntity kultura)
        {
            kultura.KulturaID = Guid.NewGuid();
            Kulture.Add(kultura);
            KulturaEntity k = GetKulturaById(kultura.KulturaID);
            return k;
        }

        public void DeleteKultura(Guid kulturaID)
        {
            Kulture.Remove(Kulture.FirstOrDefault(k => k.KulturaID == kulturaID));
        }

        public KulturaEntity GetKulturaById(Guid kulturaID)
        {
            return Kulture.FirstOrDefault(k => k.KulturaID == kulturaID);
        }

        public List<KulturaEntity> GetKulture()
        {
            return (from k in Kulture select k).ToList();
        }

        public KulturaEntity UpdateKultura(KulturaEntity kultura)
        {
            KulturaEntity k = GetKulturaById(kultura.KulturaID);

            k.KulturaNaziv = kultura.KulturaNaziv;

            return k;
        }
    }
}
