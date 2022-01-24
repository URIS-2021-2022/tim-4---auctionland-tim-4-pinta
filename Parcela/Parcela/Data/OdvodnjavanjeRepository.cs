using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public class OdvodnjavanjeRepository : IOdvodnjavanjeRepository
    {
        public static List<OdvodnjavanjeModel> Odvodnjavanja { get; set; } = new List<OdvodnjavanjeModel>();

        public OdvodnjavanjeRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Odvodnjavanja.AddRange(new List<OdvodnjavanjeModel>
            {
                new OdvodnjavanjeModel
                {
                    OdvodnjavanjeID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    OdvodnjavanjeNaziv = "Odvodnjavanje1"
                },
                new OdvodnjavanjeModel
                {
                    OdvodnjavanjeID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    OdvodnjavanjeNaziv = "Odvodnjavanje2"
                }
            });
        }

        public OdvodnjavanjeModel CreateOdvodnjavanje(OdvodnjavanjeModel odvodnjavanje)
        {
            odvodnjavanje.OdvodnjavanjeID = Guid.NewGuid();
            Odvodnjavanja.Add(odvodnjavanje);
            OdvodnjavanjeModel o = GetOdvodnjavanjeById(odvodnjavanje.OdvodnjavanjeID);
            return o;
        }

        public void DeleteOdvodnjavanje(Guid odvodnjavanjeID)
        {
            Odvodnjavanja.Remove(Odvodnjavanja.FirstOrDefault(o => o.OdvodnjavanjeID == odvodnjavanjeID));
        }

        public List<OdvodnjavanjeModel> GetOdvodnjavanja()
        {
            return (from o in Odvodnjavanja select o).ToList();
        }

        public OdvodnjavanjeModel GetOdvodnjavanjeById(Guid odvodnjavanjeID)
        {
            return Odvodnjavanja.FirstOrDefault(o => o.OdvodnjavanjeID == odvodnjavanjeID);
        }

        public OdvodnjavanjeModel UpdateOdvodnjavanje(OdvodnjavanjeModel odvodnjavanje)
        {
            OdvodnjavanjeModel o = GetOdvodnjavanjeById(odvodnjavanje.OdvodnjavanjeID);

            o.OdvodnjavanjeNaziv = odvodnjavanje.OdvodnjavanjeNaziv;

            return o;
        }
    }
}
