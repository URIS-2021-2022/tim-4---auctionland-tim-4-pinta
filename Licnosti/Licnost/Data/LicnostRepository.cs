using AutoMapper;
using Licnost.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Data
{

        public class LicnostRepository : ILicnostRepository
        {
            private readonly LicnostContext context;
            private readonly IMapper mapper;

            public LicnostRepository(LicnostContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
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

            public LicnostEntity CreateLicnost(LicnostEntity licnost)
            {
                var createdEntity = context.Add(licnost);
                return mapper.Map<LicnostEntity>(createdEntity.Entity);
            }

            public void UpdateLicnost(LicnostEntity licnost)
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
