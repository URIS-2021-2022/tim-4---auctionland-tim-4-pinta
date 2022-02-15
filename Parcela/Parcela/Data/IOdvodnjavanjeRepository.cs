using Parcela.Entities;
using Parcela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Data
{
    public interface IOdvodnjavanjeRepository
    {
        List<OdvodnjavanjeEntity> GetOdvodnjavanja();

        OdvodnjavanjeEntity GetOdvodnjavanjeById(Guid odvodnjavanjeID);

        OdvodnjavanjeEntity CreateOdvodnjavanje(OdvodnjavanjeEntity odvodnjavanje);

        void UpdateOdvodnjavanje(OdvodnjavanjeEntity odvodnjavanje);

        void DeleteOdvodnjavanje(Guid odvodnjavanjeID);

        bool SaveChanges();
    }
}
