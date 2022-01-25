using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ZasticenaZonaRepository : IZasticenaZonaRepository
    {
        public static List<ZasticenaZonaModel> ZasticeneZone { get; set; } = new List<ZasticenaZonaModel>();

        public ZasticenaZonaRepository()
        {
            FillData();
        }

        private void FillData()
        {
            ZasticeneZone.AddRange(new List<ZasticenaZonaModel>
            {
                new ZasticenaZonaModel
                {
                    ZasticenaZonaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ZasticenaZonaOznaka = 1
                },
                new ZasticenaZonaModel
                {
                    ZasticenaZonaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ZasticenaZonaOznaka = 2
                }
            });
        }

        public ZasticenaZonaModel CreateZasticenaZona(ZasticenaZonaModel zasticenaZona)
        {
            zasticenaZona.ZasticenaZonaID = Guid.NewGuid();
            ZasticeneZone.Add(zasticenaZona);
            ZasticenaZonaModel z = GetZasticenaZonaById(zasticenaZona.ZasticenaZonaID);
            return z;
        }

        public void DeleteZasticenaZona(Guid zasticenaZonaID)
        {
            ZasticeneZone.Remove(ZasticeneZone.FirstOrDefault(z => z.ZasticenaZonaID == zasticenaZonaID));
        }

        public ZasticenaZonaModel GetZasticenaZonaById(Guid zasticenaZonaID)
        {
            return ZasticeneZone.FirstOrDefault(z => z.ZasticenaZonaID == zasticenaZonaID);
        }

        public List<ZasticenaZonaModel> GetZasticeneZone()
        {
            return (from z in ZasticeneZone select z).ToList();
        }

        public ZasticenaZonaModel UpdateZasticenaZona(ZasticenaZonaModel zasticenaZona)
        {
            ZasticenaZonaModel z = GetZasticenaZonaById(zasticenaZona.ZasticenaZonaID);

            z.ZasticenaZonaOznaka = zasticenaZona.ZasticenaZonaOznaka;

            return z;
        }
    }
}
