using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class KulturaRepository : IKulturaRepository
    {
        public static List<KulturaModel> Kulture { get; set; } = new List<KulturaModel>();

        public KulturaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Kulture.AddRange(new List<KulturaModel>
            {
                new KulturaModel
                {
                    KulturaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    KulturaNaziv = "Kukuruz"
                },
                new KulturaModel
                {
                    KulturaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    KulturaNaziv = "Soja"
                }
            });
        }

        public KulturaModel CreateKultura(KulturaModel kultura)
        {
            kultura.KulturaID = Guid.NewGuid();
            Kulture.Add(kultura);
            KulturaModel k = GetKulturaById(kultura.KulturaID);
            return k;
        }

        public void DeleteKultura(Guid kulturaID)
        {
            Kulture.Remove(Kulture.FirstOrDefault(k => k.KulturaID == kulturaID));
        }

        public KulturaModel GetKulturaById(Guid kulturaID)
        {
            return Kulture.FirstOrDefault(k => k.KulturaID == kulturaID);
        }

        public List<KulturaModel> GetKulture()
        {
            return (from k in Kulture select k).ToList();
        }

        public KulturaModel UpdateKultura(KulturaModel kultura)
        {
            KulturaModel k = GetKulturaById(kultura.KulturaID);

            k.KulturaNaziv = kultura.KulturaNaziv;

            return k;
        }
    }
}
