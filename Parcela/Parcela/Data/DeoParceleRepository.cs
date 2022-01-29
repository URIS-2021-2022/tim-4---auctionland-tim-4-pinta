using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class DeoParceleRepository : IDeoParceleRepository
    {
        public static List<DeoParceleEntity> DeloviParcela { get; set; } = new List<DeoParceleEntity>();

        public DeoParceleRepository()
        {
            FillData();
        }

        private void FillData()
        {
            DeloviParcela.AddRange(new List<DeoParceleEntity>
            {
                new DeoParceleEntity
                {
                    DeoParceleID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    RedniBroj = 1,
                    PovrsinaDelaParcele = 1000
                },
                new DeoParceleEntity
                {
                    DeoParceleID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    RedniBroj = 2,
                    PovrsinaDelaParcele = 2000
                }
            });
        }
        public DeoParceleEntity CreateDeoParcele(DeoParceleEntity deoParcele)
        {
            deoParcele.DeoParceleID = Guid.NewGuid();
            DeloviParcela.Add(deoParcele);
            DeoParceleEntity dp = GetDeoParceleById(deoParcele.DeoParceleID);
            return dp;
        }

        public void DeleteDeoParcele(Guid deoParceleID)
        {
            DeloviParcela.Remove(DeloviParcela.FirstOrDefault(dp => dp.DeoParceleID == deoParceleID));
        }

        public List<DeoParceleEntity> GetDeloviParcela()
        {
            return (from dp in DeloviParcela select dp).ToList();
        }

        public DeoParceleEntity GetDeoParceleById(Guid deoParceleID)
        {
            return DeloviParcela.FirstOrDefault(dp => dp.DeoParceleID == deoParceleID);
        }

        public DeoParceleEntity UpdateDeoParcele(DeoParceleEntity deoParcele)
        {
            DeoParceleEntity dp = GetDeoParceleById(deoParcele.DeoParceleID);

            dp.RedniBroj = deoParcele.RedniBroj;
            dp.PovrsinaDelaParcele = deoParcele.PovrsinaDelaParcele;

            return dp;
        }
    }
}
