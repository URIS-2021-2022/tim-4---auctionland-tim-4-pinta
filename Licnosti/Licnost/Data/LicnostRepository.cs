using Licnost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Data
{
    public class LicnostRepository : ILicnostRepository
    {
        public static List<LicnostModel> Licnosti { get; set; } = new List<LicnostModel>();

        public LicnostRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Licnosti.AddRange(new List<LicnostModel>
            {
                new LicnostModel
                {
                   LicnostId =Guid.Parse("E91B29CC-79A5-4DE8-8030-77DF6E514DEF"),
                   LicnostIme="Simona",
                   LicnostPrezime="Bolehradsky",
                   LicnostFunkcija="IT strucnjak"

      
                }
                
            });
        }

        public List<LicnostModel> GetLicnosti(string licnostIme = null, string licnostPrezime = null)
        {
            return (from l in Licnosti
                    where string.IsNullOrEmpty(licnostIme) || l.LicnostIme == licnostIme &&
                          string.IsNullOrEmpty(licnostPrezime) || l.LicnostPrezime == licnostPrezime
                    select l).ToList();
        }

        public LicnostModel GetLicnostById(Guid licnostId)
        {
            return Licnosti.FirstOrDefault(l => l.LicnostId == licnostId);
        }

        public LicnostModel CreateLicnost(LicnostModel licnost)
        {
            Licnosti.Add(licnost);
            var l = GetLicnostById(licnost.LicnostId);
            return new LicnostModel
            {
                LicnostId = l.LicnostId,
                LicnostIme = l.LicnostIme,
                LicnostPrezime = l.LicnostPrezime,
                LicnostFunkcija = l.LicnostFunkcija
            };
            //licnost.LicnostId = Guid.NewGuid();
            //Parcele.Add(parcela);
            //ParcelaEntity p = GetParcelaById(parcela.ParcelaID);
            //return p;
        }

        public LicnostModel UpdateLicnost(LicnostModel licnost)
        {
            LicnostModel l = GetLicnostById(licnost.LicnostId);

            l.LicnostId = licnost.LicnostId;
            l.LicnostIme = licnost.LicnostIme;
            l.LicnostPrezime = licnost.LicnostPrezime;
            l.LicnostFunkcija = licnost.LicnostFunkcija;

            return new LicnostModel
            {
                LicnostId = l.LicnostId,
                LicnostIme = l.LicnostIme,
                LicnostPrezime = l.LicnostPrezime,
                LicnostFunkcija = l.LicnostFunkcija
            };
        }

        public void DeleteLicnost(Guid licnostId)
        {
            Licnosti.Remove(Licnosti.FirstOrDefault(l => l.LicnostId == licnostId));
        }
    }
}

