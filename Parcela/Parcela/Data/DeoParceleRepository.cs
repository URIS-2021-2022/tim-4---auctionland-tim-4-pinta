using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class DeoParceleRepository : IDeoParceleRepository
    {
        public static List<DeoParceleModel> DeloviParcela { get; set; } = new List<DeoParceleModel>();

        public DeoParceleRepository()
        {
            FillData();
        }

        private void FillData()
        {
            DeloviParcela.AddRange(new List<DeoParceleModel>
            {
                new DeoParceleModel
                {
                    DeoParceleID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    RedniBroj = 1,
                    PovrsinaDelaParcele = 1000
                },
                new DeoParceleModel
                {
                    DeoParceleID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    RedniBroj = 2,
                    PovrsinaDelaParcele = 2000
                }
            });
        }
        public DeoParceleModel CreateDeoParcele(DeoParceleModel deoParcele)
        {
            deoParcele.DeoParceleID = Guid.NewGuid();
            DeloviParcela.Add(deoParcele);
            DeoParceleModel dp = GetDeoParceleById(deoParcele.DeoParceleID);
            return dp;
        }

        public void DeleteDeoParcele(Guid deoParceleID)
        {
            DeloviParcela.Remove(DeloviParcela.FirstOrDefault(dp => dp.DeoParceleID == deoParceleID));
        }

        public List<DeoParceleModel> GetDeloviParcele()
        {
            return (from dp in DeloviParcela select dp).ToList();
        }

        public DeoParceleModel GetDeoParceleById(Guid deoParceleID)
        {
            return DeloviParcela.FirstOrDefault(dp => dp.DeoParceleID == deoParceleID);
        }

        public DeoParceleModel UpdateDeoParcele(DeoParceleModel deoParcele)
        {
            DeoParceleModel dp = GetDeoParceleById(deoParcele.DeoParceleID);

            dp.RedniBroj = deoParcele.RedniBroj;
            dp.PovrsinaDelaParcele = deoParcele.PovrsinaDelaParcele;

            return dp;
        }
    }
}
