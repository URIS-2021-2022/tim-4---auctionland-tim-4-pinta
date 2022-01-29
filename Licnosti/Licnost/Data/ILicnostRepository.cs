using Licnost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licnost.Data
{
    public interface ILicnostRepository
    {
        List<LicnostModel> GetLicnosti(string licnostIme = null, string licnostPrezime = null);

        LicnostModel GetLicnostById(Guid licnostId);

        LicnostModel CreateLicnost(LicnostModel licnostModel);

        LicnostModel UpdateLicnost(LicnostModel licnostModel);

        void DeleteLicnost(Guid licnostId);
    }



}
