using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class OdvodnjavanjeMockRepository : IOdvodnjavanjeRepository
    {
        public static List<OdvodnjavanjeEntity> Odvodnjavanja { get; set; } = new List<OdvodnjavanjeEntity>();

        public OdvodnjavanjeMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Odvodnjavanja.AddRange(new List<OdvodnjavanjeEntity>
            {
                new OdvodnjavanjeEntity
                {
                    OdvodnjavanjeID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    OdvodnjavanjeNaziv = "Odvodnjavanje1"
                },
                new OdvodnjavanjeEntity
                {
                    OdvodnjavanjeID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    OdvodnjavanjeNaziv = "Odvodnjavanje2"
                }
            });
        }

        public OdvodnjavanjeEntity CreateOdvodnjavanje(OdvodnjavanjeEntity odvodnjavanje)
        {
            odvodnjavanje.OdvodnjavanjeID = Guid.NewGuid();
            Odvodnjavanja.Add(odvodnjavanje);
            OdvodnjavanjeEntity o = GetOdvodnjavanjeById(odvodnjavanje.OdvodnjavanjeID);
            return o;
        }

        public void DeleteOdvodnjavanje(Guid odvodnjavanjeID)
        {
            Odvodnjavanja.Remove(Odvodnjavanja.FirstOrDefault(o => o.OdvodnjavanjeID == odvodnjavanjeID));
        }

        public List<OdvodnjavanjeEntity> GetOdvodnjavanja()
        {
            return (from o in Odvodnjavanja select o).ToList();
        }

        public OdvodnjavanjeEntity GetOdvodnjavanjeById(Guid odvodnjavanjeID)
        {
            return Odvodnjavanja.FirstOrDefault(o => o.OdvodnjavanjeID == odvodnjavanjeID);
        }

        public OdvodnjavanjeEntity UpdateOdvodnjavanje(OdvodnjavanjeEntity odvodnjavanje)
        {
            OdvodnjavanjeEntity o = GetOdvodnjavanjeById(odvodnjavanje.OdvodnjavanjeID);

            o.OdvodnjavanjeNaziv = odvodnjavanje.OdvodnjavanjeNaziv;

            return o;
        }
    }
}
