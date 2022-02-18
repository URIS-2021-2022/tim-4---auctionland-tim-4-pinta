using Licnost.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Data
{
    public class LicnostMockRepository : ILicnostRepository
    {
        public static List<LicnostEntity> Licnosti { get; set; } = new List<LicnostEntity>();

        public LicnostMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Licnosti.AddRange(new List<LicnostEntity>
            {
                new LicnostEntity
                {
                   LicnostId =Guid.Parse("E91B29CC-79A5-4DE8-8030-77DF6E514DEF"),
                   LicnostIme="Simona",
                   LicnostPrezime="Bolehradsky",
                   LicnostFunkcija="IT strucnjak"


                }

            });
        }

        public List<LicnostEntity> GetLicnosti(string licnostIme = null, string licnostPrezime = null)
        {
            return (from l in Licnosti
                    where string.IsNullOrEmpty(licnostIme) || l.LicnostIme == licnostIme &&
                          string.IsNullOrEmpty(licnostPrezime) || l.LicnostPrezime == licnostPrezime
                    select l).ToList();
        }

        public LicnostEntity GetLicnostById(Guid licnostId)
        {
            return Licnosti.FirstOrDefault(l => l.LicnostId == licnostId);
        }

        public LicnostEntity CreateLicnost(LicnostEntity licnost)
        {
            licnost.LicnostId = Guid.NewGuid();
            Licnosti.Add(licnost);
            var l = GetLicnostById(licnost.LicnostId);
            return new LicnostEntity
            {
                LicnostId = l.LicnostId,
                LicnostIme = l.LicnostIme,
                LicnostPrezime = l.LicnostPrezime,
                LicnostFunkcija = l.LicnostFunkcija

            };

        }

        public void UpdateLicnost(LicnostEntity licnost)
        {
            LicnostEntity l = GetLicnostById(licnost.LicnostId);

            l.LicnostId = licnost.LicnostId;
            l.LicnostIme = licnost.LicnostIme;
            l.LicnostPrezime = licnost.LicnostPrezime;
            l.LicnostFunkcija = licnost.LicnostFunkcija;


        }

        public void DeleteLicnost(Guid licnostId)
        {
            Licnosti.Remove(Licnosti.FirstOrDefault(l => l.LicnostId == licnostId));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}

