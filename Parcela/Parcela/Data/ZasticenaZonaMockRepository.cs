using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class ZasticenaZonaMockRepository : IZasticenaZonaRepository
    {
        public static List<ZasticenaZonaEntity> ZasticeneZone { get; set; } = new List<ZasticenaZonaEntity>();

        public ZasticenaZonaMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            ZasticeneZone.AddRange(new List<ZasticenaZonaEntity>
            {
                new ZasticenaZonaEntity
                {
                    ZasticenaZonaID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ZasticenaZonaOznaka = 1
                },
                new ZasticenaZonaEntity
                {
                    ZasticenaZonaID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ZasticenaZonaOznaka = 2
                }
            });
        }

        public ZasticenaZonaEntity CreateZasticenaZona(ZasticenaZonaEntity zasticenaZona)
        {
            zasticenaZona.ZasticenaZonaID = Guid.NewGuid();
            ZasticeneZone.Add(zasticenaZona);
            ZasticenaZonaEntity z = GetZasticenaZonaById(zasticenaZona.ZasticenaZonaID);
            return z;
        }

        public void DeleteZasticenaZona(Guid zasticenaZonaID)
        {
            ZasticeneZone.Remove(ZasticeneZone.FirstOrDefault(z => z.ZasticenaZonaID == zasticenaZonaID));
        }

        public ZasticenaZonaEntity GetZasticenaZonaById(Guid zasticenaZonaID)
        {
            return ZasticeneZone.FirstOrDefault(z => z.ZasticenaZonaID == zasticenaZonaID);
        }

        public List<ZasticenaZonaEntity> GetZasticeneZone()
        {
            return (from z in ZasticeneZone select z).ToList();
        }

        public ZasticenaZonaEntity UpdateZasticenaZona(ZasticenaZonaEntity zasticenaZona)
        {
            ZasticenaZonaEntity z = GetZasticenaZonaById(zasticenaZona.ZasticenaZonaID);

            z.ZasticenaZonaOznaka = zasticenaZona.ZasticenaZonaOznaka;

            return z;
        }
    }
}
