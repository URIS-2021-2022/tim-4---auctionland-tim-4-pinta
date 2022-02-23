using Licnost.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Licnost.Data
{
    public class LicnostRepository : ILicnostRepository
    {
        private readonly LicnostContext context;

        public LicnostRepository(LicnostContext context)
        {
            this.context = context;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<LicnostEntity> GetLicnosti(string licnostIme = null, string licnostPrezime = null)
        {
            return context.Licnosti.Where(l => (licnostIme == null || l.LicnostIme == licnostIme) &&
                                                        (licnostPrezime == null || l.LicnostPrezime == licnostPrezime)).ToList();
        }

        public LicnostEntity GetLicnostById(Guid licnostId)
        {
            return context.Licnosti.FirstOrDefault(l => l.LicnostId == licnostId);
        }

        public LicnostEntity CreateLicnost(LicnostEntity licnostModel)
        {
            licnostModel.LicnostId = Guid.NewGuid();
            context.Licnosti.Add(licnostModel);
            return licnostModel;
        }

        public void UpdateLicnost(LicnostEntity licnostModel)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteLicnost(Guid licnostId)
        {
            var licnost = GetLicnostById(licnostId);
            context.Remove(licnost);
        }
    }
}