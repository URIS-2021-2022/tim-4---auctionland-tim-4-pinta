using Licnost.Entities;
using Licnost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Data
{
    public interface ILicnostRepository
    {
        List<LicnostEntity> GetLicnosti(string licnostIme = null, string licnostPrezime = null);

        LicnostEntity GetLicnostById(Guid licnostId);

        LicnostEntity CreateLicnost(LicnostEntity licnostModel);

        LicnostEntity UpdateLicnost(LicnostEntity licnostModel);

        void DeleteLicnost(Guid licnostId);
    }



}
